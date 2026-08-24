using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace FINAL_DotNet
{
    internal sealed class DongBangTinhXlsx
    {
        public DongBangTinhXlsx(int soDong, IDictionary<string, object> giaTri)
        {
            SoDong = soDong;
            GiaTri = new Dictionary<string, object>(giaTri, StringComparer.OrdinalIgnoreCase);
        }

        public int SoDong { get; }
        public IReadOnlyDictionary<string, object> GiaTri { get; }

        public object Lay(string tenCot)
        {
            object giaTri;
            return GiaTri.TryGetValue(tenCot, out giaTri) ? giaTri : null;
        }
    }

    internal sealed class BangTinhXlsx
    {
        public BangTinhXlsx(IList<string> cacCot, IList<DongBangTinhXlsx> cacDong)
        {
            CacCot = new List<string>(cacCot);
            CacDong = new List<DongBangTinhXlsx>(cacDong);
        }

        public IReadOnlyList<string> CacCot { get; }
        public IReadOnlyList<DongBangTinhXlsx> CacDong { get; }
    }

    internal static class XlsxImportService
    {
        private const int SoDongToiDa = 5000;
        private const int SoCotToiDa = 50;
        private static readonly XNamespace NsBangTinh = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
        private static readonly XNamespace NsQuanHeVanPhong = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";
        private static readonly XNamespace NsQuanHeGoi = "http://schemas.openxmlformats.org/package/2006/relationships";

        public static BangTinhXlsx DocTrangTinhDauTien(string duongDan)
        {
            if (string.IsNullOrWhiteSpace(duongDan) || !File.Exists(duongDan))
                throw new InvalidOperationException("Không tìm thấy file Excel đã chọn.");
            if (!string.Equals(Path.GetExtension(duongDan), ".xlsx", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Chỉ hỗ trợ file Excel định dạng .xlsx.");
            if (new FileInfo(duongDan).Length > 20 * 1024 * 1024)
                throw new InvalidOperationException("File Excel không được lớn hơn 20 MB.");

            using (var stream = new FileStream(duongDan, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var goi = new ZipArchive(stream, ZipArchiveMode.Read))
            {
                XDocument workbook = DocXml(goi, "xl/workbook.xml");
                XDocument quanHe = DocXml(goi, "xl/_rels/workbook.xml.rels");
                XElement sheet = workbook.Descendants(NsBangTinh + "sheet").FirstOrDefault();
                if (sheet == null) throw new InvalidOperationException("File Excel không có trang tính.");

                string relationId = (string)sheet.Attribute(NsQuanHeVanPhong + "id");
                XElement relationship = quanHe.Descendants(NsQuanHeGoi + "Relationship")
                    .FirstOrDefault(item => string.Equals((string)item.Attribute("Id"), relationId, StringComparison.Ordinal));
                if (relationship == null) throw new InvalidOperationException("Không xác định được trang tính đầu tiên.");

                string duongDanTrangTinh = ChuanHoaDuongDanTrongGoi((string)relationship.Attribute("Target"));
                XDocument worksheet = DocXml(goi, duongDanTrangTinh);
                List<string> sharedStrings = DocSharedStrings(goi);
                List<bool> kieuNgayTheoStyle = DocKieuNgayTheoStyle(goi);
                return DocBangTinh(worksheet, sharedStrings, kieuNgayTheoStyle);
            }
        }

        private static BangTinhXlsx DocBangTinh(
            XDocument worksheet,
            IList<string> sharedStrings,
            IList<bool> kieuNgayTheoStyle)
        {
            List<XElement> rows = worksheet.Descendants(NsBangTinh + "row").ToList();
            if (rows.Count == 0) throw new InvalidOperationException("Trang tính đầu tiên không có dữ liệu.");
            if (rows.Count > SoDongToiDa + 1)
                throw new InvalidOperationException("File Excel vượt quá giới hạn " + SoDongToiDa + " dòng dữ liệu.");

            XElement headerRow = rows.FirstOrDefault(row => row.Elements(NsBangTinh + "c").Any());
            if (headerRow == null) throw new InvalidOperationException("Không tìm thấy dòng tiêu đề trong file Excel.");

            var headersByColumn = new Dictionary<int, string>();
            foreach (XElement cell in headerRow.Elements(NsBangTinh + "c"))
            {
                int columnIndex = LayChiSoCot((string)cell.Attribute("r"));
                if (columnIndex < 0 || columnIndex >= SoCotToiDa) continue;
                string header = Convert.ToString(DocGiaTriO(cell, sharedStrings, kieuNgayTheoStyle), CultureInfo.InvariantCulture)?.Trim();
                if (string.IsNullOrWhiteSpace(header)) continue;
                if (headersByColumn.Values.Any(value => string.Equals(value, header, StringComparison.OrdinalIgnoreCase)))
                    throw new InvalidOperationException("Tiêu đề cột bị trùng: " + header + ".");
                headersByColumn[columnIndex] = header;
            }
            if (headersByColumn.Count == 0) throw new InvalidOperationException("Dòng tiêu đề không có tên cột hợp lệ.");

            int headerRowNumber = LaySoDong(headerRow, 1);
            var resultRows = new List<DongBangTinhXlsx>();
            foreach (XElement row in rows.Where(row => LaySoDong(row, 0) > headerRowNumber))
            {
                int rowNumber = LaySoDong(row, headerRowNumber + resultRows.Count + 1);
                var values = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                bool hasValue = false;
                foreach (XElement cell in row.Elements(NsBangTinh + "c"))
                {
                    int columnIndex = LayChiSoCot((string)cell.Attribute("r"));
                    string header;
                    if (!headersByColumn.TryGetValue(columnIndex, out header)) continue;
                    object value = DocGiaTriO(cell, sharedStrings, kieuNgayTheoStyle);
                    values[header] = value;
                    if (value != null && !string.IsNullOrWhiteSpace(Convert.ToString(value, CultureInfo.InvariantCulture)))
                        hasValue = true;
                }
                if (hasValue) resultRows.Add(new DongBangTinhXlsx(rowNumber, values));
            }

            return new BangTinhXlsx(headersByColumn.OrderBy(item => item.Key).Select(item => item.Value).ToList(), resultRows);
        }

        private static object DocGiaTriO(XElement cell, IList<string> sharedStrings, IList<bool> kieuNgayTheoStyle)
        {
            string type = (string)cell.Attribute("t");
            if (type == "inlineStr")
                return string.Concat(cell.Descendants(NsBangTinh + "t").Select(item => item.Value));

            string raw = (string)cell.Element(NsBangTinh + "v");
            if (raw == null) return null;
            if (type == "s")
            {
                int index;
                return int.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out index) &&
                       index >= 0 && index < sharedStrings.Count
                    ? sharedStrings[index]
                    : raw;
            }
            if (type == "b") return raw == "1";
            if (type == "str" || type == "e") return raw;

            double number;
            if (!double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out number)) return raw;
            int styleIndex;
            bool dateStyle = int.TryParse((string)cell.Attribute("s"), NumberStyles.Integer,
                                 CultureInfo.InvariantCulture, out styleIndex) &&
                             styleIndex >= 0 && styleIndex < kieuNgayTheoStyle.Count && kieuNgayTheoStyle[styleIndex];
            if (dateStyle)
            {
                try { return DateTime.FromOADate(number); }
                catch (ArgumentException) { return raw; }
            }
            return number;
        }

        private static List<string> DocSharedStrings(ZipArchive goi)
        {
            ZipArchiveEntry entry = TimEntry(goi, "xl/sharedStrings.xml");
            if (entry == null) return new List<string>();
            using (Stream stream = entry.Open())
            {
                XDocument document = XDocument.Load(stream);
                return document.Descendants(NsBangTinh + "si")
                    .Select(item => string.Concat(item.Descendants(NsBangTinh + "t").Select(text => text.Value)))
                    .ToList();
            }
        }

        private static List<bool> DocKieuNgayTheoStyle(ZipArchive goi)
        {
            ZipArchiveEntry entry = TimEntry(goi, "xl/styles.xml");
            if (entry == null) return new List<bool> { false };
            using (Stream stream = entry.Open())
            {
                XDocument document = XDocument.Load(stream);
                var customFormats = document.Descendants(NsBangTinh + "numFmt")
                    .Where(item => item.Attribute("numFmtId") != null)
                    .ToDictionary(
                        item => (int)item.Attribute("numFmtId"),
                        item => (string)item.Attribute("formatCode") ?? string.Empty);
                XElement cellXfs = document.Descendants(NsBangTinh + "cellXfs").FirstOrDefault();
                if (cellXfs == null) return new List<bool> { false };
                return cellXfs.Elements(NsBangTinh + "xf").Select(xf =>
                {
                    int numFmtId = (int?)xf.Attribute("numFmtId") ?? 0;
                    string format;
                    customFormats.TryGetValue(numFmtId, out format);
                    return LaDinhDangNgay(numFmtId, format);
                }).ToList();
            }
        }

        private static bool LaDinhDangNgay(int numFmtId, string format)
        {
            if ((numFmtId >= 14 && numFmtId <= 22) || (numFmtId >= 45 && numFmtId <= 47)) return true;
            if (string.IsNullOrWhiteSpace(format)) return false;
            string cleaned = Regex.Replace(format, @"""[^""]*""|\[[^\]]*\]", string.Empty).ToLowerInvariant();
            return cleaned.Contains("yy") || cleaned.Contains("dd") ||
                   (cleaned.Contains("mm") && (cleaned.Contains("/") || cleaned.Contains("-"))) ||
                   cleaned.Contains("hh") || cleaned.Contains("ss");
        }

        private static XDocument DocXml(ZipArchive goi, string duongDan)
        {
            ZipArchiveEntry entry = TimEntry(goi, duongDan);
            if (entry == null) throw new InvalidOperationException("File Excel thiếu thành phần " + duongDan + ".");
            using (Stream stream = entry.Open()) return XDocument.Load(stream);
        }

        private static ZipArchiveEntry TimEntry(ZipArchive goi, string duongDan)
        {
            string normalized = duongDan.Replace('\\', '/').TrimStart('/');
            return goi.Entries.FirstOrDefault(entry =>
                string.Equals(entry.FullName, normalized, StringComparison.OrdinalIgnoreCase));
        }

        private static string ChuanHoaDuongDanTrongGoi(string target)
        {
            string value = (target ?? string.Empty).Replace('\\', '/').TrimStart('/');
            if (!value.StartsWith("xl/", StringComparison.OrdinalIgnoreCase)) value = "xl/" + value;
            var parts = new List<string>();
            foreach (string part in value.Split('/'))
            {
                if (part == "..") { if (parts.Count > 0) parts.RemoveAt(parts.Count - 1); }
                else if (part.Length > 0 && part != ".") parts.Add(part);
            }
            return string.Join("/", parts);
        }

        private static int LayChiSoCot(string cellReference)
        {
            if (string.IsNullOrWhiteSpace(cellReference)) return -1;
            int result = 0;
            int letters = 0;
            foreach (char character in cellReference)
            {
                if (!char.IsLetter(character)) break;
                result = result * 26 + (char.ToUpperInvariant(character) - 'A' + 1);
                letters++;
            }
            return letters == 0 ? -1 : result - 1;
        }

        private static int LaySoDong(XElement row, int fallback)
        {
            int result;
            return int.TryParse((string)row.Attribute("r"), NumberStyles.Integer,
                CultureInfo.InvariantCulture, out result) ? result : fallback;
        }
    }
}

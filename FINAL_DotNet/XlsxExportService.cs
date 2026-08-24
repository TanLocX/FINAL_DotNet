using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Xml;

namespace FINAL_DotNet
{
    internal enum KieuDuLieuExcel
    {
        VanBan,
        SoNguyen,
        SoThapPhan,
        TienTe,
        Ngay,
        NgayGio,
        PhanTram
    }

    internal sealed class CotXuatExcel
    {
        public CotXuatExcel(string tieuDe, double doRong, KieuDuLieuExcel kieuDuLieu = KieuDuLieuExcel.VanBan)
        {
            TieuDe = tieuDe;
            DoRong = doRong;
            KieuDuLieu = kieuDuLieu;
        }

        public string TieuDe { get; }
        public double DoRong { get; }
        public KieuDuLieuExcel KieuDuLieu { get; }
    }

    internal static class XlsxExportService
    {
        private const string SpreadsheetNamespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
        private const string RelationshipNamespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";

        public static void Xuat(
            string duongDan,
            string tenTrangTinh,
            IReadOnlyList<CotXuatExcel> cacCot,
            IEnumerable<object[]> duLieu)
        {
            if (string.IsNullOrWhiteSpace(duongDan)) throw new ArgumentException("Thiếu đường dẫn file.", nameof(duongDan));
            if (cacCot == null || cacCot.Count == 0) throw new ArgumentException("Cần ít nhất một cột xuất.", nameof(cacCot));

            List<object[]> cacDong = (duLieu ?? Enumerable.Empty<object[]>()).ToList();
            if (cacDong.Any(dong => dong == null || dong.Length != cacCot.Count))
                throw new ArgumentException("Số ô trên mỗi dòng phải bằng số cột.", nameof(duLieu));

            string thuMuc = Path.GetDirectoryName(Path.GetFullPath(duongDan));
            if (!Directory.Exists(thuMuc)) Directory.CreateDirectory(thuMuc);

            using (var tep = new FileStream(duongDan, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            using (var goi = new ZipArchive(tep, ZipArchiveMode.Create))
            {
                GhiNoiDungVanBan(goi, "[Content_Types].xml", GhiContentTypes);
                GhiNoiDungVanBan(goi, "_rels/.rels", GhiRootRelationships);
                GhiNoiDungVanBan(goi, "xl/workbook.xml", writer => GhiWorkbook(writer, tenTrangTinh));
                GhiNoiDungVanBan(goi, "xl/_rels/workbook.xml.rels", GhiWorkbookRelationships);
                GhiNoiDungVanBan(goi, "xl/styles.xml", GhiStyles);
                GhiNoiDungVanBan(goi, "xl/worksheets/sheet1.xml", writer => GhiWorksheet(writer, cacCot, cacDong));
            }
        }

        private static void GhiNoiDungVanBan(ZipArchive goi, string ten, Action<XmlWriter> ghi)
        {
            ZipArchiveEntry muc = goi.CreateEntry(ten, CompressionLevel.Optimal);
            using (Stream stream = muc.Open())
            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(false),
                Indent = false,
                CloseOutput = false
            }))
            {
                ghi(writer);
            }
        }

        private static void GhiContentTypes(XmlWriter writer)
        {
            writer.WriteStartDocument(true);
            writer.WriteStartElement("Types", "http://schemas.openxmlformats.org/package/2006/content-types");
            GhiPhanTuRong(writer, "Default", new Dictionary<string, string> { { "Extension", "rels" }, { "ContentType", "application/vnd.openxmlformats-package.relationships+xml" } });
            GhiPhanTuRong(writer, "Default", new Dictionary<string, string> { { "Extension", "xml" }, { "ContentType", "application/xml" } });
            GhiPhanTuRong(writer, "Override", new Dictionary<string, string> { { "PartName", "/xl/workbook.xml" }, { "ContentType", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml" } });
            GhiPhanTuRong(writer, "Override", new Dictionary<string, string> { { "PartName", "/xl/worksheets/sheet1.xml" }, { "ContentType", "application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml" } });
            GhiPhanTuRong(writer, "Override", new Dictionary<string, string> { { "PartName", "/xl/styles.xml" }, { "ContentType", "application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml" } });
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        private static void GhiRootRelationships(XmlWriter writer)
        {
            writer.WriteStartDocument(true);
            writer.WriteStartElement("Relationships", "http://schemas.openxmlformats.org/package/2006/relationships");
            GhiPhanTuRong(writer, "Relationship", new Dictionary<string, string>
            {
                { "Id", "rId1" },
                { "Type", "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument" },
                { "Target", "xl/workbook.xml" }
            });
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        private static void GhiWorkbook(XmlWriter writer, string tenTrangTinh)
        {
            writer.WriteStartDocument(true);
            writer.WriteStartElement("workbook", SpreadsheetNamespace);
            writer.WriteAttributeString("xmlns", "r", null, RelationshipNamespace);
            writer.WriteStartElement("sheets", SpreadsheetNamespace);
            writer.WriteStartElement("sheet", SpreadsheetNamespace);
            writer.WriteAttributeString("name", LamSachTenTrangTinh(tenTrangTinh));
            writer.WriteAttributeString("sheetId", "1");
            writer.WriteAttributeString("r", "id", RelationshipNamespace, "rId1");
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        private static void GhiWorkbookRelationships(XmlWriter writer)
        {
            writer.WriteStartDocument(true);
            writer.WriteStartElement("Relationships", "http://schemas.openxmlformats.org/package/2006/relationships");
            GhiPhanTuRong(writer, "Relationship", new Dictionary<string, string>
            {
                { "Id", "rId1" },
                { "Type", "http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet" },
                { "Target", "worksheets/sheet1.xml" }
            });
            GhiPhanTuRong(writer, "Relationship", new Dictionary<string, string>
            {
                { "Id", "rId2" },
                { "Type", "http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles" },
                { "Target", "styles.xml" }
            });
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        private static void GhiStyles(XmlWriter writer)
        {
            writer.WriteStartDocument(true);
            writer.WriteStartElement("styleSheet", SpreadsheetNamespace);

            writer.WriteStartElement("numFmts", SpreadsheetNamespace);
            writer.WriteAttributeString("count", "4");
            GhiNumFmt(writer, 164, "dd/mm/yyyy");
            GhiNumFmt(writer, 165, "dd/mm/yyyy hh:mm:ss");
            GhiNumFmt(writer, 166, "#,##0\" đ\"");
            GhiNumFmt(writer, 167, "0.00%");
            writer.WriteEndElement();

            writer.WriteStartElement("fonts", SpreadsheetNamespace);
            writer.WriteAttributeString("count", "2");
            writer.WriteStartElement("font", SpreadsheetNamespace);
            writer.WriteStartElement("sz", SpreadsheetNamespace); writer.WriteAttributeString("val", "11"); writer.WriteEndElement();
            writer.WriteStartElement("name", SpreadsheetNamespace); writer.WriteAttributeString("val", "Segoe UI"); writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteStartElement("font", SpreadsheetNamespace);
            writer.WriteElementString("b", SpreadsheetNamespace, string.Empty);
            writer.WriteStartElement("color", SpreadsheetNamespace); writer.WriteAttributeString("rgb", "FFFFFFFF"); writer.WriteEndElement();
            writer.WriteStartElement("sz", SpreadsheetNamespace); writer.WriteAttributeString("val", "11"); writer.WriteEndElement();
            writer.WriteStartElement("name", SpreadsheetNamespace); writer.WriteAttributeString("val", "Segoe UI"); writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();

            writer.WriteStartElement("fills", SpreadsheetNamespace);
            writer.WriteAttributeString("count", "3");
            writer.WriteStartElement("fill", SpreadsheetNamespace); writer.WriteStartElement("patternFill", SpreadsheetNamespace); writer.WriteAttributeString("patternType", "none"); writer.WriteEndElement(); writer.WriteEndElement();
            writer.WriteStartElement("fill", SpreadsheetNamespace); writer.WriteStartElement("patternFill", SpreadsheetNamespace); writer.WriteAttributeString("patternType", "gray125"); writer.WriteEndElement(); writer.WriteEndElement();
            writer.WriteStartElement("fill", SpreadsheetNamespace); writer.WriteStartElement("patternFill", SpreadsheetNamespace); writer.WriteAttributeString("patternType", "solid"); writer.WriteStartElement("fgColor", SpreadsheetNamespace); writer.WriteAttributeString("rgb", "FF1B2735"); writer.WriteEndElement(); writer.WriteStartElement("bgColor", SpreadsheetNamespace); writer.WriteAttributeString("indexed", "64"); writer.WriteEndElement(); writer.WriteEndElement(); writer.WriteEndElement();
            writer.WriteEndElement();

            writer.WriteStartElement("borders", SpreadsheetNamespace);
            writer.WriteAttributeString("count", "1");
            writer.WriteStartElement("border", SpreadsheetNamespace);
            writer.WriteElementString("left", SpreadsheetNamespace, string.Empty);
            writer.WriteElementString("right", SpreadsheetNamespace, string.Empty);
            writer.WriteElementString("top", SpreadsheetNamespace, string.Empty);
            writer.WriteElementString("bottom", SpreadsheetNamespace, string.Empty);
            writer.WriteElementString("diagonal", SpreadsheetNamespace, string.Empty);
            writer.WriteEndElement();
            writer.WriteEndElement();

            writer.WriteStartElement("cellStyleXfs", SpreadsheetNamespace);
            writer.WriteAttributeString("count", "1");
            GhiXf(writer, 0, 0, 0, 0, false, false);
            writer.WriteEndElement();

            writer.WriteStartElement("cellXfs", SpreadsheetNamespace);
            writer.WriteAttributeString("count", "7");
            GhiXf(writer, 0, 0, 0, 0, false, false);
            GhiXf(writer, 0, 1, 2, 0, true, true);
            GhiXf(writer, 164, 0, 0, 0, true, false);
            GhiXf(writer, 165, 0, 0, 0, true, false);
            GhiXf(writer, 166, 0, 0, 0, true, false);
            GhiXf(writer, 3, 0, 0, 0, true, false);
            GhiXf(writer, 167, 0, 0, 0, true, false);
            writer.WriteEndElement();

            writer.WriteStartElement("cellStyles", SpreadsheetNamespace);
            writer.WriteAttributeString("count", "1");
            writer.WriteStartElement("cellStyle", SpreadsheetNamespace);
            writer.WriteAttributeString("name", "Normal"); writer.WriteAttributeString("xfId", "0"); writer.WriteAttributeString("builtinId", "0");
            writer.WriteEndElement();
            writer.WriteEndElement();

            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        private static void GhiNumFmt(XmlWriter writer, int id, string dinhDang)
        {
            writer.WriteStartElement("numFmt", SpreadsheetNamespace);
            writer.WriteAttributeString("numFmtId", id.ToString(CultureInfo.InvariantCulture));
            writer.WriteAttributeString("formatCode", dinhDang);
            writer.WriteEndElement();
        }

        private static void GhiXf(XmlWriter writer, int numFmtId, int fontId, int fillId, int borderId, bool apDungDinhDang, bool canGiua)
        {
            writer.WriteStartElement("xf", SpreadsheetNamespace);
            writer.WriteAttributeString("numFmtId", numFmtId.ToString(CultureInfo.InvariantCulture));
            writer.WriteAttributeString("fontId", fontId.ToString(CultureInfo.InvariantCulture));
            writer.WriteAttributeString("fillId", fillId.ToString(CultureInfo.InvariantCulture));
            writer.WriteAttributeString("borderId", borderId.ToString(CultureInfo.InvariantCulture));
            writer.WriteAttributeString("xfId", "0");
            if (apDungDinhDang) writer.WriteAttributeString("applyNumberFormat", "1");
            if (fontId != 0) writer.WriteAttributeString("applyFont", "1");
            if (fillId != 0) writer.WriteAttributeString("applyFill", "1");
            if (canGiua)
            {
                writer.WriteAttributeString("applyAlignment", "1");
                writer.WriteStartElement("alignment", SpreadsheetNamespace);
                writer.WriteAttributeString("horizontal", "center");
                writer.WriteAttributeString("vertical", "center");
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private static void GhiWorksheet(XmlWriter writer, IReadOnlyList<CotXuatExcel> cacCot, IReadOnlyList<object[]> cacDong)
        {
            writer.WriteStartDocument(true);
            writer.WriteStartElement("worksheet", SpreadsheetNamespace);
            writer.WriteAttributeString("xmlns", "r", null, RelationshipNamespace);

            writer.WriteStartElement("sheetViews", SpreadsheetNamespace);
            writer.WriteStartElement("sheetView", SpreadsheetNamespace);
            writer.WriteAttributeString("workbookViewId", "0");
            writer.WriteStartElement("pane", SpreadsheetNamespace);
            writer.WriteAttributeString("ySplit", "1"); writer.WriteAttributeString("topLeftCell", "A2"); writer.WriteAttributeString("activePane", "bottomLeft"); writer.WriteAttributeString("state", "frozen");
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();

            writer.WriteStartElement("cols", SpreadsheetNamespace);
            for (int i = 0; i < cacCot.Count; i++)
            {
                writer.WriteStartElement("col", SpreadsheetNamespace);
                writer.WriteAttributeString("min", (i + 1).ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("max", (i + 1).ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("width", Math.Max(8, Math.Min(60, cacCot[i].DoRong)).ToString("0.##", CultureInfo.InvariantCulture));
                writer.WriteAttributeString("customWidth", "1");
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteStartElement("sheetData", SpreadsheetNamespace);
            writer.WriteStartElement("row", SpreadsheetNamespace);
            writer.WriteAttributeString("r", "1");
            writer.WriteAttributeString("ht", "24");
            writer.WriteAttributeString("customHeight", "1");
            for (int cot = 0; cot < cacCot.Count; cot++)
                GhiOChuoi(writer, DiaChiO(cot, 1), cacCot[cot].TieuDe, 1);
            writer.WriteEndElement();

            for (int dong = 0; dong < cacDong.Count; dong++)
            {
                int soDong = dong + 2;
                writer.WriteStartElement("row", SpreadsheetNamespace);
                writer.WriteAttributeString("r", soDong.ToString(CultureInfo.InvariantCulture));
                for (int cot = 0; cot < cacCot.Count; cot++)
                    GhiO(writer, DiaChiO(cot, soDong), cacDong[dong][cot], cacCot[cot].KieuDuLieu);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            string vung = "A1:" + DiaChiO(cacCot.Count - 1, Math.Max(1, cacDong.Count + 1));
            writer.WriteStartElement("autoFilter", SpreadsheetNamespace);
            writer.WriteAttributeString("ref", vung);
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        private static void GhiO(XmlWriter writer, string diaChi, object giaTri, KieuDuLieuExcel kieu)
        {
            if (giaTri == null || giaTri == DBNull.Value) return;
            if (kieu == KieuDuLieuExcel.VanBan)
            {
                GhiOChuoi(writer, diaChi, Convert.ToString(giaTri, CultureInfo.CurrentCulture), 0);
                return;
            }

            double so;
            int style;
            switch (kieu)
            {
                case KieuDuLieuExcel.Ngay:
                    so = Convert.ToDateTime(giaTri, CultureInfo.CurrentCulture).ToOADate(); style = 2; break;
                case KieuDuLieuExcel.NgayGio:
                    so = Convert.ToDateTime(giaTri, CultureInfo.CurrentCulture).ToOADate(); style = 3; break;
                case KieuDuLieuExcel.TienTe:
                    so = Convert.ToDouble(giaTri, CultureInfo.CurrentCulture); style = 4; break;
                case KieuDuLieuExcel.SoNguyen:
                    so = Convert.ToDouble(giaTri, CultureInfo.CurrentCulture); style = 5; break;
                case KieuDuLieuExcel.PhanTram:
                    so = Convert.ToDouble(giaTri, CultureInfo.CurrentCulture); style = 6; break;
                default:
                    so = Convert.ToDouble(giaTri, CultureInfo.CurrentCulture); style = 0; break;
            }

            writer.WriteStartElement("c", SpreadsheetNamespace);
            writer.WriteAttributeString("r", diaChi);
            if (style != 0) writer.WriteAttributeString("s", style.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("v", SpreadsheetNamespace, so.ToString("R", CultureInfo.InvariantCulture));
            writer.WriteEndElement();
        }

        private static void GhiOChuoi(XmlWriter writer, string diaChi, string giaTri, int style)
        {
            writer.WriteStartElement("c", SpreadsheetNamespace);
            writer.WriteAttributeString("r", diaChi);
            writer.WriteAttributeString("t", "inlineStr");
            if (style != 0) writer.WriteAttributeString("s", style.ToString(CultureInfo.InvariantCulture));
            writer.WriteStartElement("is", SpreadsheetNamespace);
            writer.WriteStartElement("t", SpreadsheetNamespace);
            writer.WriteAttributeString("xml", "space", null, "preserve");
            writer.WriteString(LamSachChuoiXml(giaTri));
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        private static void GhiPhanTuRong(XmlWriter writer, string ten, IDictionary<string, string> thuocTinh)
        {
            writer.WriteStartElement(ten);
            foreach (KeyValuePair<string, string> item in thuocTinh) writer.WriteAttributeString(item.Key, item.Value);
            writer.WriteEndElement();
        }

        private static string DiaChiO(int chiSoCot, int dong)
        {
            int giaTri = chiSoCot + 1;
            string cot = string.Empty;
            while (giaTri > 0)
            {
                giaTri--;
                cot = (char)('A' + giaTri % 26) + cot;
                giaTri /= 26;
            }
            return cot + dong.ToString(CultureInfo.InvariantCulture);
        }

        private static string LamSachTenTrangTinh(string ten)
        {
            string ketQua = string.IsNullOrWhiteSpace(ten) ? "Du lieu" : ten.Trim();
            foreach (char kyTu in new[] { ':', '\\', '/', '?', '*', '[', ']' }) ketQua = ketQua.Replace(kyTu, ' ');
            if (ketQua.Length > 31) ketQua = ketQua.Substring(0, 31);
            return ketQua.Length == 0 ? "Du lieu" : ketQua;
        }

        private static string LamSachChuoiXml(string giaTri)
        {
            if (string.IsNullOrEmpty(giaTri)) return string.Empty;
            var builder = new StringBuilder(giaTri.Length);
            foreach (char kyTu in giaTri)
                if (XmlConvert.IsXmlChar(kyTu)) builder.Append(kyTu);
            return builder.ToString();
        }
    }
}

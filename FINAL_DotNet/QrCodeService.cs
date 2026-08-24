using System;
using System.Collections.Generic;
using System.Drawing;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace FINAL_DotNet
{
    internal static class QrCodeService
    {
        public static Bitmap TaoMaQr(string noiDung, int kichThuoc = 320)
        {
            if (string.IsNullOrWhiteSpace(noiDung))
                throw new ArgumentException("Nội dung QR không được để trống.", nameof(noiDung));
            if (kichThuoc < 120 || kichThuoc > 1200)
                throw new ArgumentOutOfRangeException(nameof(kichThuoc));

            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Width = kichThuoc,
                    Height = kichThuoc,
                    Margin = 2,
                    CharacterSet = "UTF-8",
                    ErrorCorrection = ErrorCorrectionLevel.M
                }
            };
            return writer.Write(noiDung.Trim());
        }

        public static string DocMaQr(Bitmap anh)
        {
            if (anh == null) throw new ArgumentNullException(nameof(anh));
            var reader = new BarcodeReader
            {
                AutoRotate = true,
                Options = new DecodingOptions
                {
                    TryHarder = true,
                    CharacterSet = "UTF-8",
                    PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE }
                }
            };
            Result result = reader.Decode(anh);
            return result?.Text?.Trim();
        }
    }
}

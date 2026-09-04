using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace FINAL_DotNet
{
    internal static class ImageOptimizationHelper
    {
        public const int MaxProductDimension = 500;
        public const int MaxIconDimension = 256;
        public const int MaxBannerDimension = 1200;
        public const long DefaultJpegQuality = 85L;

        /// <summary>
        /// Resizes an image to fit within the specified maximum dimension while preserving aspect ratio.
        /// Uses high-quality bicubic interpolation.
        /// </summary>
        public static Bitmap CreateOptimizedThumbnail(Image sourceImage, int maxDimension = MaxProductDimension)
        {
            if (sourceImage == null)
            {
                throw new ArgumentNullException(nameof(sourceImage));
            }

            int originalWidth = sourceImage.Width;
            int originalHeight = sourceImage.Height;

            if (originalWidth <= maxDimension && originalHeight <= maxDimension)
            {
                return new Bitmap(sourceImage);
            }

            float scale = Math.Min((float)maxDimension / originalWidth, (float)maxDimension / originalHeight);
            int newWidth = Math.Max(1, (int)Math.Round(originalWidth * scale));
            int newHeight = Math.Max(1, (int)Math.Round(originalHeight * scale));

            var destinationBitmap = new Bitmap(newWidth, newHeight, PixelFormat.Format32bppArgb);
            using (var graphics = Graphics.FromImage(destinationBitmap))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                graphics.DrawImage(sourceImage, new Rectangle(0, 0, newWidth, newHeight));
            }

            return destinationBitmap;
        }

        /// <summary>
        /// Scales and crops the source image to completely cover the target area (Center-Crop / Aspect-Ratio Cover)
        /// with zero letterboxing margins and no aspect ratio distortion.
        /// </summary>
        public static Bitmap CreateCoverCroppedImage(Image sourceImage, int targetWidth, int targetHeight)
        {
            if (sourceImage == null || targetWidth <= 0 || targetHeight <= 0)
            {
                return null;
            }

            float scale = Math.Max((float)targetWidth / sourceImage.Width, (float)targetHeight / sourceImage.Height);
            int scaledWidth = Math.Max(targetWidth, (int)Math.Ceiling(sourceImage.Width * scale));
            int scaledHeight = Math.Max(targetHeight, (int)Math.Ceiling(sourceImage.Height * scale));

            int posX = (targetWidth - scaledWidth) / 2;
            int posY = (targetHeight - scaledHeight) / 2;

            var resultBitmap = new Bitmap(targetWidth, targetHeight, PixelFormat.Format32bppArgb);
            using (var graphics = Graphics.FromImage(resultBitmap))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                graphics.DrawImage(sourceImage, new Rectangle(posX, posY, scaledWidth, scaledHeight));
            }

            return resultBitmap;
        }

        /// <summary>
        /// Locates an image file by relative path across BaseDirectory, Assembly Directory, and Project Directory.
        /// </summary>
        public static string FindImageFile(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath) || Path.IsPathRooted(relativePath))
            {
                return null;
            }

            string normalized = relativePath.Replace('/', Path.DirectorySeparatorChar)
                                            .Replace('\\', Path.DirectorySeparatorChar);

            // 1. Check in BaseDirectory
            string candidate = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, normalized));
            if (File.Exists(candidate)) return candidate;

            // 2. Check in Assembly Directory
            try
            {
                string asmLocation = typeof(ImageOptimizationHelper).Assembly.Location;
                if (!string.IsNullOrWhiteSpace(asmLocation))
                {
                    string asmDir = Path.GetDirectoryName(asmLocation);
                    if (!string.IsNullOrWhiteSpace(asmDir))
                    {
                        string asmVien = Path.GetFullPath(Path.Combine(asmDir, normalized));
                        if (File.Exists(asmVien)) return asmVien;
                    }
                }
            }
            catch { }

            // 3. Check in Project Directory
            string projectDir = FindProjectDirectory();
            if (!string.IsNullOrWhiteSpace(projectDir))
            {
                candidate = Path.GetFullPath(Path.Combine(projectDir, normalized));
                if (File.Exists(candidate)) return candidate;
            }

            return null;
        }

        /// <summary>
        /// Takes an external source image file from any location, optimizes it to max product dimensions,
        /// and saves it into the project Resources folder with a sanitized unique name.
        /// Returns the relative path (e.g. "Resources\sp_20260904_123456.png").
        /// </summary>
        public static string SaveOptimizedProductImage(string sourceFilePath, string projectDirectory = null)
        {
            if (string.IsNullOrWhiteSpace(sourceFilePath) || !File.Exists(sourceFilePath))
            {
                throw new FileNotFoundException("Tệp ảnh nguồn không tồn tại.", sourceFilePath);
            }

            string baseDir = projectDirectory ?? FindProjectDirectory();
            if (string.IsNullOrWhiteSpace(baseDir))
            {
                baseDir = AppDomain.CurrentDomain.BaseDirectory;
            }

            string resourcesDir = Path.Combine(baseDir, "Resources");
            if (!Directory.Exists(resourcesDir))
            {
                Directory.CreateDirectory(resourcesDir);
            }

            string rawFileName = Path.GetFileNameWithoutExtension(sourceFilePath);
            string sanitizedName = SanitizeFileName(rawFileName);
            if (string.IsNullOrWhiteSpace(sanitizedName))
            {
                sanitizedName = "sp";
            }

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string outputFileName = $"{sanitizedName}_{timestamp}.png";
            string targetPath = Path.Combine(resourcesDir, outputFileName);

            using (var fileStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var originalImage = Image.FromStream(fileStream))
            using (var optimizedBitmap = CreateOptimizedThumbnail(originalImage, MaxProductDimension))
            {
                optimizedBitmap.Save(targetPath, ImageFormat.Png);

                // Mirror to bin/Debug folder if running during development
                string binResourcesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
                if (!string.Equals(resourcesDir, binResourcesDir, StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        if (!Directory.Exists(binResourcesDir))
                        {
                            Directory.CreateDirectory(binResourcesDir);
                        }
                        string binTargetPath = Path.Combine(binResourcesDir, outputFileName);
                        optimizedBitmap.Save(binTargetPath, ImageFormat.Png);
                    }
                    catch
                    {
                        // Best effort mirror
                    }
                }
            }

            return Path.Combine("Resources", outputFileName);
        }

        public static string FindProjectDirectory()
        {
            string startDir = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                string asmLocation = typeof(ImageOptimizationHelper).Assembly.Location;
                if (!string.IsNullOrWhiteSpace(asmLocation))
                {
                    startDir = Path.GetDirectoryName(asmLocation);
                }
            }
            catch { }

            var directory = new DirectoryInfo(startDir);
            while (directory != null)
            {
                if (File.Exists(Path.Combine(directory.FullName, "FINAL_DotNet.csproj")))
                {
                    return directory.FullName;
                }
                directory = directory.Parent;
            }
            return null;
        }

        private static string SanitizeFileName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "image";
            char[] invalidChars = Path.GetInvalidFileNameChars();
            var sanitized = new string(name.Where(c => !invalidChars.Contains(c) && c != ' ').ToArray());
            return sanitized.ToLowerInvariant();
        }
    }
}

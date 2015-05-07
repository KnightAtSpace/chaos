using log4net.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace Knight.Common.Resources
{
    /// <summary>
    /// A singelton that provides all icons from resources as bitmap images used for binding purposes in WPF controls.
    /// </summary>
    public class BitmapImageProvider
    {
        private static BitmapImageProvider instance;
        private IDictionary<string, BitmapImage> availableImages;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static BitmapImageProvider Instance
        {
            get
            {
                if (instance != null) return instance;
                return instance = new BitmapImageProvider();
            }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="BitmapImageProvider"/> class from being created.
        /// </summary>
        private BitmapImageProvider()
        {
            availableImages = new Dictionary<string, BitmapImage>();

            ConvertIconBitmaps();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="BitmapImageProvider"/> class.
        /// </summary>
        ~BitmapImageProvider()
        {
            this.availableImages.Clear();
        }

        private void ConvertIconBitmaps()
        {
            // get all bitmaps from the generated icon resources
            IList<Bitmap> bitmaps = new List<Bitmap>();

            foreach (PropertyInfo pInfo in typeof(Icons).GetProperties(BindingFlags.Public | BindingFlags.Static))
            {
                if (pInfo.PropertyType == typeof(Bitmap))
                {
                    Bitmap bitmap = pInfo.GetValue(null, null) as Bitmap;
                    if (bitmap == null) continue;

                    // convert into bitmap images
                    MemoryStream mStream = new MemoryStream();
                    bitmap.Save(mStream, bitmap.RawFormat);
                    BitmapImage bImage = new BitmapImage();

                    bImage.BeginInit();
                    bImage.CacheOption = BitmapCacheOption.OnLoad;
                    bImage.StreamSource = mStream;
                    bImage.EndInit();

                    this.availableImages.Add(pInfo.Name, bImage);
                }
            }
        }

        /// <summary>
        /// Gets a bitmap image by the name of the image file without extension.
        /// </summary>
        /// <param name="imageName">Name of the image.</param>
        /// <returns>Null if not found.</returns>
        public BitmapImage GetByName(string imageName)
        {
            foreach (KeyValuePair<string, BitmapImage> pair in this.availableImages)
            {
                if (pair.Key.Equals(imageName, StringComparison.OrdinalIgnoreCase))
                {
                    return this.availableImages[pair.Key];
                }
            }

            return null;
        }

        /// <summary>
        /// Gets a bitmap image for a log4net level.
        /// Supported level: Debug, Info, Warn, Alert, Error, Fatal.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns>Null if not found.</returns>
        public BitmapImage GetForLogLevel(Level level)
        {
            if (level == null) throw new ArgumentNullException("level");

            switch (level.DisplayName)
            {
                case "DEBUG":
                    return GetByName("unknown");
                case "INFO":
                    return GetByName("info");
                case "WARN":
                    return GetByName("warning");
                case "ALERT":
                    return GetByName("warning");
                case "ERROR":
                    return GetByName("error");
                case "FATAL":
                    return GetByName("error");
                case "Verbose":
                    return GetByName("unknown");
                default:
                    return GetByName("unknown");
            }
        }
    }
}

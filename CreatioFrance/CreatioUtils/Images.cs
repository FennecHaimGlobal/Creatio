using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatioUtils
{
    public class Images
    {
        /// <summary>
        /// Images to base64.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public static string ImageToBase64(Image image, ImageFormat format = null)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                if (format == null)
                {
                    format = image.RawFormat;
                }
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        /// <summary>
        /// Base64s to image.
        /// </summary>
        /// <param name="base64String">The base64 string.</param>
        /// <returns></returns>
        public static Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
    }
}

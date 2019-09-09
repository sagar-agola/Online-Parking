using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using System;
using System.IO;

namespace PBS.Business.Utilities.Helpers
{
    public static class ImageProcessing
    {
        public static string ProcessIFormFile (IFormFile file)
        {
            using (MemoryStream stream = new MemoryStream ())
            {
                file.CopyTo (stream);
                byte[] bytes = stream.ToArray ();

                using (Image image = Image.Load (bytes))
                {
                    if (image.Height > image.Width)
                    {
                        return ResizeImage (image, image.Height);
                    }
                    else if (image.Width > image.Height)
                    {
                        return ResizeImage (image, image.Width);
                    }
                    else
                    {
                        return Convert.ToBase64String (bytes);
                    }
                }
            }
        }

        private static string ResizeImage (Image image, int length)
        {
            image.Mutate (i =>
            {
                i.Resize (new ResizeOptions
                {
                    Mode = ResizeMode.BoxPad,
                    Position = AnchorPositionMode.Center,
                    Size = new Size (length, length)
                }).Crop (length, length);
            });

            return EncodeImageInString (image);
        }

        private static string EncodeImageInString (Image image)
        {
            using (MemoryStream newStream = new MemoryStream ())
            {
                image.SaveAsJpeg (newStream);
                byte[] newBytes = newStream.ToArray ();

                return Convert.ToBase64String (newBytes);
            }
        }
    }
}

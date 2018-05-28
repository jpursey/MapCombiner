using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCombiner
{
    class ImageDatabase
    {
        public static Result<Bitmap> Load(string filename)
        {

            Bitmap bitmap = null;
            if (s_bitmaps.TryGetValue(filename, out bitmap))
            {
                return Result<Bitmap>.Success(bitmap);
            }
            try
            {
                using (Image fileImage = Image.FromFile(filename))
                {
                    bitmap = new Bitmap(fileImage);
                }
            }
            catch (OutOfMemoryException)
            {
                return Result<Bitmap>.Error("Unsupported image format: " + filename);
            }
            catch (System.IO.FileNotFoundException)
            {
                return Result<Bitmap>.Error("File not found: " + filename);
            }
            catch
            {
                return Result<Bitmap>.Error("Unknown error");
            }
            s_bitmaps[filename] = bitmap;
            return Result<Bitmap>.Success(bitmap);
        }

        private static Dictionary<string, Bitmap> s_bitmaps = new Dictionary<string, Bitmap>();
    }
}

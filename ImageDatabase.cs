// Copyright 2018 John Pursey
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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

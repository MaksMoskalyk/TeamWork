using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StaffDatabaseUnit
{
    class PhotoDatabaseConvertor : IPhotoConverter
    {
        public Bitmap GetImageFromBytes(byte[] byteArray)
        {
            Bitmap image = null;
            using (var memoryStream = new MemoryStream(byteArray))
            {
                image = new Bitmap(memoryStream);
            }
            return image;
        }

        public byte[] GetBytesFromImage(string fileName)
        {
            byte[] byteArray = File.ReadAllBytes(fileName);
            return byteArray;
        }
    }
}

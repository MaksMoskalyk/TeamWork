using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffDatabaseUnit
{
    public interface IPhotoConverter
    {
        Bitmap GetImageFromBytes(byte[] byteArray);
        byte[] GetBytesFromImage(string fileName);
    }
}

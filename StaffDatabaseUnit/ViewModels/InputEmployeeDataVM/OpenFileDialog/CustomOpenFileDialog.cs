using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StaffDatabaseUnit
{
    class CustomOpenFileDialog : IOpenFileDialog
    {
        public string GetFilePath()
        {
            string path = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "JPG Files (*.jpg)|*.jpeg|PNG Files (*.png)|*.png";
            openFileDialog.Filter = "Image files (*.jpg, *.png) | *.jpg; *.png";
            openFileDialog.InitialDirectory = @"C:\Desktop";

            if (openFileDialog.ShowDialog() == true)
            {
                path = openFileDialog.FileName;
            }

            return path;
        }
    }
}

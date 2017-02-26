using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileSharing.Client;
using System.ServiceModel;
using Client.ServiceReference1;

using System.IO;


namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select File";
                
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] bytes = File.ReadAllBytes(openFileDialog.FileName);
                    string name = openFileDialog.FileName.Substring(openFileDialog.FileName.LastIndexOf('\\')+1);
                    FileTransferManager transfer = new FileTransferManager();
                    FileTransferResponse success = transfer.SendFileToServerTempFolder(name, bytes);                   
                }
            }           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileTransferManager transfer = new FileTransferManager();
            FileTransferResponse success = transfer.GetFile("App.config.transform");
            FileDescription[] descs = transfer.GetServerFilesInfoFromTempFolder();
            foreach (var item in descs)
            {
                listBox1.Items.Add(item.Name + " " + (item.SizeBytes/1000000) +"Mb");
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FileTransferManager transfer = new FileTransferManager();
            string item = listBox1.SelectedItem.ToString().Remove(listBox1.SelectedItem.ToString().IndexOf(' ')).Trim();
            FileTransferResponse success = transfer.DeleteFileFromServerTempFolder(item);
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace FileReaderSystem
{
    public partial class Form1 : Form
    {
        
        //List<String> finasNumber=new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Filter = "XML files (*.vrx)|*.vrx";
            dialog.InitialDirectory = @"C:\";
            dialog.Title = "Please select an file.";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string[] selectedfilenamewithpath = dialog.FileNames;

                //string[] full_file_name = selectedfilenamewithpath.Split('\\');
                //string file_name = full_file_name[full_file_name.Length - 1];
                //string[] file_extension_split = full_file_name[full_file_name.Length - 1].Split('.');
                //string file_extension = file_extension_split[file_extension_split.Length - 1];
                #region read xml and get codes and versions
                foreach (var item in selectedfilenamewithpath)
                {
                    loaAndSaveFile(item);
                }
                foreach (var item in AllVehicleInfo.allVehicleInfo)
                {
                    VehicleInfo vehicleInfo = item.Value;
                    foreach (var codes in vehicleInfo.allCodesAndVersions)
                    {
                        checkedListBox1.Items.Add(codes.Key + " | " + vehicleInfo.finasNumber );
                    }
                    
                }
                #endregion
                
            }
        }

        private void loaAndSaveFile(String path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            GetCodesAndVersions(xmlDocument);
        }

        private void proceed_Click(object sender, EventArgs e)
        {
           // Dictionary<string, VehicleInfo> selectedCodesAndVersions= new Dictionary<string, VehicleInfo>();
            foreach (string item in checkedListBox1.CheckedItems)
            {
                string finasNumber = item.Split('|')[1].Trim();
                string code = item.Split('|')[0].Trim();
                AllVehicleInfo.allVehicleInfo[finasNumber].allCodesAndVersions[code].isSelected = true;
            }
            this.Hide();
           
            Form2 frm2 = new Form2();
            frm2.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void GetCodesAndVersions(XmlDocument xmlDocument)
        {
            var componentsList = xmlDocument.GetElementsByTagName("Component");
            var versions = xmlDocument.GetElementsByTagName("SMR");
            VehicleInfo vehicleInfo = new VehicleInfo();
            vehicleInfo.allCodesAndVersions = new Dictionary<string, VersionInfo>(); // Code and Versions
            vehicleInfo.finasNumber =  xmlDocument.GetElementsByTagName("FiNASNumber")[0].InnerText; // Vehicle Name
            foreach (XmlNode item in componentsList)
            {
                string shortCode = item.ChildNodes[0].InnerText;
                foreach (XmlNode version in versions)
                {
                    if (version.Attributes["ShortName"]?.Value == shortCode)
                    {
                        vehicleInfo.allCodesAndVersions[shortCode] = new VersionInfo() { xmlVersion = new Version(version.InnerText) };
                    }
                }

            }
            AllVehicleInfo.allVehicleInfo.Add(vehicleInfo.finasNumber, vehicleInfo);
            
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

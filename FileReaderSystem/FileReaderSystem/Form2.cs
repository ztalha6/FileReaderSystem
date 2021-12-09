using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileReaderSystem
{
    public partial class Form2 : Form
    {
        //Dictionary<string, VehicleInfo> allVehicleInfo ;
        //List<String> finasNumber;
        public Form2()
        {
            InitializeComponent();
        }
        //public Form2()
        //{
        //    InitializeComponent();
        //    //this.allVehicleInfo = selections;
        //    //this.finasNumber = numbers;
        //}

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                int pointX = 30;
                int pointY = 40;
                panel2.Controls.Clear();
                var selectedCodesAndVersions = AllVehicleInfo.getSelectedCodesAndVersions();
                foreach (var item in selectedCodesAndVersions)
                {
                    Label l = new Label();
                    //l.Text = "AD_FL223 | 297-00345";
                    l.Text = item.Key;
                    l.Location = new Point(pointX, pointY);
                    l.Size = new Size(300,30);
                    panel2.Controls.Add(l);
                    panel2.Show();
                    pointY += 50;
                    TextBox a = new TextBox();
                    a.Text = item.Value.ToString();
                    a.Location = new Point(pointX + 350, pointY - 55);
                    a.Name = "TextBox" + item.Key;
                    panel2.Controls.Add(a);
                    panel2.Show();
                    pointY += 20;
                }
               
            }
            catch (Exception)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void next_Click(object sender, EventArgs e)
        {
            var selectedCodesAndVersions = AllVehicleInfo.getSelectedCodesAndVersions();
            Dictionary<string,Version> enteredVersions = new Dictionary<string, Version>();
            foreach (var item in selectedCodesAndVersions)
            {
                enteredVersions[item.Key] = new Version(((TextBox)panel2.Controls["TextBox" + item.Key]).Text) ;
            }
            this.Hide();
            Form3 frm3 = new Form3(selectedCodesAndVersions, enteredVersions);
            frm3.ShowDialog();
        }
    }
}

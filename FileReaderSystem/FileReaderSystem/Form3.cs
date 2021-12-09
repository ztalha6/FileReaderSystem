using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileReaderSystem
{
    public partial class Form3 : Form
    {
        DataTable dt = new DataTable();
        DataGridView dataGridView1 = new DataGridView();
        Dictionary<string, Version> selectedCodesAndVersions;
        List<String> finasNumber;
        Dictionary<string, Version> newVersions;

        public PrintDocument PrintDocument1;

        public Form3()
        {
            InitializeComponent();
        }
        public Form3(Dictionary<string, Version> selections, Dictionary<string,Version> entries)
        {
            InitializeComponent();
            this.selectedCodesAndVersions = selections;
            this.finasNumber = AllVehicleInfo.getFinasNumbers();
            this.newVersions = entries;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

            tableLayoutPanel1.ColumnCount = finasNumber.Count + 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tableLayoutPanel1.Controls.Add(new Label() { Text = "Finas", BackColor = Color.Transparent }, 0, 0);
            dt.Columns.Add("Finas");
            for (int i = 0; i < finasNumber.Count; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = finasNumber.ElementAt(i), BackColor = Color.Transparent }, i + 1, 0);
                dt.Columns.Add(finasNumber.ElementAt(i));
            }
            tableLayoutPanel1.RowCount = selectedCodesAndVersions.Count;
            for (int i = 0; i < selectedCodesAndVersions.Count; i++)
            {

                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
                
                tableLayoutPanel1.Controls.Add(new Label() { Text = selectedCodesAndVersions.ElementAt(i).Key.Split('|')[0].Trim(), BackColor = Color.Transparent }, 0, i + 1);
                dt.Rows.Add(selectedCodesAndVersions.ElementAt(i).Key.Split('|')[0].Trim());
            }

            //logic for green black and red
            #region To compare versions
            //Less than zero The current Version object is a version before version.
            //Zero The current Version object is the same version as version.
            //Greater than zero   The current Version object is a version subsequent to version, or version is null.
            //foreach (var item in selectedCodesAndVersions)
            //{
            //    if (item.Value.CompareTo(newVersions[item.Key]) == 0)
            //    {
            //        //show newVersions[item.Key].ToString() in green color only.
            //    }

            //    else if (item.Value.CompareTo(newVersions[item.Key]) < 0)
            //    {
            //        //show show newVersions[item.Key].ToString() in black.
            //        //show show item.Value.ToString() in red.
            //    }
            //    else if (item.Value.CompareTo(newVersions[item.Key]) > 0)
            //    {
            //        //show show newVersions[item.Key].ToString() in black.
            //        //show show item.Value.ToString() in green.
            //    }
            //}

            #endregion

            for (int j = 0; j < finasNumber.Count; j++)
            {
                for (int i = 0; i < selectedCodesAndVersions.Count; i++)
                {
                    Panel pn = new Panel();
                    tableLayoutPanel1.Controls.Add(pn, j+1, i + 1);
                    Label l1 = new Label();
                    l1.AutoSize = true;
                    Label l2 = new Label();
                    l2.AutoSize = true;
                    
                    if (selectedCodesAndVersions.ElementAt(i).Key.Contains(finasNumber.ElementAt(j)))
                    {
                        if (selectedCodesAndVersions.ElementAt(i).Value.CompareTo(newVersions[selectedCodesAndVersions.ElementAt(i).Key]) == 0)
                        {
                            //show newVersions[item.Key].ToString() in green color only.
                            l1.Text = newVersions[selectedCodesAndVersions.ElementAt(i).Key].ToString();
                            l1.ForeColor = Color.Green;
                            l2.Text = "";
                            DataRow dr = dt.Rows[i];
                            dr[j+1] = "New version:" + newVersions[selectedCodesAndVersions.ElementAt(i).Key].ToString();

                        }
                        else if (selectedCodesAndVersions.ElementAt(i).Value.CompareTo(newVersions[selectedCodesAndVersions.ElementAt(i).Key]) < 0)
                        {
                            //show show newVersions[item.Key].ToString() in black.
                            //show show item.Value.ToString() in red.
                            l1.Text = newVersions[selectedCodesAndVersions.ElementAt(i).Key].ToString();
                            l1.ForeColor = Color.Black;
                            l2.Text = selectedCodesAndVersions.ElementAt(i).Value.ToString();
                            l2.ForeColor = Color.Red;

                            DataRow dr = dt.Rows[i];
                            dr[j + 1] = "Current version:" + newVersions[selectedCodesAndVersions.ElementAt(i).Key].ToString() + "\n Selected version:" + selectedCodesAndVersions.ElementAt(i).Value.ToString();
                        }
                        else if (selectedCodesAndVersions.ElementAt(i).Value.CompareTo(newVersions[selectedCodesAndVersions.ElementAt(i).Key]) > 0)
                        {
                            //show show newVersions[item.Key].ToString() in black.
                            //show show item.Value.ToString() in green.
                            l1.Text = newVersions[selectedCodesAndVersions.ElementAt(i).Key].ToString();
                            l1.ForeColor = Color.Black;
                            l2.Text = selectedCodesAndVersions.ElementAt(i).Value.ToString();
                            l2.ForeColor = Color.Green;
                            DataRow dr = dt.Rows[i];
                            dr[j + 1] = "Current version:" + newVersions[selectedCodesAndVersions.ElementAt(i).Key].ToString() +"\n Selected version:" + selectedCodesAndVersions.ElementAt(i).Value.ToString();

                        }
                    }
                    l1.BackColor = Color.Transparent;
                    l2.BackColor = Color.Transparent;
                    l1.Location = new Point(0, 0);
                    l2.Location = new Point(0, 25);
                   
                    pn.Controls.Add(l1);
                    pn.Controls.Add(l2);
                    pn.BackColor = Color.Transparent;
                }
            }
            
        }

        private void tableLayoutPanel1_CellPaint_1(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Row == 0)
            {
                e.Graphics.FillRectangle(Brushes.Orange, e.CellBounds);
            }
            else
            {
                if (e.Row % 2 == 1)
                {
                    e.Graphics.FillRectangle(Brushes.LightYellow, e.CellBounds);
                }
                if (e.Row % 2 == 0)
                {
                    e.Graphics.FillRectangle(Brushes.WhiteSmoke, e.CellBounds);
                }
            }
            
            //if (e.Row == 3)
            //{
            //    e.Graphics.FillRectangle(Brushes.LightYellow, e.CellBounds);
            //}
        }

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if ( e.Row == 0)
            {
                e.Graphics.FillRectangle(Brushes.Orange, e.CellBounds);
            }
            if (e.Row == 1)
            {
                e.Graphics.FillRectangle(Brushes.LightYellow, e.CellBounds);
            }
            if (e.Row == 2)
            {
                e.Graphics.FillRectangle(Brushes.WhiteSmoke, e.CellBounds);
            }
            if (e.Row == 3)
            {
                e.Graphics.FillRectangle(Brushes.LightYellow, e.CellBounds);
            }
        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //dt.Clear();
            //for (int i = 0; i < finasNumber.Count; i++)
            //{
            //    if (i == 0)
            //    {
            //        dt.Columns.Add("Finas");
            //    }

            //    dt.Columns.Add(finasNumber.ElementAt(i));

            //}


            //for (int i = 0; i < selectedCodesAndVersions.Count; i++)
            //{
            //    DataRow row = dt.NewRow();
            //    row["Finas"] = selectedCodesAndVersions.ElementAt(i).Key.Split('|')[0].Trim();
            //    dt.Rows.Add(row);
            //}
            //for (int j = 1; j < finasNumber.Count; j++)
            //{
            //    for (int i = 1; i < selectedCodesAndVersions.Count; i++)
            //    {
            //        DataRow dataRow = dt.Rows[i];
                    
            //        if (selectedCodesAndVersions.ElementAt(i).Key.Contains(finasNumber.ElementAt(j)))
            //        {
            //            if (selectedCodesAndVersions.ElementAt(i).Value.CompareTo(newVersions[selectedCodesAndVersions.ElementAt(i).Key]) == 0)
            //            {
            //                dataRow.SetField(j, newVersions[selectedCodesAndVersions.ElementAt(i).Key].ToString());
                            

            //            }
            //            else if (selectedCodesAndVersions.ElementAt(i).Value.CompareTo(newVersions[selectedCodesAndVersions.ElementAt(i).Key]) < 0)
            //            {
            //                //show show newVersions[item.Key].ToString() in black.
            //                //show show item.Value.ToString() in red.
            //                dataRow.SetField(j + 1, newVersions[selectedCodesAndVersions.ElementAt(i).Key].ToString() 
            //                    + Environment.NewLine +
            //                    selectedCodesAndVersions.ElementAt(i).Value.ToString());
                            
            //            }
            //            else if (selectedCodesAndVersions.ElementAt(i).Value.CompareTo(newVersions[selectedCodesAndVersions.ElementAt(i).Key]) > 0)
            //            {
            //                dataRow.SetField(j, newVersions[selectedCodesAndVersions.ElementAt(i).Key].ToString()
            //                     + Environment.NewLine +
            //                     selectedCodesAndVersions.ElementAt(i).Value.ToString());
            //            }
            //        }
                   
            //    }
            //}

            var lines = new List<string>();

            string[] columnNames = dt.Columns
                .Cast<DataColumn>()
                .Select(column => column.ColumnName)
                .ToArray();

            var header = string.Join(",", columnNames.Select(name => $"\"{name}\""));
            lines.Add(header);

            var valueLines = dt.AsEnumerable()
                .Select(row => string.Join(",", row.ItemArray.Select(val => $"\"{val}\"")));

            lines.AddRange(valueLines);
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = @"C:\";
            dialog.Title = "Please select an file.";
            

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = dialog.FileName;
                File.WriteAllLines(filePath+".csv", lines);
            }
        }

        private void next_Click(object sender, EventArgs e)
        {
            //PrintPreviewDialog PD = new PrintPreviewDialog();
            //PD.Document = PrintDocument1;
            //PD.ShowDialog();

            PrintDocument document = new PrintDocument();
            document.PrintPage += new PrintPageEventHandler(document_PrintPage);

            PrintPreviewDialog ppDialog = new PrintPreviewDialog();
            ppDialog.Document = document;
            ppDialog.Show();
        }

        void document_PrintPage(object sender, PrintPageEventArgs e)
        {
            PrintDocument document = (PrintDocument)sender;
            Graphics g = e.Graphics;

            Brush brush = new SolidBrush(Color.Black);
            Pen pen = new Pen(brush);
            Font font = new Font("Arial", 10, FontStyle.Bold);
            Font fonte = new Font("Arial", 15, FontStyle.Bold);
            int x = 0, y = 0, width = 200, height = 30;

            SizeF sizeeee = g.MeasureString("TIME :: ", fonte);
            float xPaddingeee = (width - sizeeee.Width) / 2;
            g.DrawString("TIME :: ", fonte, brush, x + xPaddingeee, y + 5);
            x += width;


            SizeF sizee = g.MeasureString(DateTime.Now.ToString(), fonte);
            float xPaddinge = (width - sizee.Width) / 2;

            g.DrawString(DateTime.Now.ToString(), fonte, brush, x + xPaddinge, y + 5);
            x += width;

            for (int kk = 0; kk < 2; kk++)
            {
                SizeF sizeee = g.MeasureString("", font);
                float xPaddingee = (width - sizee.Width) / 2;

                g.DrawString("", font, brush, x + xPaddingee, y + 5);
                x += width;
            }
            x = 0;
            y += 60;


            foreach (DataColumn column in dt.Columns)
            {
                g.DrawRectangle(pen, x, y, width, height);
                SizeF size = g.MeasureString(column.ColumnName, fonte);
                float xPadding = (width - size.Width) / 2;

                g.DrawString(column.ColumnName, fonte, brush, x + xPadding, y + 5);
                x += width;
            }


            x = 0;
            y += 30;
            int columnCount = dt.Columns.Count;

            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < columnCount; i++)
                {
                    g.DrawRectangle(pen, x, y, width, height);
                    SizeF size = g.MeasureString(row[i].ToString(), font);
                    float xPadding = (width - size.Width) / 2;

                    g.DrawString(row[i].ToString(), font, brush, x + xPadding, y + 5);
                    x += width;
                }
                x = 0;
                y += 30;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ukrotitel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.ShowDialog();
            foreach (var item in openFileDialog.FileNames)
            {
                string path = item;
                string newpatch = new FileInfo(path).FullName.Substring(0, new FileInfo(path).FullName.Length - 4) + "_p" + new FileInfo(path).Extension;
                using (StreamReader sr = new StreamReader(path, true))
                {
                    string line;
                    using (StreamWriter sw = new StreamWriter(newpatch, false))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.ToLower().Contains(textBox1.Text))
                            {
                                if (numericUpDown1.Value > line.Length)
                                {
                                    numericUpDown1.Value = line.Length;
                                }
                                double x = Convert.ToDouble(numericUpDown1.Value) / 2;
                                line = line.Substring(0, Convert.ToInt16(Math.Ceiling(x))) + line.Substring(line.Length - Convert.ToInt16(Math.Floor(x)), Convert.ToInt16(Math.Floor(x)));
                            }
                            sw.WriteLine(line);
                        }
                    }
                }
            }            
        }
    }
}

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
                using (StreamReader sr = new StreamReader(path, Encoding.Default))
                {
                    string line;
                    using (StreamWriter sw = new StreamWriter(newpatch, false, Encoding.Default))
                    {
                        bool flag = false;
                        string substr = "";
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.ToLower().Contains(textBox1.Text.ToLower()))
                            {
                                if (flag) 
                                {                                    
                                    substr = substr.Length <120 ? substr : substr.Substring(0, 18) + substr.Substring(substr.Length-102);
                                    sw.WriteLine(substr);
                                    substr = "";
                                }
                                flag = true;
                                substr = line.Substring(0, line.Length);
                            }
                            else if (line!="" && line[0] == '-') 
                            {
                                substr = substr.Substring(0, substr.Length - 2);
                                substr += line.Substring(4);
                            }
                            else
                            {
                                if (flag)
                                {                                    
                                    substr = substr.Length < 120 ? substr : substr.Substring(0, 18) + substr.Substring(substr.Length - 102);
                                    sw.WriteLine(substr);
                                    substr = "";
                                    flag = false;
                                }
                                sw.WriteLine(line);
                            }                            
                        }
                    }
                }
            }            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

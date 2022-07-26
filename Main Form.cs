using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace drag_and_drop
{
    public partial class MainForm : Form
    {


        public static string varFileType;
        public static string varFileAdresse;
        public static string varFileNewName;
        public static string varTodaysTime;
        public static string varDateDDMMYYYY;
        public static string varObjectName;


        public void LoadUsers()
        { 
            using (System.IO.StreamReader sr = new System.IO.StreamReader(@sPath))
            {
                while (!sr.EndOfStream)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        string strListItem = sr.ReadLine();


                        if (!String.IsNullOrEmpty(strListItem))
                        {
                            ListBoxObjects.Items.Add(strListItem);
                        }
                    }
                }
            }
        }

        //******** variables **********
        const string sPath = "Data//ObjectList.txt";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            LoadUsers();

        }

        private void label1_DragOver(object sender, DragEventArgs e)
        {

            label2.ForeColor = Color.Red;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void label1_DragDrop(object sender, DragEventArgs e)
        {
            label1.BackColor = Color.Transparent;

            varFileType = guna2ComboBox1.SelectedItem.ToString();
            if (varFileType == "")
            {
                varFileType = ".pdf";
            }

            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[]; // get all files droppeds  

            if (files != null && files.Any())
            {
                varFileAdresse = files.First(); //select the first one 

                FileInfo file = new FileInfo(varFileAdresse); //rename file adresse
                file.Rename(varObjectName + " " + guna2TextDate.Text + varFileType); //rename file new name
            }
            label2.ForeColor = Color.White;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            varDateDDMMYYYY = DateTime.Now.ToString("dd.MM.yyyy");
            guna2TextDate.Text = varDateDDMMYYYY;
            timer1.Enabled = false;

        }

        private void ListBoxObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            varObjectName = ListBoxObjects.SelectedItem.ToString();
        }

        private void label1_DragLeave(object sender, EventArgs e)
        {
            label1.BackColor = Color.Transparent;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_DragOver(object sender, DragEventArgs e)
        {
            label2.ForeColor = Color.Red;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void label2_DragDrop(object sender, DragEventArgs e)
        {



            label1.BackColor = Color.Transparent;



            varFileType = guna2ComboBox1.SelectedItem.ToString();
            if (ListBoxObjects.SelectedItem == null)
            {
                varFileType = ".pdf";
            }

            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[]; // get all files droppeds  
            
            if (files != null && files.Any())
            {
                varFileAdresse = files.First(); //select the first one 

                FileInfo file = new FileInfo(varFileAdresse); //rename file adresse
                file.Rename(varObjectName + " " + guna2TextDate.Text + varFileType); //rename file new name
            }
            label2.ForeColor = Color.White;
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblPin_Click(object sender, EventArgs e)
        {
            this.TopMost = true;
            lblPin.Visible = false;
            lblPin.Enabled = false;
            lblUnpin.Visible = true;
            lblUnpin.Enabled = true;
        }

        private void lblUnpin_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            lblPin.Visible = true;
            lblPin.Enabled = true;
            lblUnpin.Visible = false;
            lblUnpin.Enabled = false;
        }
    }
}



namespace System.IO
{

    public static class ExtendedMethod
    {
        public static void Rename(this FileInfo fileInfo, string newName)
        {
            fileInfo.MoveTo(fileInfo.Directory.FullName + "\\" + newName);
        }
    }
}
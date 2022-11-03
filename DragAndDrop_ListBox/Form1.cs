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

namespace DragAndDrop_ListBox
{
    public partial class Form1 : Form
    {
        int _y = 0;
        int _x = 0;
        int _count = 0;
        bool _flag = false;

        public Form1()
        {
            InitializeComponent();
            lbItems.DragDrop += LbItems_DragDrop;
            lbItems.DragEnter += LbItems_DragEnter;
        }

        private void LbItems_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void LbItems_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            for (int i = 0; i < files.Length; i++)
            {
                if (Path.GetExtension(files[i]) != ".jpg" &&
                    Path.GetExtension(files[i]) != ".png" &&
                    Path.GetExtension(files[i]) != ".bmp" &&
                    Path.GetExtension(files[i]) != ".gif")
                {
                    MessageBox.Show("Incorrect Format! Only Images allow!");
                    continue;
                }

                PictureBox pictureBox = new PictureBox();
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Size = new Size(300, 300);
                pictureBox.Location = new Point(_x, _y);
                ChangePosition();

                Bitmap bitmap = new Bitmap(files[i]);
                pictureBox.Image = bitmap;
                lbItems.Controls.Add(pictureBox);
            }
        }

        private void ChangePosition()
        {
            if(_flag)
            {
                _x = 0;
            }
            else
            {
                _x += 300;
            }
            _count++;

            if (_count == 2)
            {
                _count = 0;
                _y += 300;
            }

            _flag = !_flag;
        }
    }
}

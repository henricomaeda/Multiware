using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Multiware
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            Select();

            string directory = @"C:\Multiware";
            string file = @"C:\Multiware\icon.png";
            string temp = @"C:\Multiware\icon_temp.png";

            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            else if (File.Exists(temp))
            {
                if (File.Exists(temp)) File.Delete(file);
                File.Copy(temp, file);
                File.Delete(temp);
                picImage.Image = Image.FromFile(file);
            }

            if (File.Exists(file)) picImage.Image = Image.FromFile(file);
        }

        private void BtnChangeImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Filter = "Arquivo de imagem|*.JPG; *.JPEG; *.PNG|" + "Todos os arquivos (*.*)|*.*";
            ofd.Title = "Procurar imagem para atualizar";

            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string title = "";
                MessageBoxIcon icon = MessageBoxIcon.Error;
                MessageBoxButtons buttons = MessageBoxButtons.OK;

                try
                {
                    string fileName = Path.GetFileNameWithoutExtension(ofd.FileName);
                    string fileExtension = Path.GetExtension(ofd.FileName);
                    string[] fileExtensions = { ".jpg", ".jpeg", ".png" };

                    if (!Array.Exists(fileExtensions, element => element == fileExtension)) MessageBox.Show("Nome do arquivo: " + fileName + "\nFormato de arquivo: " + fileExtension + "\n\nO formato de arquivo deve ser JPG, JPEG ou PNG.", title, buttons, icon);
                    else
                    {
                        string file = @"C:\Multiware\icon_temp.png";
                        if (File.Exists(file)) File.Delete(file);
                        File.Copy(@ofd.FileName, file);

                        dr = MessageBox.Show("Imagem atualizada com sucesso!" + "\nPara visualizá-la, re-abra o mesmo.\n\nAperte em SIM, para reinicia-lo.", title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dr == DialogResult.Yes) Application.Restart();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERRO: " + ex.Message, title, buttons, icon);
                }
            }
        }

        private void Animation(Button b)
        {
            int x = lblLine.Location.X;
            int y = lblLine.Location.Y;
            int y_02 = b.Location.Y;

            if (b.Location.Y > y)
            {
                int y_03 = y_02 - y;
                Task.Run(() =>
                {
                    for (int i = 0; i <= (y_03 - 7); i++)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            lblLine.Location = new Point(x, y++);
                        });
                    }
                });
            }
            else
            {
                int y_03 = y - y_02;
                Task.Run(() =>
                {
                    for (int i = (y_03 + 7); i >= 0; i--)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            lblLine.Location = new Point(x, y--);
                        });
                    }
                });
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Animation(button1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Animation(button2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Animation(button3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Animation(button4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Animation(button5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Animation(button6);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.ExitThread();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yizhandaodi
{
    public partial class Form1 : Form
    {
        public string analyase;
        public bool answer_flg;
        public Form1()
        {
            InitializeComponent();

        }
    

        private void Form1_Load_1(object sender, EventArgs e)
        {
            //skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            //skinEngine1.SkinFile = Application.StartupPath + @"/skin/RealOne.ssk";

            //加载皮肤
            skinEngine1.Active = true;
            if (answer_flg == true)
            {
                pictureBox1.Show();
                pictureBox2.Hide();
                label1.Text = "正";
                label1.ForeColor = Color.Green;
                label2.Text = "确";
                label2.ForeColor = Color.Green;
                this.Text = "正确";
            }
            else
            {
                pictureBox2.Show();
                pictureBox1.Hide();
                label1.Text = "错";
                label1.ForeColor = Color.Red;
                label2.Text = "误";
                label2.ForeColor = Color.Red;
                this.Text = "错误";
            }
            txt_Any.Text = analyase;
        }
    }
}

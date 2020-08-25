using CCWin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace yizhandaodi
{
    public partial class yizhandaodi : Form
    {
        public yizhandaodi()
        {
            InitializeComponent();
        }
        //题号
        int Question_No = 0;
        int Question_Max = 0;
        List<cls_Question> Question = new List<cls_Question>();
        List<cls_Answer> Answers = new List<cls_Answer>();
        List<int> Question_count = new List<int>();
        bool delete = false;
        bool timer_s = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            //skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            skinEngine1.SkinFile = Application.StartupPath + @"/skin/office2007.ssk";

            //加载皮肤
            skinEngine1.Active = true;

            string filepath_Question = System.AppDomain.CurrentDomain.BaseDirectory + "txt_data" + @"\" + "Question.txt";
            OpenCSVFile_Question(filepath_Question);
            string filepath_Answer = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "txt_data" + @"\" + "Answer.txt";
            OpenCSVFile_Answer(filepath_Answer);
            for (int a = 1; a <= Question.Count; a++)
            {
                Question_count.Add(a);
            }
            question_read(Question_count[Question_No]);
            Question_Max = Question.Count;

        }

        private bool OpenCSVFile_Question(string filepath)
        {
            string strpath = filepath; //csv文件的路径
            try
            {
                string strline;
                string[] aryline;

                StreamReader mysr = new StreamReader(strpath, System.Text.Encoding.Default);

                while ((strline = mysr.ReadLine()) != null)
                {
                    aryline = strline.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    foreach (string a in aryline)
                    {
                        cls_Question cls_Question = new cls_Question();
                        string[] cell = a.Split(',');
                        cls_Question.Question_id = Convert.ToInt32(cell[0]);
                        cls_Question.Question_No = cell[1];
                        cls_Question.Question = cell[2];
                        cls_Question.Question_Anwser = cell[3];
                        cls_Question.Question_Analyse = cell[4];
                        cls_Question.Question_True_Answer_Flg = cell[5];
                        cls_Question.Question_Type = cell[6];
                        Question.Add(cls_Question);
                    }

                }
                return true;

            }
            catch (Exception e)
            {
                return true;
            }
        }
        private bool OpenCSVFile_Answer(string filepath)
        {
            string strpath = filepath; //csv文件的路径
            try
            {

                string strline;
                string[] aryline;

                StreamReader mysr = new StreamReader(strpath, System.Text.Encoding.Default);

                while ((strline = mysr.ReadLine()) != null)
                {
                    aryline = strline.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    foreach (string a in aryline)
                    {
                        cls_Answer cls_Question = new cls_Answer();
                        string[] cell = a.Split(',');
                        cls_Question.Answer_id = Convert.ToInt32(cell[0]);
                        cls_Question.Answer_Branch = cell[1];
                        cls_Question.Anwser = cell[2];
                        Answers.Add(cls_Question);
                    }

                }
                return true;

            }
            catch (Exception e)
            {
                return true;
            }
        }

        private void btn_Answer_Click(object sender, EventArgs e)
        {

            try
            {

                Button button = sender as Button;
                IEnumerable<cls_Question> Question_Select = from n in Question
                                                            where n.Question_id == Question_count[Question_No]
                                                            select n;

                cls_Question result = Question_Select.ToList()[0];
                if (result.Question_Type != "D")
                {
                    btn_A.Enabled = false;
                    btn_B.Enabled = false;
                    btn_C.Enabled = false;
                    btn_D.Enabled = false;
                    string True_Answer_Flg = result.Question_True_Answer_Flg;
                    Form1 form1 = new Form1();
                    if (button.Text == True_Answer_Flg)
                    {
                        form1.answer_flg = true;

                    }
                    else
                    {
                        form1.answer_flg = false;
                    }
                    form1.analyase = result.Question_Anwser + "\r\n" + result.Question_Analyse;
                    this.Hide();
                    form1.StartPosition = FormStartPosition.CenterParent;
                    form1.ShowDialog();
                    this.Show();
                    if (delete == true)
                    {
                        Question_count.RemoveAt(Question_No);
                        delete = false;
                    }
                }
                else
                {
                    if (txt_Double.Text != "")
                    {
                        txt_Double.Text += "," + button.Text;
                    }
                    else
                    {

                        txt_Double.Text = button.Text;
                    }
                }

            }
            catch (Exception EE)
            {

            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            btn_A.Enabled = true;
            btn_B.Enabled = true;
            btn_C.Enabled = true;
            btn_D.Enabled = true;
            btn_E.Enabled = true;
            delete = true;
            if (timer_s == false)
            {
                timer1.Start();
                btn_next.Text = "停止";
                timer_s = true;

            }
            else
            {
                timer1.Stop();
                btn_next.Text = "开始";
                timer_s = false;

            }

        }

        private void question_read(int question_No)
        {
            try
            {
                IEnumerable<cls_Question> Question_Select = from n in Question
                                                            where n.Question_id == question_No
                                                            select n;

                cls_Question result = Question_Select.ToList()[0];
                txtQueston.Text = result.Question_No + "\r\n" + result.Question;

                if (result.Question_Type == "S")
                {
                    btn_C.Visible = true;
                    txt_C.Visible = true;
                    btn_D.Visible = true;
                    txt_D.Visible = true;
                    btn_E.Visible = false;
                    txt_E.Visible = false;
                    txt_OK.Visible = false;
                    txt_Double.Visible = false;
                }
                else if (result.Question_Type == "D")
                {
                    btn_C.Visible = true;
                    txt_C.Visible = true;
                    btn_D.Visible = true;
                    txt_D.Visible = true;
                    btn_E.Visible = true;
                    txt_E.Visible = true;
                    txt_OK.Visible = true;
                    txt_Double.Visible = true;
                }
                else if (result.Question_Type == "J")
                {
                    btn_C.Visible = false;
                    txt_C.Visible = false;
                    btn_D.Visible = false;
                    txt_D.Visible = false;
                    btn_E.Visible = false;
                    txt_E.Visible = false;
                    txt_OK.Visible = false;
                    txt_Double.Visible = false;
                }
                if (result.Question_Type != "J")
                {
                    //A
                    IEnumerable<cls_Answer> Ansewr_A = from n in Answers
                                                       where n.Answer_Branch == "A" &&
                                                       n.Answer_id == question_No
                                                       select n;
                    txt_A.Text = Ansewr_A.ToList()[0].Anwser;

                    IEnumerable<cls_Answer> Ansewr_B = from n in Answers
                                                       where n.Answer_Branch == "B" &&
                                                       n.Answer_id == question_No
                                                       select n;
                    txt_B.Text = Ansewr_B.ToList()[0].Anwser;


                    IEnumerable<cls_Answer> Ansewr_C = from n in Answers
                                                       where n.Answer_Branch == "C" &&
                                                       n.Answer_id == question_No
                                                       select n;
                    txt_C.Text = Ansewr_C.ToList()[0].Anwser;

                    IEnumerable<cls_Answer> Ansewr_D = from n in Answers
                                                       where n.Answer_Branch == "D" &&
                                                       n.Answer_id == question_No
                                                       select n;
                    txt_D.Text = Ansewr_D.ToList()[0].Anwser;
                    if (result.Question_Type == "D")
                    {
                        IEnumerable<cls_Answer> Ansewr_E = from n in Answers
                                                           where n.Answer_Branch == "E" &&
                                                           n.Answer_id == question_No
                                                           select n;
                        txt_E.Text = Ansewr_E.ToList()[0].Anwser;

                    }
                }
                else
                {
                    txt_A.Text = "正确";
                    txt_B.Text = "错误";
                }

            }
            catch(Exception ee)
            {

            }
        }

        private void Thread1()
        {
            Random random = new Random();
            Question_No = random.Next(Question_count.Count);
            question_read(Question_count[Question_No]);
            //if (Question_No < Question_count.Count - 1)
            //{
            //    Question_No++;
            //    question_read(Question_count[Question_No]);

            //}
            //else
            //{
            //    Question_No = 0;
            //}

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Thread1();
        }

        private void txt_OK_Click(object sender, EventArgs e)
        {
            btn_A.Enabled = false;
            btn_B.Enabled = false;
            btn_C.Enabled = false;
            btn_D.Enabled = false;           
            btn_E.Enabled = false; 
            bool tf_flg = true;
          
            Button button = sender as Button;
            IEnumerable<cls_Question> Question_Select = from n in Question
                                                        where n.Question_id == Question_count[Question_No]
                                                        select n;

            cls_Question result = Question_Select.ToList()[0];
            string[] answer_flg = result.Question_True_Answer_Flg.Split(';');
            string[] Answer_collect = txt_Double.Text.Split(',');
            if (answer_flg.Length != Answer_collect.Length)
            {
                tf_flg = false;
            }
            else
            {
                foreach (string a in Answer_collect)
                {
                    if (answer_flg.Contains(a) == false)
                    {
                        tf_flg = false;
                    }
                }
            }
            Form1 form1 = new Form1();
            form1.analyase = result.Question_Anwser + "\r\n" + result.Question_Analyse;
            form1.answer_flg = tf_flg;
            this.Hide();
            form1.StartPosition = FormStartPosition.CenterParent;
            form1.ShowDialog();
            this.Show();
            txt_Double.Text = "";
            if (delete == true)
            {
                Question_count.RemoveAt(Question_No);
                delete = false;
            }
        }
    }
}

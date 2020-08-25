using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yizhandaodi
{
    class cls_Question
    {
        //题号ID
        public int Question_id { get; set; }
        //题号ID
        public string Question_No { get; set; }
        //题目
        public string Question { get; set; }
        //答案
        public string Question_Anwser { get; set; }
        //答案解析
        public string Question_Analyse { get; set; }
        //正确答案选项
        public string Question_True_Answer_Flg { get; set; }
        //题型
        public string Question_Type { get; set; }
    }
}

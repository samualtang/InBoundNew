using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound;
using highSpeed.PubFunc;
using System.Data.OracleClient;


namespace highSpeed.orderHandle
{
    public partial class ReHihgSpeedForm : Form
    {
        public ReHihgSpeedForm()
        {
            InitializeComponent();
        }
        DataBase Db;
        private void ReHihgSpeedForm_Load(object sender, EventArgs e)
        {
            Db = new DataBase();
            var producedata = Db.Query("select t.regioncode,sum(t.taskquantity) allqty,decode(t.state,0,'新增',15,'已排程',30,'分拣完成',t.state) status,decode(t.state,0,sum(t.taskquantity)) untask,decode(t.state,15,sum(t.taskquantity)) unpoke ,decode(t.state,30,sum(t.taskquantity)) havepoke from t_produce_task t group by t.regioncode,t.state");
            var undata = Db.Query("select t.regioncode,sum(t.taskquantity) allqty,decode(t.state,0,'新增',15,'已排程',30,'分拣完成',t.state) status,decode(t.state,0,sum(t.taskquantity)) untask,decode(t.state,15,sum(t.taskquantity)) unpoke ,decode(t.state,30,sum(t.taskquantity)) havepoke from t_un_task t group by t.regioncode,t.state");

            dataGridView1.DataSource = producedata;
            dataGridView2.DataSource = undata;

        }

        private void btn_produce_Click(object sender, EventArgs e)
        {
            if (txt_codestr1.Text.Length <= 0)
            {
                MessageBox.Show("请选择车组！");
                return;
            }
            DialogResult result = MessageBox.Show("确认重置常规烟车组：" + txt_codestr1.Text + "？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result==DialogResult.Cancel)
            {
                return;
            }
            try
            {
                if (txt_codestr1.Text != "")
                {
                    Db.Open();
                    String[] code = txt_codestr1.Text.Substring(1).Split(',');
                    int len = code.Length;
                    string codes = "";
                    foreach (var item in code)
                    {
                        codes = codes + item + ",";
                    }
                    string errcode = "", errmsg = "";
                    OracleParameter[] sqlpara;
                    List<string> havecode = new List<string>();
                    for (int i = 0; i < code.Length; i++)
                    {
                        sqlpara = new OracleParameter[3];
                        sqlpara[0] = new OracleParameter("p_code", code[i]);
                        sqlpara[1] = new OracleParameter("p_ErrCode", OracleType.VarChar, 30);
                        sqlpara[2] = new OracleParameter("p_ErrMsg", OracleType.VarChar, 1000);

                        sqlpara[1].Direction = ParameterDirection.Output;
                        sqlpara[2].Direction = ParameterDirection.Output;
                        Db.ExecuteNonQueryWithProc("P_REPACE_PRODUCE_REGIONCODE", sqlpara);

                        errcode = sqlpara[1].Value.ToString();
                        errmsg = sqlpara[2].Value.ToString();

                        havecode.Add(code[i]);
                        if (errcode == "0")
                        {
                            havecode.Remove(code[i]);
                            break;
                        }
                    }
                    //输出已重置的车组
                    if (havecode.Count > 0)
                    {
                        string txt = "";
                        foreach (var item in havecode)
                        {
                            txt = txt + item + ",";
                        }
                        MessageBox.Show(txt + "车组已完成重置，可以重新进行预排程！");
                    }

                }
                txt_codestr1.Clear();
            }
            catch (Exception)
            {
                MessageBox.Show("重置失败，请重试！");
            }
            update1();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                bool obj = (bool)this.dataGridView1.CurrentRow.Cells[0].EditedFormattedValue;

                String czcode = this.dataGridView1.CurrentRow.Cells[1].Value + "";//modify by tjl
                String czcodestr = this.txt_codestr1.Text;
                if (obj)
                {
                    if (!czcodestr.Contains(czcode))
                    {
                        czcodestr = czcodestr + "," + czcode;
                    }
                }
                else
                {
                    czcodestr = czcodestr.Replace("," + czcode, "");
                }
                this.txt_codestr1.Text = czcodestr;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.txt_codestr1.Text = "";
            update1();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            String czcodestr = "";
            for (int i = 0; i < this.dataGridView1.RowCount; i++)
            {
                  dataGridView1.Rows[i].Cells[0].Value = "true";
                  czcodestr = czcodestr + "," + dataGridView1.Rows[i].Cells[1].Value + "";
            }
            this.txt_codestr1.Text = czcodestr;
        }


        private void update1()
        {
            Db = new DataBase();
            var producedata = Db.Query("select t.regioncode,(select sum(taskquantity) from t_produce_task where regioncode = t.regioncode) allqty,decode(t.state, 0, '新增', 15, '已排程', 30, '分拣完成', t.state) status,decode(t.state, 0, sum(t.taskquantity)) untask,decode(t.state, 15, sum(t.taskquantity)) unpoke,decode(t.state, 30, sum(t.taskquantity)) havepoke from t_produce_task t group by t.regioncode, t.state");
            dataGridView1.DataSource = producedata; 
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.txt_codestr2.Text = "";
            update2();
        }
        private void update2()
        {
            Db = new DataBase();
            var producedata = Db.Query("select t.regioncode,(select sum(taskquantity) from t_un_task where regioncode = t.regioncode) allqty,decode(t.state,0,'新增',15,'已排程',30,'分拣完成',t.state) status,decode(t.state,0,sum(t.taskquantity)) untask,decode(t.state,15,sum(t.taskquantity)) unpoke ,decode(t.state,30,sum(t.taskquantity)) havepoke from t_un_task t group by t.regioncode,t.state");

            dataGridView2.DataSource = producedata; 
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                bool obj = (bool)this.dataGridView2.CurrentRow.Cells[0].EditedFormattedValue;

                String czcode = this.dataGridView2.CurrentRow.Cells[1].Value + "";//modify by tjl
                String czcodestr = this.txt_codestr2.Text;
                if (obj)
                {
                    if (!czcodestr.Contains(czcode))
                    {
                        czcodestr = czcodestr + "," + czcode;
                    }
                }
                else
                {
                    czcodestr = czcodestr.Replace("," + czcode, "");
                }
                this.txt_codestr2.Text = czcodestr;
            }
        }

        private void btn_un_Click(object sender, EventArgs e)
        {
            if (txt_codestr2.Text.Length<=0)
            {
                MessageBox.Show("请选择车组！");
                return;
            }
            DialogResult result = MessageBox.Show("确认重置异型烟车组：" + txt_codestr2.Text + "？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                if (txt_codestr2.Text != "")
                {
                    Db.Open();
                    String[] code = txt_codestr2.Text.Substring(1).Split(',');
                    int len = code.Length;
                    string codes = "";
                    foreach (var item in code)
                    {
                        codes = codes + item + ",";
                    }
                    string errcode = "", errmsg = "";
                    OracleParameter[] sqlpara;
                    List<string> havecode = new List<string>();
                    for (int i = 0; i < code.Length; i++)
                    {
                        sqlpara = new OracleParameter[3];
                        sqlpara[0] = new OracleParameter("p_code", code[i]);
                        sqlpara[1] = new OracleParameter("p_ErrCode", OracleType.VarChar, 30);
                        sqlpara[2] = new OracleParameter("p_ErrMsg", OracleType.VarChar, 1000);

                        sqlpara[1].Direction = ParameterDirection.Output;
                        sqlpara[2].Direction = ParameterDirection.Output;
                        Db.ExecuteNonQueryWithProc("P_REPACE_UN_REGIONCODE", sqlpara);

                        errcode = sqlpara[1].Value.ToString();
                        errmsg = sqlpara[2].Value.ToString();

                        havecode.Add(code[i]);
                        if (errcode == "0")
                        {
                            havecode.Remove(code[i]);
                            break;
                        }
                    }
                    //输出已重置的车组
                    if (havecode.Count > 0)
                    {
                        string txt = "";
                        foreach (var item in havecode)
                        {
                            txt = txt + item + ",";
                        }
                        MessageBox.Show(txt + "车组已完成重置，可以重新进行预排程！");
                    }

                }
                txt_codestr2.Clear();
            }
            catch (Exception)
            {
                MessageBox.Show("重置失败，请重试！");
            }
            update2();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            String czcodestr = "";
            for (int i = 0; i < this.dataGridView2.RowCount; i++)
            {
                dataGridView2.Rows[i].Cells[0].Value = "true";
                czcodestr = czcodestr + "," + dataGridView2.Rows[i].Cells[1].Value + "";
            }
            this.txt_codestr2.Text = czcodestr;
        }

      
 

      

       
    }
}

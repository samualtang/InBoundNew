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
using InBound.Model;


namespace highSpeed.orderHandle
{
    public partial class ReHihgSpeedForm : Form
    {
        public ReHihgSpeedForm()
        {
            InitializeComponent();
        }
        DataBase Db;
        List<ReSchedule> ReScheduleList = new List<ReSchedule>();
        List<ReSchedule> ReUnScheduleList = new List<ReSchedule>();
        string querystr = "select t.orderdate,t.regioncode,sum(t.taskquantity) allqty,decode(t.state,0,'新增',15,'已排程',30,'分拣完成',t.state) status,decode(t.state,0,sum(t.taskquantity)) untask,decode(t.state,15,sum(t.taskquantity)) unpoke ,decode(t.state,30,sum(t.taskquantity)) havepoke from t_produce_task t group by t.orderdate,t.regioncode,t.state order by orderdate";
        string unquerystr = "select t.orderdate,t.regioncode,sum(t.taskquantity) allqty,decode(t.state,0,'新增',15,'已排程',30,'分拣完成',t.state) status,decode(t.state,0,sum(t.taskquantity)) untask,decode(t.state,15,sum(t.taskquantity)) unpoke ,decode(t.state,30,sum(t.taskquantity)) havepoke from t_un_task t group by t.orderdate,t.regioncode,t.state order by orderdate";
        private void ReHihgSpeedForm_Load(object sender, EventArgs e)
        {
            Db = new DataBase();
            var producedata = Db.Query(querystr);
            var undata = Db.Query(unquerystr);

            dataGridView1.DataSource = producedata;
            dataGridView2.DataSource = undata;

        }

        private void btn_produce_Click(object sender, EventArgs e)
        {
            if (ReScheduleList.Count <= 0)
            {
                MessageBox.Show("请选择车组！");
                return;
            }
            string txt = "";
            foreach (var item in ReScheduleList)
            {
                txt = txt + "\r\n订单日期：" + item.OrderDate + "   车组：" + item.RegionCode;
            }

            DialogResult result = MessageBox.Show("确认重置常规烟" + txt + "？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            int index = 0;
            try
            {
                if (ReScheduleList.Count > 0)
                {
                    Db.Open();
                    string errcode = "", errmsg = "";
                    OracleParameter[] sqlpara;

                    foreach (var item in ReScheduleList)
                    {
                        sqlpara = new OracleParameter[3];
                        sqlpara[0] = new OracleParameter("p_code", item.RegionCode);
                        sqlpara[1] = new OracleParameter("p_orderdate", item.OrderDate);
                        sqlpara[2] = new OracleParameter("p_ErrCode", OracleType.VarChar, 30);
                        sqlpara[3] = new OracleParameter("p_ErrMsg", OracleType.VarChar, 1000);

                        sqlpara[2].Direction = ParameterDirection.Output;
                        sqlpara[3].Direction = ParameterDirection.Output;
                        Db.ExecuteNonQueryWithProc("P_REPACE_PRODUCE_REGIONCODE", sqlpara);

                        errcode = sqlpara[2].Value.ToString();
                        errmsg = sqlpara[3].Value.ToString();
                        index++;
                    }
                    //输出已重置的车组
                    if (index == ReScheduleList.Count)
                    {
                        MessageBox.Show(txt + "车组已完成重置，可以重新进行预排程！");
                    }

                }
                ReScheduleList.Clear();
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

                String czdate = ((DateTime)this.dataGridView1.CurrentRow.Cells[1].Value).ToString("yyyy-MM-dd");
                String czcode = this.dataGridView1.CurrentRow.Cells[2].Value + "";
                if (obj)
                {
                    if (ReScheduleList.Where(x => x.RegionCode == czcode && x.OrderDate == czdate).Count() == 0)
                    {
                        ReScheduleList.Add(new ReSchedule() { OrderDate = czdate, RegionCode = czcode });
                    }
                }
                else
                {
                    ReScheduleList.Remove(ReScheduleList.Where(x => x.RegionCode == czcode && x.OrderDate == czdate).FirstOrDefault());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReScheduleList.Clear();
            update1();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ReScheduleList.Clear();
            for (int i = 0; i < this.dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = "true";
                ReScheduleList.Add(new ReSchedule() { OrderDate = ((DateTime)(dataGridView1.Rows[i].Cells[1].Value)).ToString("yyyy-MM-dd"), RegionCode = dataGridView1.Rows[i].Cells[2].Value.ToString() });
            }
        }


        private void update1()
        {
            Db = new DataBase();
            var producedata = Db.Query(querystr);
            dataGridView1.DataSource = producedata;
            dataGridView1.DataSource = producedata;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ReUnScheduleList.Clear();
            update2();
        }
        private void update2()
        {
            Db = new DataBase();
            var producedata = Db.Query(unquerystr);

            dataGridView2.DataSource = producedata;
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                bool obj = (bool)this.dataGridView2.CurrentRow.Cells[0].EditedFormattedValue;

                String czdate = ((DateTime)this.dataGridView2.CurrentRow.Cells[1].Value).ToString("yyyy-MM-dd");
                String czcode = this.dataGridView2.CurrentRow.Cells[2].Value + "";
                if (obj)
                {
                    if (ReUnScheduleList.Where(x => x.RegionCode == czcode && x.OrderDate == czdate).Count() == 0)
                    {
                        ReUnScheduleList.Add(new ReSchedule() { OrderDate = czdate, RegionCode = czcode });
                    }
                }
                else
                {
                    ReUnScheduleList.Remove(ReUnScheduleList.Where(x => x.RegionCode == czcode && x.OrderDate == czdate).FirstOrDefault());
                }
            }
        }

        private void btn_un_Click(object sender, EventArgs e)
        {
            if (ReUnScheduleList.Count <= 0)
            {
                MessageBox.Show("请选择车组！");
                return;
            }
            string txt = "";
            foreach (var item in ReUnScheduleList)
            {
                txt = txt + "\r\n订单日期：" + item.OrderDate + "   车组：" + item.RegionCode;
            }

            DialogResult result = MessageBox.Show("确认重置异型烟" + txt + "？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            int index = 0;
            try
            {
                if (ReUnScheduleList.Count > 0)
                {
                    Db.Open();
                    string errcode = "", errmsg = "";
                    OracleParameter[] sqlpara;

                    foreach (var item in ReUnScheduleList)
                    {
                        sqlpara = new OracleParameter[4];
                        sqlpara[0] = new OracleParameter("p_code", item.RegionCode);
                        sqlpara[1] = new OracleParameter("p_orderdate", item.OrderDate);
                        sqlpara[2] = new OracleParameter("p_ErrCode", OracleType.VarChar, 30);
                        sqlpara[3] = new OracleParameter("p_ErrMsg", OracleType.VarChar, 1000);

                        sqlpara[2].Direction = ParameterDirection.Output;
                        sqlpara[3].Direction = ParameterDirection.Output;
                        Db.ExecuteNonQueryWithProc("P_REPACE_UN_REGIONCODE", sqlpara);

                        errcode = sqlpara[2].Value.ToString();
                        errmsg = sqlpara[3].Value.ToString();
                        index++;
                    }
                    //输出已重置的车组
                    if (index == ReUnScheduleList.Count)
                    {
                        MessageBox.Show(txt + "车组已完成重置，可以重新进行预排程！");
                    }

                }
                ReUnScheduleList.Clear();
            }
            catch (Exception)
            {
                MessageBox.Show("重置失败，请重试！");
            }
            update2();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReUnScheduleList.Clear();
            for (int i = 0; i < this.dataGridView2.RowCount; i++)
            {
                dataGridView2.Rows[i].Cells[0].Value = "true";
                ReUnScheduleList.Add(new ReSchedule() { OrderDate = ((DateTime)(dataGridView2.Rows[i].Cells[1].Value)).ToString("yyyy-MM-dd"), RegionCode = dataGridView2.Rows[i].Cells[2].Value.ToString() });
            }
        } 

    }
}
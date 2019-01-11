using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound;
using InBound.Business;
using System.Data.OracleClient;
using highSpeed.PubFunc;

namespace highSpeed.orderHandle
{
    public partial class w_order_Split : Form
    {
        public w_order_Split()
        {
            InitializeComponent(); 
        }
        DataBase Db = new DataBase();
        private void txtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            Bind();

        }
        void Bind()
        {


            try
            {
                List<T_UN_TASK> list;
                if (!string.IsNullOrWhiteSpace(txtCutName.Text) && !string.IsNullOrWhiteSpace(txtNum.Text))
                {
                    list = ScheduleService.GetUnnormalOrderInfo().Where(a => a.CUSTOMERNAME.Contains(txtCutName.Text) && a.TASKQUANTITY >= Convert.ToDecimal(txtNum.Text)).OrderBy(a => a.REGIONCODE).ToList();
                }
                else if (!string.IsNullOrWhiteSpace(txtCutName.Text))
                {
                    list = ScheduleService.GetUnnormalOrderInfo().Where(a => a.CUSTOMERNAME.Contains(txtCutName.Text)).OrderBy(a => a.REGIONCODE).ToList();
                }
                else if (!string.IsNullOrWhiteSpace(txtNum.Text))
                {
                    list = ScheduleService.GetUnnormalOrderInfo().Where(a => a.TASKQUANTITY >= Convert.ToDecimal(txtNum.Text)).OrderBy(a => a.REGIONCODE).ToList();
                }
                else
                {
                    list = ScheduleService.GetUnnormalOrderInfo().Where(a => a.TASKQUANTITY >= 90).OrderBy(a => a.REGIONCODE).ToList();
                }
                if (list.Count  == 0)
                { 
                    MessageBox.Show("当前没有待拆分的批次!"  );
                    return;
                }

                orderdata.Rows.Clear();

                foreach (var item in list)
                {
                    DataGridViewCellStyle dgvStyle = new DataGridViewCellStyle();
                    int index = this.orderdata.Rows.Add();
                    this.orderdata.Rows[index].Cells[1].Value = index + 1;//编号
                    this.orderdata.Rows[index].Cells[2].Value = item.REGIONCODE;//车组信息 
                    this.orderdata.Rows[index].Cells[3].Value = item.CUSTOMERNAME;//客户名称
                    this.orderdata.Rows[index].Cells[4].Value = item.TASKQUANTITY;//异形烟量
                    this.orderdata.Rows[index].Cells[5].Value = item.BILLCODE;//客户编号 
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("查询错误0:"+ex.Message );
            }
            finally
            {

            }
        }
        bool isRecieve;
        private void btnCheous_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnCheous.Enabled = false;//接收按钮屏蔽，防止再次点击
                isRecieve = true;
                //handlerecieve(1, true);//订单接收
                String biilcodestr = this.lblBcd.Text.Trim();
                //DateTime time = DateTime.Parse(this.datePick.Value.ToString());
                //String date = string.Format("{0:d}", time);
                OracleParameter[] sqlpara;
                string errcode = "", errmsg = ""; 
                if (biilcodestr != "")
                {
                    Db.Open();
                    String[] code = biilcodestr.Substring(1).Split(',');
                    int len = code.Length;

                    string indexstr = "";// 已完成拆分的订单



                    for (int i = 0; i < len; i++)
                    {
                        panel2.Visible = true;
                        label2.Visible = true;
                        progressBar1.Visible = true; 
                        progressBar1.Value = 0;
                        Application.DoEvents();
                        if (i == 0) label2.Text = "正在接收" + code[i] + "订单数据...";
                        //MessageBox.Show(label2.Text);
                        sqlpara = new OracleParameter[3];
                        sqlpara[0] = new OracleParameter("p_billcode", code[i]); 
                        sqlpara[1] = new OracleParameter("p_ErrCode", OracleType.VarChar, 30);
                        sqlpara[2] = new OracleParameter("p_ErrMsg", OracleType.VarChar, 1000);

                        sqlpara[1].Direction = ParameterDirection.Output;
                        sqlpara[2].Direction = ParameterDirection.Output;

                        Db.ExecuteNonQueryWithProc("P_un_resendtasknum", sqlpara);
                        //MessageBox.Show(date);
                        //MessageBox.Show(code[i]+"订单数据接收完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        errcode = sqlpara[1].Value.ToString();
                        errmsg = sqlpara[2].Value.ToString();
                        //进度条显示 

                        progressBar1.Value = ((i + 1) * 100 / len);
                        progressBar1.Refresh();
                        String tmpstr = "";

                        if (errcode == "1")
                        {
                            if (i + 1 < len) tmpstr = "正在接收" + code[i + 1] + "车组订单数据...";
                            else tmpstr = "";
                            label2.Text = code[i] + "车组订单数据接收完毕..." + tmpstr;
                            label2.Refresh();
                            indexstr = indexstr + "," + code[i];
                        }
                        else
                        {
                            label2.Text = errmsg;
                            label2.Refresh();
                            break;
                        }
                        //MessageBox.Show(label2.Text);
                    }

                    //string msg = "订单";

                    panel2.Visible = false;
                    label2.Visible = false;
                    progressBar1.Visible = false; 
                    MessageBox.Show(errmsg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    

                    /*for (int i = 0; i < orderdata.RowCount;i++ )
                    {
                        bool obj = (bool)this.orderdata.Rows[i].Cells[0].EditedFormattedValue;
                        if (obj) indexstr=indexstr +","+ i;
                    }*/

                    

                }
                else
                {
                    MessageBox.Show("请至少选择一个要拆分的订单!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.btnCheous.Enabled = true;//接收按钮放开
            }
            catch (Exception ex)
            {
                WriteLog.GetLog().Write("订单接收异常" + ex.Message);  

            }
            finally
            {
                lblBcd.Text = "";
                Bind();
                isRecieve = false; 
            }
        }

        private void orderdata_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                bool obj = (bool)this.orderdata.CurrentRow.Cells[0].EditedFormattedValue;

                String czcode = this.orderdata.CurrentRow.Cells[5].Value + "";//modify by tjl
                String czcodestr = this.lblBcd.Text;
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
                this.lblBcd.Text = czcodestr;
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            String czcodestr = "";
            for (int i = 0; i < this.orderdata.RowCount; i++)
            {
                orderdata.Rows[i].Cells[0].Value = "true";
                czcodestr = czcodestr + "," + orderdata.Rows[i].Cells[5].Value + "";
            }
            this.lblBcd.Text = czcodestr;
        }

        private void w_order_Split_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isRecieve)
            {
                e.Cancel = true;
                MessageBox.Show("正在拆分订单！请耐心等待接收完毕！");
                return;
            }
        }
 
    }
}

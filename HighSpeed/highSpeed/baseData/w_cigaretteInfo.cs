using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using highSpeed.PubFunc;
using System.Data.SqlClient;

namespace highSpeed.baseData
{

   
    public partial class w_cigaretteInfo : Form
    {
        OracleConnection cn = null;
        OracleCommand cmd = new OracleCommand();
        OracleDataAdapter da = new OracleDataAdapter();
        DataSet ds = new DataSet();
        DataSet detail_ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
       public  List<dxtype> list = new List<dxtype>();
        public w_cigaretteInfo()
        {
            
            list.Add(new dxtype {  name="自动拆垛", cdtype="10"});
            list.Add(new dxtype { name = "人工拆垛", cdtype = "0" });
            
            InitializeComponent();
            init();
        }

        public void init()
        {
            //初始化查询条件下拉框
            DataTable conditiontable = new DataTable();
            conditiontable.Columns.Add("value", typeof(string));
            conditiontable.Columns.Add("txtvalue", typeof(string));

            DataRow dr = conditiontable.NewRow();
            dr["value"] = "itemno";
            dr["txtvalue"] = "卷烟编码";

            DataRow dr1 = conditiontable.NewRow();
            dr1["value"] = "itemname";
            dr1["txtvalue"] = "品牌名称";

            conditiontable.Rows.Add(dr);
            conditiontable.Rows.Add(dr1);

            //this.box_type = new ComboBox();
            this.box_type.DataSource = conditiontable;
            this.box_type.DisplayMember = "txtvalue";
            this.box_type.ValueMember = "value";
            this.box_type.SelectedIndex = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                seek();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void seek()
        {
            String strsql = "select itemno,itemname,shortname,fullcount,cdtype,bigbox_bar,weight,ilength,iwidth,iscanscancode || ''iscanscancodes ,dxtype,outtype from t_wms_item where length(itemno)=7";

            string types = this.box_type.SelectedValue + "";
            string keywd = this.txt_keywd.Text.Trim();
            String tmp = "";
            
            if (types != null && types != "" && keywd != null && keywd != "")
            {
                strsql = strsql + " and " + types + " like'%" + keywd + "%'";
            }
            

           
            Bind(strsql);
        }

        #region 查询
        /// <summary>
        /// 绑定DataGridView1
        /// </summary>
        /// <param name="sql">要查询的sql</param>
        private void Bind(string sql)
        {
            try
            {
                ds.Clear();
                //da.SelectCommand = new OracleCommand(sql, cn);
                //da.Fill(ds, "TB_Inpatient_info");

                
                ds = Db.QueryDs(sql);



                
                //进度条显示

                panel2.Visible = true;
                label2.Visible = true;
                progressBar1.Visible = true;
                int rcounts = ds.Tables[0].Rows.Count;
                progressBar1.Value = 0;
                for (int i = 0; i < rcounts; i++)
                {
                    Application.DoEvents();
                    progressBar1.Value = ((i + 1) * 100 / rcounts);
                    progressBar1.Refresh();
                    label2.Text = "正在读取数据..."+((i + 1) * 100 / rcounts).ToString() + "%";
                    label2.Refresh();
                }
                panel2.Visible = false;
                label2.Visible = false;
                progressBar1.Visible = false;

                this.dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.DataSource = ds.Tables[0];
               


                string columnwidths = pub.IniReadValue(this.Name, this.dataGridView1.Name);
                if (columnwidths != "")
                {
                    string[] columns = columnwidths.Split(',');
                    int j = 0;
                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (dataGridView1.Columns[i].Visible == true)
                        {
                            dataGridView1.Columns[j].Width = Convert.ToInt32(columns[i]);
                            j = j + 1;
                        }
                    }
                }
                //for (int i = 0; i < dataGridView1.RowCount; i++)
                //{
                //    //this.dataGridView1.Rows[i].Cells[5]. = "自动拆垛";
                //    ((DataGridViewComboBoxCell)dataGridView1.Rows[i].Cells[5]).Value = "自动拆垛";

                // //String s=  cell.Value;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                Db.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void w_cigaretteInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Db.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                this.txt_keywd.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dgVprint1.MainTitle = "卷烟信息表";
           // dgVprint1.SubTitle = "这是子标题，当然也可以不设的";
            dgVprint1.PaperLandscape = true;//用横向打印，默认是纵向哦
            dgVprint1.Print(dataGridView1);
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dgVprint1.ExportDGVToExcel2(dataGridView1,"卷烟基础信息","cigaretteInfo.xls",true);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int rowcount = this.dataGridView1.SelectedCells.Count;
            if (rowcount > 0)
            {
                //MessageBox.Show("--");
                String itemno = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                String barcode = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                String fullcount = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
                String cdtype = this.dataGridView1.CurrentRow.Cells[5].EditedFormattedValue.ToString();
                String weight = this.dataGridView1.CurrentRow.Cells[6].Value.ToString();
                String ilength = this.dataGridView1.CurrentRow.Cells[7].Value.ToString();
                String iwidth = this.dataGridView1.CurrentRow.Cells[8].Value.ToString(); 
                String dx = this.dataGridView1.CurrentRow.Cells[9].EditedFormattedValue.ToString();
                String iscanscancodes = this.dataGridView1.CurrentRow.Cells[10].EditedFormattedValue.ToString();
                String outType = this.dataGridView1.CurrentRow.Cells[11].EditedFormattedValue.ToString();
                if (cdtype == "人工拆垛")
                {
                    cdtype = "0";
                }
                else
                {
                    cdtype = "10";
                }
                if (iscanscancodes == "Yes")
                {
                    iscanscancodes = "10";
                }
                else
                {
                    iscanscancodes = "0";
                }
                try
                {
                    Db.Open();
                    String sql = "update t_wms_item set  iscanscancode=" + iscanscancodes + ", dxtype=" + dx + ",outtype=" + outType + ",weight=" + weight + ",bigbox_bar='" + barcode + "', fullcount=" + fullcount + ",cdtype=" + cdtype + ",ilength=" + ilength + ",iwidth=" + iwidth + " where itemno='" + itemno + "'";
                   // String batchcodesql = "select count(*) from highspeed.t_produce_brandcoderelative where cigarettecode='" + itemno + "'";

                    int len = Db.ExecuteNonQuery(sql);
                    if (len != 0) MessageBox.Show("卷烟品牌信息修改成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    seek();
                }
                catch (SqlException se)
                {
                    MessageBox.Show(se.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Db.Close();
                    //this.Close();
                }
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.ColumnIndex == 5)
            {
                if (ds.Tables[0].Rows[e.RowIndex][4].ToString() == "10")
                {

                    e.Value = "自动拆垛";
                }
                else
                {
                    e.Value = "人工拆垛";
                }
            }
            if (e.ColumnIndex == 9)
            {
                e.Value = ds.Tables[0].Rows[e.RowIndex][10].ToString();

            }
            if (e.ColumnIndex == 11)
            {
                e.Value = ds.Tables[0].Rows[e.RowIndex][11].ToString();

            }
            if (e.ColumnIndex == 10)
            {

                if (ds.Tables[0].Rows[e.RowIndex][9].ToString() == "10")
                {

                    e.Value = "Yes";
                }
                else
                {
                    e.Value = "No";
                }
            }

        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //String s = e.Exception.Message;
        }

      
        /*
        private void OnPrintPage(Object sender,PrintPageEventArgs e)
        {
            int iX = 60;
            int iY = 40;

        }
         */
    }
   public class dxtype
    {
        public String name;
        public String cdtype;
    }
}

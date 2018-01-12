using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using highSpeed.PubFunc;
using System.Data.SqlClient;

namespace highSpeed.orderHandle
{
    public partial class troughmove : Form
    {
        DataBase Db = new DataBase();
        String type = "20";
        String ctype = "2";
        public troughmove()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            initCB(120);
            label4.Text = GetResult(getSql("20","1"));
            label5.Text = GetResult(getSql("20", "1"));
        }
        public string getSql(String troughtype, String troughno)
        {
            String sql = "select cigarettename from t_produce_sorttrough where troughtype='" + troughtype + "' and machineseq=" + troughno + " and cigarettetype=20";
            return sql;        
        }
        public string GetResult(String sql)
        {

            string result="";
            try {
                        Db.Open();
                      result=   Db.ExecuteScalar(sql).ToString();
                       // MessageBox.Show("描述为【" + desc + "】的分拣通道已启用!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      ;

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
            return result;
        }
        public void initCB(int total)
        {
            cbsource.Items.Clear();
            cbTarget.Items.Clear();
            for (int i = 1; i <= total; i++)
            {
                cbsource.Items.Add(i);
                cbTarget.Items.Add(i);
            }
            cbsource.SelectedIndex = 0;
            cbTarget.SelectedIndex = 0;
            if (total == 120)
            {
                type = "20";
                ctype = "2";
            }
            else
            {
                type = "10";
                ctype = "3";
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                initCB(120);
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                initCB(88);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbsource.SelectedIndex == cbTarget.SelectedIndex)
            {
                MessageBox.Show("源通道不能等于目标通道");
            }
            else
            {

              //  string sql = "select cigarettecode from t_produce_sorttrough where troughtype='" + type + "' and cigarettetype=20 and troughnum=" + (cbsource.SelectedIndex + 1);
               // string sql2 = "select cigarettecode from t_produce_sorttrough where troughtype='" + type + "' and cigarettetype=20 and troughnum=" +( cbTarget.SelectedIndex + 1);

             //   String idSource = GetResult(sql);
             //   String idTarget = GetResult(sql2);
                 Db.Open();
                 String sourcetroughnum = Db.ExecuteScalar("select troughnum from t_produce_sorttrough where machineseq=" + (cbsource.SelectedIndex + 1) + " and cigarettetype=20 and troughtype=" + type).ToString();
                String targettroughnum = Db.ExecuteScalar("select troughnum from t_produce_sorttrough where machineseq=" + (cbTarget.SelectedIndex + 1) + " and cigarettetype=20 and troughtype=" + type).ToString();
                String updateSql = "update t_produce_sorttrough set troughnum='TEMP" + targettroughnum + "',machineseq=" + (cbTarget.SelectedIndex + 1 + 100000) + " where troughtype='" + type + "' and cigarettetype=20 and troughnum=" + sourcetroughnum;
                String updateSql1 = "update t_produce_sorttrough set  troughnum='" + sourcetroughnum + "',machineseq=" + (cbsource.SelectedIndex + 1) + " where troughtype='" + type + "' and cigarettetype=20 and troughnum='" + targettroughnum + "'";
                String updateSql5 = "update t_produce_sorttrough set  troughnum=replace(troughnum,'TEMP',''),machineseq=machineseq-100000 where machineseq>100000";
                String updateSql2 = "update t_wms_storagearea_inout set cellno='" + "TEMP" + targettroughnum + "' where areaid='" + ctype + "' and cellno='" + sourcetroughnum + "'";
                String updateSql3 = "update t_wms_storagearea_inout set cellno='" + sourcetroughnum + "' where areaid='" + ctype + "' and cellno='" + targettroughnum + "'";
                String updateSql4 = "update t_wms_storagearea_inout set cellno=replace(cellno,'TEMP','')";

               
                Db.ExecuteNonQuery(updateSql);
                Db.ExecuteNonQuery(updateSql1);
                Db.ExecuteNonQuery(updateSql5);
                Db.ExecuteNonQuery(updateSql2);
                Db.ExecuteNonQuery(updateSql3);
                Db.ExecuteNonQuery(updateSql4);
                Db.Close();
                MessageBox.Show("转移完成");
            }
        }

        private void cbsource_SelectedIndexChanged(object sender, EventArgs e)
        {
            label4.Text = GetResult(getSql(type, cbsource.SelectedIndex + 1 + ""));
           
        }

        private void cbTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            label5.Text = GetResult(getSql(type, cbTarget.SelectedIndex + 1 + ""));
        }
    }
}

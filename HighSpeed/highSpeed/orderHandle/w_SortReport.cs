using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using highSpeed.PubFunc;

namespace highSpeed.orderHandle
{
    public partial class w_SortReport : Form
    {
        DataBase Db = new DataBase();
        DataSet ds = new DataSet();
        public w_SortReport()
        {
            InitializeComponent();

            CmbAddress.SelectedIndex = 0;
            Seek();
        }

        void Seek() 
        {
              
            string str = "CS%";
            
            switch (CmbAddress.SelectedIndex) 
            {
                case 0:
                    str = "1=1";
                    break;
                case 1:
                    str = "f.billcode like CS%";
                    break;
                case 2:
                    str = "f.billcode like ZZ%";
                    break;
                case 3:
                    str = "f.billcode like XT%";
                    break;
            }
            string sql = "select f.cigarettename 品牌,decode(f.allowsort,'分拣','常规烟','非标','异型烟') 类型,sum(f.quantity) 数量 from v_produce_packageinfo f where  " +
              str + " group by f.cigarettename,f.allowsort order by allowsort desc,sum(quantity) desc";
            Bind(sql);
        }


        private void Bind(string sql) 
        {
            try
            {
                Db = new DataBase();
                ds.Clear();
                ds = Db.QueryDs(sql);
                this.DGVData.DataSource = ds.Tables[0];
                this.DGVData.AutoGenerateColumns = false;
                Db.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            string str = "长沙";
            switch (CmbAddress.SelectedIndex)
            {
                case 0:
                    str = "全部";
                    break;
                case 1:
                    str = "长沙";
                    break;
                case 2:
                    str = "株洲";
                    break;
                case 3:
                    str = "湘潭";
                    break;
            }
            string name = DateTime.Now.ToString("yyyy年MM月dd日");
            name = name + "__" + str;
            dgVprint2.ExportDGVToExcel2(this.DGVData,name + "分拣品牌报表", name +".xls", true);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Seek();
        }
    }
}

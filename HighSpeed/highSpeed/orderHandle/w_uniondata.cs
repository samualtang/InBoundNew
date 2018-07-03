using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using highSpeed.PubFunc;
using InBound.Business;
using InBound.Model;

namespace highSpeed.orderHandle
{
    public partial class w_uniondata : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public w_uniondata()
        {
            InitializeComponent();
          
        }

      
        private void btn_print_Click(object sender, EventArgs e)
        {
            dgVprint2.MainTitle = "拨烟顺序明细";
            //dgVprint1.SubTitle = "这是子标题，当然也可以不设的";
            //dgVprint1.PageHeaderLeft = "白沙物流";
            //dgVprint1.PageHeaderRight = "分拣日期：" + DateTime.Now.Date.ToShortDateString();
            dgVprint2.TableHeaderLeft = "长株潭烟草物流配送中心";
            dgVprint2.TableHeaderRight = "分拣日期：" + DateTime.Now.Date.ToShortDateString();
            //dgVprint2.PaperLandscape = true;//用横向打印，默认是纵向哦
            dgVprint2.Print(orderdata);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dgVprint2.ExportDGVToExcel2(this.orderdata, "拨烟顺序明细", "taskinfo.xls", true);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            decimal sortNum = 0;
            decimal.TryParse(txtSortNum.Text,out sortNum);
            orderdata.AutoGenerateColumns = false;
            
            orderdata.DataSource=    UnionTaskInfoService.GetUnionTaskInfo(sortNum);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            decimal sortNum = 0;
            decimal.TryParse(txtSortNum.Text, out sortNum);
            orderdata.AutoGenerateColumns = false;
            decimal beforeNum = UnionTaskInfoService.getBeforeSortNum(sortNum);
            decimal afterNum = UnionTaskInfoService.getNextSortNum(sortNum);
            List<UnionTaskInfo>  list = UnionTaskInfoService.GetUnionTaskInfo(beforeNum);
            list.AddRange(UnionTaskInfoService.GetUnionTaskInfo(sortNum));
            list.AddRange(UnionTaskInfoService.GetUnionTaskInfo(afterNum));

            orderdata.DataSource = list;
        }
    }
}

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
using InBound.Model;

namespace FollowTask
{
    public partial class Fm_SortDetails : Form
    {
        public Fm_SortDetails()
        {
            InitializeComponent();
            this.listViewYaobaiDetails.DoubleBufferedListView(true);
             
             
        }
        decimal MainBelt; //摇摆过后前往的主皮带号
        decimal groupNo;//组号
        public Fm_SortDetails(string  storText,decimal SwingBelt)
        {
            InitializeComponent();
            MainBelt = SwingBelt;
            Text = storText+"区域";
            groupNo =Convert.ToDecimal(storText.Substring(5, 1));
        }
        List<FollowTaskDeail> list1;
        private void Fm_SortDetails_Load(object sender, EventArgs e)
        {
            try
            {
                listViewYaobaiDetails.Items.Clear();
                list1 = FolloTaskService.GetSortingBeltTask(MainBelt, groupNo);
                ListViewBind(list1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常信息:" + "\r\n" + ex.Message);
            }
        }
        /// <summary>
        /// lv绑定
        /// </summary>
        void ListViewBind(List<FollowTaskDeail> list)
        {
            if ( list != null)
            {

                for (int i = 0; i < list.Count; i++)
                {
                    ListViewItem lv = new ListViewItem();
                    var mod = list[i];
                    lv.SubItems[0].Text = mod.Billcode;
                    lv.SubItems.Add(mod.SortNum.ToString());
                    lv.SubItems.Add(mod.UnionTasknum.ToString());
                    lv.SubItems.Add(mod.MainBelt.ToString());
                    lv.SubItems.Add(mod.CIGARETTDECODE.ToString());
                    lv.SubItems.Add(mod.CIGARETTDENAME.ToString());
                    listViewYaobaiDetails.Items.Add(lv);
                }
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ListViewBind(list1);
        }
    }
}

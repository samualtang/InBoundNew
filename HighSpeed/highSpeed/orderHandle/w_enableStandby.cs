using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound.Business;
using InBound;
using highSpeed.PubFunc;

namespace highSpeed.orderHandle
{
    public partial class w_enableStandby : Form
    {
        LoadDataHandler loadData; 

        public w_enableStandby()
        {
            InitializeComponent();
            loadData = new LoadDataHandler();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            loadData.LoadDatas<List<T_PRODUCE_SORTTROUGH>>(() =>
            {
                var list = SortTroughService.GetTrough(10, 20).GroupBy(g => g.GROUPNO).Select(s => new T_PRODUCE_SORTTROUGH { GROUPNO = s.Key.Value }).ToList();
                return list;
            }, (list) =>
            {
                cmb_GroupList.DataSource = list;
                cmb_GroupList.DisplayMember = "GROUPNO";
                cmb_GroupList.ValueMember = "GROUPNO";
            }, (exceptionInfo) =>
            {
                MessageBox.Show(exceptionInfo);
            });
        }

        private void cmb_GroupList_SelectedIndexChanged(object sender, EventArgs e)
        {
            T_PRODUCE_SORTTROUGH obj = cmb_GroupList.SelectedItem as T_PRODUCE_SORTTROUGH;
            loadData.LoadDatas<List<T_PRODUCE_SORTTROUGH>>(() =>
            {
                var list = SortTroughService.GetTrough(10, 20).Where(w => w.GROUPNO == obj.GROUPNO).OrderBy(ord => Decimal.Parse(ord.TROUGHNUM.Trim())).ToList();
                return list;
            }, (list) =>
            {
                cbsource.DataSource = list;
                cbsource.DisplayMember = "TROUGHNUM";
                cbsource.ValueMember = "TROUGHNUM";

                var chagelist = new DeepCope().DeepCopy(list);
                cbStandby.DataSource = chagelist;
                cbStandby.DisplayMember = "TROUGHNUM";
                cbStandby.ValueMember = "TROUGHNUM";

            }, (exceptionInfo) =>
            {
                MessageBox.Show(exceptionInfo);
            });
        }

        private void cbsource_SelectedIndexChanged(object sender, EventArgs e)
        {
            T_PRODUCE_SORTTROUGH selectedItem = cbsource.SelectedItem as T_PRODUCE_SORTTROUGH;
            cbciagrettcode.Text = selectedItem.CIGARETTENAME;
        }


        private void btnEnableStandby_Click(object sender, EventArgs e)
        {

        }
    }
}

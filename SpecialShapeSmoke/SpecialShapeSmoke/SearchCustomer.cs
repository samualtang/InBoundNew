using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound.Business;

namespace SpecialShapeSmoke
{
    public partial class SearchCustomer : Form
    {
        public SearchCustomer()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "卷烟名称":
                    DgvNowView.DataSource = HunHeService_new.GetSearchCigarette(txt_search.Text,1);
                    break;
                case "商户名称":
                    DgvNowView.DataSource = HunHeService_new.GetSearchCigarette(txt_search.Text,2);
                    break;
            }
            
        }

        private void SearchCustomer_Deactivate(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
    }
}

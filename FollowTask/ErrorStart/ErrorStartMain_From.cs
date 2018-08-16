using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FollowTask.DataModle;

namespace FollowTask.ErrorStart
{
    public partial class ErrorStartMain_From : Form
    {
        
        public ErrorStartMain_From()
        {
            InitializeComponent();
        }

        private void Btn_start_Click(object sender, EventArgs e)
        {
            if (DicForm.DicFormList.ContainsKey(this))
            {
                MessageBox.Show(DicForm.DicFormList.Where(x => x.Key == this).FirstOrDefault(x => x.Value).ToString());
            }
        }

        private void ErrorStartMain_From_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DicForm.DicFormList.ContainsKey(this))
            {
                DicForm.DicFormList[this] = false;
            }
        }
    }
}

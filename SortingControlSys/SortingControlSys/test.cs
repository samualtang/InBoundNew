using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SortingControlSys.PubFunc;

namespace SortingControlSys
{
    public partial class test : Form
    {
        SortingFun sortFun = new SortingFun();
        public test()
        {
            InitializeComponent();
            string tasknum = "868";
            //sortFun.updateTaskState(tasknum);
           // string str = sortFun.allocateCigarStr(); ;
            //this.textBox1.Text = str;
            //this.listBox1.Items.Add(str);
        }
    }
}

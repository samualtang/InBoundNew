using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SortingControlSys.PubFunc;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using System.Collections;
using System.Configuration;

namespace SortingControlSys.SortingControl
{
    public partial class w_SortingControlMainTest : Form
    {
        OracleConnection cn = null;
        OracleCommand cmd = new OracleCommand();
        OracleDataAdapter da = new OracleDataAdapter();
        DataSet ds = new DataSet();
        DataSet detail_ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        WriteLog writeLog = new WriteLog();

        Socket m_socket = null; 
        private  readonly Encoding ASCII;
        private static int PORT = 2000;
        //private const string ipaddress = "127.0.0.1";
        private static string ipaddress = "10.75.56.114";
        private const String SEND_MESSAGE = "ab";
        private Thread thread;
        private Boolean plcConnIsOk = false;
        private Boolean plcHasError = false;//无故障


        private SortingFun sFun = new SortingFun();
        private string plcRetSorting = "0";//电控收到上位发送的拨烟数据后返回的串。1接收，0未接收

        List<TextBox> tblist = new List<TextBox>();
        private byte[] result = new byte[1024];
        private int taskNum = 0;
        public delegate void startReceive();
        public Boolean hasReturn = false;//拨烟数据发送成功，但未接收到




        public delegate void SomeHandler(object sender, System.EventArgs e);
        public event SomeHandler SomeEvent;
        
        //public delegate void ParameterizedThreadStart(Object obj);
        //public void Thread(ParameterizedThreadStart start); 


        public w_SortingControlMainTest()
        {
            InitializeComponent();
            ASCII = Encoding.ASCII;
            this.SomeEvent += new SomeHandler(this.ProcessSomeEvent);
            this.SomeEvent += new SomeHandler(this.ResponseSomeEvent);
            writeLog.Write("应用程序启动");
            updateListBox("应用程序启动");
            ipaddress = ConfigurationManager.AppSettings.Get("ServerIP");
            PORT = int.Parse(ConfigurationManager.AppSettings.Get("Port"));
            //ListBox.CheckForIllegalCrossThreadCalls = false;
          //  initdata();
            tblist.Add(textBox1);
            tblist.Add(textBox2);
            tblist.Add(textBox3);
            tblist.Add(textBox4);
            tblist.Add(textBox5);
            tblist.Add(textBox6);
            tblist.Add(textBox7);
            tblist.Add(textBox8);
            tblist.Add(textBox9);
            tblist.Add(textBox10);
            tblist.Add(textBox11);
            tblist.Add(textBox12);
            tblist.Add(textBox13);
            tblist.Add(textBox14);
            tblist.Add(textBox15);
            tblist.Add(textBox16);
            tblist.Add(textBox17);
            tblist.Add(textBox18);
            tblist.Add(textBox19);
            tblist.Add(textBox20);
            tblist.Add(textBox21);
            tblist.Add(textBox22);
            tblist.Add(textBox23);
            tblist.Add(textBox24);
            tblist.Add(textBox25);
            tblist.Add(textBox26);
            tblist.Add(textBox27);
            tblist.Add(textBox28);
            tblist.Add(textBox29);
            tblist.Add(textBox30);
            tblist.Add(textBox31);
            tblist.Add(textBox32);
            tblist.Add(textBox33);
            tblist.Add(textBox34);
            tblist.Add(textBox35);
            tblist.Add(textBox36);
            tblist.Add(textBox37);
            tblist.Add(textBox38);
            tblist.Add(textBox39);
            tblist.Add(textBox40);
            tblist.Add(textBox41);
            tblist.Add(textBox42);
            tblist.Add(textBox43);
            tblist.Add(textBox44);
            tblist.Add(textBox45);
            tblist.Add(textBox46);
            tblist.Add(textBox47);
            tblist.Add(textBox48);
            tblist.Add(textBox49);
            tblist.Add(textBox50);
            tblist.Add(textBox51);
            tblist.Add(textBox52);
            tblist.Add(textBox53);
            tblist.Add(textBox54);
            tblist.Add(textBox55);
            tblist.Add(textBox56);
            tblist.Add(textBox57);
            tblist.Add(textBox58);
            tblist.Add(textBox59);
            tblist.Add(textBox60);
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
              //  seek();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*
         * 开始分拣


         */
        private delegate void HandleDelegate1(string info, Label label);
        public void updateLabel(string info,Label label)
        {
            String time = DateTime.Now.ToLongTimeString();
            if (label.InvokeRequired)
            {
                //   this.txtreceive.BeginInvoke(new ShowDelegate(Show), strshow);//这个也可以

                label.Invoke(new HandleDelegate1(updateLabel), new Object[]{info,label});
            }
            else
            {
                label.Text=info;

            }
        }
        private delegate void HandleDelegate2(Boolean visible, Control control);
        public void updateControlVisible(Boolean visible,Control control)
        {
            if (control.InvokeRequired)
            {
                //   this.txtreceive.BeginInvoke(new ShowDelegate(Show), strshow);//这个也可以

                control.Invoke(new HandleDelegate2(updateControlVisible), new Object[] { visible, control });
            }
            else
            {
                control.Visible = visible;

            }
        }
        public void updateControlEnable(Boolean enable, Control control)
        {
            if (control.InvokeRequired)
            {
                //   this.txtreceive.BeginInvoke(new ShowDelegate(Show), strshow);//这个也可以

                control.Invoke(new HandleDelegate2(updateControlEnable), new Object[] { enable, control });
            }
            else
            {
                control.Enabled = enable;

            }
        }
      
        void showDialog(object sender, EventArgs e)
        {
            DialogResult MsgBoxResult = MessageBox.Show("电控连接不成功，是否继续尝试连接?",//对话框的显示内容 
                                "操作提示",//对话框的标题 
                                MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                MessageBoxDefaultButton.Button2);
                            //继续尝试链接
                            if (MsgBoxResult == DialogResult.Yes)
                            {
                                count = 0;
                            }
                            //否则激活分拣按钮,跳出方法
                            else
                            {
                                updateControlEnable(true, button10);
                                //this.button10.Enabled = true;
                                return;
                            }

        }
        public int count;
        delegate void AysncFinish(object sender, EventArgs e);
        public void startFenJian()
        {
            writeLog.Write("开始分拣");
            updateListBox("开始分拣");
            updateControlEnable(false, button10);
           // this.button10.Enabled = false;
            //链接电控plc
            //1.判断电控是否开机


            /*
            thread = new Thread(new ThreadStart(tryConnect));
            thread.Name = "连接线程";
                thread.Start();
             */

            //首次连接
            tryConnect();
            checkSocket();

            //连接正常
            if (m_socket != null)
            {
                if (m_socket.Connected)
                {
                    ListenPlcData();
                    updateLabel( "连接成功，请稍候...",label3);
                    //label3.Text = "连接成功，请稍候...";

                  //  updateControlVisible(false, panel4);
                   // updateControlVisible(false, label3);
                    //panel4.Visible = false;
                    //label3.Visible = false;
                    //发送信号查看电控系统是否已经准备好
                    //Thread checkPlcThread = new Thread(new ThreadStart(checkPlcIsOk));
                    //checkPlcThread.Name = "检查电控是否准备好线程";
                     count = 0;
                    checkPlcIsOk();
                    while (!plcConnIsOk)//plc未就绪
                    {
                        count = count + 1;
                        updateLabel("检查电控是否就绪，请稍候...", label3);
                        //label3.Text = "检查电控是否就绪，请稍候...";
                        checkPlcIsOk();
                        //sendData("103");
                        if (!plcConnIsOk)
                        {
                            Thread.Sleep(3000);
                        }
                        //尝试链接plc三次
                        if (count == 3)
                        {
                            this.BeginInvoke(new AysncFinish(showDialog), new Object[] { null, null });
                            //DialogResult MsgBoxResult = MessageBox.Show("电控连接不成功，是否继续尝试连接?",//对话框的显示内容 
                            //                                "操作提示",//对话框的标题 
                            //                                MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                            //                                MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                            //                                MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
                            ////继续尝试链接
                            //if (MsgBoxResult == DialogResult.Yes)
                            //{
                            //    count = 0;
                            //}
                            ////否则激活分拣按钮,跳出方法
                            //else
                            //{
                            //    updateControlEnable(true, button10);
                            //    //this.button10.Enabled = true;
                            //    return;
                            //}
                        }
                    }
                    while (!startMachine)
                    {
                        sendStartSignal();
                    }
                    //while (plcConnIsOk)//PLC准备就绪，开始分拣信息发送
                    //{

                    //    //发送分拣开机信号001
                    //    sendStartSignal();
                    //    updateLabel("电控就绪，准备拨烟数据，请稍候...", label3);
                    //    //label3.Text = "电控就绪，准备拨烟数据，请稍候...";
                    //    //异步调用
                    //    //异步启动本地接收数据，比如电控发来的故障信号，分拣完成信息

                    //  //  ListenPlcData();
                      
                    //    //startReceive gchandler = new startReceive(ListenPlcData);
                    //    //IAsyncResult result = gchandler.BeginInvoke(new AsyncCallback(CallBackMethod), null);
                    //    //checkPlcIsOk();
                    //    //异步调用结束

                    //    //String sendResult = sendSortingData(allocateCigarStr);
                    //    //Thread sendSortingThread = new Thread(sendSortingData);
                    //    //sendSortingThread.Name = "发送分拣任务线程";
                    //    //sendSortingThread.Start();

                    //    while (!plcHasError)//无plc故障
                    //    {
                    //        while (plcRetSorting.Equals("0"))//发送分拣拨烟数据
                    //        {
                    //            string sortStr = sFun.allocateCigarStr();//分拣数据
                    //            ParameterizedThreadStart ParStart = new ParameterizedThreadStart(sendData);
                    //            Thread myThread = new Thread(ParStart);
                    //            object sortObj = sortStr;
                    //            myThread.Start(sortObj);
                    //            Thread.Sleep(1000);//为了启动监听线程
                    //            /*
                    //            Thread sendSortingThread = new Thread(sendData);
                    //            sendSortingThread.Name = "发送分拣任务线程";
                    //            sendSortingThread.Start(sortStr);
                    //             * */
                    //            while (plcRetSorting.Equals("0") && !plcHasError)//未正常接收分拣拨烟数据,需要重发
                    //            {
                    //                try
                    //                {
                    //                    updateLabel("电未正常接收拨烟数据，请稍候...", label3);
                    //                   // label3.Text = "电未正常接收拨烟数据，请稍候...";
                    //                    Thread.Sleep(1000);//1000
                    //                    if (plcRetSorting.Equals("0") && !plcHasError)
                    //                    {
                    //                        //taskNum = 0;//重新开始计数


                    //                        sendData(sortObj);
                    //                    }
                    //                }
                    //                catch (ThreadStateException exc)
                    //                {
                    //                }
                    //            }
                    //        }
                    //        if (!plcHasError && plcRetSorting.Equals("1"))
                    //        {
                    //            //分拣数据发送成功


                    //            // 开始接收任务完成信号


                    //            while (!plcHasError && sFun.getTaskFinishNum(taskNum) > 5)//如果拨烟任务大于5，则继续等待直至剩5条分拣任务
                    //            {

                    //                //sendSortingThread.Resume();
                    //                if (m_socket == null || m_socket.Connected == false)
                    //                {
                    //                    connect();
                    //                }
                    //                if (m_socket.Connected == false)
                    //                {
                    //                    DialogResult MsgBoxResult = MessageBox.Show("电控连接不成功，是否继续尝试连接?",//对话框的显示内容 
                    //                                        "操作提示",//对话框的标题 
                    //                                        MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                    //                                        MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                    //                                        MessageBoxDefaultButton.Button2);//定义对话框的按钮式样

                    //                    if (MsgBoxResult == DialogResult.No)
                    //                    {
                    //                        updateControlEnable(true, button10);
                    //                       // this.button10.Enabled = true;
                    //                        return;
                    //                    }
                    //                    plcConnIsOk = false;
                    //                }
                    //            }
                    //        }

                    //        //taskNum = 0;//重新开始计数


                    //        plcRetSorting = "0";
                    //    }
                    //    //判断是否只剩5个拨烟计划未完成，如果是则继续分拣信息发送





                    //}
                }
            }
            else
            {
                DialogResult MsgBoxResult = MessageBox.Show("电控连接不成功，是否继续尝试连接?",//对话框的显示内容 
                                                            "操作提示",//对话框的标题 
                                                            MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                            MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                            MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
                if (MsgBoxResult == DialogResult.Yes)
                {
                    button10_Click(null, null);
                }
            }
            updateControlEnable(true, button10);
            //this.button10.Enabled = true;
            //thread.Abort(); 
        }


        private void checkSocket()
        {
            bool isConnected = false;
            bool isRead = false;
            bool isWrite = false;
            bool isError = false;
            int avalSize = 0;
            bool ret = false;

            int trycount = 0;
            while (m_socket == null || m_socket.Connected == false)
            {
               // panel4.Visible = true;
               // label3.Visible = true;
                //updateLabel("正尝试连接电控服务器，请稍候...", label3);
                //label3.Text = "正尝试连接电控服务器，请稍候...";
                connect();
                ret = checkSocket(ref isConnected, ref isRead, ref isWrite, ref isError, ref avalSize);
                try
                {
                    if (ret = false || isConnected == false || isError == true || (isRead == true && avalSize == 0))
                        Thread.Sleep(1000);
                }
                catch (ThreadStateException exc)
                {
                    // exc.Message;
                }
                //累加尝试链接次数,三次不成功则跳出
                trycount = trycount + 1;
                if (trycount == 3) return;
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
           // startFenJian();

            Thread thread = new Thread(new ThreadStart(startFenJian));
            thread.Start();
           
        }

        private void tryConnect()
        {
            //首次链接尝试
            if (m_socket == null || m_socket.Connected == false)
            {
                connect();
            }
        }


        private Socket createClientSocket()
        {
            Socket s = null;           
            // 构造用于发送的 字节缓冲.
            try
            {
                
                // IP地址.
                IPAddress localAddr = IPAddress.Parse(ipaddress);
                // 接入点.
                IPEndPoint ephost = new IPEndPoint(localAddr, PORT);
                s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                s.Connect(ephost);
            }
            catch(Exception e){
                s = null;
            }
            return s;
        }

        public bool connect()
        {
            //在这里调用上面的连接方法 
            m_socket = null;
            m_socket = createClientSocket();
            if (m_socket == null || m_socket.Connected==false)
            { 
                return false; 
            }
            
            return true;
        }

        ///再有一个检查Socket状态的方法
        public bool checkSocket(
         ref bool isConnected,
         ref bool isRead,
         ref bool isWrite,
         ref bool isError,
         ref int avalSize
        )
        {
            bool ret = true;
            try
            {
                isConnected = m_socket.Connected;
                isRead = m_socket.Poll(50, SelectMode.SelectRead);
                avalSize = m_socket.Available;
                isWrite = m_socket.Poll(50, SelectMode.SelectWrite);
                isError = m_socket.Poll(50, SelectMode.SelectError);
            }
            catch (Exception e)
            {
                ret = false;
            }
            return ret;
        }

        public void sendStartSignal()
        {
             int avalSize = 0;

            Byte[] sendBytes = ASCII.GetBytes(SEND_MESSAGE);
            // 构造用于接收的 字节缓冲.
            Byte[] recvBytes = new Byte[1024];

            string input = "";
            input = "001|1";
            
            
            sendBytes = Encoding.ASCII.GetBytes(input);
            checkSocket();
            m_socket.Send(sendBytes, sendBytes.Length, SocketFlags.None);
            Console.WriteLine("向服务器发送到了:{0}", input);
        }


        public void checkPlcIsOk()   
        {
           // Boolean retVal = false;
            int avalSize = 0;

            Byte[] sendBytes = ASCII.GetBytes(SEND_MESSAGE);
            // 构造用于接收的 字节缓冲.
            Byte[] recvBytes = new Byte[1024];

            string input = "";
            input = "103|1";
            Console.WriteLine("向服务器发送到了:{0}", input);
            sendBytes = Encoding.ASCII.GetBytes(input);


            checkSocket();
            m_socket.Send(sendBytes, sendBytes.Length, SocketFlags.None);

            //try
            //{
            //    //m_socket.ReceiveTimeout = 3000;
            //    byte[] data = new byte[1024];
            //    int recv = 0;
            //    checkSocket();
            //    recv = m_socket.Receive(data);

            //    String stringdata = Encoding.ASCII.GetString(data, 0, recv);
            //    Console.WriteLine("服务器接收到了:" + recv + "," + stringdata + ",");
            //    if (stringdata.Equals("1"))
            //    {
            //        //retVal = true;
            //        plcConnIsOk = true;
            //    }
            //}
            //catch {
            //    return;
            //}
            //return retVal;
        }

                    ///专门用来发送数据的方法
              public void sendData(Object sortStrObj)
              {
                  bool retVal = false;
                  String sortStr = sortStrObj.ToString();
                  checkSocket();

                  Byte[] sendBytes = ASCII.GetBytes(SEND_MESSAGE);
                  sendBytes = Encoding.ASCII.GetBytes(sortStr);
                  try
                  {
                      m_socket.Send(sendBytes, sendBytes.Length, SocketFlags.None);
                      retVal = true;
                      Console.WriteLine("SendData is Ok! Data is " + sortStr + " ");

                      /*
                      byte[] data = new byte[1024];
                      int recv = 0;
                      checkSocket();
                      recv = m_socket.Receive(data);
                      String stringdata = Encoding.ASCII.GetString(data, 0, recv);
                      Console.WriteLine("服务器接收到了:" + recv + "," + stringdata + ",");

                      String dataHeader = sortStr.Substring(0, 3);
                      Console.WriteLine("dataHeader=" + dataHeader);
                      if (dataHeader.Equals("103"))
                      {
                          plcConnIsOk = false;
                          if (stringdata.Equals("1"))
                          {
                              plcConnIsOk = true;
                          }
                      }
                      if (dataHeader.Equals("101"))
                      {
                          plcRetSorting = "0";
                          if (stringdata.Equals("1"))//成功接收
                          {
                              plcRetSorting = "1";
                          }
                      }*/

                  }
                  catch (Exception e)
                  {
                      retVal = false;
                  }
                 // return retVal;
                }

        /*
         *发送拨烟数据 
         */
        

        /*
         * 等待分拣信号返回
         */
        public void waitSortingSignal()
        {
            // Boolean retVal = false;
            int avalSize = 0;

            Byte[] sendBytes = ASCII.GetBytes(SEND_MESSAGE);
            // 构造用于接收的 字节缓冲.
            Byte[] recvBytes = new Byte[1024];

            string input = "";
            input = "104";
            Console.WriteLine("向服务器发送到了:{0}", input);
            sendBytes = Encoding.ASCII.GetBytes(input);
            m_socket.Send(sendBytes, sendBytes.Length, SocketFlags.None);

        }

        /// <summary>
        /// 监听客户端连接


        /// </summary>
        private void ListenPlcData()
        {
            //Thread receiveThread = new Thread(ReceiveMessage);
            //while (true)
            //{
                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Name = "消息接收线程";
                receiveThread.Start(m_socket);
            //}
        }

        Boolean startMachine = false;
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="clientSocket"></param>
        private void ReceiveMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {
                try
                {                    
                    checkSocket();
                    //通过clientSocket接收数据
                    int receiveNumber = myClientSocket.Receive(result);
                    Console.WriteLine("接收plc{0}消息{1}", myClientSocket.RemoteEndPoint.ToString(), Encoding.ASCII.GetString(result, 0, receiveNumber));
                    String data = Encoding.ASCII.GetString(result, 0, receiveNumber);
                    String dataHeader = "";
                    String dataEnd = "";

                    String datatemp = "";
                    if (data != null && data.Length > 3)
                    {
                        dataHeader = data.Substring(0, 3);
                        dataEnd = data.Substring(4);
                    }
                    if (dataHeader.Equals("103"))//检查plc是否准备就绪
                    {
                        plcConnIsOk = false;
                        if (dataEnd.Equals("1"))
                        {
                            plcConnIsOk = true;
                        }
                    }
                    if (dataHeader.Equals("001"))
                    {
                        startMachine = false;
                        if (dataEnd.Equals("1"))//成功接收拨烟数据
                        {
                            startMachine = true;
                        }
                    }
                    if (dataHeader.Equals("101"))
                    {
                        plcRetSorting = "0";
                        if (dataEnd.Equals("1"))//成功接收拨烟数据
                        {
                            plcRetSorting = "1";
                        }
                    }
                    if (dataHeader.Equals("102"))//任务完成信号
                    {
                        string[] dataarr = data.Split('|');
                        for (int i = 1; i < dataarr.Length;i++ )
                        {
                            //若非重复数据
                            if(data!=datatemp){
                                dataEnd = dataarr[i].Substring(3);
                                taskNum = taskNum + 1;
                                string tnum = SortingPub.unformatData(dataEnd);
                                Console.WriteLine("任务号：" + tnum);
                                //updateListBox("任务:" + tnum + "  分拣完成");
                                sFun.updateTaskState(tnum);//更新该任务对应的各表标志
                               // sFun.getAllocateCigarDataFromDB();
                                string taskinfo = sFun.getTaskinfo(tnum);
                              
                                Console.WriteLine("taskinfo：" + taskinfo);
                               
                                this.BeginInvoke(new UpdateDataGridView(updateTaskInfo), new Object[] { taskinfo });
                               // updateTaskInfo(taskinfo);
                            }
                            updateLabel("正在分拣，请耐心等候...", label3);
                            datatemp = data;
                        }
                       // sendCompletedFeed();
                         myClientSocket.Send(Encoding.ASCII.GetBytes("1021"));
                    }
                    if (dataHeader.Equals("300"))//分拣故障
                    {
                       // myClientSocket.Send(Encoding.ASCII.GetBytes("2"));
                        plcHasError = true;
                        Console.WriteLine("分拣故障，故障编号：" + dataEnd);
                        SortingFun.WriteErrorLog(dataEnd);
                        sendErrorFeed(dataHeader);
                    }
                    
                    if (dataHeader.Equals("302"))//分拣故障恢复
                    {
                        // myClientSocket.Send(Encoding.ASCII.GetBytes("2"));
                        plcHasError = false;
                        Console.WriteLine("故障恢复，故障编号：" + dataEnd);
                        //myClientSocket.Send(Encoding.ASCII.GetBytes("1"));
                        sendErrorFeed(dataHeader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    updateListBox(ex.Message);
                    myClientSocket.Shutdown(SocketShutdown.Both);
                    myClientSocket.Close();
                    break;
                }

            }
        }

        //异步调用结束
        private static void CallBackMethod(IAsyncResult result)
        {
            startReceive gchandler = (startReceive)((AsyncResult)result).AsyncDelegate;
            //DataPublic.dataTable = (DataTable)handler.EndInvoke(result);
            gchandler.EndInvoke(result);
        }

        public void RaiseSomeEvent()
        {

           EventArgs e = new EventArgs();
            Console.Write("Please input 'a':");
            string s = Console.ReadLine();
            //在用户输入一个小a的情况下触发事件，否则不触发
            if (s == "a")
            {
                SomeEvent(this, e);
            }
        }

        //事件的触发者自己对事件进行处理，这个方法的参数必须和代理中声名的一致


        private void ProcessSomeEvent(object sender, EventArgs e)
        {
            //plc分拣故障，故障信息入库


            Console.WriteLine("hello");
        }

        //这是事件的接受者对事件的响应


         private void ResponseSomeEvent(object sender, EventArgs e)
        {
            Console.WriteLine("Some event occur!");

        }

       public void writeListBox(string info) {
           //String time = DateTime.Now.ToLongTimeString();
           //this.list_data.Items.Add(time + "    "+info);
       }

       private delegate void HandleDelegate(string strshow);

       public void updateListBox(string info)
       {
           //String time = DateTime.Now.ToLongTimeString();
           //if (this.list_data.InvokeRequired)
           //{
           //    //   this.txtreceive.BeginInvoke(new ShowDelegate(Show), strshow);//这个也可以

           //    this.list_data.Invoke(new HandleDelegate(updateListBox), info);
           //}
           //else
           //{
           //    this.list_data.Items.Add(time + "    " + info);

           //}
       }

       public void initdata() {
           //String sql = "SELECT total.*,finish.finishcuscount,finish.finishqty,round(finishqty/qty*100,2)AS percent FROM " +
           //             "(SELECT o.regioncode,COUNT(o.billcode) AS cuscount,SUM(o.orderquantity)AS qty FROM t_produce_order o GROUP BY o.regioncode)total left outer join "+
           //             "(SELECT o.regioncode,COUNT(o.billcode) AS finishcuscount,SUM(o.orderquantity)AS finishqty FROM t_produce_order o WHERE state='完成' GROUP BY o.regioncode)finish "+
           //             "on total.regioncode=finish.regioncode  ORDER BY total.regioncode";
           //OracleDataReader myread = DataPublic.ReadDb(sql);
           //try
           //{
           //    String regioncode = "", cuscount = "", qty = "", finishcuscount = "", finishqty="",percent="";
           //    while (myread!=null && myread.Read())
           //    {
           //        regioncode = myread["regioncode"].ToString().Trim();
           //        cuscount = myread["cuscount"].ToString().Trim();
           //        qty = myread["qty"].ToString().Trim();
           //        finishcuscount = myread["finishcuscount"].ToString().Trim();
           //        if (finishcuscount == "") finishcuscount = "0";
           //        finishqty = myread["finishqty"].ToString().Trim();
           //        if (finishqty == "") finishqty = "0";
           //        percent = myread["percent"].ToString().Trim();
           //        if (finishqty == "0") percent = "0";

           //        int index = this.task_data.Rows.Add();
           //        this.task_data.Rows[index].Cells[0].Value = regioncode;
           //        this.task_data.Rows[index].Cells[1].Value = regioncode;
           //        this.task_data.Rows[index].Cells[2].Value = finishcuscount + "/" + cuscount;
           //        this.task_data.Rows[index].Cells[3].Value = finishcuscount + "/" + cuscount;
           //        this.task_data.Rows[index].Cells[4].Value = finishqty + "/" + qty;
           //        this.task_data.Rows[index].Cells[5].Value = percent+"%";
           //    }
           //    //MessageBox.Show(sFun.getCzcode("950"));
           //    /*int indexj=0;
           //    for (int i = 0; i < task_data.RowCount;i++ )
           //    {
           //        if(this.task_data.Rows[i].Cells[0].Value.ToString()=="0204")
           //        {
           //             indexj=i;
           //             break;
           //        }
           //    }
           //    for (int i = 0; i < 10;i++ )
           //    {
           //        double per=double.Parse(this.task_data.Rows[indexj].Cells[5].Value.ToString());
           //        per = per + i;
           //        this.task_data.Rows[indexj].Cells[5].Value = per;
           //    }
           //    */

           //}
           //finally
           //{
           //    if (myread != null)
           //    {
           //        myread.Close();
           //        myread.Dispose();
           //    }
           //}
           
       }
       delegate void UpdateDataGridView(string data);
       public void updateTaskInfo(string taskinfo) 
       {
           //Console.WriteLine("进入方法updateTaskInfo");
           // if(taskinfo!=null&&taskinfo.Length>0){
           //     string[] info = taskinfo.Split('-');

           //     int len=task_data.RowCount;
           //     int indexj = 0;
           //     //取要修改分拣数据的行标

           //     if (len > 0)
           //     {
           //         for (int i = 0; i < len; i++)
           //         {
           //             if (this.task_data.Rows[i].Cells[0].Value.ToString() == info[0].ToString())
           //             {
           //                 indexj = i;
           //                 break;
           //             }
           //         }
           //         Console.WriteLine(this.task_data.Rows[indexj].Cells[0].Value.ToString());
           //         string[] boxcount = this.task_data.Rows[indexj].Cells[2].Value.ToString().Split('/');
           //         string[] cuscount = this.task_data.Rows[indexj].Cells[3].Value.ToString().Split('/');
           //         string[] finishqty = this.task_data.Rows[indexj].Cells[4].Value.ToString().Split('/');

           //         //修改完成箱数
           //         int boxc = int.Parse(boxcount[0].ToString());
           //         boxc = boxc + 1;
           //         this.task_data.Rows[indexj].Cells[2].Value = boxc + "/" + boxcount[1];

           //         //修改完成客户数

           //         int cusc = int.Parse(cuscount[0].ToString());
           //         cusc = cusc + 1;
           //         this.task_data.Rows[indexj].Cells[3].Value = cusc + "/" + cuscount[1];

           //         //修改完成分拣量

           //         int finish = int.Parse(finishqty[0].ToString());
           //         finish = finish + int.Parse(info[1].ToString());
           //         this.task_data.Rows[indexj].Cells[4].Value = finish + "/" + finishqty[1];

           //         //修改分拣完成百分比

           //         double percent = Math.Round(double.Parse(finish + "") / double.Parse(finishqty[1].ToString()) * 100, 2);
           //         this.task_data.Rows[indexj].Cells[5].Value = percent+"%";

           //         Console.WriteLine(boxc + "/" + boxcount[1]);
           //         Console.WriteLine(cusc + "/" + cuscount[1]);
           //         Console.WriteLine(finish + "/" + finishqty[1]);
           //         Console.WriteLine(percent + "%");
           //     }
                
           // }
       }

       private void w_SortingControlMain_FormClosing(object sender, FormClosingEventArgs e)
       {
           //DialogResult MsgBoxResult = MessageBox.Show("确定要退出程序?",//对话框的显示内容 
           //                                                 "操作提示",//对话框的标题 
           //                                                 MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
           //                                                 MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
           //                                                 MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
           ////Console.WriteLine(MsgBoxResult);
           //if (MsgBoxResult == DialogResult.Yes)
           //{
           //    System.Environment.Exit(System.Environment.ExitCode);
           //    this.Dispose();
           //    this.Close();
           //}
           //else {
           //    e.Cancel = true;
           //}
       }

       public void exitFunc() { 
       
       }

       private void button11_Click(object sender, EventArgs e)
       {
            /*tryConnect();
            checkSocket();

            //连接正常
            if (m_socket != null)
            {
                if (m_socket.Connected)
                {
                    startReceive gchandler = new startReceive(ListenPlcData);
                    IAsyncResult result = gchandler.BeginInvoke(new AsyncCallback(CallBackMethod), null);
                    //checkPlcIsOk();
                }
            }*/
       }
       //接收故障或故障恢复信息后返回
       public void sendErrorFeed(string datahead)
       {
           Console.WriteLine(datahead);
           Byte[] sendBytes = ASCII.GetBytes(SEND_MESSAGE);
           // 构造用于接收的 字节缓冲.
           Byte[] recvBytes = new Byte[1024];

           string input = datahead+"1";
           //if (datahead == "302") input = "3021";
           Console.WriteLine("向服务器发送故障反馈:{0}", input);

           sendBytes = Encoding.ASCII.GetBytes(input);
           checkSocket();
           m_socket.Send(sendBytes, sendBytes.Length, SocketFlags.None);
       }

       //接收故障或故障恢复信息后返回
       public void sendCompletedFeed()
       {
           //Console.WriteLine(datahead);
           Byte[] sendBytes = ASCII.GetBytes(SEND_MESSAGE);
           // 构造用于接收的 字节缓冲.
           Byte[] recvBytes = new Byte[1024];

           string input = "1021";
           //if (datahead == "302") input = "3021";
           
           sendBytes = Encoding.ASCII.GetBytes(input);
           checkSocket();
           m_socket.Send(sendBytes, sendBytes.Length, SocketFlags.None);
           Console.WriteLine("向服务器发送收到完成信号反馈:{0}", input);
       }

       private void button12_Click(object sender, EventArgs e)
       {
           if (m_socket != null)
           {
               m_socket.Send(Encoding.ASCII.GetBytes("901|1"));
           }
           updateControlEnable(true, button10);
           updateLabel("分拣结束", label3);
       }

       private void button7_Click(object sender, EventArgs e)
       {
           //w_SortingControlMain w_SortingControlMain = new w_SortingControlMain();

           ////w_SortingControlMain.MdiParent = this;
           //w_SortingControlMain.WindowState = FormWindowState.Maximized;
           //w_SortingControlMain.Show();
           this.Close();
       }
       public static int index = 10000000;
       private void button6_Click(object sender, EventArgs e)
       {
           index += 1;
           string data = "101|";
           string temp = "";
           int length=0;
           for (var i = 0; i < tblist.Count; i++)
           {
               if (tblist[i].Text != "")
               {
                   temp += SortingPub.formatData((i + 1).ToString(), 2) + SortingPub.formatData(tblist[i].Text,3);
               }
           }
           length=temp.Length+4+4+8+4+1+1;
           data += SortingPub.formatData(length.ToString(), 4) + index+textBox61.Text + "1000" + temp+".";
           sendData(data);
       }

       private void button8_Click(object sender, EventArgs e)
       {
           foreach (var item in tblist)
           {
               item.Text = "";
           }
       }

    }
}

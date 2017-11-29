using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text;
using System;

namespace SortingControlSys.SortingControl
{
    class socketServer
    {
        private  byte[] result = new byte[1024];
        private  int myProt = 2001;   //端口
        static Socket serverSocket;

        public socketServer()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

         public void serverStart()
        {
            //服务器IP地址
            IPAddress ip = IPAddress.Parse("10.75.56.114");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, myProt));  //绑定IP地址：端口
            serverSocket.Listen(10);    //设定最多10个排队连接请求
            Console.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());
            //通过Clientsoket发送数据
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();
            Console.ReadLine();
        }

        /// <summary>
        /// 监听客户端连接
        /// </summary>
        private  void ListenClientConnect()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                clientSocket.Send(Encoding.ASCII.GetBytes("Server Say Hello"));
                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Start(clientSocket);
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="clientSocket"></param>
        private  void ReceiveMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    //通过clientSocket接收数据
                    int receiveNumber = myClientSocket.Receive(result);
                    Console.WriteLine("接收客户端{0}消息{1}", myClientSocket.RemoteEndPoint.ToString(), Encoding.ASCII.GetString(result, 0, receiveNumber));
                    String data = Encoding.ASCII.GetString(result, 0, receiveNumber);
                    String dataHeader = data.Substring(0, 3);
                    if (dataHeader.Equals("102"))//任务完成信号
                    {
                        myClientSocket.Send(Encoding.ASCII.GetBytes("1"));
                    }
                    if (dataHeader.Equals("101"))
                    {
                        myClientSocket.Send(Encoding.ASCII.GetBytes("2"));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    myClientSocket.Shutdown(SocketShutdown.Both);
                    myClientSocket.Close();
                    break;
                }
            }
        }
    }
}

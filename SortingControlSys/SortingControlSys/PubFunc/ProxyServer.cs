using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace SortingControlSys.PubFunc
{
    public class ProxyServer
    {
        WriteLog log = new WriteLog();
        int ServerPort = 0;
        List<Socket> socketList = new List<Socket>();
        public ProxyServer(int port)
        {
            ServerPort = port;

        }

        public void Send(byte[] msg, String RemoteEndPoint)
        {
            
            foreach (var item in socketList)
            {
                if (item.RemoteEndPoint.ToString().Contains(RemoteEndPoint))
                {
                    item.Send(msg);
                    break;
                }
            }
        }
       private bool stopServer;
       Socket listener;
        public void StopServer()
        {
            stopServer=true;
            if (listener != null)
            {
                listener.Close();
                listener.Dispose();
                listener = null;
                log.Write("Server Is Stop Listen");
            }
        }
        public void Listen()
        {

            // Create a TCP/IP socket.
           

            // Bind the socket to the local endpoint and listen for incoming connections.
            var port = ServerPort;
            try
            {
                if (listener != null)
                {
                    listener.Close();
                    listener.Dispose();
                    listener = null;
                    Thread.Sleep(1000);
                }
                listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(new IPEndPoint(IPAddress.Any, port));
                listener.Listen(10);
                log.Write("Server Is Start Listen");
            }
            catch(Exception e)
            {
                log.Write(e.Message);
            }
            
            while (true && !stopServer)
            {
                try
                {
                    var clientSocket = listener.Accept();
                    
                   socketList.Add(clientSocket);
                }
                catch (ThreadAbortException)
                {
                    log.Write("Server Is Stop Listen");
                    socketList.Clear();
                  
                    break;
                    

                }
                catch (Exception e)
                {
                    break;
                }
            }
            if (!stopServer)
            {
                //listener = null;
                Thread.Sleep(1000);
                Listen();

            }
           
        } 
    }
}

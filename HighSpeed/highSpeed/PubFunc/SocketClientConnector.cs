using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;

namespace highSpeed.PubFunc
{
    public class SocketClientConnector
    {
        public static byte[] intToBytes(int num, int len)
		{
			byte[] array = new byte[len];
			int num2 = (len - 1) * 8;
			int num3 = 255 << num2;
			for (int i = 0; i < len; i++)
			{
				array[i] = (byte)(((ulong)num & (ulong)((long)num3)) >> num2);
				num2 -= 8;
				num3 >>= 8;
			}
			return array;
		}

        public static byte[] BuildByte(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
			{
                return null;
				//throw new ZipException("生成压缩文件字符流时异常,文件名为空!");
			}
			byte[] result=null;
			try
			{
				byte[] array = File.ReadAllBytes(fileName);
				byte[] array2 = intToBytes(array.Length, 4);
				byte[] array3 = new byte[array.Length + array2.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					array3[i] = array2[i];
				}
				for (int j = 0; j < array.Length; j++)
				{
					array3[array2.Length + j] = array[j];
				}
				result = array3;
			}
			catch (Exception ex)
			{
				//this.logger.Error("根据文件名称生成字节数组过程异常:", ex);
				//throw new ZipException("根据文件名称生成字节数组过程异常:" + ex.StackTrace);
			}
			return result;
		}

        /// <summary>
        /// 向远程主机发送文件
        /// </summary>
        /// <param name="socket" >要发送数据且已经连接到远程主机的 socket</param>
        /// <param name="fileName">待发送的文件名称</param>
        /// <param name="maxBufferLength">文件发送时的缓冲区大小</param>
        /// <param name="outTime">发送缓冲区中的数据的超时时间</param>
        /// <returns>0:发送文件成功；-1:超时；-2:发送文件出现错误；-3:发送文件出现异常；-4:读取待发送文件发生错误</returns>
        /// <remarks >
        /// 当 outTime 指定为-1时，将一直等待直到有数据需要发送
        /// </remarks>
        public static int SendFile(Socket socket, string fileName, int maxBufferLength, int outTime)
        {
            if (fileName == null || maxBufferLength <= 0)
            {
                
                throw new ArgumentException("待发送的文件名称为空或发送缓冲区的大小设置不正确.");
            }
            int flag = 0;
            try
            {
               
                //FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                //long fileLen = fs.Length;                        // 文件长度
                //long leftLen = fileLen;                            // 未读取部分
                //int readLen = 0;                                // 已读取部分
                //byte[] buffer = null;
                //int tfileLen = (fileLen).ToString().Length;
                //int needZero = 4 - tfileLen;
                //String preStr = (fileLen).ToString();
                //if (needZero > 0)
                //{
                //    while (needZero > 0)
                //    {
                //        preStr = "0" + preStr;
                //        needZero--;
                //    }

                //}
                //if (fileLen <= maxBufferLength)
                //{            /* 文件可以一次读取*/
                //    buffer = new byte[fileLen+4+1];
                //   Byte[] b= System.Text.Encoding.Default.GetBytes(preStr);
                //   for (int i = 0; i < b.Length; i++)
                //   {
                //       buffer[i] = b[i];
                //   }
                //   buffer[fileLen +4] = 2;
                //    readLen = fs.Read(buffer, 4, (int)fileLen);
                byte[] buffer = BuildByte(fileName);
                    flag = SendData(socket, buffer, outTime);
                //}
                //else
                //{
                //    /* 循环读取文件,并发送 */

                //    while (leftLen != 0)
                //    {
                //        if (leftLen < maxBufferLength)
                //        {
                //            buffer = new byte[leftLen];
                //            readLen = fs.Read(buffer, 0, Convert.ToInt32(leftLen));
                //        }
                //        else
                //        {
                //            buffer = new byte[maxBufferLength];
                //            readLen = fs.Read(buffer, 0, maxBufferLength);
                //        }
                //        if ((flag = SendData(socket, buffer, outTime)) < 0)
                //        {
                //            break;
                //        }
                //        leftLen -= readLen;
                //    }
                //}
                //fs.Flush();
                //fs.Close();
            }
            catch (IOException e)
            {
                flag = -4;
            }
            return flag;
        }

        /// <summary>
        /// 向远程主机发送数据
        /// </summary>
        /// <param name="socket">要发送数据且已经连接到远程主机的 Socket</param>
        /// <param name="buffer">待发送的数据</param>
        /// <param name="outTime">发送数据的超时时间，以秒为单位，可以精确到微秒</param>
        /// <returns>0:发送数据成功；-1:超时；-2:发送数据出现错误；-3:发送数据时出现异常</returns>
        /// <remarks >
        /// 当 outTime 指定为-1时，将一直等待直到有数据需要发送
        /// </remarks>
        public static int SendData(Socket socket, byte[] buffer, int outTime)
        {
            if (socket == null || socket.Connected == false)
            {
                throw new ArgumentException("参数socket 为null，或者未连接到远程计算机");
            }
            if (buffer == null || buffer.Length == 0)
            {
                throw new ArgumentException("参数buffer 为null ,或者长度为 0");
            }

            int flag = 0;
            try
            {
                int left = buffer.Length;
                int sndLen = 0;

                while (true)
                {
                    if ((socket.Poll(outTime * 100, SelectMode.SelectWrite) == true))
                    {
                        // 收集了足够多的传出数据后开始发送
                        sndLen = socket.Send(buffer, sndLen, left, SocketFlags.None);
                        left -= sndLen;
                        if (left == 0)
                        {                                        // 数据已经全部发送
                            flag = 0;
                            break;
                        }
                        else
                        {
                            if (sndLen > 0)
                            {                                    // 数据部分已经被发送
                                continue;
                            }
                            else
                            {                                                // 发送数据发生错误
                                flag = -2;
                                break;
                            }
                        }
                    }
                    else
                    {                                                        // 超时退出
                        flag = -1;
                        break;
                    }
                }
            }
            catch (SocketException e)
            {

                flag = -3;
            }
            return flag;
        }
    }
}

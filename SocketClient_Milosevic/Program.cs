using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient_Milosevic
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            string strIPAddress = "";
            string strPort = "";
            IPAddress ipAddress = null;
            int nPort;

            try
            {
                Console.WriteLine("IP del server: ");
                strIPAddress = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Porta del server: ");
                strPort = Console.ReadLine();

                if (!IPAddress.TryParse(strIPAddress.Trim(), out ipAddress))
                {
                    Console.WriteLine("IP non valido.");
                    return;
                }

                if (!int.TryParse(strPort, out nPort))
                {
                    Console.WriteLine("Porta non valida.");
                    return;
                }

                if (nPort <= 0 || nPort >= 65535)
                {
                    Console.WriteLine("Porta non valida.");
                    return;
                }

                Console.WriteLine();
                Console.WriteLine("Endpoint del server " + ipAddress.ToString() + " " + nPort);

                client.Connect(ipAddress, nPort);

                byte[] buff = new byte[128];
                string sendString = "";
                string receiveString = "";
                int recevivedBytes = 0;

                while (true)
                {
                    Console.WriteLine("Manda un messaggio: ");
                    sendString = Console.ReadLine();
                    buff = Encoding.ASCII.GetBytes(sendString);
                    client.Send(buff);
                    if (sendString.ToUpper().Trim() == "QUIT")
                    {
                        break;
                    }
                    Array.Clear(buff, 0, buff.Length);
                    recevivedBytes = client.Receive(buff);
                    receiveString = Encoding.ASCII.GetString(buff, 0, recevivedBytes);

                    Console.WriteLine("S: " + receiveString);


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

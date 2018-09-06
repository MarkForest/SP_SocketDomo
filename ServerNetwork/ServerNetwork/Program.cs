using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ServerNetwork
{
    class Program
    {
        static int port = 8005; //порт для приема входных запросов
        static void Main(string[] args)
        {
            //получаем адреса для запуска сокета
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            //создаем сокет
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                //связать сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);
                //начинаем прослушивание
                listenSocket.Listen(10);
                Console.WriteLine("Сервер запущен. Ожидание соединения ....");
                while (true)
                {
                    Socket clientSocket = listenSocket.Accept();
                    //Получение сообщения
                    Console.WriteLine($"Ip: {clientSocket.RemoteEndPoint}");
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; //количество полученных байтов
                    byte[] data = new byte[1024]; //буфер для получаемых данных
                    do
                    {
                        bytes = clientSocket.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data));

                    } while (clientSocket.Available>0);
                    Console.WriteLine($"{DateTime.Now.ToShortTimeString()} {builder.ToString()}");
                    //отправка ответа
                    string message = "Ваше сообщение достовлено";
                    data = Encoding.Unicode.GetBytes(message);
                    clientSocket.Send(data);
                    //закрываем клиентский сокет
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

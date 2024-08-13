using System;
using System.Net.Sockets;
using System.Text;

namespace DragEngine
{
    public class Client
    {
        public TCP tcp;
        public int id;

        readonly static int dataSize = 4096;

        public Client(int clientId)
        {
            id = clientId;
            tcp = new TCP(id);
        }

        public class TCP
        {
            public TcpClient client;
            
            NetworkStream stream;
            readonly int id;
            byte[] data;

            public TCP(int id)
            {
                this.id = id;
            }

            public void Connect(TcpClient client)
            {
                this.client = client;
                client.ReceiveBufferSize = dataSize;
                client.SendBufferSize = dataSize;

                data = new byte[dataSize];

                stream.BeginRead(data, 0, dataSize, ReceiveCallBack, null);
            }

            void ReceiveCallBack(IAsyncResult result)
            {
                try
                {
                    int byteLenght = stream.EndRead(result);
                    if (byteLenght <= 0) return;

                    byte[] newData = new byte[byteLenght];
                    Array.Copy(data, newData, byteLenght);

                    stream.BeginRead(data, 0, dataSize, ReceiveCallBack, null);
                }
                catch (Exception e)
                {
                    Debug.Log($"Error {e}");
                }
            }
        }
    }

}

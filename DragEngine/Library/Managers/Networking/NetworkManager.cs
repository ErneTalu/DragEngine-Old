using Riptide;

namespace DragEngine
{
    public class NetworkManager
    {
        public static Client client { get; set; }

        public static void Connect()
        {
            client = new Client();
            client.ClientDisconnected += (s, e) => NetworkPlayer.List.Remove(e.Id);

            client.Connect("127.0.0.1:7777");
        }
    }
}

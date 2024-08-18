using Riptide;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.Xna.Framework;

namespace DragEngine
{
    public enum MessageId : ushort
    {
        PlayerSpawn,
        PlayerPosition,
    }

    public class NetworkPlayer
    {
        internal static readonly Dictionary<ushort, NetworkPlayer> List = new Dictionary<ushort, NetworkPlayer>();

        public Vector2 position;
        private readonly ushort id;


        public NetworkPlayer(ushort clientId, Vector2 position)
        {
            id = clientId;
            this.position = position;
        }

        internal void Update(float deltaTime)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector2 pos = new Vector2(h, v);
            if (h != 0 && v != 0)
                pos = pos.normalized;

            position += pos * deltaTime * 96;
            SendPosition();
        }

        private void SendPosition()
        {
            Riptide.Message message = Riptide.Message.Create(MessageSendMode.Unreliable, MessageId.PlayerPosition);
            message.AddFloat(position.x).AddFloat(position.y);

            NetworkManager.client.Send(message);
        }

        [MessageHandler((ushort)MessageId.PlayerPosition)]
        private static void HandlePosition(Riptide.Message message)
        {
            ushort id = message.GetUShort();
            Vector2 position = new Vector2(message.GetFloat(), message.GetFloat());

            if (List.TryGetValue(id, out NetworkPlayer player))
                player.position = position;
        }

        [MessageHandler((ushort)MessageId.PlayerSpawn)]
        private static void HandleSpawn(Riptide.Message message)
        {
            ushort id = message.GetUShort();
            Vector2 position = new Vector2(message.GetFloat(), message.GetFloat());

            List.Add(id, new NetworkPlayer(id, position));
        }
    }
}
using Riptide.Utils;
using Riptide;


namespace DragEngine
{
    public class Test : DragEngine
    {
        public Test() : base(new Vector2(500, 500), "Test") {  }

        public static Server server;
        public VarObject player;

        public override void Start()
        {
            RiptideLogger.Initialize(Debug.Log, false);

            //server = new Server();
            //server.Start(7777, 4);

            NetworkManager.Connect();
            if (server == null)
            {
                NetworkManager.client.Connected += (s, e) => player = new VarObject(new Vector2(50, 50), new Vector2(50, 50)).AddProp<Sprite>().AddProp<Physics>().varObject;
                Debug.Log("server is null");
            }

            //server.ClientConnected += (s, e) => player = new VarObject(new Vector2(50, 50), new Vector2(50, 50)).AddProp<Sprite>().AddProp<Physics>().varObject;
            //server.ClientDisconnected += (s, e) => NetworkPlayer.List.Remove(e.Client.Id);
        }

        public void SetRoom()
        {
            string[,] map = new string[10, 30]
            {
                {"w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w"},
                {"w",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
                {"w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w"}
            };
            Map.AddMap(map);

            foreach (Vector2 v in Map.GetTiles("a"))
            {
                
            }
        }

        public override void Update()
        {
            NetworkManager.client.Update();

            float horInput = Input.GetAxisRaw("Horizontal");
            float verInput = Input.GetAxisRaw("Vertical");

            if (player != null)
            {
                player.physics.Move(new Vector2(horInput, verInput) * Time.deltaTime * 16);
            }
        }

        public override void FixedUpdate()
        {
            //server.Update();
        }
    }
}

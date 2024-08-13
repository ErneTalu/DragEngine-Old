using FarseerPhysics.Dynamics;
using System;
using System.Drawing;

namespace DragEngine
{
    public class Game : DragEngine
    {
        public Game() : base(new Vector2(1250, 500), "Game") {  }

        Animator anim = new Animator();
        VarObject player, follower;
        int speed = 8;

        public override void Awake()
        {
            anim.AddAnimation("idle");
            anim.AddAnimation("run");
            anim.AddAnimation("jump");
            anim.AddAnimation("idlef");
            anim.AddAnimation("runf");

            //anim.animations["idle"].AddTileFrames(new Bitmap(@"C:\Users\erent\Desktop\Free\Main Characters\Ninja Frog\Idle (32x32).png"), 32, 32);
            //anim.animations["run"].AddTileFrames(new Bitmap(@"C:\Users\erent\Desktop\Free\Main Characters\Ninja Frog\Run (32x32).png"),32,32);
            //anim.animations["jump"].AddFrame(new Bitmap(@"C:\Users\erent\Desktop\Free\Main Characters\Ninja Frog\Jump (32x32).png"));

            //anim.animations["idlef"].AddTileFrames(new Bitmap(@"C:\Users\erent\Desktop\Free\Main Characters\Mask Dude\Idle (32x32).png"), 32, 32);
            //anim.animations["runf"].AddTileFrames(new Bitmap(@"C:\Users\erent\Desktop\Free\Main Characters\Mask Dude\Run (32x32).png"), 32, 32);
        }

        public override void Start()
        {
            SetMap();
        }

        public void SetMap()
        {
            string[,] map = new string[10, 30]
            {
                {"w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w"},
                {"w",".",".",".",".",".",".",".",".",".",".",".","p",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".","a",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".","a",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".","a",".",".",".",".","f",".",".",".",".",".",".",".","e",".",".",".",".",".",".",".",".",".","g",".","w"},
                {"w",".",".",".","a",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","a","a",".","w"},
                {"w",".",".",".","a",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","a",".",".",".","w"},
                {"w",".",".",".",".",".",".",".","a","a","a","a","a","a","a","a",".","a","a",".",".","a",".","a",".",".",".",".",".","w"},
                {"w",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
                {"w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w"}
            };
            Map.AddMap(map);

            //Bitmap tileImage = new Bitmap(@"C:\Users\erent\Desktop\Free\Background\Gray.png"); 
            //Tilemap tilemap = new Tilemap(tileImage, 32, 32, 75, 30);
            Vector2 s = new Vector2(50, 50);

            foreach (Vector2 v in Map.GetTiles("a"))
            {
                new VarObject(v, s).AddProp<Sprite>().AddProp<Collider>();
            }
            foreach (Vector2 v in Map.GetTiles("p"))
            {
                player = new VarObject(v, s).AddProp<Sprite>(Color.Blue).AddProp<Physics>(true).AddProp<Collider>().varObject;
            }
            foreach (Vector2 v in Map.GetTiles("f"))
            {
                follower = new VarObject(v, s).AddProp<Sprite>(Color.Gray).AddProp<Collider>().varObject;
            }
            foreach (Vector2 v in Map.GetTiles("g"))
            {
                follower = new VarObject(v, s, "flag").AddProp<Sprite>(Color.Red).AddProp<Physics>().AddProp<Collider>().varObject;
            }
            foreach (Vector2 v in Map.GetTiles("e"))
            {
                new VarObject(v, s, "box").AddProp<Sprite>(Color.Yellow).AddProp<Physics>().AddProp<Collider>();
            }

            //player.physics.physicMat = new PhysicMat(1,0);
        }

        public override void Update()
        {
            float horInput = Input.GetAxis("Horizontal");

            Vector2 movement = new Vector2(horInput, 0).normalized * speed * Time.deltaTime;
            player.physics.Move(movement);

            if (Input.GetKeyDown("Space") && player.physics.checkPos.y == -1) player.physics.AddForce(new Vector2(0, -8));
            if (Input.GetMouseButtonDown(0)) Debug.Log("left clicked");

            player.physics.Update();

            if (player.physics.collObject != null && player.physics.collObject.name == "flag") End();
        }

        void End()
        {
            new VarObject(new Vector2(player.position.x, player.position.y + 5), player.scale).AddProp<Text>("You Won !!!");
        }

    }
}

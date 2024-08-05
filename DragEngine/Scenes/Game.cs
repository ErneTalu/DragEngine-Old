using System;
using System.Drawing;

namespace DragEngine
{
    public class Game : DragEngine
    {
        public Game() : base(new Vector2(500, 500), "Game") {  }

        Animator anim = new Animator();
        Sprite player, enemy, follower;
        int speed = 6;

        public override void Awake()
        {
            anim.AddAnimation("idle");
            anim.AddAnimation("run");
            anim.AddAnimation("jump");
            anim.AddAnimation("idlef");
            anim.AddAnimation("runf");

            anim.animations["idle"].AddTileFrames(new Bitmap(@"C:\Users\erent\Desktop\Free\Main Characters\Ninja Frog\Idle (32x32).png"), 32, 32);
            anim.animations["run"].AddTileFrames(new Bitmap(@"C:\Users\erent\Desktop\Free\Main Characters\Ninja Frog\Run (32x32).png"),32,32);
            anim.animations["jump"].AddFrame(new Bitmap(@"C:\Users\erent\Desktop\Free\Main Characters\Ninja Frog\Jump (32x32).png"));

            anim.animations["idlef"].AddTileFrames(new Bitmap(@"C:\Users\erent\Desktop\Free\Main Characters\Mask Dude\Idle (32x32).png"), 32, 32);
            anim.animations["runf"].AddTileFrames(new Bitmap(@"C:\Users\erent\Desktop\Free\Main Characters\Mask Dude\Run (32x32).png"), 32, 32);
        }

        public override void Start()
        {
            string[,] map = new string[10, 30]
            {
                {"w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w"},
                {"w",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".","a",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".","a",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".","a",".",".",".",".","f",".",".","p",".","",".",".","e",".",".",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".","a",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".","a",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
                {"w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w","w"}
            };
            Room.AddRoom(map);

            Bitmap tileImage = new Bitmap(@"C:\Users\erent\Desktop\Free\Background\Gray.png"); 
            Tilemap tilemap = new Tilemap(tileImage, 32, 32, 75, 30);

            foreach (Vector2 v in Room.GetTiles("a"))
            {
                
            }
            foreach (Vector2 v in Room.GetTiles("p"))
            {
               
            }
            foreach (Vector2 v in Room.GetTiles("f"))
            {
                
            }
            foreach (Vector2 v in Room.GetTiles("e"))
            {
                
            }
        }

        public override void Update()
        {
            float horInput = Input.GetAxis("Horizontal");
            float verInput = Input.GetAxis("Vertical");
            float jumpInput = Input.GetAxis("Jump");

            Vector2 movement = new Vector2(horInput, verInput).normalized * speed * Time.deltaTime;
           

        }

    }
}

using FarseerPhysics.Common;
using System;
using System.Drawing;

namespace DragEngine
{
    public class PingPong : DragEngine
    {
        public PingPong() : base(new Vector2(550, 575), "Game") { }

        Sprite player1,player2, ball;
        float speed = 5;

        public override void Awake()
        {

        }

        public override void Start()
        {
            string[,] map = new string[11, 11]
            {
                {"w","w","w","w","w","w","w","w","w","w","w"},
                {"w",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".",".",".",".",".",".",".","w"},
                {"w",".","1",".",".","b",".",".","2",".","w"},
                {"w",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".",".",".",".",".",".",".","w"},
                {"w",".",".",".",".",".",".",".",".",".","w"},
                {"w","w","w","w","w","w","w","w","w","w","w"}
            };
            Room.AddRoom(map);

            foreach (Vector2 v in Room.GetTiles("w"))
            {
                
            }
            foreach (Vector2 v in Room.GetTiles("1"))
            {
                
            }
            foreach (Vector2 v in Room.GetTiles("2"))
            {
                
            }
            foreach (Vector2 v in Room.GetTiles("b"))
            {
                
            }
        }

        public override void Update()
        {
            float verInput = Input.GetAxis("Vertical");
            Vector2 movement1 = new Vector2(0, verInput).normalized * speed * Time.deltaTime;

            float verInput2 = Input.GetAxis("Vertical2");
            Vector2 movement2 = new Vector2(0, verInput2).normalized * speed * Time.deltaTime;

        }
    }
}

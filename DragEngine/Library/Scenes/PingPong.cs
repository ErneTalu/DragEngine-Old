using System;
using System.Drawing;
using System.Net.Sockets;

namespace DragEngine
{
    public class PingPong : DragEngine
    {
        public PingPong() : base(new Vector2(550, 575), "Game") { }

        VarObject player1,player2, ball;
        Text score1, score2;
        float speed = 5;

        public override void Start()
        {
            SetMap();

            ball.physics.AddForce(new Vector2(Random.Select(-2,2), Random.Select(-1,1)));
            ball.physics.OnCollEnter = () => { ball.physics.IncreaseVelocity(new Vector2(0.25f,0)); };

            score1 = new VarObject(new Vector2(player1.position.x, player1.position.y + 250), new Vector2(50,50)).AddProp<Text>("0", 24, Color.Red);
            score2 = new VarObject(new Vector2(player2.position.x, player2.position.y + 250), new Vector2(50,50)).AddProp<Text>("0", 24, Color.Blue);
        }

        public void SetMap()
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
            Map.AddMap(map);

            foreach (Vector2 v in Map.GetTiles("w"))
            {
                new VarObject(v, new Vector2(50, 50)).AddProp<Sprite>(Color.Black).AddProp<Collider>();
            }
            foreach (Vector2 v in Map.GetTiles("1"))
            {
                player1 = new VarObject(v, new Vector2(35, 100)).AddProp<Sprite>(Color.Red).AddProp<Physics>().AddProp<Collider>().varObject;
            }
            foreach (Vector2 v in Map.GetTiles("2"))
            {
                player2 = new VarObject(v, new Vector2(35, 100)).AddProp<Sprite>(Color.Blue).AddProp<Physics>().AddProp<Collider>().varObject;
            }
            foreach (Vector2 v in Map.GetTiles("b"))
            {
                ball = new VarObject(v, new Vector2(25, 25)).AddProp<Sprite>(Color.Gray, SpriteType.Circle).AddProp<Physics>().AddProp<Collider>().varObject;
                ball.physics.physicMat = new PhysicMat(0, 1);
            }
        }

        public override void Update()
        {
            ball.physics.Update();

            float verInput = Input.GetAxis("Vertical");
            Vector2 movement1 = new Vector2(0, verInput).normalized * speed * Time.deltaTime;
            player1.physics.Move(movement1);

            float verInput2 = Input.GetAxis("Vertical2");
            Vector2 movement2 = new Vector2(0, verInput2).normalized * speed * Time.deltaTime;
            player2.physics.Move(movement2);

            if (ball.position.x < player1.position.x) ResetBall(ref score2);
            if (ball.position.x > player2.position.x) ResetBall(ref score1);
        }

        public void ResetBall(ref Text score)
        {
            score.text = (int.Parse(score.text) + 1).ToString();
            ball.position = new Vector2(250,250);
            ball.physics.AddForce(new Vector2(Random.Select(-2,2), Random.Select(-1 ,1)));
        }
    }
}

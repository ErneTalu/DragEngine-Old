
using System.Drawing;

namespace DragEngine
{
    public class Test : DragEngine
    {
        public Test() : base(new Vector2(500, 500), "Test") {  }

        VarObject varObject, varObject2;
        Physics physics;
        Vector2 movement;

        public override void Awake()
        {
            
        }

        public override void Start()
        {
            SetRoom();

            varObject = new VarObject(new Vector2(0,20), new Vector2(50,50));
            varObject.AddProp<Sprite>(Color.Red);
            physics = varObject.AddProp<Physics>(true);

            varObject2 = new VarObject(new Vector2(100,0), new Vector2(50, 50));
            varObject2.AddProp<Sprite>(Color.Blue);
            varObject2.AddProp<Physics>(true);

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
            Room.AddRoom(map);

            foreach (Vector2 v in Room.GetTiles("a"))
            {
                
            }
        }

        public override void Update()
        {
            float horInput = Input.GetAxis("Horizontal");
            float verInput = Input.GetAxis("Vertical");
            movement = new Vector2(horInput, verInput);

            physics.Move(movement.normalized * Time.deltaTime * 5);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console_snale
{    
    internal class Game
    {
        public static int width = 80;
        public static int height = 20;
        public static SnakeLorry snakeLorry = new SnakeLorry();
        
        public static int xDirection = 1;//moving left to right-- 0 = not moving in x direction, 1 = left to right, -1= right to left
        public static int yDirection = 0;//moving down to up-- 0 = not moving in y direction, 1 = up to down, -1= down to up  
        public void Start()
        {
            Initialize();
            while (true)
            {
                Console.Clear();
                if (Console.KeyAvailable == true)
                {
                    SnakeDirection(Console.ReadKey(true));
                }
                MoveSnake();
                DrawSnake();
                Thread.Sleep(50);
            }
        }
        public void Initialize()
        {
            if(snakeLorry.Head==null) snakeLorry.Head= new CoOrdinate();
            if(snakeLorry.Tail==null) snakeLorry.Tail= new List<CoOrdinate>();
            snakeLorry.Head.XLocation = width / 2;
            snakeLorry.Head.YLocation = height / 2;
            snakeLorry.Length = 3;
            for (int i = 1; i < snakeLorry.Length; i++)
            {
                snakeLorry.Tail.Add(new CoOrdinate() { XLocation = (width / 2) - i, YLocation = height / 2 });
            }
        }
        public void SnakeDirection(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    {
                        if (xDirection != 1)
                        {
                            xDirection = -1;
                            yDirection = 0;
                        }
                    }
                    break;

                case ConsoleKey.RightArrow:
                    {
                        if(xDirection!=-1)
                        {
                            xDirection = 1;
                            yDirection = 0;
                        }
                    }
                    break;
                case ConsoleKey.UpArrow:
                    {
                        if(yDirection != 1)
                        {
                            yDirection = -1;
                            xDirection = 0;
                        }
                    }
                    break;
                case ConsoleKey.DownArrow:
                    {
                        if (yDirection != -1) 
                        { 
                            yDirection = 1;
                            xDirection = 0;
                        }
                    }
                    break;
            }
        }
        public void MoveSnake()
        {
            MoveTails();
            if (snakeLorry.Head.XLocation == width && xDirection==1)
            {
                snakeLorry.Head.XLocation = 0;
                snakeLorry.Head.XLocation += xDirection;
            }
            else if(snakeLorry.Head.XLocation==0 && xDirection == -1)
            {
                snakeLorry.Head.XLocation = width;
                snakeLorry.Head.XLocation += xDirection;
            }
            else
            {
                snakeLorry.Head.XLocation += xDirection;
            }
            if(snakeLorry.Head.YLocation == height && yDirection==1)
            {
                snakeLorry.Head.YLocation = 0;
                snakeLorry.Head.YLocation += yDirection;
            }
            else if(snakeLorry.Head.YLocation==0 && yDirection == -1)
            {                
                snakeLorry.Head.YLocation = height;
                snakeLorry.Head.YLocation += yDirection;
            }
            else
            {
                snakeLorry.Head.YLocation += yDirection;
            }
        }
        private void MoveTails()
        {
            for (int i = snakeLorry.Tail.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    snakeLorry.Tail[i].XLocation = snakeLorry.Head.XLocation;
                    snakeLorry.Tail[i].YLocation = snakeLorry.Head.YLocation;
                }
                else
                {
                    snakeLorry.Tail[i].XLocation = snakeLorry.Tail[i-1].XLocation;
                    snakeLorry.Tail[i].YLocation = snakeLorry.Tail[i-1].YLocation;
                }
            }
                
            
        }
        public void DrawSnake()
        {            
            for(int i = 0;i<snakeLorry.Length;i++)
            {
                string snakeBody=string.Empty;
                if(i==0)
                {
                    snakeBody = "O";
                    Console.SetCursorPosition(snakeLorry.Head.XLocation, snakeLorry.Head.YLocation);
                }
                else
                {
                    snakeBody = "=";
                    Console.SetCursorPosition(snakeLorry.Tail[i-1].XLocation, snakeLorry.Tail[i-1].YLocation);
                }
               
                
                Console.WriteLine(snakeBody);
            }
           
        }
    }
}

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
        public static SnakeLorry snakeLorry;
        public static int initialSnakeLength = 3;
        public static int xDirection = 1;//moving left to right-- 0 = not moving in x direction, 1 = left to right, -1= right to left
        public static int yDirection = 0;//moving down to up-- 0 = not moving in y direction, 1 = up to down, -1= down to up  
        public static double movementSpeed = 0.2;
        public static double movement = 0;
        public static int score = 0;
        public static int HiScore = 0;
        public static bool gameOver=false;
        public static bool gamePaused=false;
        public static Random Random = new();
        public static CoOrdinate target = new();
        public int Start()
        {
            ReadGameFile gameFile = new();
            HiScore = gameFile.ReadSettingFile().HighScore;
            gameOver = false;
            score = 0;
            movementSpeed = 0.2;
            movement = 0;
            initialSnakeLength = 3;
            snakeLorry = new();
            Initialize();
            while (!gameOver)
            {
                Console.Clear();
                CheckTailCollision();
                CheckCollisionWithTarget();
                if (Console.KeyAvailable == true)
                {
                    SnakeDirection(Console.ReadKey(true));
                }
                while(gamePaused) { 
                    Console.Clear();
                    Console.SetCursorPosition(width/2, height/2);
                    Console.WriteLine("Paused!!");
                    Console.SetCursorPosition(width / 2, (height / 2)+2);
                    Console.WriteLine("Press Enter to Continue.");
                    if( Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        gamePaused= false;
                    };
                    Thread.Sleep(100);
                }
                MoveSnake();
                Console.ForegroundColor = System.ConsoleColor.DarkGreen;
                DrawSnake();
                
                Console.ForegroundColor = System.ConsoleColor.Yellow;
                DrawTarget();
                Console.ForegroundColor = System.ConsoleColor.Blue;
                DrawScore();
                Thread.Sleep(50);
            }
            Console.Clear();
            
            Console.ForegroundColor = System.ConsoleColor.Red;
            Console.SetCursorPosition(width/2, height/2);
            Console.WriteLine("Game Over");
            Console.SetCursorPosition(width / 2, (height / 2)+2);
            Console.ForegroundColor = System.ConsoleColor.Green;
            Console.WriteLine("Press R to restart.");
            Console.ForegroundColor = System.ConsoleColor.Cyan;
            Console.SetCursorPosition(width / 2, (height / 2)+4);
            Console.WriteLine("Press Esc to exit!");
            return score;
        }
        public void Initialize()
        {
            if(snakeLorry.Head==null) snakeLorry.Head= new CoOrdinate();
            if(snakeLorry.Tail==null) snakeLorry.Tail= new List<CoOrdinate>();
            snakeLorry.Head.XLocation = width / 2;
            snakeLorry.Head.YLocation = height / 2;
            snakeLorry.Length = initialSnakeLength;
            for (int i = 1; i < snakeLorry.Length; i++)
            {
                snakeLorry.Tail.Add(new CoOrdinate() { XLocation = (width / 2) - i, YLocation = height / 2 });
            }
            GenerateTarget();
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
                case ConsoleKey.Escape:
                    {
                        gamePaused = true;
                    }
                    break;
            }
        }
        public void MoveSnake()
        {
            if (movement > 2)
            {

                MoveTails();
                if (snakeLorry.Head.XLocation == width && xDirection == 1)
                {
                    snakeLorry.Head.XLocation = 0;
                    snakeLorry.Head.XLocation += xDirection;
                }
                else if (snakeLorry.Head.XLocation == 0 && xDirection == -1)
                {
                    snakeLorry.Head.XLocation = width;
                    snakeLorry.Head.XLocation += xDirection;
                }
                else
                {
                    snakeLorry.Head.XLocation += xDirection;
                }
                if (snakeLorry.Head.YLocation == height && yDirection == 1)
                {
                    snakeLorry.Head.YLocation = 0;
                    snakeLorry.Head.YLocation = yDirection;
                }
                else if (snakeLorry.Head.YLocation == 0 && yDirection == -1)
                {
                    snakeLorry.Head.YLocation = height;
                    snakeLorry.Head.YLocation += yDirection;
                }
                else
                {
                    snakeLorry.Head.YLocation += yDirection;
                }

                movement = 0;
            }
            movement += movement + movementSpeed;
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
        public void DrawTarget()
        {
            Console.SetCursorPosition(target.XLocation, target.YLocation);
            Console.WriteLine("#");
        }
        public void DrawScore()
        {
            string scoreRecord = "Current Score: " + score;
            Console.SetCursorPosition(0, height + 2);
            Console.WriteLine(scoreRecord);
            Console.SetCursorPosition(width - (scoreRecord.Length + 4), height + 2);
            Console.WriteLine("High Score: " + HiScore);
        }
        public void CheckTailCollision()
        {
            foreach(var tailCordinate  in snakeLorry.Tail)
            {
                if(snakeLorry.Head.XLocation==tailCordinate.XLocation && snakeLorry.Head.YLocation==tailCordinate.YLocation)
                {
                    gameOver = true; 
                    break;
                }
            }
        }
        public void GenerateTarget()
        {
            int xLoc = Random.Next(0, width);
            int yLoc = Random.Next(0, height);
            target.XLocation = xLoc;
            target.YLocation = yLoc;            
        }
        
        public void CheckCollisionWithTarget()
        {
            if(snakeLorry.Head.XLocation==target.XLocation && snakeLorry.Head.YLocation== target.YLocation)
            {
                score += 10;
                GenerateTarget();
                snakeLorry.Length += 1;
                snakeLorry.Tail.Add(new CoOrdinate()
                {
                    XLocation = snakeLorry.Tail[snakeLorry.Tail.Count-1].XLocation,
                    YLocation = snakeLorry.Tail[snakeLorry.Tail.Count-1].YLocation
                });
                if (score % 100 == 0)
                {
                    movementSpeed += 0.1;
                }
            }
        }

    }
}

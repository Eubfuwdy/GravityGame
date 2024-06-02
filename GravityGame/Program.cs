using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading;

class GravityGame
{
    static int screenWidth = 30;
    static int screenHeight = 20;
    static char[,] screenBuffer = new char[screenWidth, screenHeight];  
    static int playerX = screenWidth / 2;
    static int playerY = 0;
    static bool isRunning = true;
    static int gravity = 1;
    static int speed = 1;
    static int fallSpeed = 0;

    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        Thread inputThread = new Thread(Input);
        inputThread.Start();

        while (isRunning)
        {
            Update();
            Draw();
            Thread.Sleep(100);
        }

        inputThread .Join();
    }

    static void Input()
    {
        while(isRunning) { }
        {
            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    if (playerX > 0) playerX -= speed;
                    break;
                case ConsoleKey.RightArrow:
                    if (playerY < screenWidth - 1) playerY += speed;
                    break;
                case ConsoleKey.Escape:
                    isRunning = false;
                    break;
            }
        }
    }

    static void Update()
    {
        fallSpeed += gravity;
        playerY += fallSpeed / 10;

        if (playerY >= screenHeight)
        {
            playerY = screenHeight - 1;
            fallSpeed = 0;
        }
    }

    static void Draw()
    {
        Console.Clear();
        Array.Clear(screenBuffer, 0, screenBuffer.Length);
        screenBuffer[playerY, playerX] = '0';

        for (int y = 0; y < screenHeight; y++)
        {
            for (int x = 0; x < screenWidth; x++)
            {
                Console.Write(screenBuffer[x, y] == '0' ? '0' : ' ');
            }
            Console.WriteLine();
        }
    }
}


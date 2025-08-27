using System;
using System.Threading;

namespace TetrisMultiplayer.Game
{
    public class MiniGravityTetris
    {
        private const int Size = 25;
        private int[,] grid = new int[Size, Size];
        private Tetromino? currentPiece;
        private Tetromino? nextPiece;
        private Random rng = new Random();
        private bool running = true;
        private bool paused = false;

        public void Run()
        {
            while (true)
            {
                Reset();
                running = true;
                paused = false;
                while (running)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.P) paused = !paused;
                        if (key.Key == ConsoleKey.Q) return;
                    }
                    if (!paused)
                    {
                        Step();
                        Draw();
                        Thread.Sleep(100);
                    }
                    else
                    {
                        Console.SetCursorPosition(0, Size + 4);
                        Console.WriteLine("[P] Pause  [Q] Quit Minigame");
                        Thread.Sleep(100);
                    }
                }
            }
        }

        private void Reset()
        {
            Array.Clear(grid, 0, grid.Length);
            // Always generate both pieces on reset
            currentPiece = GenerateTetrominoAtEdge();
            nextPiece = GenerateTetrominoAtEdge();
        }

        private Tetromino GenerateTetrominoAtEdge()
        {
            var type = (TetrominoType)rng.Next(0, 7);
            var t = new Tetromino(type);
            int edge = rng.Next(4);
            switch (edge)
            {
                case 0: t.X = 0; t.Y = rng.Next(Size - 3); break; // left
                case 1: t.X = Size - 4; t.Y = rng.Next(Size - 3); break; // right
                case 2: t.X = rng.Next(Size - 3); t.Y = 0; break; // top
                case 3: t.X = rng.Next(Size - 3); t.Y = Size - 4; break; // bottom
            }
            t.Rotation = 0;
            return t;
        }

        private void Step()
        {
            if (currentPiece == null) return;
            int mx = Size / 2 - 2, my = Size / 2 - 2;
            int dx = Math.Sign(mx - currentPiece.X);
            int dy = Math.Sign(my - currentPiece.Y);
            int nx = currentPiece.X + dx;
            int ny = currentPiece.Y + dy;
            if (IsValid(currentPiece, nx, ny, currentPiece.Rotation))
            {
                currentPiece.X = nx;
                currentPiece.Y = ny;
            }
            else
            {
                // Place piece
                foreach (var (x, y) in currentPiece.Blocks())
                {
                    if (x >= 0 && x < Size && y >= 0 && y < Size)
                        grid[y, x] = (int)currentPiece.Type + 1;
                }
                // Check for game over (center blocked)
                bool gameOver = false;
                for (int y = Size / 2 - 2; y < Size / 2 + 2; y++)
                    for (int x = Size / 2 - 2; x < Size / 2 + 2; x++)
                        if (grid[y, x] != 0) gameOver = true;
                if (gameOver)
                {
                    Console.SetCursorPosition(0, Size + 4);
                    Console.WriteLine("Game Over! Neustart...");
                    Thread.Sleep(1000);
                    running = false;
                }
                else
                {
                    // Always generate a new nextPiece and move it to currentPiece
                    currentPiece = nextPiece;
                    nextPiece = GenerateTetrominoAtEdge();
                }
            }
        }

        private bool IsValid(Tetromino t, int x, int y, int rot)
        {
            foreach (var (bx, by) in t.Blocks(x, y, rot))
            {
                if (bx < 0 || bx >= Size || by < 0 || by >= Size) return false;
                if (grid[by, bx] != 0) return false;
            }
            return true;
        }

        private void Draw()
        {
            Console.SetCursorPosition(0, 0);
            // Draw next piece preview (4x4)
            int previewLeft = Size * 2 + 4;
            int previewTop = 1;
            Console.SetCursorPosition(previewLeft, previewTop);
            Console.Write("Next:");
            int previewSize = 4;
            // Clear preview area first
            for (int py = 0; py < previewSize; py++)
            {
                Console.SetCursorPosition(previewLeft, previewTop + 1 + py);
                Console.Write(new string(' ', previewSize * 2));
            }
            int[,] preview = new int[previewSize, previewSize];
            if (nextPiece != null)
            {
                foreach (var (x, y) in nextPiece.Blocks(1, 1, 0))
                {
                    if (x >= 0 && x < previewSize && y >= 0 && y < previewSize)
                        preview[y, x] = (int)nextPiece.Type + 1;
                }
                for (int py = 0; py < previewSize; py++)
                {
                    Console.SetCursorPosition(previewLeft, previewTop + 1 + py);
                    for (int px = 0; px < previewSize; px++)
                        Console.Write(preview[py, px] == 0 ? "  " : "[]");
                }
            }
            // Draw field
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    bool isPiece = false;
                    if (currentPiece != null)
                    {
                        foreach (var (px, py) in currentPiece.Blocks())
                        {
                            if (px == x && py == y) { isPiece = true; break; }
                        }
                    }
                    if (isPiece)
                        Console.Write("[]");
                    else if (grid[y, x] != 0)
                        Console.Write("##");
                    else
                        Console.Write("  ");
                }
                Console.WriteLine();
            }
            Console.SetCursorPosition(0, Size + 2);
            Console.WriteLine("[P] Pause  [Q] Quit Minigame");
        }
    }
}

using System;
using TetrisMultiplayer.Tests;

namespace TetrisMultiplayer
{
    partial class Program
    {
        static void RunTests()
        {
            Console.WriteLine("=== TETRIS MULTIPLAYER - SERIALIZATION BUGFIX TESTS ===");
            Console.WriteLine();
            
            // Run serialization tests
            SerializationBugfixTests.RunAllSerializationTests();
            
            Console.WriteLine();
            Console.WriteLine("=== TEST SUMMARY ===");
            Console.WriteLine("If all tests show ? PASSED, the serialization bug has been fixed!");
            Console.WriteLine("The game should no longer crash when the host fills up their Tetris field.");
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
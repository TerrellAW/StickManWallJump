using System;
using StickManWallJump;

public static class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        // Parse command line arguments for debug mode
        bool debugMode = false;

        foreach (string arg in args)
        {
            if (arg.ToLower() == "--debug" || arg.ToLower() == "-d")
            {
                debugMode = true;
                break;
            }
        }

        // Create level manager
        LevelManager levelManager = new LevelManager();

        // Pass debug flag to Engine constructor
        using var game = new Engine(levelManager, "Level1", debugMode);
        game.Run();
    }
}
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

        levelManager.CreateLevel("Level1", "Textures/platform", "Textures/wall", 60f);

        string currentLevelName = "Level1"; // TODO: Implement level selection logic

        // Pass debug flag to Engine constructor
        using var game = new Engine(levelManager, currentLevelName, debugMode); // TODO: Use index as key instead of name
        game.Run();
    }
}

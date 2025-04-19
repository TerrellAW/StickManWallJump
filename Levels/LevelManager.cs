using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StickManWallJump;

public class LevelManager
{
    private Dictionary<string, Level> _levels = new Dictionary<string, Level>();
    private Level _currentLevel;

    public void CreateLevel1()
    {
        var level = new Level("Level 1", "platform", "wall");

        // Add platforms
        level.AddPlatform(100, 200);
        level.AddPlatform(300, 400);

        // Add walls
        level.AddWall(-50, 0);
        // Gonna need to pass screen width and height to level manager for object placement
        // Gonna need to add facing direction to wall objects

        _levels.Add(level.Name, level);
    }
}
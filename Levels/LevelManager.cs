using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StickManWallJump;

public class LevelManager
{
    private Dictionary<string, Level> _levels = new Dictionary<string, Level>();
    private Level _currentLevel;

    public Level GetLevel(string levelName)
    {
        if (_levels.ContainsKey(levelName))
        {
            return _levels[levelName];
        }
        else
        {
            throw new KeyNotFoundException($"Level '{levelName}' not found.");
        }
    }

    public void CreateLevel1(string levelName, string platformTexture, string wallTexture, int screenWidth)
    {
        _currentLevel = new Level(levelName, platformTexture, wallTexture, Color.White, Color.Black);

        // Add platforms TODO: Make this more dynamic, props or parameters or objects
        _currentLevel.AddPlatform(100, 200);
        _currentLevel.AddPlatform(300, 400);

        // Add walls TODO: Make this more dynamic, props or parameters or objects
        _currentLevel.AddWall(-50, 0, false); // Left wall
        _currentLevel.AddWall(screenWidth - 50, 0, true); // Right wall
        // Gonna need to pass screen width and height to level manager for object placement
        // Gonna need to add facing direction to wall objects

        _levels.Add(_currentLevel.Name, _currentLevel);
    }
}
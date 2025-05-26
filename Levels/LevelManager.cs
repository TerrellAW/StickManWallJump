using System.Collections.Generic;
using Microsoft.Xna.Framework;

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

    public void CreateLevel1(string levelName, string platformTexture, string wallTexture, float playerSpeed)
    {
        _currentLevel = new Level(levelName, platformTexture, wallTexture, Color.White, Color.Black);

        // Add platforms TODO: Make this more dynamic, props or parameters or objects
        _currentLevel.AddPlatform(100, 200, true);
        _currentLevel.AddPlatform(180, 200, true);
        _currentLevel.AddPlatform(300, 150, true);
        _currentLevel.AddPlatform(400, 100, true);
        _currentLevel.AddPlatform(300, 400, true);
        _currentLevel.AddPlatform(380, 400, true);
        _currentLevel.AddPlatform(500, 400, true);
        _currentLevel.AddPlatform(580, 400, true);
        _currentLevel.AddPlatform(650, 300, true);

        // Add walls TODO: Make this more dynamic, props or parameters or objects
        _currentLevel.AddWall(-50, 10, true, false); // Left wall
        _currentLevel.AddWall(750, 10, true, true); // Right wall
        // Gonna need to pass screen width and height to level manager for object placement
        // Gonna need to add facing direction to wall objects

        // Set player position
        _currentLevel.Player = new Player(new Vector2(140, 50), playerSpeed, 0f, 300f, 0f, 0f, true);
        _currentLevel.PlayerSpeed = playerSpeed;

        // Set level properties
        _currentLevel.AirFriction = 0.999f; // Low air friction

        _levels.Add(_currentLevel.Name, _currentLevel);
    }
}

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace StickManWallJump;

internal class Level
{
    /// <summary>
    /// 
    /// Levels will have their own variables, such as ground/platform friction, so some levels are slippery and some aren't.
    /// Levels will also have their own textures, such as the background, platforms, and walls.
    /// Backgrounds will be audio visualizers that react to that level's music. Levels will have different music.
    /// 
    /// Lists that contain each level's objects, such as platforms and walls, will be stored in the level class.
    /// These lists will be used to check for collisions with the player in the engine class.
    /// 
    /// Level object instances will be defined in the LevelManager class, which will pass them to Engine in Program.cs.
    ///
    /// </summary>

    // Properties
    internal string Name { get; set; }
    private string platformTexturePath { get; set; }
    private string wallTexturePath { get; set; }
    internal List<Platform> Platforms { get; set; } = new List<Platform>();
    internal List<Wall> Walls { get; set; } = new List<Wall>();
    internal Player Player { get; set; }
    internal int ScreenWidth { get; set; }
    internal Color FilterColor { get; set; } // Color filter for the level
    internal Color BackgroundColor { get; set; } // Background color for the level
    internal float PlayerSpeed { get; set; } = 60f; // Default player speed

    // Physics properties
    internal float Gravity = 6.0f; // Default gravity value
    internal float AirFriction = 0.98f; // Default air friction value

    // Constructor
    public Level(string name, string platformTexturePath, string wallTexturePath, Color filterColor, Color backgroundColor)
    {
        Name = name;
        this.platformTexturePath = platformTexturePath;
        this.wallTexturePath = wallTexturePath;
        FilterColor = filterColor;
        BackgroundColor = backgroundColor;
    }

    //Methods

    // Textures
    internal string SetPlatformTexture()
    {
        return platformTexturePath;
    }
    internal string SetWallTexture()
    {
        return wallTexturePath;
    }

    // Add objects to the level
    internal void AddPlatform(float x, float y, bool isSolid)
    {
        int id = Platforms.Count + 1; // Unique ID for each platform
        Platforms.Add(new Platform(id, x, y, isSolid));
    }
    internal void AddWall(float x, float y, bool isSolid, bool facingRight)
    {
        int id = Walls.Count + 1; // Unique ID for each wall
        Walls.Add(new Wall(id, x, y, isSolid, facingRight));
    }
}

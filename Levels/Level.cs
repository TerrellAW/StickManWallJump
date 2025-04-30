using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace StickManWallJump;

public class Level
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
    public string Name { get; set; }
    public string platformTexturePath { get; set; }
    public string wallTexturePath { get; set; }
    public List<Platform> Platforms { get; set; } = new List<Platform>();
    public List<Wall> Walls { get; set; } = new List<Wall>();
    public Player Player { get; set; }
    public int ScreenWidth { get; set; }
    public Color FilterColor { get; set; } // Color filter for the level
    public Color BackgroundColor { get; set; } // Background color for the level

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
    public string SetPlatformTexture()
    {
        return platformTexturePath;
    }
    public string SetWallTexture()
    {
        return wallTexturePath;
    }

    // Add objects to the level
    public void AddPlatform(float x, float y, bool isSolid)
    {
        int id = Platforms.Count + 1; // Unique ID for each platform
        Platforms.Add(new Platform(id, x, y, isSolid));
    }
    public void AddWall(float x, float y, bool isSolid, bool facingRight)
    {
        int id = Walls.Count + 1; // Unique ID for each wall
        Walls.Add(new Wall(id, x, y, isSolid, facingRight));
    }
}

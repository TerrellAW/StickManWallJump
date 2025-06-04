using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace StickManWallJump.Levels.LevelLayout;

// TODO: Class which contains all info needed for createLevel function in LevelManager
// LevelLayout will contain array of level objects and their locations
internal class LevelLayout
{
    // TODO: key-value pair arrays with platform index and Vector2 position
    private Dictionary<int, Vector2> _platforms = new Dictionary<int, Vector2>();

    // TODO: key-value pair arrays with wall index and Vector2 position
    private Dictionary<int, Vector2> _walls = new Dictionary<int, Vector2>();

    // TODO: Methods to modify _platforms and _walls, full CRUD capabilities for level editor
}

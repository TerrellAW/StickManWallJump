using System.Collections.Generic;

using StickManWallJump.Levels.LevelLayout;

namespace StickManWallJump;

// TODO: Take index from LevelManager and use it to find the correct LevelLayout
// LevelLayout will contain array of game objects and their locations
internal class LevelLayoutManager
{
    // TODO: List of LevelLayouts and methods to access and utilize them
    private List<LevelLayout> _levelLayouts = new List<LevelLayout>();

    // TODO: Methods to modify _levelLayouts, full CRUD capabilities for level editor
}

using System.Collections.Generic;

namespace StickManWallJump;

public class LevelLayout
{
    private Level _currentLevel;

    // Function that accepts levelName string and _currentLevel from LevelManager
    // Will run all the necessary AddPlatform and AddWall commands to generate Level
    // LevelManager will just need this LevelLayout function and the necessary information to generate the correct layout for each level
}
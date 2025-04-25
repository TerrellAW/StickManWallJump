using System.Collections.Generic;

namespace StickManWallJump;

public class LevelLayoutManager // TODO: Make separate LevelLayout class
{
    private Level _currentLevel;

    // Store level layout data here, each level will be identified with levelName string
    // Function will be able to identify which level layout it needs and run the commands to create it

    // Function that accepts levelName string and _currentLevel from LevelManager
    // Will run all the necessary AddPlatform and AddWall commands to generate Level
    // LevelManager will just need this LevelLayout function and the necessary information to generate the correct layout for each level
}
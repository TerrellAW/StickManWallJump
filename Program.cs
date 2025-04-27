using Microsoft.Xna.Framework;
using StickManWallJump;

// Create level manager
LevelManager levelManager = new LevelManager();

// Add conditions for switching levels, menus, etc.
using var game = new Engine(levelManager, "Level1");
game.Run();

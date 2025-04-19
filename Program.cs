using StickManWallJump;

Level level1 = new Level("Level 1", "platform", "wall");

// Add conditions for switching levels, menus, etc.
using var game = new Engine(level1);
game.Run();

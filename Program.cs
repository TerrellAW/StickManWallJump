using StickManWallJump;

Level level1 = new Level("Level 1", "platform", "wall");

using var game = new Engine(level1); // Add parameters for switching levels, menus, etc.
game.Run();

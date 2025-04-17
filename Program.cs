using StickManWallJump;

Level level1 = new();

using var game = new StickManWallJump.Engine(level1); // Add parameters for switching levels, menus, etc.
game.Run();

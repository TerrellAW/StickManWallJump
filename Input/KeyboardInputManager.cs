using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace StickManWallJump.Input;

/// <summary>
/// Keyboard input manager for handling keyboard input.
/// </summary>
public class KeyboardInputManager : IInputManager
{
    // State Initialization
    private KeyboardState _currentKeyboardState;
    private KeyboardState _previousKeyboardState;

    // Fields
    private readonly Player _player;

    // Constructor
    public KeyboardInputManager(Player player, GraphicsDevice graphicsDevice = null)
    {
        // Initialize the player
        _player = player;
        // Initialize the keyboard state
        _currentKeyboardState = Keyboard.GetState();
        _previousKeyboardState = _currentKeyboardState;
    }

    // Movement actions
    public bool IsJump() => _currentKeyboardState.IsKeyDown(Keys.Space) && _previousKeyboardState.IsKeyUp(Keys.Space);
    public bool IsWallJump(Player player) => IsJump() && player.CanWallJump();
    public bool IsWallJump() => IsWallJump(_player); // Overloaded

    // Interface actions
    public bool IsPause() => _currentKeyboardState.IsKeyDown(Keys.Escape) && _previousKeyboardState.IsKeyUp(Keys.Escape);

    public void Update(GameTime gameTime)
    {
        // Update the keyboard state
        _previousKeyboardState = _currentKeyboardState;
        _currentKeyboardState = Keyboard.GetState();
    }
}

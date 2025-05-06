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

    // Constructor
    public KeyboardInputManager(GraphicsDevice graphicsDevice = null)
    {
        // Initialize the keyboard state
        _currentKeyboardState = Keyboard.GetState();
        _previousKeyboardState = _currentKeyboardState;
    }

    // Movement actions
    public bool IsJump() => _currentKeyboardState.IsKeyDown(Keys.Space) && _previousKeyboardState.IsKeyUp(Keys.Space);
    public bool IsWallJump() => IsJump();

    // Interface actions
    public bool IsPause() => _currentKeyboardState.IsKeyDown(Keys.Escape) && _previousKeyboardState.IsKeyUp(Keys.Escape);

    public void Update(GameTime gameTime)
    {
        // Update the keyboard state
        _previousKeyboardState = _currentKeyboardState;
        _currentKeyboardState = Keyboard.GetState();
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace StickManWallJump.Input;

/// <summary>
/// Gamepad input manager for handling gamepad input.
/// </summary>

public class GamepadInputManager : IInputManager
{
    // State Initialization
    private GamePadState _currentGamePadState;
    private GamePadState _previousGamePadState;

    // Fields
    private readonly Player _player;
    private readonly PlayerIndex _playerIndex;

    // Constructor
    public GamepadInputManager(Player player, GraphicsDevice graphicsDevice = null, PlayerIndex playerIndex = PlayerIndex.One)
    {
        // Initialize the player
        _player = player;

        // Initialize the gamepad state
        _playerIndex = playerIndex;
        _currentGamePadState = GamePad.GetState(_playerIndex);
        _previousGamePadState = _currentGamePadState;
    }

    // Movement actions
    public bool IsJump() => _currentGamePadState.Buttons.A == ButtonState.Pressed && _previousGamePadState.Buttons.A == ButtonState.Released;
    public bool IsWallJump(Player player) => IsJump() && player.IsOnWall && player.CanWallJump();
    public bool IsWallJump() => IsWallJump(_player); // Overloaded

    // Interface actions
    public bool IsPause() => _currentGamePadState.Buttons.Start == ButtonState.Pressed && _previousGamePadState.Buttons.Start == ButtonState.Released;

    public void Update(GameTime gameTime)
    {
        // Update the previous state to the current state
        _previousGamePadState = _currentGamePadState;
        // Get the current gamepad state
        _currentGamePadState = GamePad.GetState(_playerIndex);
    }
}
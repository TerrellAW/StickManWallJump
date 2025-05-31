using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace StickManWallJump.Input;

/// <summary>
/// Touch input manager for handling touch input on touch screen devices.
/// </summary>
internal class TouchInputManager : IInputManager
{
    private readonly GraphicsDevice _graphicsDevice;
    private TouchCollection _touchState;
    private bool _jump;
    private bool _pause;
    private Rectangle _pauseButtonBounds;

    // Fields
    private readonly Player _player;

    public TouchInputManager(Player player, GraphicsDevice graphicsDevice)
    {
        // Initialize the player
        _player = player;
        // Initialize the graphics device
        _graphicsDevice = graphicsDevice;
        TouchPanel.EnabledGestures = GestureType.Tap;

        // Define pause button in top-right corner
        int buttonSize = (int)(graphicsDevice.Viewport.Width * 0.15f); // 15% of screen width
        _pauseButtonBounds = new Rectangle(
            graphicsDevice.Viewport.Width - buttonSize, // Right edge
            0,                                          // Top edge
            buttonSize,                                 // Width
            buttonSize                                  // Height
        );
    }

    public bool IsJump() => _jump;
    public bool IsWallJump(Player player) => IsJump() && player.CanWallJump();
    public bool IsWallJump() => IsWallJump(_player); // Overloaded
    public bool IsPause() => _pause;

    public void Update(GameTime gameTime)
    {
        // Reset state
        _jump = false;
        _pause = false;

        // Update touch state
        _touchState = TouchPanel.GetState();

        // Process taps
        while (TouchPanel.IsGestureAvailable)
        {
            GestureSample gesture = TouchPanel.ReadGesture();

            if (gesture.GestureType == GestureType.Tap)
            {
                // Check if tap is within pause button bounds
                if (_pauseButtonBounds.Contains((int)gesture.Position.X, (int)gesture.Position.Y))
                {
                    _pause = true;
                }
                // Otherwise, treat as jump if within main screen area
                else if (gesture.Position.X < _graphicsDevice.Viewport.Width &&
                         gesture.Position.Y < _graphicsDevice.Viewport.Height)
                {
                    _jump = true;
                }
            }
        }

        // Handle direct touch input
        foreach (TouchLocation touch in _touchState)
        {
            // Check if touch is within pause button bounds
            if (_pauseButtonBounds.Contains((int)touch.Position.X, (int)touch.Position.Y))
            {
                _pause = true;
            }
            // Otherwise, treat as jump if within main screen area
            else if (touch.State == TouchLocationState.Pressed &&
                     touch.Position.X < _graphicsDevice.Viewport.Width &&
                     touch.Position.Y < _graphicsDevice.Viewport.Height)
            {
                _jump = true;
            }
        }
    }
}

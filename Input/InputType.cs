using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StickManWallJump.Input;

public enum InputType
{
    Keyboard,
    Gamepad,
    Touch
}

/// <summary>
/// Input Manager Factory.
/// 
/// GraphicsDevice is optional and can be null.
/// The GraphicsDevice is used to display control content on the screen.
/// The factory creates the appropriate input manager based on the input type.
/// </summary>
public static class InputManagerFactory
{
    public static IInputManager CreateInputManager(InputType inputType, Player player, GraphicsDevice graphicsDevice = null)
    {
        return inputType switch
        {
            InputType.Keyboard => new KeyboardInputManager(player, graphicsDevice),
            InputType.Gamepad => new GamepadInputManager(player, graphicsDevice),
            InputType.Touch => new TouchInputManager(player, graphicsDevice),
            _ => new KeyboardInputManager(player) // Default to keyboard input manager
        };
    }
}
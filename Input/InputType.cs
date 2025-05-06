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
    public static IInputManager CreateInputManager(InputType inputType, GraphicsDevice graphicsDevice = null)
    {
        return inputType switch
        {
            InputType.Keyboard => new KeyboardInputManager(graphicsDevice),
            InputType.Gamepad => new GamepadInputManager(graphicsDevice),
            InputType.Touch => new TouchInputManager(graphicsDevice),
            _ => new KeyboardInputManager() // Default to keyboard input manager
        };
    }
}
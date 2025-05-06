using Microsoft.Xna.Framework;

namespace StickManWallJump.Input;

/// <summary>
/// Interface for managing input methods.
/// </summary>
public interface IInputManager
{
    // Movement actions
    bool IsMoveLeft() => false; // Default value renders it optional
    bool IsMoveRight() => false;
    bool IsJump();
    bool IsWallJump();

    // Interface actions
    bool IsPause();

    void Update(GameTime gameTime);
}
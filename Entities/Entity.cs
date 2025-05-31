using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StickManWallJump;

public abstract class Entity
{
    // Texture
    public abstract Texture2D Texture { get; set; }

    // Entity constants
    public abstract float MaxSpeed { get; set; }

    // Entity variables
    internal float JumpForce { get; set; }
    internal float SpeedX { get; set; }
    internal float SpeedY { get; set; }
    internal float NextPositionX { get; set; }
    internal float NextPositionY { get; set; }
    internal Vector2 Position { get; set; }
    internal bool FacingRight { get; set; }

    // Collision bounds
    internal Rectangle CollisionBounds { get; set; }
    internal Rectangle CollisionBoundsNext { get; set; }
}
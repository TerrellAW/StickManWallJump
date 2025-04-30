using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StickManWallJump;

public class Player : Entity
{
    // Texture
    public override Texture2D Texture { get; set; }

    // Player constants
    public override float MaxJumpForce { get; set; } = 100f;
    public override float MinJumpForce { get; set; } = 20f;
    public override float MaxSpeed { get; set; } = 400f;

    // Constructor
    public Player(Vector2 Position, float SpeedX, float SpeedY, float JumpForce, float NextPositionX, float NextPositionY, bool facingRight)
    {
        this.Position = Position;
        this.SpeedX = SpeedX;
        this.SpeedY = SpeedY;
        this.JumpForce = JumpForce;
        this.NextPositionX = NextPositionX;
        this.NextPositionY = NextPositionY;
        this.FacingRight = facingRight;
        this.CollisionBounds = new Rectangle((int)Position.X, (int)Position.Y, 24, 49); // Player is 24x49 pixels
        this.CollisionBoundsNext = new Rectangle((int)NextPositionX, (int)NextPositionY, 24, 49); // Player is 24x49 pixels
    }
}
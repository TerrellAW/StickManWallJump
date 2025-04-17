using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StickManWallJump;

internal class Player : Entity
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
        this.facingRight = facingRight;
    }
}
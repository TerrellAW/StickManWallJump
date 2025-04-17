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

    // Player variables
    public override float JumpForce { get; set; }
    public override float SpeedX { get; set; }
    public override float SpeedY { get; set; }
    public override float NextPositionX { get; set; }
    public override float NextPositionY { get; set; }
    public override Vector2 Position { get; set; }
    public override bool facingRight { get; set; }

    // Constructor
    public Player()
    {
        Position = new Vector2(0, 0);
        SpeedX = 0f;
        SpeedY = 0f;
        JumpForce = 0f;
        NextPositionX = 0f;
        NextPositionY = 0f;
        facingRight = true; // Default facing right
    }
}
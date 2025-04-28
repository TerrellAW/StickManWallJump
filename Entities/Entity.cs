using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StickManWallJump;

public abstract class Entity
{
    // Texture
    public abstract Texture2D Texture { get; set; }

    // Entity constants
    public abstract float MaxJumpForce { get; set; }
    public abstract float MinJumpForce { get; set; }
    public abstract float MaxSpeed { get; set; }

    // Entity variables
    public float JumpForce { get; set; }
    public float SpeedX { get; set; }
    public float SpeedY { get; set; }
    public float NextPositionX { get; set; }
    public float NextPositionY { get; set; }
    public Vector2 Position { get; set; }
    public bool FacingRight { get; set; }

    // Collision bounds
    public Rectangle CollisionBounds { get; set; }
    public Rectangle CollisionBoundsNext { get; set; }
}
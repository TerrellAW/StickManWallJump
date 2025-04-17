using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StickManWallJump;

internal abstract class Entity
{
    // Texture
    public abstract Texture2D Texture { get; set; }

    // Player constants
    public abstract float MaxJumpForce { get; set; }
    public abstract float MinJumpForce { get; set; }
    public abstract float MaxSpeed { get; set; }

    // Player variables
    public abstract float JumpForce { get; set; }
    public abstract float SpeedX { get; set; }
    public abstract float SpeedY { get; set; }
    public abstract float NextPositionX { get; set; }
    public abstract float NextPositionY { get; set; }
    public abstract Vector2 Position { get; set; }
    public abstract bool facingRight { get; set; }
}
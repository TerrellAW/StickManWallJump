using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StickManWallJump;

internal class Wall : LevelObject
{
    // Texture
    public override Texture2D Texture { get; set; }

    // Constructor
    public Wall(int id, float x, float y, float width, float height) : base(id, x, y, width, height)
    {
        this.ID = id;
        this.X = x;
        this.Y = y;
        this.Width = width;
        this.Height = height;
    }
}
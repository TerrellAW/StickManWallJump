using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StickManWallJump;

public abstract class LevelObject
{
    // Texture
    public abstract Texture2D Texture { get; set; }

    // Properties
    public int ID { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }

    // Constructor
    public LevelObject(int id, float x, float y, float width, float height)
    {
        ID = id;
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }
}

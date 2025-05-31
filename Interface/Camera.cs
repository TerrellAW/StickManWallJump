using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StickManWallJump;

internal class Camera
{
    private Vector2 _position;

    public Camera(Vector2 position)
    {
        _position = position;
    }

    internal Vector2 Position
    {
        get { return _position; }
        set { _position = value; }
    }

    internal Matrix GetViewMatrix(GraphicsDevice graphicsDevice)
    {
        var translationMatrix = Matrix.CreateTranslation(new Vector3(-_position, 0));
        var viewportMatrix = Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
        return translationMatrix * viewportMatrix;
    }
}
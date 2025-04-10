using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StickManWallJump;

public class Engine : Game
{
    Texture2D platformTexture;
    Texture2D wallTexture;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Engine()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Level textures
        platformTexture = Content.Load<Texture2D>("platform");
        wallTexture = Content.Load<Texture2D>("wall");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // Draw platforms
        _spriteBatch.Begin();
        _spriteBatch.Draw(platformTexture, new Vector2(100, 300), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipVertically, 0f);
        _spriteBatch.Draw(platformTexture, new Vector2(300, 400), Color.White);
        _spriteBatch.End();

        // Draw walls
        _spriteBatch.Begin();
        _spriteBatch.Draw(wallTexture, new Vector2(50, 200), Color.White);
        _spriteBatch.Draw(wallTexture, new Vector2(400, 100), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StickManWallJump;

/// <summary>
/// TODO: 
/// - Store variables/objects for platforms and walls
/// - Dynamic collision detection system that can be easily extended to other objects
/// </summary>

public class Engine : Game
{
    // Textures
    Texture2D platformTexture;
    Texture2D wallTexture;
    Texture2D playerTexture;

    // Physics constants
    float gravity = 6.0f;
    float airFriction = 0.98f;

    // Player constants
    float playerMaxJumpForce = 100f;
    float playerMinJumpForce = 20f;
    float playerMaxSpeed = 400f;

    // Player variables
    float playerJumpForce;
    float playerSpeedX;
    float playerSpeedY;
    float NextPlayerPositionX;
    float NextPlayerPositionY;
    Vector2 playerPosition;

    // Game components
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Engine()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
    }

    protected override void Initialize()
    {
        // Initialize player
        playerPosition = new Vector2(340, 300);
        playerSpeedX = 0f;
        playerSpeedY = 0f;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Level textures
        platformTexture = Content.Load<Texture2D>("platform");
        wallTexture = Content.Load<Texture2D>("wall");

        // Player texture
        playerTexture = Content.Load<Texture2D>("stickman");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Gravity
        if (playerSpeedY < playerMaxSpeed)
            playerSpeedY += gravity;

        // Air friction
        playerSpeedX *= airFriction;

        // Framerate based speed
        float updatedPlayerSpeedX = playerSpeedX * (float)gameTime.ElapsedGameTime.TotalSeconds;
        float updatedPlayerSpeedY = playerSpeedY * (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Position Prediction
        NextPlayerPositionY = playerPosition.Y + updatedPlayerSpeedY;
        NextPlayerPositionX = playerPosition.X + updatedPlayerSpeedX;

        // Collision detection


        // Motion
        playerPosition.Y = NextPlayerPositionY;
        playerPosition.X = NextPlayerPositionX;

        base.Update(gameTime);
    }


    /// <summary>
    /// Draws the game.
    /// 
    /// This recursive method is called once per frame and is responsible for rendering the game objects to the screen.
    /// 
    /// Vector2 objects position drawn elements with 2 coordinates, x and y.
    /// The x coordinate is horizontal and starts at 0 on the left side of the screen.
    /// The y coordinate is vertical and starts at 0 at the top of the screen.
    /// </summary>
    /// <param name="gameTime"></param>
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black); // Background colour

        // Screen dimensions
        int screenWidth = GraphicsDevice.Viewport.Width;
        int screenHeight = GraphicsDevice.Viewport.Height;

        // Begin sprite batch
        _spriteBatch.Begin();

        // Draw platforms
        _spriteBatch.Draw(platformTexture, new Vector2(100, 200), Color.White);
        _spriteBatch.Draw(platformTexture, new Vector2(300, 400), Color.White);

        // Draw walls
        _spriteBatch.Draw(
            wallTexture,
            new Vector2(-50, 0),
            null,
            Color.White,
            0f,
            Vector2.Zero,
            1f,
            SpriteEffects.FlipHorizontally,
            0f
        );
        _spriteBatch.Draw(wallTexture, new Vector2(screenWidth - 50, 0), Color.White);

        // Draw player
        _spriteBatch.Draw(
            playerTexture,
            playerPosition,
            null,
            Color.White,
            0f,
            new Vector2(playerTexture.Width / 2, playerTexture.Height / 2),
            Vector2.One,
            SpriteEffects.None,
            0f
        );

        // End sprite batch
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}

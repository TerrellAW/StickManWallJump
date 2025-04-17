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

    // Physics constants TODO: Store in a class
    float gravity = 6.0f;
    float airFriction = 0.98f;

    // Player constants TODO: Store in a class
    float playerMaxJumpForce = 100f;
    float playerMinJumpForce = 20f;
    float playerMaxSpeed = 400f;

    // Player variables TODO: Store in a class
    float playerJumpForce;
    float playerSpeedX;
    float playerSpeedY;
    float NextPlayerPositionX;
    float NextPlayerPositionY;
    Vector2 playerPosition;
    bool facingRight = true;

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
        // Initialize player TODO: player position initialization from level class
        playerPosition = new Vector2(340, 300);
        playerSpeedX = 0f;
        playerSpeedY = 0f;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Level textures TODO: load from level class or platform/wall class
        platformTexture = Content.Load<Texture2D>("platform");
        wallTexture = Content.Load<Texture2D>("wall");

        // Player texture TODO: load from player class
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
        // foreach loop that uses lists of platforms and walls to check for collisions based on their coords, height and width
        // lists will come from level class, each level will have its own lists of platforms and walls

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
        GraphicsDevice.Clear(Color.Black); // Background colour TODO: color from level class

        // Screen dimensions
        int screenWidth = GraphicsDevice.Viewport.Width;
        int screenHeight = GraphicsDevice.Viewport.Height;

        // Begin sprite batch
        _spriteBatch.Begin();

        // Draw platforms TODO: Relocate to level class, some vars from platform class
        // Texture from platform class
        // Position from level class
        // Color from level class
        _spriteBatch.Draw(platformTexture, new Vector2(100, 200), Color.White);
        _spriteBatch.Draw(platformTexture, new Vector2(300, 400), Color.White);

        // Draw walls TODO: Relocate to level class, some vars from wall class
        // Texture from wall class
        // Position from level class
        // Color from level class
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

        // Draw player TODO: Relocate to player class, some vars from player class
        // Texture from player class
        // Position from player class
        // Color from player class
        _spriteBatch.Draw(
            playerTexture,
            playerPosition,
            null,
            Color.White,
            0f,
            new Vector2(playerTexture.Width / 2, playerTexture.Height / 2),
            Vector2.One,
            facingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally, // Flip sprite if not facing right
            0f
        );

        // End sprite batch
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}

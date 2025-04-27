using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StickManWallJump;

/// <summary>
/// TODO: 
/// - Store variables/objects for platforms and walls
/// - Dynamic collision detection system that can be easily extended to other objects
/// - Get height and width from sprites for collision system
/// </summary>

public class Engine : Game
{
    // Level object
    public Level level;
    private string _levelName;

    // Level textures
    Texture2D platformTexture;
    Texture2D wallTexture;

    // Physics constants
    float gravity = 6.0f;
    float airFriction = 0.98f;

    // Game components
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private LevelManager _levelManager;

    // Player object
    internal static Player Player = new Player(new Vector2(340, 300), 0f, 0f, 0f, 0f, 0f, true); // TODO: Move to level class

    public Engine(LevelManager levelManager, string levelName)
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;

        // Store for use in initializer
        this._levelManager = levelManager;
        this._levelName = levelName;
    }

    protected override void Initialize()
    {
        // Screen dimensions
        int screenWidth = GraphicsDevice.Viewport.Width;
        int screenHeight = GraphicsDevice.Viewport.Height;

        // Create level
        _levelManager.CreateLevel1(_levelName, "platform", "wall", screenWidth);

        // Get level
        level = _levelManager.GetLevel(_levelName);

        // Initialize player TODO: player position initialization from level class
        Player.Position = new Vector2(340, 300);
        Player.SpeedX = 0f;
        Player.SpeedY = 0f;

        base.Initialize();
    }

    // TODO: Load textures in level class and pass them to the engine here
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        platformTexture = Content.Load<Texture2D>(level.SetPlatformTexture());
        wallTexture = Content.Load<Texture2D>(level.SetWallTexture());

        Player.Texture = Content.Load<Texture2D>("stickman");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Gravity
        if (Player.SpeedY < Player.MaxSpeed)
            Player.SpeedY += gravity;

        // Air friction
        Player.SpeedX *= airFriction;

        // Framerate based speed
        float updatedPlayerSpeedX = Player.SpeedX * (float)gameTime.ElapsedGameTime.TotalSeconds;
        float updatedPlayerSpeedY = Player.SpeedY * (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Position Prediction
        Player.NextPositionY = Player.Position.Y + updatedPlayerSpeedY;
        Player.NextPositionX = Player.Position.X + updatedPlayerSpeedX;

        // Collision detection
        // foreach loop that uses lists of platforms and walls to check for collisions based on their coords, height and width
        // lists will come from level class, each level will have its own lists of platforms and walls

        // Motion
        Player.Position = new Vector2(Player.NextPositionX, Player.NextPositionY);

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

        // Begin sprite batch
        _spriteBatch.Begin();

        // Draw platforms TODO: Relocate to level class, some vars from platform class
        // Texture from platform class
        // Position from level class
        // Color from level class
        foreach (var platform in level.Platforms)
        {
            _spriteBatch.Draw(
                platformTexture,
                new Vector2(platform.X, platform.Y),
                Color.White
            );
        }

        // Draw walls TODO: Relocate to level class, some vars from wall class
        // Texture from wall class
        // Position from level class
        // Color from level class
        foreach (var wall in level.Walls)
        {
            _spriteBatch.Draw(
                wallTexture,
                new Vector2(wall.X, wall.Y),
                null,
                Color.White,
                0f,
                Vector2.Zero,
                1f,
                wall.FacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally, // Flip sprite if not facing right
                0f
            );
        }

        // Draw player TODO: Relocate to player class, some vars from player class
        // Texture from player class
        // Position from player class
        // Color from player class
        _spriteBatch.Draw(
            Player.Texture,
            Player.Position,
            null,
            Color.White,
            0f,
            new Vector2(Player.Texture.Width / 2, Player.Texture.Height / 2),
            Vector2.One,
            Player.FacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally, // Flip sprite if not facing right
            0f
        );

        // End sprite batch
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}

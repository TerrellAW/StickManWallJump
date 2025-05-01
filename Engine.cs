using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StickManWallJump;

public class Engine : Game
{
    // Level object
    public Level level;
    private string _levelName;

    // Level textures
    Texture2D platformTexture;
    Texture2D wallTexture;
    private Texture2D _debugTexture;

    // Colors
    Color levelFilterColor;
    Color levelBackgroundColor;

    // Physics constants
    float gravity;
    float airFriction;

    // Game components
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private LevelManager _levelManager;

    // Player object
    internal static Player Player = new Player(new Vector2(340, 300), 0f, 0f, 0f, 0f, 0f, true); // TODO: Move to level class

    // Debug mode
    private bool _debugMode;

    public Engine(LevelManager levelManager, string levelName, bool debugMode = false)
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;

        // Store for use in initializer
        this._levelManager = levelManager;
        this._levelName = levelName;
        this._debugMode = debugMode;
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

        // Get level colors
        levelFilterColor = level.FilterColor;
        levelBackgroundColor = level.BackgroundColor;

        // Set physics constants
        gravity = level.Gravity;
        airFriction = level.AirFriction;

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

        // Debug 1x1 white texture
        _debugTexture = new Texture2D(GraphicsDevice, 1, 1);
        _debugTexture.SetData(new[] { Color.White });
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

        // Player collision bounds update
        Player.CollisionBounds = new Rectangle(
            (int)Player.Position.X - (Player.Texture.Width / 2),
            (int)Player.Position.Y - (Player.Texture.Height / 2),
            Player.Texture.Width,
            Player.Texture.Height
        );

        // Player collision bounds next update
        Player.CollisionBoundsNext = new Rectangle(
            (int)Player.NextPositionX - (Player.Texture.Width / 2),
            (int)Player.NextPositionY - (Player.Texture.Height / 2),
            Player.Texture.Width,
            Player.Texture.Height
        );

        // Check for collisions
        foreach (var platform in level.Platforms) // Platform collisions
        {
            if (platform.isSolid && Player.CollisionBoundsNext.Intersects(platform.CollisionBounds))
            {
                // Calculate movement direction
                bool movingDown = Player.SpeedY > 0;
                bool movingUp = Player.SpeedY < 0;

                // For platforms, we're most concerned with top/bottom collisions

                // Top collision (player landing on platform) - this is most common
                if (movingDown && Player.CollisionBounds.Bottom <= platform.CollisionBounds.Top + 5)
                {
                    // Player landed on top of platform
                    Player.NextPositionY = platform.CollisionBounds.Top - Player.Texture.Height / 2;
                    Player.SpeedY = 0;
                }
                // Bottom collision (player hitting platform from below)
                else if (movingUp && Player.CollisionBounds.Top >= platform.CollisionBounds.Bottom - 5)
                {
                    Player.NextPositionY = platform.CollisionBounds.Bottom + Player.Texture.Height / 2;
                    Player.SpeedY = 0;
                }
                // Handle horizontal collisions with platforms as well
                else
                {
                    // Left collision with platform
                    if (Player.SpeedX > 0 && Player.CollisionBounds.Right <= platform.CollisionBounds.Left + 5)
                    {
                        Player.NextPositionX = platform.CollisionBounds.Left - Player.Texture.Width / 2;
                        Player.SpeedX = 0;
                    }
                    // Right collision with platform
                    else if (Player.SpeedX < 0 && Player.CollisionBounds.Left >= platform.CollisionBounds.Right - 5)
                    {
                        Player.NextPositionX = platform.CollisionBounds.Right + Player.Texture.Width / 2;
                        Player.SpeedX = 0;
                    }
                }

                // Update next position collision bounds after adjustment
                Player.CollisionBoundsNext = new Rectangle(
                    (int)Player.NextPositionX - (Player.Texture.Width / 2),
                    (int)Player.NextPositionY - (Player.Texture.Height / 2),
                    Player.Texture.Width,
                    Player.Texture.Height
                );
            }
        }
        foreach (var wall in level.Walls) // Wall collisions
        {
            if (wall.isSolid && Player.CollisionBoundsNext.Intersects(wall.CollisionBounds))
            {
                // Calculate movement direction
                bool movingRight = Player.SpeedX > 0;
                bool movingLeft = Player.SpeedX < 0;
                bool movingDown = Player.SpeedY > 0;
                bool movingUp = Player.SpeedY < 0;

                // For walls, prioritize horizontal collisions

                // Left collision (player hitting wall from the left)
                if (movingRight && Player.CollisionBounds.Right <= wall.CollisionBounds.Left &&
                    Player.CollisionBoundsNext.Right >= wall.CollisionBounds.Left)
                {
                    Player.NextPositionX = wall.CollisionBounds.Left - Player.Texture.Width / 2;
                    Player.SpeedX = 0;
                }
                // Right collision (player hitting wall from the right)
                else if (movingLeft && Player.CollisionBounds.Left >= wall.CollisionBounds.Right &&
                        Player.CollisionBoundsNext.Left <= wall.CollisionBounds.Right)
                {
                    Player.NextPositionX = wall.CollisionBounds.Right + Player.Texture.Width / 2;
                    Player.SpeedX = 0;
                }
                // Top collision (player landing on top of wall)
                else if (movingDown && Player.CollisionBounds.Bottom <= wall.CollisionBounds.Top &&
                        Player.CollisionBoundsNext.Bottom >= wall.CollisionBounds.Top)
                {
                    Player.NextPositionY = wall.CollisionBounds.Top - Player.Texture.Height / 2;
                    Player.SpeedY = 0;
                }
                // Bottom collision (player hitting wall from below)
                else if (movingUp && Player.CollisionBounds.Top >= wall.CollisionBounds.Bottom &&
                        Player.CollisionBoundsNext.Top <= wall.CollisionBounds.Bottom)
                {
                    Player.NextPositionY = wall.CollisionBounds.Bottom + Player.Texture.Height / 2;
                    Player.SpeedY = 0;
                }

                // Update next position collision bounds after adjustment
                Player.CollisionBoundsNext = new Rectangle(
                    (int)Player.NextPositionX - (Player.Texture.Width / 2),
                    (int)Player.NextPositionY - (Player.Texture.Height / 2),
                    Player.Texture.Width,
                    Player.Texture.Height
                );
            }
        }

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
        GraphicsDevice.Clear(levelBackgroundColor);

        // Begin sprite batch
        _spriteBatch.Begin();

        // Draw platforms
        foreach (var platform in level.Platforms)
        {
            _spriteBatch.Draw(
                platformTexture,
                new Vector2(platform.X, platform.Y),
                levelFilterColor
            );
        }

        // Draw walls
        foreach (var wall in level.Walls)
        {
            _spriteBatch.Draw(
                wallTexture,
                new Vector2(wall.X, wall.Y),
                null,
                levelFilterColor,
                0f,
                Vector2.Zero,
                1f,
                wall.FacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally, // Flip sprite if not facing right
                0f
            );
        }

        // Draw player
        _spriteBatch.Draw(
            Player.Texture,
            Player.Position,
            null,
            levelFilterColor,
            0f,
            new Vector2(Player.Texture.Width / 2, Player.Texture.Height / 2),
            Vector2.One,
            Player.FacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally, // Flip sprite if not facing right
            0f
        );

        // Draw debug info
        if (_debugMode)
        {
            // Draw player collision bounds
            DrawRectangle(Player.CollisionBounds, Color.Red, 4);
            DrawRectangle(Player.CollisionBoundsNext, Color.Yellow, 2);

            // Draw platform collision bounds
            foreach (var platform in level.Platforms)
            {
                DrawRectangle(platform.CollisionBounds, Color.Blue, 2);
            }

            // Draw wall collision bounds
            foreach (var wall in level.Walls)
            {
                DrawRectangle(wall.CollisionBounds, Color.Green, 2);
            }
        }

        // End sprite batch
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    // Draw collision boundaries for debugging
    private void DrawRectangle(Rectangle rectangle, Color color, int lineWidth = 1)
    {
        // Draw top line
        _spriteBatch.Draw(_debugTexture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, lineWidth), color);
        // Draw bottom line
        _spriteBatch.Draw(_debugTexture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height - lineWidth, rectangle.Width, lineWidth), color);
        // Draw left line
        _spriteBatch.Draw(_debugTexture, new Rectangle(rectangle.X, rectangle.Y, lineWidth, rectangle.Height), color);
        // Draw right line
        _spriteBatch.Draw(_debugTexture, new Rectangle(rectangle.X + rectangle.Width - lineWidth, rectangle.Y, lineWidth, rectangle.Height), color);
    }
}

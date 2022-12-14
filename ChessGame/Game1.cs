
using AWFrameWork;
namespace ChessGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Scene scene;
    public static new ContentManager Content;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        base.Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Game1.Content = base.Content;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        scene = new ChessGameScene();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        scene.Batch = _spriteBatch;
        scene.Load();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        scene.Update(gameTime, Keyboard.GetState(), Mouse.GetState());
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        scene.Draw(gameTime);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MyRPG.Input;
using MyRPG.Screens;
using MyRPG.Xml;

namespace MyRPG {
  public class RpgGame : Game {
    public SpriteBatch SpriteBatch { get; private set; }
    public InputManager InputManager { get; private set; }
    public GraphicsDeviceManager Graphics { get; private set; }
    public ScreenManager ScreenManager { get; private set; }
    public XmlManager XmlManager { get; private set; }
    public GameSettings GameSettings { get; private set; }

    public static RpgGame Instance {
      get {
        if (_instance == null) _instance = new RpgGame();
        return _instance;
      }
    }
    private static RpgGame _instance { get; set; }

    public RpgGame() {
      XmlManager = new XmlManager();
      Graphics = new GraphicsDeviceManager(this);
      ScreenManager = new ScreenManager();
      InputManager = new InputManager();

      Components.Add(ScreenManager);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
    }

    protected override void Initialize() {
      GameSettings = XmlManager.Load<GameSettings>("Content/GameSettings.xml");
      ScreenManager.LoadScreen(new MainScreen());

      base.Initialize();
    }

    protected override void LoadContent() {
      SpriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime) {
      InputManager.Update(gameTime);
      ScreenManager.Update(gameTime);

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      SpriteBatch.Begin();
      base.Draw(gameTime);
      SpriteBatch.End();
    }
  }
}

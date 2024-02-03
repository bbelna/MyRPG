using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using MyRPG.GameObjects;
using MyRPG.Input;
using MyRPG.Screens;
using MyRPG.Xml;
using System;

namespace MyRPG {
  public class RpgGame : Game {
    public SpriteBatch SpriteBatch { get; private set; }
    public InputManager InputManager { get; private set; }
    public GraphicsDeviceManager Graphics { get; private set; }
    public ScreenManager ScreenManager { get; private set; }
    public XmlManager XmlManager { get; private set; }
    public InputBindings InputBindings { get; private set; }
    public SpriteFont SpriteFont { get; private set; }
    public ViewportAdapter ViewportAdapter { get; private set; }
    public OrthographicCamera Camera { get; private set; }
    public GameObjectManager GameObjectManager { get; private set; }

    public static RpgGame Instance {
      get {
        if (_instance == null) _instance = new RpgGame();
        return _instance;
      }
    }
    private static RpgGame _instance { get; set; }
    private bool _halt { get; set; } = false;
    private Exception _haltException { get; set; }

    public RpgGame() {
      XmlManager = new XmlManager();
      Graphics = new GraphicsDeviceManager(this);
      ScreenManager = new ScreenManager();
      InputManager = new InputManager();
      GameObjectManager = new GameObjectManager();

      Components.Add(ScreenManager);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
    }

    public void Halt() => _halt = true;

    public void HaltWithException(Exception e) {
      _haltException = e;
      Halt();
    }

    protected override void Initialize() {
      base.Initialize(); // this MUST come first or else screen LoadContent() gets called twice

      ViewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 480);
      Camera = new OrthographicCamera(ViewportAdapter);
      Camera.Position = new Vector2(0, 0);
      SpriteFont = Content.Load<SpriteFont>("Fonts\\Font");
      InputBindings = XmlManager.Load<InputBindings>(Content.RootDirectory + "\\Config\\Bindings.xml");

      Graphics.SynchronizeWithVerticalRetrace = true;

      ScreenManager.LoadScreen(new MainScreen(GameObjectManager));
    }

    protected override void LoadContent() {
      SpriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime) {
      if (!_halt) {
        InputManager.Update(gameTime);
        ScreenManager.Update(gameTime);

        base.Update(gameTime);
      }
    }

    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.Black);

      var transformMatrix = Camera.GetViewMatrix();
      SpriteBatch.Begin(
          transformMatrix: transformMatrix,
          samplerState: SamplerState.PointClamp,
          blendState: BlendState.AlphaBlend
        );
      if (!_halt) {
        base.Draw(gameTime);
      } else if (_haltException != null) {
        SpriteBatch.DrawString(SpriteFont, "HALT: EXCEPTION", new Vector2(0, 0), Color.Red);
        SpriteBatch.DrawString(SpriteFont, _haltException.Message, new Vector2(0, 16), Color.White);
      } else {
        SpriteBatch.DrawString(SpriteFont, "HALT", new Vector2(0, 0), Color.Red);
      }
      SpriteBatch.End();
    }
  }
}

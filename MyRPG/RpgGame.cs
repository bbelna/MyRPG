﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MyRPG.Input;
using MyRPG.Screens;

namespace MyRPG {
  public class RpgGame : Game {
    public SpriteBatch SpriteBatch { get; private set; }
    public InputManager InputManager { get; private set; }
    public GraphicsDeviceManager Graphics { get; private set; }
    public ScreenManager ScreenManager { get; private set; }

    public static RpgGame Instance {
      get {
        if (_instance == null) _instance = new RpgGame();
        return _instance;
      }
    }
    private static RpgGame _instance { get; set; }

    public RpgGame() {
      Graphics = new GraphicsDeviceManager(this);
      ScreenManager = new ScreenManager();
      InputManager = new InputManager();

      Components.Add(ScreenManager);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
    }

    protected override void Initialize() {
      base.Initialize();
      ScreenManager.LoadScreen(new MainScreen());
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
﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyRPG.Input;
using System;

namespace MyRPG.GameObjects {
  public abstract class GameObject {
    public string Id { get; protected set; } = Guid.NewGuid().ToString();
    public bool Initialized { get; protected set; } = false;
    public Vector2 Position = new Vector2(0, 0);
    public int ZIndex { get; set; } = 0;

    protected ContentManager _content { get; } = RpgGame.Instance.Content;
    protected InputManager _input { get; } = RpgGame.Instance.InputManager;
    protected GraphicsDevice _graphicsDevice = RpgGame.Instance.GraphicsDevice;
    protected GraphicsDeviceManager _graphics { get; } = RpgGame.Instance.Graphics;
    protected SpriteBatch _spriteBatch { get; } = RpgGame.Instance.SpriteBatch;
    protected RpgGame _game { get; } = RpgGame.Instance;

    public virtual void Update(GameTime gameTime) { }

    public virtual void Draw(GameTime gameTime) { }

    public virtual void LoadContent() {
      Initialized = true;
    }
  }
}

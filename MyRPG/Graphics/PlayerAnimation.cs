using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyRPG.ISupports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRPG.Graphics {
  public class PlayerAnimation : Drawable, ISupportContent {
    private Texture2D _texture;
    private float _timer = 0;
    private int _threshold = 250;
    private byte _previousAnimationIndex;
    private byte _currentAnimationIndex;
    private Rectangle[] _sourceRectangles;
    private Vector2 _position;

    public PlayerAnimation() {
      _position = new Vector2(0, 0);
    }

    public void SetPosition(Vector2 position) {
      _position = position;
    }

    public void Update(GameTime gameTime) {
      if (_timer > _threshold) {
        if (_currentAnimationIndex == 1) {
          if (_previousAnimationIndex == 0) {
            _currentAnimationIndex = 2;
          } else {
            _currentAnimationIndex = 0;
          }
          _previousAnimationIndex = _currentAnimationIndex;
        } else {
          _currentAnimationIndex = 1;
        }
        _timer = 0;
      } else {
        _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
      }
    }

    public void Draw(GameTime gameTime) {
      _spriteBatch.Draw(_texture, _position, _sourceRectangles[_currentAnimationIndex], Color.White);
    }

    public void LoadContent() {
      _texture = _content.Load<Texture2D>("charaset");

      _sourceRectangles = new Rectangle[3];
      _sourceRectangles[0] = new Rectangle(0, 128, 48, 64);
      _sourceRectangles[1] = new Rectangle(48, 128, 48, 64);
      _sourceRectangles[2] = new Rectangle(96, 128, 48, 64);

      _previousAnimationIndex = 2;
      _currentAnimationIndex = 1;
    }
  }
}

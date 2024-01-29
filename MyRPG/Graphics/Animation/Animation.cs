using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace MyRPG.Graphics.Animation {
  public class Animation : Drawable {
    protected string _texturePath { get; set; }
    protected Texture2D _texture { get; set; }
    protected IEnumerable<Rectangle> _frames { get; set; }
    protected int _threshold { get; set; } = 250;
    protected Vector2 _position { get; set; }
    protected int _currentAnimationIndex = 0;
    protected float _timer = 0;

    public void Update(GameTime gameTime) {
      if (_timer > _threshold) {
        _currentAnimationIndex++;
        if (_currentAnimationIndex >= _frames.Count()) {
          _currentAnimationIndex = 0;
        }
        _timer = 0;
      } else {
        _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
      }
    }

    public void Draw(GameTime gameTime) {
      if (_texture != null && _frames != null && _frames.Count() > 0) {
        _spriteBatch.Draw(_texture, _position, _frames.ElementAt(_currentAnimationIndex), Color.White);
      }
    }

    public void LoadContent() {
      _texture = _content.Load<Texture2D>(_texturePath);
    }

    public void SetPosition(Vector2 position) {
      _position = position;
    }
  }
}

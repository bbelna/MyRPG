using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace MyRPG.Graphics.Animation {
  public abstract class Animation : Drawable {
    protected bool _idle { get; set; } = false;
    protected AnimationDataSet _animationDataSet { get; set; }
    protected AnimationData _activeAnimation { get; set; }
    protected AnimationBehavior _behavior { get; set; }
    protected string _texturePath { get; set; }
    protected Texture2D _texture { get; set; }
    protected int _threshold { get; set; } = 250;
    protected Vector2 _position { get; set; } = new Vector2(0, 0);
    protected int _currentAnimationIndex { get; set; } = 0;
    protected float _timer { get; set; } = 0;

    public void Update(GameTime gameTime) {
      if (_activeAnimation != null) {
        if (!_idle) {
          if (_timer > _threshold) {
            _currentAnimationIndex++;
            if (_currentAnimationIndex >= _activeAnimation.Frames.Count()) {
              _currentAnimationIndex = 0;
            }
            _timer = 0;
          } else {
            _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
          }
        } else {
          _currentAnimationIndex = _behavior.IdleFrame;
        }
      }
    }

    public void Draw(GameTime gameTime) {
      if (_texture != null
       && _activeAnimation != null
       && _activeAnimation.Frames != null
       && _activeAnimation.Frames.Count() > 0) {
        _spriteBatch.Draw(
          _texture,
          _position,
          _activeAnimation.Frames.ElementAt(_currentAnimationIndex).ToRectangle(),
          Color.White
        );
      }
    }

    public void LoadContent() {
      _texture = _content.Load<Texture2D>(_texturePath);
    }

    public void SetPosition(Vector2 position) {
      _position = position;
    }

    public void Play() => _idle = false;

    public void Pause() => _idle = true;

    public void LoadAnimationDataSet(string path) {
      var dataSet = _xmlManager.Load<AnimationDataSet>(path);
      if (dataSet != null) {
        SetAnimationDataSet(dataSet);
      }
    }

    public void SetAnimationDataSet(AnimationDataSet dataSet) {
      if (dataSet.Animations.Count() > 0) {
        _animationDataSet = dataSet;
        _activeAnimation = _animationDataSet.Animations[0];
      }
    }

    public void SetAnimation(string name, bool play = false) {
      if (_animationDataSet == null) return;
      _activeAnimation = _animationDataSet.Animations.FirstOrDefault(d => d.Name == name);
      if (play) Play();
    }
  }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Linq;

namespace MyRPG.Graphics.Animation {
  public abstract class Animation : Drawable {
    public bool Idle { get; set; } = false;
    public AnimationDataSet AnimationDataSet { get; set; }
    public AnimationData ActiveAnimation { get; set; }
    public AnimationBehavior Behavior { get; set; }
    public Vector2 Position { get; set; } = new Vector2(0, 0);

    public string TexturePath { get; protected set; }

    protected Texture2D _texture { get; set; }
    protected int _threshold { get; set; } = 250;
    protected int _currentAnimationIndex { get; set; } = 0;
    protected float _timer { get; set; } = 0;

    public void Update(GameTime gameTime) {
      if (ActiveAnimation != null) {
        if (!Idle) {
          if (_timer > _threshold) {
            _currentAnimationIndex++;
            if (_currentAnimationIndex >= ActiveAnimation.Frames.Count()) {
              _currentAnimationIndex = 0;
            }
            _timer = 0;
          } else {
            _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
          }
        } else {
          _currentAnimationIndex = Behavior.IdleFrame;
        }
      }
    }

    public void Draw(GameTime gameTime) {
      if (_texture != null
       && ActiveAnimation != null
       && ActiveAnimation.Frames != null
       && ActiveAnimation.Frames.Count() > 0) {
        _spriteBatch.Draw(
          _texture,
          Position,
          ActiveAnimation.Frames.ElementAt(_currentAnimationIndex).ToRectangle(),
          Color.White
        );
      }
    }

    public void LoadContent() {
      using (FileStream fileStream = new FileStream(TexturePath, FileMode.Open)) {
        _texture = Texture2D.FromStream(_graphicsDevice, fileStream);
      }
    }

    public void SetPosition(Vector2 position) {
      Position = position;
    }

    public void Play() => Idle = false;

    public void Pause() => Idle = true;

    public void LoadAnimationDataSet(string path) {
      var dataSet = _xmlManager.Load<AnimationDataSet>(path);
      if (dataSet != null) {
        SetAnimationDataSet(dataSet);
      }
    }

    public void SetAnimationDataSet(AnimationDataSet dataSet) {
      if (dataSet.Animations.Count() > 0) {
        AnimationDataSet = dataSet;
        ActiveAnimation = AnimationDataSet.Animations[0];
      }
    }

    public void SetAnimation(string name, bool play = false) {
      if (AnimationDataSet == null) return;
      ActiveAnimation = AnimationDataSet.Animations.FirstOrDefault(d => d.Name == name);
      if (play) Play();
    }

    public void Reload() => LoadContent();
  }
}

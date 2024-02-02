using Microsoft.Xna.Framework;
using MyRPG.Graphics.Animation;

namespace MyRPG.GameObjects.PlayerCharacter {
  public class PlayerAnimation : Animation {
    public PlayerAnimation() {
      _behavior = new AnimationBehavior() { IdleFrame = 1 };
      _texturePath = "charaset";
      _position = new Vector2(0, 0);

      Pause();
      LoadAnimationDataSet("Content/Animations/PlayerAnimation.xml");
    }
  }
}

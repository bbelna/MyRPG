using Microsoft.Xna.Framework;
using MyRPG.Graphics.Animation;

namespace MyRPG.GameObjects.Player {
  public class PlayerAnimation : Animation {
    public PlayerAnimation() {
      Behavior = new AnimationBehavior() { IdleFrame = 1 };
      TexturePath = "charaset";

      Pause();
      LoadAnimationDataSet("Content/Animations/PlayerAnimation.xml");
    }
  }
}

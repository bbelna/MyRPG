using MyRPG.Graphics.Animation;

namespace MyRPG.GameObjects.Player {
  public class PlayerAnimation : Animation {
    public PlayerAnimation() {
      Behavior = new AnimationBehavior() { IdleFrame = 1 };
      TexturePath = _content.RootDirectory + "\\Textures\\charaset.png";

      Pause();
      LoadAnimationDataSet(_content.RootDirectory + "\\Animations\\PlayerAnimation.xml");
    }
  }
}

using Microsoft.Xna.Framework;
using MyRPG.Graphics.Animation;
using MyRPG.Xml;
using System.Linq;

namespace MyRPG.GameObjects.PlayerCharacter
{
    public class PlayerAnimation : Animation {
    public bool Idle { get; set; } = true;

    protected XmlManager _xmlManager = RpgGame.Instance.XmlManager;
    protected AnimationDataSet _animationDataSet;

    public PlayerAnimation() {
      _texturePath = "charaset";
      _position = new Vector2(0, 0);
      _animationDataSet = _xmlManager.Load<AnimationDataSet>("Content/Animations/PlayerAnimation.xml");
      _frames = _animationDataSet.AnimationData.FirstOrDefault(d => d.Name == "WalkDown").Frames;
    }
  }
}

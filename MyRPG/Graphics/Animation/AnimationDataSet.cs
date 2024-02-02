using System.Collections.Generic;
using System.Xml.Serialization;

namespace MyRPG.Graphics.Animation {
  public class AnimationDataSet {
    public string Texture { get; set; }

    [XmlElement("Animation")]
    public List<AnimationData> Animations { get; set; }
  }
}

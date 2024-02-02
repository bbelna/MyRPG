using System.Collections.Generic;
using System.Xml.Serialization;

namespace MyRPG.Graphics.Animation {
  public class AnimationData {
    public string Name { get; set; }

    [XmlElement("Frame")]
    public List<AnimationFrame> Frames { get; set; }
  }
}


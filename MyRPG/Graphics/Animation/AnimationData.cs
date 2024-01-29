using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MyRPG.Graphics.Animation {
  public class AnimationData {
    public string Name { get; set; }

    [XmlElement("Frame")]
    public List<Rectangle> Frames { get; set; }
  }
}

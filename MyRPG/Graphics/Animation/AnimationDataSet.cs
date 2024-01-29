using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MyRPG.Graphics.Animation {
  public class AnimationDataSet {
    public string Texture { get; set; }

    [XmlElement("AnimationData")]
    public List<AnimationData> AnimationData { get; set; }
  }
}

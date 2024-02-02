using MyRPG.Input;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MyRPG {
  public class GameSettings {
    [XmlElement("InputBinding")]
    public List<InputBinding> InputBindings { get; set; }
  }
}

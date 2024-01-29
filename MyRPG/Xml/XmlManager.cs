using System.IO;
using System.Xml.Serialization;

namespace MyRPG.Xml {
  public class XmlManager {
    public T Load<T>(string path) {
      T instance;
      using (TextReader  reader = new StreamReader(path)) {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        instance = (T)xml.Deserialize(reader);
      }
      return instance;
    }

    public void Save<T>(string path, T obj) {
      using (TextWriter writer = new StreamWriter(path)) {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        xml.Serialize(writer, obj);
      }
    }
  }
}

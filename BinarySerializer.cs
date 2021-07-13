// Gehört zu Arvid
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Info2021 {
    public static class BinarySerializer {
        // uses C# serialization features to write objects to a file
        public static void Serialize<T>(T obj, Stream stream) {
            var serializer = new DataContractSerializer(typeof(T));
            using (var writer =
                XmlDictionaryWriter.CreateBinaryWriter(stream)) {
                serializer.WriteObject(writer, obj);
            }
        }

        public static T Deserialize<T>(Stream stream) {
            var serializer = new DataContractSerializer(typeof(T));
            using (var reader =
                XmlDictionaryReader.CreateBinaryReader(
                    stream, XmlDictionaryReaderQuotas.Max)) {
                return (T)serializer.ReadObject(reader);
            }
        }
    }

}
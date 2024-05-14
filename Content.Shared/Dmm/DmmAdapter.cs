using System.Linq;
using Robust.Shared.ContentPack;
using Robust.Shared.Utility;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace Content.Shared.Dmm
{
    public sealed class DmmAdapter
    {
        [Dependency] private readonly IResourceManager _resource = default!;
        private ResPath FilePath { get; set; } = default!;
        public Dictionary<string, string> EntityMap = new Dictionary<string, string>();
        // SS14 to byond's tile mappings
        public Dictionary<string, string> TileMap = new Dictionary<string, string>();
        // Byond to SS14 tile mappings
        public Dictionary<string, string> ReverseMap = new Dictionary<string, string>();
        public DmmAdapter(ResPath fPath)
        {
            FilePath = fPath;
            IoCManager.InjectDependencies(this);
            ReadFile();
        }

        private void ReadFile()
        {
            //var yamlDeserializer = new DeserializerBuilder().Build();
            var content = _resource.ContentFileReadYaml(FilePath);
            var yamlText = new YamlStream(content.ElementAt(0));
            EntityMap = yamlText.ToDictionary(x =>
            {
                var s = x.ToString();
                if (s != null) return s;
                else throw new Exception("Key in the entity node was null");
            },
            y =>
            {
                var s = y.ToString();
                if (s != null) return s;
                else throw new Exception("Value in the entity node was null");
            }
            );
            yamlText = new YamlStream(content.ElementAt(1));
            TileMap = yamlText.ToDictionary(x =>
            {
                var s = x.ToString();
                if (s != null) return s;
                else throw new Exception("Key in the tile node was null");
            },
            y =>
            {
                var s = y.ToString();
                if (s != null) return s;
                else throw new Exception("Value in the tile node was null");
            }
            );
        }
    }
}

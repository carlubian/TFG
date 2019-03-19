using ConfigAdapter;
using ConfigAdapter.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaomi.Legacy.Model
{
    /// <summary>
    /// Allows proceses to read and write persistent
    /// settings in a configuration file.
    /// </summary>
    public class KaomiPluginConfiguration : KaomiPlugin
    {
        private Config config;

        public override void Initialize(string callingAssembly)
        {
            config = XmlConfig.From($"{callingAssembly}.xml");
        }

        public string Read(string key) => config.Read(key);

        public void Write(string key, string value) => config.Write(key, value);

        public IDictionary<string, string> SettingsIn(string section) => config.SettingsIn(section);
    }
}

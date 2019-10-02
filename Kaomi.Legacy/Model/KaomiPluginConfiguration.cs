using ConfigAdapter;
using ConfigAdapter.Xml;
using System.Collections.Generic;

namespace Kaomi.Legacy.Model
{
    /// <summary>
    /// Allows proceses to read and write persistent
    /// settings in a configuration file.
    /// </summary>
    public class KaomiPluginConfiguration : KaomiPlugin
    {
        private Config config;

        public override void Initialize(string callingAssembly) => this.config = XmlConfig.From($"{callingAssembly}.xml");

        public string Read(string key) => this.config.Read(key);

        public void Write(string key, string value) => this.config.Write(key, value);

        public IDictionary<string, string> SettingsIn(string section) => this.config.SettingsIn(section);
    }
}

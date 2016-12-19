using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcl.Core.ConfigEntities
{
    public class BclConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("sourceFolders")]
        public FoldersCollection SourceFoldersCollection 
            => (FoldersCollection) base["sourceFolders"];

        [ConfigurationProperty("rules")]
        public RuleCollection Rules
         => (RuleCollection)base["rules"];

        [ConfigurationProperty("cultureInfo")]
        public CultureInfoElement CultureInfo
            => (CultureInfoElement) base["cultureInfo"];

        [ConfigurationProperty("isVerbose")]
        public IsVerboseElement IsVerbose
            => (IsVerboseElement) base["isVerbose"];

        [ConfigurationProperty("defaultDestinationFolder")]
        public DefaultDestinationFolderElement DefaultDestinationFolder
            => (DefaultDestinationFolderElement) base["defaultDestinationFolder"];

        public static BclConfigSection GetConfig()
        {
            return (BclConfigSection)ConfigurationManager.GetSection("bclConfigSection") ?? new BclConfigSection();
        }
    }
}

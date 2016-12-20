using System.Configuration;

namespace Bcl.Core.ConfigEntities
{
    public class BclConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("cultureInfo")]
        public CultureInfoElement CultureInfo
            => (CultureInfoElement)base["cultureInfo"];

        [ConfigurationProperty("defaultDestinationFolder")]
        public DefaultDestinationFolderElement DefaultDestinationFolder
            => (DefaultDestinationFolderElement)base["defaultDestinationFolder"];

        [ConfigurationProperty("isVerbose")]
        public IsVerboseElement IsVerbose
            => (IsVerboseElement)base["isVerbose"];

        [ConfigurationProperty("mustAddNumber")]
        public MustAddNumberElement MustAddNumber
            => (MustAddNumberElement)base["mustAddNumber"];

        [ConfigurationProperty("mustAddDate")]
        public MustAddDateElement MustAddDate
            => (MustAddDateElement)base["mustAddDate"];

        [ConfigurationProperty("rules")]
        public RuleCollection Rules
         => (RuleCollection)base["rules"];

        [ConfigurationProperty("sourceFolders")]
        public FoldersCollection SourceFoldersCollection
            => (FoldersCollection)base["sourceFolders"];

        public static BclConfigSection GetConfig()
        {
            return (BclConfigSection)ConfigurationManager.GetSection("bclConfigSection") ?? new BclConfigSection();
        }
    }
}
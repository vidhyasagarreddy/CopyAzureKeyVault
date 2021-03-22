using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyAzureKeyVault
{
    public class Config
    {
        public AzureKeyVaultConfig SourceKeyVaultConfig { get; set; }

        public AzureKeyVaultConfig TargetKeyVaultConfig { get; set; }
    }

    public class AzureKeyVaultConfig
    {
        public string Name { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string AzureTenantId { get; set; }
    }
}

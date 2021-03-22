using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyAzureKeyVault
{
    public static class KeyVaultExtensions
    {
        public static SecretClient BuildKeyVaultClient(this AzureKeyVaultConfig config)
        {
            var keyVaultEndpoint = $"https://{config.Name}.vault.azure.net";
            return new SecretClient(new Uri(keyVaultEndpoint), new ClientSecretCredential(config.AzureTenantId, config.ClientId, config.ClientSecret));
        }
    }
}

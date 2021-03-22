using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyAzureKeyVault
{
    public class KeyVaultCopier
    {
        private readonly Config config;

        public KeyVaultCopier(Config config)
        {
            this.config = config;
        }

        public async Task Copy()
        {
            // Build KeyVault Client for source 
            var sourceKeyVaultClient = this.config.SourceKeyVaultConfig.BuildKeyVaultClient();

            // Get properties of all Secrets
            var secretProperties = sourceKeyVaultClient.GetPropertiesOfSecretsAsync().ConfigureAwait(false);

            // Build KeyVault Client for Target
            var targetKeyVaultClient = this.config.TargetKeyVaultConfig.BuildKeyVaultClient();

            await foreach (var secretProperty in secretProperties)
            {
                // Get Source Secret value
                var secret = await sourceKeyVaultClient.GetSecretAsync(secretProperty.Name);

                // Insert into Target KeyVault
                await targetKeyVaultClient.SetSecretAsync(new KeyVaultSecret(secretProperty.Name, secret.Value.Value));
            }
        }
    }
}

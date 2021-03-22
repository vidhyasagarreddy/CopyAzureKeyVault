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
        public async Task Copy(Config config)
        {
            var sourceKeyVaultUri = $"https://{config.SourceKeyVaultConfig.Name}.vault.azure.net";
            var sourceKeyVaultClient = new SecretClient(new Uri(sourceKeyVaultUri), new ClientSecretCredential(config.SourceKeyVaultConfig.AzureTenantId, config.SourceKeyVaultConfig.ClientId, config.SourceKeyVaultConfig.ClientSecret));

            var items = sourceKeyVaultClient.GetPropertiesOfSecretsAsync().ConfigureAwait(false);

            var targetKeyVaultUri = $"https://{config.TargetKeyVaultConfig.Name}.vault.azure.net";
            var targetKeyVaultClient = new SecretClient(new Uri(targetKeyVaultUri), new ClientSecretCredential(config.TargetKeyVaultConfig.AzureTenantId, config.TargetKeyVaultConfig.ClientId, config.TargetKeyVaultConfig.ClientSecret));

            await foreach (var item in items)
            {
                var secret = await sourceKeyVaultClient.GetSecretAsync(item.Name);
                await targetKeyVaultClient.SetSecretAsync(new KeyVaultSecret(item.Name, secret.Value.Value));
            }
        }
    }
}

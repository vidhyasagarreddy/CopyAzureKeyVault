using System;
using System.Threading.Tasks;

namespace CopyAzureKeyVault
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new Config
            {
                SourceKeyVaultConfig = new AzureKeyVaultConfig
                {
                    Name = "<source-key-vault-name>",
                    ClientId = "<app-service-principal-client-id>",
                    ClientSecret = "<app-service-principal-client-secret>",
                    AzureTenantId = "<azure-tenant-id>"
                },
                TargetKeyVaultConfig = new AzureKeyVaultConfig
                {
                    Name = "<source-key-vault-name>",
                    ClientId = "<app-service-principal-client-id>",
                    ClientSecret = "<app-service-principal-client-secret>",
                    AzureTenantId = "<azure-tenant-id>"
                }
            };

            await (new KeyVaultCopier()).Copy(config);
        }
    }
}

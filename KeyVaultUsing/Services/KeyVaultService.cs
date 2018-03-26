using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.WebKey;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyVaultUsing.Services
{
    public class KeyVaultService : IKeyVaultService
    {
        public string GetKeyContent(string keyIdentifier)
        {
            var kv = GetKeyVaultClient(GetKeyVaultCallback());
            var key = kv.GetKeyAsync(keyIdentifier);
            return key.Result.Key.ToString();
        }

        public byte[] Encrypt(string keyIdentifier, string text2encrypt)
        {
            var kv = GetKeyVaultClient(GetKeyVaultCallback());
            byte[] text_as_byte = Encoding.UTF8.GetBytes(text2encrypt);
            var enc = kv.EncryptAsync(keyIdentifier, JsonWebKeyEncryptionAlgorithm.RSA15, text_as_byte).GetAwaiter().GetResult();
            return enc.Result;
        }

        public string Decrypt(string keyIdentifier, byte[] text2decrypt)
        {
            var kv = GetKeyVaultClient(GetKeyVaultCallback());
            var dec = kv.DecryptAsync(keyIdentifier, JsonWebKeyEncryptionAlgorithm.RSA15, text2decrypt).GetAwaiter().GetResult();
            return Encoding.UTF8.GetString(dec.Result);
        }

        public byte[] Sign(string keyIdentifier, string text2sign) {
            var kv = GetKeyVaultClient(GetKeyVaultCallback());

            byte[] source_as_byte = Encoding.UTF8.GetBytes(text2sign);
            var sign = kv.SignAsync(keyIdentifier, JsonWebKeySignatureAlgorithm.RSNULL, source_as_byte).GetAwaiter().GetResult();
            return sign.Result;
        }

        public bool Validate(string keyIdentifier, string signdText, byte[] signature)
        {
            var kv = GetKeyVaultClient(GetKeyVaultCallback());
            byte[] source_as_byte = Encoding.UTF8.GetBytes(signdText);
            var valid = kv.VerifyAsync(keyIdentifier, JsonWebKeySignatureAlgorithm.RSNULL, source_as_byte, signature).GetAwaiter().GetResult();
            return valid;
        }

        public string GetSecret(string secretIdentifier)
        {
            var kv = GetKeyVaultClient(GetKeyVaultCallback());
            var sec = kv.GetSecretAsync(secretIdentifier);
            return sec.Result.Value;
        }

        private AzureServiceTokenProvider.TokenCallback GetKeyVaultCallback()
        {
            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
            return azureServiceTokenProvider.KeyVaultTokenCallback;
        }

        private KeyVaultClient GetKeyVaultClient(AzureServiceTokenProvider.TokenCallback callback)
        {
            KeyVaultClient.AuthenticationCallback authCallback = new KeyVaultClient.AuthenticationCallback(callback);
            return new KeyVaultClient(authCallback);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyVaultUsing.Services
{
    public interface IKeyVaultService
    {
        string GetKeyContent(string keyIdentifier);
        byte[] Encrypt(string keyIdentifier, string text2encrypt);
        string Decrypt(string keyIdentifier, byte[] text2decrypt);
        byte[] Sign(string keyIdentifier, string text2sign);
        bool Validate(string keyIdentifier, string signdText, byte[] signature);
        string GetSecret(string secretIdentifier);
    }
}

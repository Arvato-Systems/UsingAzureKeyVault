using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyVaultUsing.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KeyVaultUsing.Pages
{
    public class CryptModel : PageModel
    {
        [BindProperty]
        public string Input { get; set; }
        [BindProperty]
        public string KeyIdentifier { get; set; }
        [BindProperty]
        public string Key { get; set; }
        [BindProperty]
        public byte[] Encrypted { get; set; }
        [BindProperty]
        public string Decrypted { get; set; }


        IKeyVaultService _service;

        public CryptModel(IKeyVaultService service)
        {
            _service = service;
        }

        public void OnGet()
        {
            
            Input = "Text that should be encrypted and decrypted";
            KeyIdentifier = "https://<KEYVAULTNAME>.vault.azure.net/keys/<KEYNAME>/<IDENTIFIER>";
        }

        
        //public async Task<IActionResult> OnPostAsync()
        public void OnPost()
        {
            Key = _service.GetKeyContent(KeyIdentifier);

            // encrypt/decrypt
            Encrypted = _service.Encrypt(KeyIdentifier, Input);
            Decrypted = _service.Decrypt(KeyIdentifier, Encrypted);
        }


    }
}

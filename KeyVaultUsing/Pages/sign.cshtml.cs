using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyVaultUsing.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KeyVaultUsing.Pages
{
    public class SignModel : PageModel
    {
        [BindProperty]
        public string Input { get; set; }
        [BindProperty]
        public string KeyIdentifier { get; set; }
        [BindProperty]
        public string Key { get; set; }
        [BindProperty]
        public byte[] Signature { get; set; }
        [BindProperty]
        public bool SignatureValidation { get; set; }


        IKeyVaultService _service;

        public SignModel(IKeyVaultService service)
        {
            _service = service;
        }

        public void OnGet()
        {
            Input = "Text that should be signed";
            KeyIdentifier = "https://<KEYVAULTNAME>.vault.azure.net/keys/<KEYNAME>/<IDENTIFIER>";
        }


        //public async Task<IActionResult> OnPostAsync()
        public void OnPost()
        {
            Key = _service.GetKeyContent(KeyIdentifier);

            // sign/validate
            Signature = _service.Sign(KeyIdentifier, Input);
            SignatureValidation = _service.Validate(KeyIdentifier, Input, Signature);
        }
    }
}

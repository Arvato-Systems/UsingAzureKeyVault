using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyVaultUsing.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KeyVaultUsing.Pages
{
    public class SecretModel : PageModel
    {
        [BindProperty]
        public string SecretIdentifier { get; set; }
        [BindProperty]
        public string SecretValue { get; set; }


        IKeyVaultService _service;

        public SecretModel(IKeyVaultService service)
        {
            _service = service;
        }

        public void OnGet()
        {
            SecretIdentifier = "https://<KEYVAULTNAME>.vault.azure.net/secrets/<SECRETNAME>/<IDENTIFIER>";
        }


        //public async Task<IActionResult> OnPostAsync()
        public void OnPost()
        {
            SecretValue = _service.GetSecret(SecretIdentifier);
        }
    }
}

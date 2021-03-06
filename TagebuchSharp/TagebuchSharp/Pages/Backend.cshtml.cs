using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TagebuchSharp.Pages {
    public class BackendModel : PageModel {

        IHostingEnvironment _environment;

        public BackendModel (IHostingEnvironment environment) {
            _environment = environment;
        }

        [BindProperty (SupportsGet = true)]
        public string Params { get; set; }

        public IActionResult OnGet () {

            return new PageResult ();
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TagebuchSharp.Pages {
    public class MainPageModel : PageModel {

        IHostingEnvironment _environment;

        public MainPageModel (IHostingEnvironment environment) {
            _environment = environment;
        }

        [BindProperty (SupportsGet = true)]
        public string Params { get; set; }

        public IActionResult OnGet () {
            /*not usefull
            var par = Params?.Split ('/')?.Where (i => !string.IsNullOrEmpty (i))?.ToList () ?? new List<string> ();
            if (par.Count >= 1) {
                if ((new [] { "lib", "css", "js" }).Contains (par[0].ToLower ())) {
                    var libFile = Path.Combine (_environment.WebRootPath, string.Join ('/', par));

                    return File (System.IO.File.ReadAllBytes (libFile), "");
                }
            }
            */
            return new PageResult ();
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TagebuchSharp.Data {
    public class ContentPage : Page {

        [Required]
        public bool HasPosts { get; set; }

        [Required]
        public bool IsInFooter { get; set; }

    }
}
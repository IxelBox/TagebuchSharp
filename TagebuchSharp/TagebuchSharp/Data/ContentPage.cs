using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tagebuchsharp.Data {
    public class ContentPage : Page {

        [Required]
        public bool HasPosts { get; set; }

    }
}
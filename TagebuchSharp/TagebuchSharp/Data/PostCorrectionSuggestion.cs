using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TagebuchSharp.Data {
    public class PostCorrectionSuggestion : PostComment {

        [Required]
        public int SelectorStart { get; set; }

        [Required]
        public int SelectorEnd { get; set; }

    }
}
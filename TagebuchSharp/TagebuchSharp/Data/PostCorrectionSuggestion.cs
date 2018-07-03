using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tagebuchsharp.Data {
    public class PostCorrectionSuggestion : PostComment {

        [MaxLength (30)]
        [Required]
        public string Selector { get; set; }

    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tagebuchsharp.Data {
    public class Attachment {

        [MaxLength (100)]
        [Required]
        public string MimeType { get; set; }

        [MaxLength (500)]
        [Key]
        public string FileName { get; set; }

        [Required]
        public string TagName { get; set; }

        [ForeignKey (nameof (TagName))]
        public Tag Tag { get; set; }
    }
}
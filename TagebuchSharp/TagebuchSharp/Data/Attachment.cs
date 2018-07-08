using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TagebuchSharp.Data {
    public class Attachment {

        [MaxLength (100)]
        [Required]
        public string MimeType { get; set; }

        [MaxLength (500)]
        [Key]
        public string FileName { get; set; }

        [Required]
        public int TagId { get; set; }

        [ForeignKey (nameof (TagId))]
        public Tag Tag { get; set; }
    }
}
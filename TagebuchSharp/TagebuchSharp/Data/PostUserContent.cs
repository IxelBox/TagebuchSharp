using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TagebuchSharp.Data {
    public class PostComment {
        [Key]
        public int PostUserContentId { get; set; }

        [MaxLength (150)]
        [Required]
        public string Email { get; set; }

        [MaxLength (60)]
        [Required]
        public string Name { get; set; }

        [Required]
        public int PostId { get; set; }

        [ForeignKey (nameof (PostId))]
        public Post Post { get; set; }

        [MaxLength (500)]
        [Required]
        public string Text { get; set; }
        public string PostCommentType { get; set; }
    }
}
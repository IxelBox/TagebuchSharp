using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TagebuchSharp.Data {
    public class Post : Page {

        [Required]
        [MaxLength (500)]
        public string Preview { get; set; }

        public List<PostTag> PostTags { get; set; }

        public List<PostComment> PostComments { get; set; }
    }
}
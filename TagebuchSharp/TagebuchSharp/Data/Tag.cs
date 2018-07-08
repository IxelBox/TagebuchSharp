using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TagebuchSharp.Data {
    public class Tag {

        [Key]
        public int TagId { get; set; }

        [MaxLength (100)]
        public string Name { get; set; }
        public List<PostTag> PostTags { get; set; }
    }

    public class PostTag {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
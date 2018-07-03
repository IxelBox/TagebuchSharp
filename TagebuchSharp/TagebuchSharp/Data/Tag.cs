using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tagebuchsharp.Data {
    public class Tag {

        [Key]
        public string Name { get; set; }
        public List<PostTag> PostTags { get; set; }
    }

    public class PostTag {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public string TagName { get; set; }
        public Tag Tag { get; set; }
    }
}
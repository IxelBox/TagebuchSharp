using System.ComponentModel.DataAnnotations;

namespace tagebuchsharp.Data {
    public abstract class Page {
        [Key]
        public int PageId { get; set; }

        [Required]
        [MaxLength (150)]
        public string Title { get; set; }

        [Required]
        [MaxLength (100)]
        public string Slug { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [MaxLength (2)]
        public string Language { get; set; }
        public string CustomMetaData { get; set; }
        public string PageType { get; set; }
    }
}
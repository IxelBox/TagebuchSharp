using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace tagebuchsharp.Data {
    public class ApplicationDbContext : IdentityDbContext {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<ContentPage> ContentPages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<PostCorrectionSuggestion> PostCorrectionSuggestions { get; set; }
        public DbSet<Page> Pages { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {

            base.OnModelCreating (modelBuilder);

            modelBuilder.Entity<PostTag> ()
                .HasKey (t => new { t.PostId, t.TagName });

            modelBuilder.Entity<PostTag> ()
                .HasOne (pt => pt.Post)
                .WithMany (p => p.PostTags)
                .HasForeignKey (pt => pt.PostId);

            modelBuilder.Entity<PostTag> ()
                .HasOne (pt => pt.Tag)
                .WithMany (t => t.PostTags)
                .HasForeignKey (pt => pt.TagName);

            modelBuilder.Entity<Post> ()
                .Property (b => b.Language)
                .HasDefaultValue ("de");

            modelBuilder.Entity<Page> ()
                .HasDiscriminator<string> (nameof (Page.PageType));

            modelBuilder.Entity<PostComment> ()
                .HasDiscriminator<string> (nameof (PostComment.PostCommentType));
        }
    }
}
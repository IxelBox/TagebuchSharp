using System.Text.Json.Serialization;
using TagebuchSharp.Messages;
using TagebuchSharp.Services;

namespace TagebuchSharp.GhostData.Data;


public record Navigation(
    [property: JsonPropertyName("label")] string Label,
    [property: JsonPropertyName("url")] string Url
);

public record Settings(
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("logo")] string Logo,
    [property: JsonPropertyName("icon")] string Icon,
    [property: JsonPropertyName("accent_color")] string AccentColor,
    [property: JsonPropertyName("cover_image")] string CoverImage,
    [property: JsonPropertyName("facebook")] string Facebook,
    [property: JsonPropertyName("twitter")] string Twitter,
    [property: JsonPropertyName("lang")] string Lang,
    [property: JsonPropertyName("timezone")] string Timezone,
    [property: JsonPropertyName("navigation")] IReadOnlyCollection<Navigation> Navigation,
    [property: JsonPropertyName("secondary_navigation")] IReadOnlyCollection<Navigation> SecondaryNavigation,
    [property: JsonPropertyName("meta_title")] string MetaTitle,
    [property: JsonPropertyName("meta_description")] string MetaDescription,
    [property: JsonPropertyName("og_image")] string OgImage,
    [property: JsonPropertyName("og_title")] string OgTitle,
    [property: JsonPropertyName("og_description")] string OgDescription,
    [property: JsonPropertyName("twitter_image")] string TwitterImage,
    [property: JsonPropertyName("twitter_title")] string TwitterTitle,
    [property: JsonPropertyName("twitter_description")] string TwitterDescription,
    [property: JsonPropertyName("members_support_address")] string MembersSupportAddress,
    [property: JsonPropertyName("url")] string Url
);

public record Tag(
        [property: JsonPropertyName("slug")] string Slug,
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("description")] string Description,
        [property: JsonPropertyName("feature_image")] string FeatureImage,
        [property: JsonPropertyName("visibility")] string Visibility,
        [property: JsonPropertyName("meta_title")] string MetaTitle,
        [property: JsonPropertyName("meta_description")] string MetaDescription,
        [property: JsonPropertyName("og_image")] string OgImage,
        [property: JsonPropertyName("og_title")] string OgTitle,
        [property: JsonPropertyName("og_description")] string OgDescription,
        [property: JsonPropertyName("twitter_image")] string TwitterImage,
        [property: JsonPropertyName("twitter_title")] string TwitterTitle,
        [property: JsonPropertyName("twitter_description")] string TwitterDescription,
        [property: JsonPropertyName("codeinjection_head")] string CodeinjectionHead,
        [property: JsonPropertyName("codeinjection_foot")] string CodeinjectionFoot,
        [property: JsonPropertyName("canonical_url")] string CanonicalUrl,
        [property: JsonPropertyName("accent_color")] string AccentColor,
        [property: JsonPropertyName("url")] string Url
    );

public record Author(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("slug")] string Slug,
        [property: JsonPropertyName("profile_image")] string ProfileImage,
        [property: JsonPropertyName("cover_image")] string CoverImage,
        [property: JsonPropertyName("bio")] string Bio,
        [property: JsonPropertyName("website")] string Website,
        [property: JsonPropertyName("location")] string Location,
        [property: JsonPropertyName("facebook")] string Facebook,
        [property: JsonPropertyName("twitter")] string Twitter,
        [property: JsonPropertyName("meta_title")] string MetaTitle,
        [property: JsonPropertyName("meta_description")] string MetaDescription,
        [property: JsonPropertyName("url")] string Url
    );

public record PostOrPage(
    [property: JsonPropertyName("slug")] string Slug,
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("uuid")] string Uuid,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("html")] string Html,
    [property: JsonPropertyName("comment_id")] string CommentId,
    [property: JsonPropertyName("feature_image")] string FeatureImage,
    [property: JsonPropertyName("feature_image_alt")] string FeatureImageAlt,
    [property: JsonPropertyName("feature_image_caption")] string FeatureImageCaption,
    [property: JsonPropertyName("featured")] bool Featured,
    [property: JsonPropertyName("meta_title")] string MetaTitle,
    [property: JsonPropertyName("meta_description")] string MetaDescription,
    [property: JsonPropertyName("created_at")] DateTime CreatedAt,
    [property: JsonPropertyName("updated_at")] DateTime UpdatedAt,
    [property: JsonPropertyName("published_at")] DateTime PublishedAt,
    [property: JsonPropertyName("custom_excerpt")] string CustomExcerpt,
    [property: JsonPropertyName("codeinjection_head")] string CodeinjectionHead,
    [property: JsonPropertyName("codeinjection_foot")] string CodeinjectionFoot,
    [property: JsonPropertyName("og_image")] string OgImage,
    [property: JsonPropertyName("og_title")] string OgTitle,
    [property: JsonPropertyName("og_description")] string OgDescription,
    [property: JsonPropertyName("twitter_image")] string TwitterImage,
    [property: JsonPropertyName("twitter_title")] string TwitterTitle,
    [property: JsonPropertyName("twitter_description")] string TwitterDescription,
    [property: JsonPropertyName("custom_template")] string CustomTemplate,
    [property: JsonPropertyName("canonical_url")] string CanonicalUrl,
    [property: JsonPropertyName("authors")] IReadOnlyCollection<Author> Authors,
    [property: JsonPropertyName("tags")] IReadOnlyCollection<Tag> Tags,
    [property: JsonPropertyName("primary_author")] Author PrimaryAuthor,
    [property: JsonPropertyName("primary_tag")] Tag PrimaryTag,
    [property: JsonPropertyName("url")] string Url,
    [property: JsonPropertyName("excerpt")] string Excerpt
);

public record Meta(
    [property: JsonPropertyName("pagination")] Pagination Pagination
);

public record Pagination(
    [property: JsonPropertyName("page")] int Page,
    [property: JsonPropertyName("limit")] int Limit,
    [property: JsonPropertyName("pages")] int Pages,
    [property: JsonPropertyName("total")] int Total,
    [property: JsonPropertyName("next")] object Next,
    [property: JsonPropertyName("prev")] object Prev
);

public record Error(
    [property: JsonPropertyName("message")] string Message,
    [property: JsonPropertyName("errorType")] string ErrorType
);



public static class ExtensionMethodes
{
    public static PageItem CreatePageItem(this PostOrPage p, IUrlRenamer urlRenamer) => new PageItem(
            p.Slug,
            p.Title,
            p.Excerpt,
            p.Html,
            p.MetaTitle,
            p.MetaDescription,
            p.OgTitle,
            p.OgDescription,
            p.TwitterTitle,
            p.TwitterDescription,
            urlRenamer.FixUrl(p.CanonicalUrl),
            p.Tags?.Select(t=> new TagItem(t.Id, t.Name, t.Slug, t.Description)).ToArray() ?? new TagItem[0],
            p.PublishedAt,
            p.UpdatedAt
         );
}

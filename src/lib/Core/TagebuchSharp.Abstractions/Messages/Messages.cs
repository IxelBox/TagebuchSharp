namespace TagebuchSharp.Messages;


public record NavigationItem(string Label, string Url);
public record GetBasicInformationsRequest();
public record GetBasicInformationsResponse(
    string Title,
    string Description,
    string Logo,
    string Icon,
    string Lang,
    string Timezone,
    NavigationItem[] Navigation,
    NavigationItem[] SecondaryNavigation
);

public record TagItem(
    string Id,
    string Name,
    string Slug,
    string Description
);


public record ShortPage(
    string Slug,
    string Title,
    DateTime UpdatedAt,
    DateTime PublishedAt,
    string Excerpt,
    string? Html,
    TagItem[] Tags
);

public record PageItem(
    string Slug,
    string Title,
    string Excerpt,
    string Html,
    string MetaTitle,
    string MetaDescription,
    string OgTitle,
    string OgDescription,
    string TwitterTitle,
    string TwitterDescription,
    string CanonicalUrl,
    TagItem[] Tags,
    DateTime PublishedAt,
    DateTime UpdatedAt
//string CustomExcerpt,
);

public record GetPageDataRequest(string Slug);
public record GetPageDataResponse(PageItem Page);

public record GetPostDataRequest(string Slug);
public record GetPostDataResponse(PageItem Page);

public record GetAllPostsRequest(int PageNumber, int ItemCount, bool WithContent);
public record GetAllPostsResponse(ShortPage[] Posts, int TotalPages, int CurrentPage);


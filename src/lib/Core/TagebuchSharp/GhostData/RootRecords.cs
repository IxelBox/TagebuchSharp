using System.Text.Json.Serialization;
using TagebuchSharp.GhostData.Data;

namespace TagebuchSharp.GhostData;

public record SettingsRoot(
    [property: JsonPropertyName("settings")] Settings Settings
);

public record PostRoot(
    [property: JsonPropertyName("posts")] IReadOnlyList<PostOrPage> Posts,
    [property: JsonPropertyName("meta")] Meta? Meta
);

public record PageRoot(
    [property: JsonPropertyName("pages")] IReadOnlyList<PostOrPage> Pages,
    [property: JsonPropertyName("meta")] Meta? Meta
);

public record TagRoot(
    [property: JsonPropertyName("tags")] IReadOnlyList<Tag> Tags,
    [property: JsonPropertyName("meta")] Meta? Meta
);

public record ErrorRoot(
    [property: JsonPropertyName("errors")] IReadOnlyList<Error> Errors
);

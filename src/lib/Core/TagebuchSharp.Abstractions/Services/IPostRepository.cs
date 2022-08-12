using TagebuchSharp.Messages;

namespace TagebuchSharp.Services;

public interface IPostRepository
{
    Task<GetPostDataResponse> GetPostDataAsync(GetPostDataRequest arg);
    Task<GetAllPostsResponse> GetAllPostsDataAsync(GetAllPostsRequest arg);
}

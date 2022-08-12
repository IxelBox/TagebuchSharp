using TagebuchSharp.Messages;

namespace TagebuchSharp.Services;

public class GhostPostRepository : IPostRepository
{
    public Task<GetAllPostsResponse> GetAllPostsDataAsync(GetAllPostsRequest arg)
    {
        throw new NotImplementedException();
    }

    public Task<GetPostDataResponse> GetPostDataAsync(GetPostDataRequest arg)
    {
        throw new NotImplementedException();
    }
}

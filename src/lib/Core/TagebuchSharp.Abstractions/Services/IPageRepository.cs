using TagebuchSharp.Messages;

namespace TagebuchSharp.Services;

public interface IPageRepository
{
    Task<GetBasicInformationsResponse> GetBasicInformationsAsync(GetBasicInformationsRequest arg);
    Task<GetPageDataResponse> GetPageDataAsync(GetPageDataRequest arg);

}

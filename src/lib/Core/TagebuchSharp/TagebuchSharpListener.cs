using MessageBus;
using TagebuchSharp.Messages;
using TagebuchSharp.Services;

namespace TagebuchSharp;

public class TagebuchSharpListener : IMessageBusListener
{
    private readonly IMessageBus _messageBus;
    private readonly IPageRepository _pageService;
    private readonly IPostRepository _postService;

    public TagebuchSharpListener(IMessageBus messageBus, IPageRepository pageService, IPostRepository postService)
    {
        _messageBus = messageBus;
        _pageService = pageService;
        _postService = postService;
    }

    public async Task StartListingAsync()
    {
        await _messageBus.AddMessageHandler(new MessageHandler<GetBasicInformationsRequest, GetBasicInformationsResponse>(_pageService.GetBasicInformationsAsync));
        await _messageBus.AddMessageHandler(new MessageHandler<GetPageDataRequest, GetPageDataResponse>(_pageService.GetPageDataAsync));
        await _messageBus.AddMessageHandler(new MessageHandler<GetPostDataRequest, GetPostDataResponse>(_postService.GetPostDataAsync));
        await _messageBus.AddMessageHandler(new MessageHandler<GetAllPostsRequest, GetAllPostsResponse>(_postService.GetAllPostsDataAsync));
    }
}
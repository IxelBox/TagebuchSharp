using Microsoft.Extensions.Logging.Abstractions;

namespace MessageBus.Tests
{
    public class MessageBusTest
    {
        static IMessageBus CreateMsgBus()
        {
            return new LocalMessageBus();
        }

        [Fact]
        public async Task BasicSubscribe()
        {
            var bus = CreateMsgBus();
            var request = "foobar";
            var responseResult = 42;


            int? response = null;
            var listener = new MessageListener<string, int>((r) => { response = r; return Task.CompletedTask; });

            var handler = new MessageHandler<string, int>((r) => Task.FromResult(responseResult));

            await bus.Subscribe(listener);
            await bus.AddMessageHandler(handler);

            await bus.Send(request);

            Assert.NotNull(response);
            Assert.Equal(responseResult, response);
        }

        [Fact]
        public async Task ExtensionBasicManyResult()
        {
            var bus = CreateMsgBus();
            var request = "foobar";
            var responseResult1 = 42;
            var responseResult2 = 6;

            int stepper = 0;

            var handler1 = new MessageHandler<string, int>((r) => { stepper++; return Task.FromResult(responseResult1); });
            var handler2 = new MessageHandler<string, int>((r) => { stepper++; return Task.FromResult(responseResult2); });

            await bus.AddMessageHandler(handler1);
            await bus.AddMessageHandler(handler2);

            var result = await bus.RequestMany<string, int>(request);
            Assert.Equal(2, stepper);
            Assert.Equal(2, result.Count);
            Assert.Contains<int>(responseResult1, result);
            Assert.Contains<int>(responseResult2, result);

            await bus.Send(request);
            Assert.Equal(2, stepper);
        }
    }
}
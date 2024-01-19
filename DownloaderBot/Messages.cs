//Messages
using DownloaderBot;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace DownloaderBot
{
    public class Messages
    {
        public bool isEnter = false;

        public string VideoLink { get; set; }

        public async Task HandleMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine("HandleMessageAsync");
            var handler = update.Message.Type switch
            {
                MessageType.Text => TextAsyncFunction(botClient, update, cancellationToken),
                MessageType.Document => DocumentAsyncFunction(botClient, update, cancellationToken),
            };
        }

        private async Task TextAsyncFunction(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
                return;
            if (message.Text is not { } messageText)
                return;

            var chatId = update.Message.Chat.Id;

            Console.WriteLine($"Received a '{message.Text}' message in chat {chatId}. UserName =>  {message.Chat.Username}");
            try
            {
                Console.WriteLine($"Message Type: {message.Type} Username=> {message.Chat.Username} Text => {message.Text} ");
                //string originalUrl = "https://www.instagram.com/p/C0bXUHTo5HP/?utm_source=ig_web_copy_link";
                //Console.WriteLine(encodedUrl);
                //return;
                ApiKey root = new ApiKey();

                IList<Model> body = JsonConvert.DeserializeObject<IList<Model>>(root.RunApi(messageText).Result);

                foreach (var item in body)
                {
                    isEnter = true;
                    Console.WriteLine($"\n{item.url}\n");
                    await botClient.SendChatActionAsync(
                        chatId: update.Message.Chat.Id,
                        chatAction: ChatAction.UploadDocument,
                        cancellationToken: cancellationToken
                    );
                    if (item.type == "video")
                    {
                        await botClient.SendVideoAsync(
                           chatId: chatId,
                           video: $"{item.url}",
                           supportsStreaming: true,
                           cancellationToken: cancellationToken);
                    }
                    else if (item.type == "photo")
                    {
                        await botClient.SendPhotoAsync(
                           chatId: chatId,
                           photo: $"{item.url}",
                           cancellationToken: cancellationToken);
                    }
                }
                if (!isEnter)
                {
                    string replasemessage = messageText.Replace("www.", "dd");
                    await botClient.SendVideoAsync
                    (
                           chatId: chatId,
                           video: $"{replasemessage}",
                           supportsStreaming: true,
                           cancellationToken: cancellationToken
                    );
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private async Task DocumentAsyncFunction(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task OtherMessage(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

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
            if (update.Message is not { } receivedMessage)
                return;
            if (receivedMessage.Text is not { } messageText)
                return;

            var chatId = receivedMessage.Chat.Id;

            if (messageText == "/start")
            {
                await botClient.SendTextMessageAsync(
                    chatId: receivedMessage.Chat.Id,
                    replyToMessageId: receivedMessage.MessageId,
                    text: "\n\n\n\t\tWelcome to Instagram Downloader🔴🟡🟢\n\n\n" +
                          "\t\t\tIn order to download a video or photo from Instagram, you have to choose buttons >>> ",
                    cancellationToken: cancellationToken);
            }
            else
            {
                // Add button creation and sending logic
                await Buttons.CreateButton(botClient, update, cancellationToken);

                // Rest of your existing code...
            }
        }

        private async Task ProcessUserLink(ITelegramBotClient botClient, Message receivedMessage, string link, CancellationToken cancellationToken)
        {
            try
            {
                ApiKey root = new ApiKey();

                // Process the link and download the content
                IList<Model> body = JsonConvert.DeserializeObject<IList<Model>>(root.RunApi(link).Result);

                foreach (var item in body)
                {
                    Console.WriteLine($"\n{item.url}\n");
                    await botClient.SendChatActionAsync(
                        chatId: receivedMessage.Chat.Id,
                        chatAction: ChatAction.UploadDocument,
                        cancellationToken: cancellationToken);

                    if (item.type == "video")
                    {
                        await botClient.SendVideoAsync(
                            chatId: receivedMessage.Chat.Id,
                            video: $"{item.url}",
                            supportsStreaming: true,
                            cancellationToken: cancellationToken);
                    }
                    else if (item.type == "photo")
                    {
                        await botClient.SendPhotoAsync(
                            chatId: receivedMessage.Chat.Id,
                            photo: $"{item.url}",
                            cancellationToken: cancellationToken);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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




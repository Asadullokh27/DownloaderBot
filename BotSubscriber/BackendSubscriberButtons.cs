using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BotSubscriber
{
    public class BackendSubscriberButtons
    {
        public Message? message;
        public Chat? chat;
        public ChatId? userId;

        public async Task HandleMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            message = update.Message;
            chat = message.Chat;
            userId = chat.Id;

            await botClient.SendChatActionAsync
            (
                chatId: userId,
                chatAction: ChatAction.Typing,
                cancellationToken: cancellationToken
            );

            await botClient.SendTextMessageAsync
            (
                chatId: userId,
                text: "Tabriklar siz kanallardan muvafaqqiyatli ro'yhatdan o'tdingiz!",
                cancellationToken: cancellationToken
            );
        }

        public async Task OtherMessage(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            await botClient.SendChatActionAsync
            (
                chatId: userId,
                chatAction: ChatAction.Typing,
                cancellationToken: cancellationToken
            );

            await botClient.SendTextMessageAsync
            (
                chatId: userId,
                text: "Iltimos faqat matn yozing!",
                replyToMessageId: message.MessageId,
                cancellationToken: cancellationToken
            );
        }
    }
}
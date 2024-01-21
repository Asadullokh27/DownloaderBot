using Telegram.Bot.Types.ReplyMarkups;

namespace BotSubscriber
{
    public static class SubscriberBotButtons
    {
        public static InlineKeyboardMarkup inlineKeyboard = new(new[]
        {
            //First row. You can also add multiple rows.
            new []
            {
                InlineKeyboardButton.WithUrl(text: "Kanal 1", url: "https://t.me/Otashaxsiy_1"),
                InlineKeyboardButton.WithUrl(text: "Kanal 2", url: "https://t.me/yangitelegramkanal1")
            },
        });
    }
}
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;

namespace DownloaderBot
{
    public class Buttons
    {
        public static async Task CreateButton(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var replyKeyboard = new ReplyKeyboardMarkup(
                new[]
            {
            new KeyboardButton[] { "Wallpaper🏙 >>> " },
            new KeyboardButton[] { "Photo🖼 >>> " },
            new KeyboardButton[] { "Video🎞 >>> " }
            })
            {
                ResizeKeyboard = true
            };

        }
    }
}

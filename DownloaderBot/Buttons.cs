using Telegram.Bot.Types.ReplyMarkups;

namespace DownloaderBot
{
    public class Buttons
    {
        public static ReplyKeyboardMarkup replyKeyboardMarkup = new(
            new[]
        {
            new KeyboardButton[] { "Please,click button for video Wallpaper >>> " },
            new KeyboardButton[] { "Please,click button for Photo🖼 >>> " },
            new KeyboardButton[] { "Please,click button for Video🎞 >>> " }
        })
        {
            ResizeKeyboard = true
        };
    }
}

using DownloaderBot;

var token = "6722307296:AAGFQcCYWkLii-UFofHPueeHJUcU7QZHNaA";
Temp temp = new Temp(token);

try
{
    temp.Run().Wait();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
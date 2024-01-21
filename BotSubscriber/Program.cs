using BotSubscriber;

var token = "6865131760:AAFlweLvRhFJ8gaHIGGCFmfCEhNFQAEPrd8";
Temp temp = new Temp(token);
try
{
    temp.Run().Wait();
}
catch (Exception ex)
{
    Console.WriteLine($"Program => {ex.Message}");
}
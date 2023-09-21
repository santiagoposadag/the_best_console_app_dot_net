using System.Text.Json;
using LibraryApp.Models;
using Microsoft.Extensions.Logging;

namespace LibraryApp.BusinessLogic;
public class Messages
{
    private readonly ILogger<Messages> _log;
    public Messages(ILogger<Messages> log)
    {
        _log = log;
    }

    public string Greeting(string language){
        string output = LookUpCustomText("Greeting", language);
        return output;
    }

    private string LookUpCustomText(string key, string language){
        JsonSerializerOptions oprtions = new(){
            PropertyNameCaseInsensitive = true
        };
        try
        {
            List<CustomText>? messageSets = JsonSerializer
                                .Deserialize<List<CustomText>>
                                (
                                    File.ReadAllText("CustomText.jason")
                                );
            CustomText? message = messageSets.Where(x => x.Language == language).First();
            
            if(message is null){
                throw new NullReferenceException("The specified language is not available");
            }

            return message.Transaltions[key];
        }
        catch (Exception ex)
        {
            _log.LogError("Error lookin up the custom tect", ex);
            throw;
        }
    }
}
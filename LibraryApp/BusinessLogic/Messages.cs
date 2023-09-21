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
        }
        catch (Exception ex)
        {
            _log.LogError("Error lookin up the custom tect", ex);
            throw;
        }
    }
}
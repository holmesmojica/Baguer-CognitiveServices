
namespace CognitiveServices.Infrastructure.Speech.Options
{
    public interface ISpeechOptions
    {
        string Language { get; set; }
        SpeechOptionsGenreEnum Gender { get; set; }
    }
}
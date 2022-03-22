
namespace CognitiveServices.Infrastructure.Speech.Options
{
    internal class SpeechOptionsAzure : ISpeechOptions
    {
        public string Language { get; set; } = string.Empty;
        public SpeechOptionsGenreEnum Gender { get; set; }
    }
}
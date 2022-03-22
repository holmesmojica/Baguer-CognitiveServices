
namespace CognitiveServices.Application.Dto
{
    public class SpeechVoiceDto
    {
        public string Language { get; set; } = string.Empty;
        public string LocalName { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;

        public int Gender { get; set; }
    }
}
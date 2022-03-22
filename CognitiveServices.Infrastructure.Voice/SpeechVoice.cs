
using Microsoft.CognitiveServices.Speech;


namespace CognitiveServices.Infrastructure.Speech
{
    public class SpeechVoice
    {
        public string Language { get; set; } = string.Empty;
        public string LocalName { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;

        public SynthesisVoiceGender Gender { get; set; }
    }
}
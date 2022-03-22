
using CognitiveServices.Infrastructure.Speech.Options;


namespace CognitiveServices.Infrastructure.Speech
{
    public interface ISpeechRepository
    {
        Task<object> GetVoicesList();
        Task<byte[]> ConvertTextToAudio(string textToConvert, ISpeechOptions options);
        Task<string> ConvertAudioToText(byte[] audioToConvert, ISpeechOptions options);
    }
}
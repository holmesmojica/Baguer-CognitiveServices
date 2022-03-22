
using CognitiveServices.Application.Dto;
using CognitiveServices.Application.Mapping;
using CognitiveServices.Infrastructure.Speech;
using CognitiveServices.Infrastructure.Speech.Options;
using System.Text.RegularExpressions;

namespace CognitiveServices.Application
{
    public class SpeechService
    {
        private readonly ISpeechRepository _speechRepository;


        public SpeechService()
        {
            SpeechRepositoryFactory speechRepositoryFactory = new SpeechRepositoryFactory();
            _speechRepository = speechRepositoryFactory.SpeechRepository;
        }

        public SpeechService(ISpeechRepository speechRepository)
        {
            _speechRepository = speechRepository;
        }


        #region ISpeechRepository implementation 

        public async Task<byte[]> ConvertTextToAudio(string textToConvert, SpeechOptionsDto options)
        {
            string cleanTextToConvert = Regex.Replace(textToConvert, "[^a-zA-Z0-9áéíóúñÑ_.,;$!¡? -]+", "", RegexOptions.Compiled);
            ISpeechOptions speechOptions = SpeechOptionsMapper.Map(options);
            byte[] audioData = await _speechRepository.ConvertTextToAudio(cleanTextToConvert, speechOptions);
            return audioData;
        }

        public async Task<string> ConvertAudioToText(byte[] audioToConvert, SpeechOptionsDto options)
        {
            ISpeechOptions speechOptions = SpeechOptionsMapper.Map(options);
            string textData = await _speechRepository.ConvertAudioToText(audioToConvert, speechOptions);
            return textData;
        }

        public async Task<object> GetVoicesList()
        {
            return await _speechRepository.GetVoicesList();
        }

        #endregion
    }
}
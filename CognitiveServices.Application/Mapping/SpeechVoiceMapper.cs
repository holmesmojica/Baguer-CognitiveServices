
using CognitiveServices.Application.Dto;
using CognitiveServices.Infrastructure.Speech;
using Microsoft.CognitiveServices.Speech;


namespace CognitiveServices.Application.Mapping
{
    internal class SpeechVoiceMapper
    {
        internal static SpeechVoice Map(SpeechVoiceDto speechVoiceDto)
        {
            if (speechVoiceDto == null)
                throw new ArgumentNullException(nameof(speechVoiceDto));

            SynthesisVoiceGender gender = (SynthesisVoiceGender)Enum
                .ToObject(typeof(SynthesisVoiceGender), speechVoiceDto.Gender);

            SpeechVoice speechVoice = new()
            {
                Gender    = gender,
                Language  = speechVoiceDto.Language,
                LocalName = speechVoiceDto.LocalName,
                ShortName = speechVoiceDto.ShortName
            };

            return speechVoice;
        }
    }
}
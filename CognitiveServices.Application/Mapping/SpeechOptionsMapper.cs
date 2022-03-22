
using CognitiveServices.Application.Dto;
using CognitiveServices.Infrastructure.Speech;
using CognitiveServices.Infrastructure.Speech.Options;


namespace CognitiveServices.Application.Mapping
{
    internal class SpeechOptionsMapper
    {
        internal static SpeechOptionsDto Map(ISpeechOptions speechOptions)
        {
            if (speechOptions == null)
                throw new ArgumentNullException(nameof(speechOptions));

            SpeechOptionsDto speechOptionDto = new()
            {
                Language = speechOptions.Language,
                Gender = (int)speechOptions.Gender
            };

            return speechOptionDto;
        }

        
        internal static ISpeechOptions Map(SpeechOptionsDto speechOptionsDto)
        {
            if (speechOptionsDto == null)
                throw new ArgumentNullException(nameof(speechOptionsDto));

            SpeechRepositoryFactory speechRepositoryFactory = new();

            SpeechOptionsGenreEnum genre = (SpeechOptionsGenreEnum)Enum
                .ToObject(typeof(SpeechOptionsGenreEnum), speechOptionsDto.Gender);

            ISpeechOptions speechOptions = speechRepositoryFactory
                .BuildSpeechOptions(speechOptionsDto.Language, genre);

            return speechOptions;
        }
    }
}
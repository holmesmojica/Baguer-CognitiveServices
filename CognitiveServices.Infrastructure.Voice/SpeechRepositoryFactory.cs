
using CognitiveServices.Infrastructure.Speech.Options;


namespace CognitiveServices.Infrastructure.Speech
{
    public class SpeechRepositoryFactory
    {
        public SpeechRepositoryFactory()
        {
            SpeechRepository = new SpeechRepositoryAzure();
        }


        public ISpeechOptions BuildSpeechOptions (string language, SpeechOptionsGenreEnum genre = SpeechOptionsGenreEnum.UNKNOWN)
        {
            return new SpeechOptionsAzure()
            {
                Language = language,
                Gender = genre
            };
        }


        public ISpeechRepository SpeechRepository { get; private set; }
    }
}

using CognitiveServices.Infrastructure.Speech.Options;
using Microsoft.CognitiveServices.Speech;


namespace CognitiveServices.Infrastructure.Speech
{
    internal class SpeechRepositoryAzure : ISpeechRepository
    {
        #region CONSTS
        private const string DEFAULT_LANGUAGE = SpeechLocales.MEXICO;
        private const string DEFAULT_REGION   = "eastus";
        private const string SUBSCRIPTION_KEY = "6f0f76ad323341f3af2cf9aca22f12e7";
        #endregion


        private readonly SpeechConfig _speechConfig;


        internal SpeechRepositoryAzure()
        {
            _speechConfig = CreateSpeechConfig();
        }


        private SpeechConfig CreateSpeechConfig()
        {
            SpeechConfig speechConfig = SpeechConfig.FromSubscription(SUBSCRIPTION_KEY, DEFAULT_REGION);
            return speechConfig;
        }


        public async Task<byte[]> ConvertTextToAudio(string textToConvert, ISpeechOptions options)
        {
            // CONFIGURANDO SPEECH
            VoiceInfo? voiceResult = await GetVoice(options);

            if (voiceResult != null)
            {
                _speechConfig.SpeechSynthesisLanguage = voiceResult.Locale;
                _speechConfig.SpeechSynthesisVoiceName = voiceResult.ShortName;
            }

            _speechConfig.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Audio24Khz160KBitRateMonoMp3);
            
            //SINTETIZANDO LA VOZ
            using SpeechSynthesizer synthesizer = new(_speechConfig);
            SpeechSynthesisResult? result = await synthesizer.SpeakTextAsync(textToConvert);
            
            return result.AudioData;
        }


        public async Task<string> ConvertAudioToText(byte[] audioToConvert, ISpeechOptions options)
        {
            throw new NotImplementedException();
        }


        public async Task<VoiceInfo?> GetVoice (ISpeechOptions options)
        {
            string language = string.IsNullOrEmpty(options.Language) ? DEFAULT_LANGUAGE : options.Language;

            using SpeechSynthesizer synthesizer = new(_speechConfig);
            SynthesisVoicesResult voicesResult  = await synthesizer.GetVoicesAsync();

            SynthesisVoiceGender voiceGender = options.Gender == SpeechOptionsGenreEnum.FEMALE
                ? SynthesisVoiceGender.Female
                : SynthesisVoiceGender.Male;

            return voicesResult.Voices.FirstOrDefault(v => v.Locale.Equals(language) && v.Gender == voiceGender);
        }


        public async Task<object> GetVoicesList()
        {
            using SpeechSynthesizer? synthesizer = new(_speechConfig);
            SynthesisVoicesResult? voicesResult  = await synthesizer.GetVoicesAsync();

            Dictionary<string, Dictionary<SynthesisVoiceGender, SpeechVoice>> voicesList = voicesResult
                .Voices
                .Where
                (
                    vInfo => vInfo.Locale.Equals(SpeechLocales.BOLIVIA)
                    || vInfo.Locale.Equals(SpeechLocales.CHILE)
                    || vInfo.Locale.Equals(SpeechLocales.COSTA_RICA)
                    || vInfo.Locale.Equals(SpeechLocales.ECUADOR)
                    || vInfo.Locale.Equals(SpeechLocales.EL_SALVADOR)
                    || vInfo.Locale.Equals(SpeechLocales.GUATEMALA)
                    || vInfo.Locale.Equals(SpeechLocales.HONDURAS)
                    || vInfo.Locale.Equals(SpeechLocales.MEXICO)
                    || vInfo.Locale.Equals(SpeechLocales.NICARAGUA)
                    || vInfo.Locale.Equals(SpeechLocales.PANAMA)
                    || vInfo.Locale.Equals(SpeechLocales.PERU)
                    || vInfo.Locale.Equals(SpeechLocales.PUERTO_RICO)
                    || vInfo.Locale.Equals(SpeechLocales.REPUBLICA_DOMINICANA)
                    || vInfo.Locale.Equals(SpeechLocales.USA)
                    || vInfo.Locale.Equals(SpeechLocales.VENEZUELA)
                )
                .GroupBy(vInfo => vInfo.Locale)
                .ToDictionary
                (
                    vd => vd.First().Locale,
                    vd => vd.ToDictionary(vd2 => vd2.Gender, vd2 => new SpeechVoice
                    {
                        Gender    = vd2.Gender,
                        Language  = vd2.Locale,
                        LocalName = vd2.LocalName,
                        ShortName = vd2.ShortName
                    })
                );

            return voicesList;
        }
    }
}
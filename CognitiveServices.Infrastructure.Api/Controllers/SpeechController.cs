
using CognitiveServices.Application;
using CognitiveServices.Application.Dto;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CognitiveServices.Infrastructure.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SpeechController : ControllerBase
    {
        private readonly SpeechService _voiceService;


        public SpeechController()
        {
            _voiceService = new SpeechService();
        }


        /// <summary>
        /// Obtiene las posibles opciones de voces en español soportadas por el sistema clasificadas por región y género
        /// </summary>
        /// <returns>Objeto con la lista de voces encontradas</returns>
        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> GetVoices()
        {
            var voicesList = await _voiceService.GetVoicesList();

            if (voicesList == null)
                return NoContent();

            return Ok(voicesList);
        }


        /// <summary>
        /// Convierte un texto ingresado en Audio
        /// </summary>
        /// <param name="textToConvert">Texto a convertir en audio</param>
        /// <param name="options">
        /// Opciones de selección de voz,
        /// Si no se envía esta información el sistema seleccionará por defecto la voz masculina de la región es-MX (Mexico)
        /// <see cref="SpeechOptionsDto"/>
        /// </param>
        /// <returns>El audio convertido en string base64</returns>
        [HttpPost]
        [Route("[Action]")]
        public async Task<ActionResult> TextToAudio(string textToConvert, SpeechOptionsDto options)
        {
            byte[] voiceData = await _voiceService.ConvertTextToAudio(textToConvert, options);
            return Ok(voiceData);
        }


        //[HttpGet("{id}")]
        //public string Get(int id)
        //{

        //}


        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}


        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}


        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
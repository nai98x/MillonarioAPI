using ConcursilloAPI.Helpers;
using ConcursilloAPI.Model.Database;
using ConcursilloAPI.Model.Dto;
using ConcursilloAPI.Model.Enums;
using ConcursilloAPI.Model.Other;
using ConcursilloAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ConcursilloAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PreguntasController : ControllerBase
    {
        private readonly ILogger<PreguntasController> _logger;
        private readonly PreguntasRepository _repository;

        public PreguntasController(ILogger<PreguntasController> logger, PreguntasRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddPregunta(PreguntaNewDto pregunta)
        {
            if (string.IsNullOrEmpty(pregunta.Texto))
            {
                return BadRequest(new MessageResponse("La pregunta no puede estar vacía"));
            }
            if (pregunta.Dificultad < 1 || pregunta.Dificultad > 3)
            {
                return BadRequest(new MessageResponse("La dificultad debe estar ser 1, 2 o 3"));
            }
            if(pregunta.Respuestas.Count != 4)
            {
                return BadRequest(new MessageResponse("Deben existir 4 respuestas posibles"));
            }
            if (pregunta.Respuestas.Where(x => x.Correcta).Count() != 1)
            {
                return BadRequest(new MessageResponse("Solo puede existir una respuesta correcta"));
            }

            await _repository.SetPregunta(pregunta);

            return Ok(new MessageResponse("Pregunta creada correctamente"));
        }
    }
}
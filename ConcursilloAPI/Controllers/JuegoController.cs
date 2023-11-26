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
    public class JuegoController : ControllerBase
    {
        private readonly ILogger<PreguntasController> _logger;
        private readonly CommonHelper _commonHelper;
        private readonly PreguntasRepository _repository;

        public JuegoController(ILogger<PreguntasController> logger, CommonHelper commonHelper, PreguntasRepository repository)
        {
            _logger = logger;
            _commonHelper = commonHelper;
            _repository = repository;
        }

        [HttpGet("Iniciar")]
        public IActionResult Iniciar()
        {
            int? posicion = _commonHelper.GetRonda();

            if (posicion is not null) return BadRequest(new MessageResponse("Hay una partida jugandose actualmente"));

            _commonHelper.IniciarJuego();

            return Ok();
        }

        [HttpGet("GetSiguientePregunta")]
        public async Task<IActionResult> GetSiguientePregunta()
        {
            int? posicion = _commonHelper.GetRonda();

            if (posicion is null) return BadRequest(new MessageResponse("No hay una partida jugandose actualmente"));

            Dificultad dificultad;
            switch (posicion)
            {
                case >= 1 and <= 4:
                    dificultad = Dificultad.Facil;
                    break;
                case >= 5 and <= 8:
                    dificultad = Dificultad.Intermedio;
                    break;
                case >= 9 and <= 12:
                    dificultad = Dificultad.Dificil;
                    break;
                default:
                    return BadRequest(new MessageResponse("La posicion debe estar entre 1 y 12"));
            }

            var preguntaActual = await _repository.GetPregunta(dificultad);
            var preguntaDto = preguntaActual.ToPreguntaDto();
            _commonHelper.SetPregunta(preguntaActual);
            _commonHelper.SetPreguntaDto(preguntaDto);

            return Ok(preguntaDto);
        }

        [HttpGet("ElegirRespuesta")]
        public IActionResult ElegirRespuesta(char respuesta)
        {
            var preguntaActual = _commonHelper.GetPregunta();
            var preguntaDtoActual = _commonHelper.GetPreguntaDto();

            if (preguntaActual is null || preguntaDtoActual is null) return BadRequest(new MessageResponse("No hay una partida jugandose actualmente"));

            RespuestaDto respuestaDto;
            switch (respuesta)
            {
                case 'A':
                    respuestaDto = preguntaDtoActual.RespuestaA;
                    break;
                case 'B':
                    respuestaDto = preguntaDtoActual.RespuestaB;
                    break;
                case 'C':
                    respuestaDto = preguntaDtoActual.RespuestaC;
                    break;
                case 'D':
                    respuestaDto = preguntaDtoActual.RespuestaD;
                    break;
                default:
                    return BadRequest(new MessageResponse("La respuesta debe ser A, B, C o D"));
            }

            Respuesta respuestaElegida = preguntaActual.Respuestas.First(x => x.Texto == respuestaDto.Respuesta);

            if (respuestaElegida.Correcta)
            {
                _commonHelper.AvanzarRonda();
                return Ok(new MessageResponse("Respuesta correcta"));
            }
            else
            {
                _commonHelper.FinalizarJuego();

                var respuestaCorrectaTexto = preguntaActual.Respuestas.Where(x => x.Correcta).First();
                char respuestaCorrecta = _commonHelper.GetLetraRespuesta(respuestaCorrectaTexto, preguntaDtoActual);

                return Ok(new JuegoFinalizadoResponse("Respuesta incorrecta", respuestaCorrecta));
            }
        }

        [HttpGet("Comodin5050")]
        public IActionResult Comodin5050()
        {
            var preguntaActual = _commonHelper.GetPregunta();
            var preguntaDtoActual = _commonHelper.GetPreguntaDto();

            if (preguntaActual is null || preguntaDtoActual is null) return BadRequest(new MessageResponse("No hay una partida jugandose actualmente"));

            var respuestasErroneas = preguntaActual.Respuestas.Where(x => !x.Correcta).ToList();
            respuestasErroneas.Shuffle();

            char resp1 = _commonHelper.GetLetraRespuesta(respuestasErroneas[0], preguntaDtoActual);
            char resp2 = _commonHelper.GetLetraRespuesta(respuestasErroneas[1], preguntaDtoActual);

            return Ok(new Comodin5050Response(resp1, resp2));
        }
    }
}
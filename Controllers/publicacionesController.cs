using L01_2021CO601_2019AC603.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace L01_2021CO601_2019AC603.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class publicacionesController : ControllerBase
    {

        private readonly blogDBContext _blogDBContext;

        public publicacionesController(blogDBContext blogDBContexto)
        {
            _blogDBContext = blogDBContexto;


        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<publicaciones> listadoPublicaciones = (from e in _blogDBContext.publicaciones select e).ToList();

            if (listadoPublicaciones.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoPublicaciones);
        }

        [HttpGet]
        [Route("GetById/{usuarioid}")]

        public IActionResult Get(int usuarioid)
        {
            publicaciones? publicacion = (from e in _blogDBContext.publicaciones where e.usuarioId == usuarioid select e).FirstOrDefault();

            if (publicacion == null)
            {
                return NotFound();
            }

            return Ok(publicacion);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarPublicacion([FromBody] publicaciones publicacion)
        {
            try
            {

                _blogDBContext.publicaciones.Add(publicacion);
                _blogDBContext.SaveChanges();
                return Ok(publicacion);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarPublicaciones(int id, [FromBody] publicaciones publicacionModificar)
        {
            publicaciones? publicacionActual = (from e in _blogDBContext.publicaciones where e.usuarioId == id select e).FirstOrDefault();

            if (publicacionActual == null)
            {
                return NotFound();
            }

            publicacionActual.titulo = publicacionModificar.titulo;
            publicacionActual.descripcion = publicacionModificar.descripcion;
            publicacionActual.usuarioId = publicacionModificar.usuarioId;

            return Ok(publicacionModificar);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]

        public IActionResult EliminarPublicacion(int id)
        {
            publicaciones? publicacion = (from e in _blogDBContext.publicaciones where e.usuarioId == id select e).FirstOrDefault();

            if (publicacion == null)
            {
                return NotFound();
            }

            _blogDBContext.publicaciones.Attach(publicacion);
            _blogDBContext.publicaciones.Remove(publicacion);
            _blogDBContext.SaveChanges();

            return Ok(publicacion);
        }

    }
}

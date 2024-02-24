using L01_2021CO601_2019AC603.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace L01_2021CO601_2019AC603.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {

        private readonly blogDBContext _blogDBContext;

        public comentariosController(blogDBContext blogDBContexto)
        {
            _blogDBContext = blogDBContexto;


        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<comentarios> listadocomentarios = (from e in _blogDBContext.comentarios select e).ToList();

            if (listadocomentarios.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadocomentarios);
        }

        [HttpGet]
        [Route("GetById/{publicacionId}")]

        public IActionResult Get(int publicacionId)
        {
            comentarios? comentario = (from e in _blogDBContext.comentarios where e.publicacionId == publicacionId select e).FirstOrDefault();

            if (comentario == null)
            {
                return NotFound();
            }

            return Ok(comentario);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarComentario([FromBody] comentarios comentario)
        {
            try
            {

                _blogDBContext.comentarios.Add(comentario);
                _blogDBContext.SaveChanges();
                return Ok(comentario);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarComentario(int id, [FromBody] comentarios comentarioModificar)
        {
            comentarios? comentarioActual = (from e in _blogDBContext.comentarios where e.cometarioId == id select e).FirstOrDefault();

            if (comentarioActual == null)
            {
                return NotFound();
            }

            comentarioActual.publicacionId = comentarioModificar.publicacionId;
            comentarioActual.comentario = comentarioModificar.comentario;
            comentarioActual.usuarioId = comentarioModificar.usuarioId;

            return Ok(comentarioModificar);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]

        public IActionResult EliminarComentario(int id)
        {
            comentarios? comentario = (from e in _blogDBContext.comentarios where e.cometarioId == id select e).FirstOrDefault();

            if (comentario == null)
            {
                return NotFound();
            }

            _blogDBContext.comentarios.Attach(comentario);
            _blogDBContext.comentarios.Remove(comentario);
            _blogDBContext.SaveChanges();

            return Ok(comentario);
        }


    }
}

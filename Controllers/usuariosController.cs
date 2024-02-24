using L01_2021CO601_2019AC603.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace L01_2021CO601_2019AC603.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {

        private readonly blogDBContext _blogDBContext;

        public usuariosController(blogDBContext blogDBContexto)
        {
            _blogDBContext = blogDBContexto;


        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<usuarios> listadoUsuarios = (from e in _blogDBContext.usuarios select e).ToList();

            if (listadoUsuarios.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoUsuarios);
        }

        [HttpGet]
        [Route("GetById/{nombre} & {apellido}")]

        public IActionResult Get(string nombre, string apellido)
        {
            usuarios? usuario = (from e in _blogDBContext.usuarios where e.nombre == nombre where e.apellido == apellido select e).FirstOrDefault();

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpGet]
        [Route("GetById/{rol}")]

        public IActionResult Get(int rol)
        {
            usuarios? usuario = (from e in _blogDBContext.usuarios where e.rolId == rol select e).FirstOrDefault();

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarUsuario([FromBody] usuarios usuario)
        {
            try
            {

                _blogDBContext.usuarios.Add(usuario);
                _blogDBContext.SaveChanges();
                return Ok(usuario);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarUsuarios(int id, [FromBody] usuarios usuariosModificar)
        {
            usuarios? usuarioActual = (from e in _blogDBContext.usuarios where e.usuarioId == id select e).FirstOrDefault();

            if (usuarioActual == null)
            {
                return NotFound();
            }

            usuarioActual.rolId = usuariosModificar.rolId;
            usuarioActual.nombreUsuario = usuariosModificar.nombreUsuario;
            usuarioActual.clave = usuariosModificar.clave;
            usuarioActual.nombre = usuariosModificar.nombre;
            usuarioActual.apellido = usuariosModificar.apellido;

            return Ok(usuariosModificar);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]

        public IActionResult EliminarUsuarios(int id)
        {
            usuarios? usuario = (from e in _blogDBContext.usuarios where e.usuarioId == id select e).FirstOrDefault();

            if (usuario == null)
            {
                return NotFound();
            }

            _blogDBContext.usuarios.Attach(usuario);
            _blogDBContext.usuarios.Remove(usuario);
            _blogDBContext.SaveChanges();

            return Ok(usuario);
        }

    


}
}

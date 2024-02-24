using System.ComponentModel.DataAnnotations;

namespace L01_2021CO601_2019AC603.Models
{
    public class publicaciones
    {

        [Key]
        public int publicacionId { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public int usuarioId { get; set; }

    }
}

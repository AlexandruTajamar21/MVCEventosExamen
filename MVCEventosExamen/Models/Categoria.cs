using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APISegundoExamen.Models
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
    }
}

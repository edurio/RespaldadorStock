using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespaldadorStock.Entity
{
    public class Bitacora
    {
        public int Id { get; set; }
        public int Fecha { get; set; }
        public string Glosa { get; set; }
        public bool EsError { get; set; }

    }
}

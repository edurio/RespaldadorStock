using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespaldadorStock.Entity
{
    public class Stock
    {
        public int Contador { get; set; }
        public int ProdId { get; set; }
        public int EmpId { get; set; }

        public int BodeId { get; set; }
        public int Fecha { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Lote { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal StockLote { get; set; }
        public double Valor { get; set; }

    }
}

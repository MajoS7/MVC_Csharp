using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchivoJson.Models.Entities
{
    public class invoice_Detail
    {
        public int id { get; set; }
        public int id_invoice { get; set; }
        public string description { get; set; }
        public int value { get; set; }

    }
}

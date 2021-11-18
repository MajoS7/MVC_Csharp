using ArchivoJson.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace archivojson.Models.Entities
{
    public class invoice
    {
        public int id { get; set; }
        public int id_client { get; set; }

        public string cod { get; set; }

        public List<invoice_Detail> listInvoiceDetail { get; set; }

    }
}

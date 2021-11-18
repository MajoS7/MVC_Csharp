using archivojson.Models;
using archivojson.Models.Entities;
using ArchivoJson.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient.Memcached;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace archivojson.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        baseDatos bd = new baseDatos();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public string insertClient([FromBody] client cliente)
        {
            string sql = "INSERT INTO facturas.client (name,last_name,document_id) VALUES ('" + cliente.name + "','" + cliente.lastName + "','" + cliente.documentId + "');";
            string resultado = bd.ejecutarSQL(sql);
            return resultado;
        }

        public string insertInvoice([FromBody] invoice factura)
        {
            string sql = "INSERT INTO invoice (id_client,cod) VALUES(" + factura.id_client + ",'" + factura.cod + "');";

            sql += "SELECT @@identity AS id;";

            foreach (invoice_Detail ID in factura.listInvoiceDetail)
            {
                sql += "insert into invoice_detail (id_invoice, description, value) values (@@identity,'" + ID.description + "'," + ID.value + ");";
            }

            string resultado = bd.ejecutarSQL(sql);

            return resultado;
        }

        public List<client> showClient(int Id)
        {
            string sql = "select * from facturas.client where id='" + Id + "';";
            DataTable datosTabla = bd.createTable(sql);
            List<client> listclient = new List<client>();
            listclient = (from DataRow filasDatos in datosTabla.Rows
                          select new client()
                          {
                              id = Convert.ToInt32(filasDatos["id"]),
                              name = filasDatos["name"].ToString(),
                              lastName = filasDatos["last_name"].ToString(),
                              documentId = filasDatos["document_id"].ToString(),

                          }).ToList();
            return listclient;
        }

        public List<invoice> showInvoice(int Id)
        {

            string sql = "select inv.*, invd.* from facturas.invoice inv left join invoice_detail invd on invd.id_invoice = inv.id where inv.id ='" + Id + "';";
            DataTable datosTabla = bd.createTable(sql);
            List<invoice> listinvoice = new List<invoice>();
            List<invoice_Detail> listInvoiceDetails = new List<invoice_Detail>();
            listInvoiceDetails = (from DataRow filasDatos in datosTabla.Rows
                                  select new invoice_Detail()
                                  {
                                      id = Convert.ToInt32(filasDatos["id_di"]),
                                      id_invoice = Convert.ToInt32(filasDatos["id_invoice"]),
                                      description = filasDatos["description"].ToString(),
                                      value = Convert.ToInt32(filasDatos["value"])
                                  }).ToList();
            listinvoice = (from DataRow filasDatos in datosTabla.Rows
                           select new invoice()
                           {
                               id = Convert.ToInt32(filasDatos["id"]),
                               id_client = Convert.ToInt32(filasDatos["id_client"]),
                               cod = filasDatos["cod"].ToString(),
                               listInvoiceDetail=listInvoiceDetails
                           }).ToList();

            return listinvoice;
        }

        public List<client> listShowClient()
        {
            string sql = "select * from facturas.client";
            DataTable datosTabla = bd.createTable(sql);

            List<client> listclients = new List<client>();

            listclients= (from DataRow filasDatos in datosTabla.Rows
                          select new client()
                          {
                              id = Convert.ToInt32(filasDatos["id"]),
                              name = filasDatos["name"].ToString(),
                              lastName = filasDatos["last_name"].ToString(),
                              documentId = filasDatos["document_id"].ToString(),

                          }).ToList();

            return listclients;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

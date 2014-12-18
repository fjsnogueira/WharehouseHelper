using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;
using System.Net.Http.Formatting;

namespace FirstREST.Controllers
{
    public class DocCompraController : ApiController
    {
        public IEnumerable<Lib_Primavera.Model.DocCompra> Get()
        {
            return Lib_Primavera.Comercial.VGR_List();
        }

        public HttpResponseMessage Post(EncomendaRecepcionada encomenda)
        {
            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();
            Lib_Primavera.Model.DocCompra docCompra = Lib_Primavera.Comercial.getEncomenda(encomenda.idEncomenda);
            if (docCompra.id != "")
            {
                Lib_Primavera.Comercial.updateEncomenda(docCompra, encomenda);
                erro = Lib_Primavera.Comercial.VGR_New(docCompra);

                if (erro.Erro == 0)
                {
                    var response = Request.CreateResponse(
                      HttpStatusCode.Created, docCompra.id);
                    //string uri = Url.Link("DefaultApi", new { DocId = docCompra.id });
                    //response.Headers.Location = new Uri(uri);
                    return response;
                }
                else
                {
                    Console.WriteLine(erro.Descricao);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, erro.Descricao);
                }
            }
            else
            {
                Console.WriteLine(erro.Descricao);
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro.Descricao);
            }

        }

    }
}

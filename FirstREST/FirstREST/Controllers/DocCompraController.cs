using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;
using FirstREST.Lib_Primavera.Model;
using System.Net.Http.Formatting;
using System.Text.RegularExpressions;

namespace FirstREST.Controllers
{
    public class DocCompraController : ApiController
    {
        public IEnumerable<Lib_Primavera.Model.DocCompra> Get()
        {
            return Lib_Primavera.Comercial.VGR_List();
        }

        // GET api/doccompra/5 
        public DocCompra Get(string id)
        {
            Lib_Primavera.Model.DocCompra docCompra = Lib_Primavera.Comercial.getEncomenda(id);
            if (String.Equals(docCompra, ""))
            {
                throw new HttpResponseException(
                    Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return docCompra;
            }
        }

        public HttpResponseMessage Post(EncomendaRecepcionada encomenda)
        {
            HttpRequestHeaders aux = Request.Headers;
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
                    erro.Status = false;
                    return Request.CreateResponse(HttpStatusCode.Accepted, erro);
                }
            }
            else
            {
                erro.Status=false;
                return Request.CreateResponse(HttpStatusCode.Accepted, erro);
            }

        }

    }
}

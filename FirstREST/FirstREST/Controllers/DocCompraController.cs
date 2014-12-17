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
        /*
        public HttpResponseMessage Post(Lib_Primavera.Model.DocCompra dc)
        {
            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();
            erro = Lib_Primavera.Comercial.VGR_New(dc);

            if (erro.Erro == 0)
            {
                var response = Request.CreateResponse( HttpStatusCode.Created , dc.id ) ;
                string uri = Url.Link("DefaultApi", new { DocId = dc.id });
                response.Headers.Location = new Uri(uri);
                return response;
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }
        */

        public HttpResponseMessage Post(EncomendaRecepcionada encomenda)
        {
            this.Request.Content.ToString();
            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();
            erro.Erro = 1;
            erro.Descricao = encomenda.ToString();

            
            //erro = Lib_Primavera.Comercial.InserirFactura_New(doc, art);

            if (erro.Erro == 0)
            {
                //var response = Request.CreateResponse(HttpStatusCode.Created, dc.id);
                //string uri = Url.Link("DefaultApi", new { DocId = dc.id });
                //response.Headers.Location = new Uri(uri);
                return Request.CreateResponse(HttpStatusCode.Created, encomenda.ToString()); ;
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }

    }
}

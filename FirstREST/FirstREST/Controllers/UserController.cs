using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;

namespace FirstREST.Controllers
{
    public class LoginController : ApiController
    {
        public HttpResponseMessage Post(Lib_Primavera.Model.Login user) {
            Lib_Primavera.Model.RespostaErro erro = Lib_Primavera.Comercial.isValid(user);
            if (erro.Status == true)
            {
                var response = Request.CreateResponse(HttpStatusCode.Accepted,erro);
                return response;
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro);
            }
        }
    }
}
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
        public HttpResponseMessage Post(Lib_Primavera.Model.Login user)
        {
            Lib_Primavera.Model.RespostaErro erro = Lib_Primavera.Comercial.isValid(user);
            if (erro.Status == true)
            {
                var response = Request.CreateResponse(HttpStatusCode.Accepted, erro);
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro);
            }
        }
    }

    public class FornecedoresController : ApiController
    {
        // GET api/Fornecedor/5    
        public Fornecedor Get(string id)
        {
            Lib_Primavera.Model.Fornecedor fornecedor = Lib_Primavera.Comercial.GetFornecedor(id);
            if (fornecedor == null)
            {
                throw new HttpResponseException(
                  Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return fornecedor;
            }
        }
    }

    public class ArmazemController : ApiController {
        
        public Armazem Get(string id)
        {
            Lib_Primavera.Model.Armazem armazem = Lib_Primavera.Comercial.GetArmazem(id);
            if (armazem == null)
            {
                throw new HttpResponseException(
                  Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return armazem;
            }
        }
    }

    public class SearchController : ApiController
    {
        public Search Get(string id)
        {
            Lib_Primavera.Model.Search resultado = Lib_Primavera.Comercial.search(id);
            if (resultado == null)
            {
                throw new HttpResponseException(
                  Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return resultado;
            }
        }

    }

}
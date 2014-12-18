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
            Lib_Primavera.Model.RespostaErro erro = Lib_Primavera.Comercial.Login(user);
            if (erro.Status == true)
            {
                Dictionary<string, string> response = new Dictionary<string, string>();
                response.Add("username", user.username);
                response.Add("session", erro.Descricao);
                return Request.CreateResponse(HttpStatusCode.Accepted, response, Configuration.Formatters.JsonFormatter);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro);
            }
        }
    }

    public class LogoutController : ApiController
    {
        public HttpResponseMessage Post(Lib_Primavera.Model.Login user)
        {
            if (Request.Headers.Contains("session"))
            {
                string token = Request.Headers.GetValues("session").First();
                Lib_Primavera.Model.RespostaErro erro = Lib_Primavera.Comercial.Logout(token, user);
                if (erro.Status == true)
                {
                    Dictionary<string, string> response = new Dictionary<string, string>();
                    response.Add("username", user.username);
                    response.Add("session", erro.Descricao);
                    return Request.CreateResponse(HttpStatusCode.Accepted, response, Configuration.Formatters.JsonFormatter);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, erro);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "No session token found");
            }
        }
    }

    public class FornecedorController : ApiController
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
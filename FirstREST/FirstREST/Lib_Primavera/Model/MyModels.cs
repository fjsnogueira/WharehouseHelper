using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class Login
    {
        public string username
        {
            get;
            set;
        }

        public string password
        {
            get;
            set;
        }
    }

    public class SessionModel
    {
        public string UserName { get; set; }
        public string Armazem { get; set; }
        public string Session_Val { get; set; }
    }

    public class Fornecedor
    {
        public string id
        {
            get;
            set;
        }

        public string nome
        {
            get;
            set;
        }

        public string morada
        {
            get;
            set;
        }

        public string local {
            get;
            set;
        }

        public string codpostal {
            get;
            set;
        }

        public string pais {
            get;
            set;
        }
        public string numContrib
        {
            get;
            set;
        }
    }

    public class Armazem
    {
        public string id
        {
            get;
            set;
        }

        public string descricao
        {
            get;
            set;
        }

        public string morada
        {
            get;
            set;
        }

        public string localidade
        {
            get;
            set;
        }

        public string Cp
        {
            get;
            set;
        }

        public string CpLocalidade
        {
            get;
            set;
        }
    }

    public class LinhaDocCompraStatus
    {
        public string EstadoTrans
        {
            get;
            set;
        }

        public double QuantTrans
        {
            get;
            set;
        }
    }

    public class Search
    {

        public List<int> Encomendas
        {
            get;
            set;
        }

        public Dictionary<string, List<int>> Artigos
        {
            get;
            set;
        }

        public Dictionary<string, List<int>> Fornecedores
        {
            get;
            set;
        }

        public Dictionary<string, List<int>> Armazens
        {
            get;
            set;
        }

    }

    public class ArtigosRecepcionados
    {
        public string idArtigo
        {
            get;
            set;
        }

        public int quantidade
        {
            get;
            set;
        }
    }

    public class EncomendaRecepcionada
    {

        public string idEncomenda
        {
            get;
            set;
        }

        public List<Model.ArtigosRecepcionados> artigos
        {
            get;
            set;
        }

    }
}
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

        public string armazem
        {
            get;
            set;
        }

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

    public class Search {

        public List<Model.Artigo> Artigos
        {
            get;
            set;
        }

        public List<Model.Fornecedor> Fornecedores
        {
            get;
            set;
        }

        public List<Model.DocCompra> Encomendas
        {
            get;
            set;
        }
    }
}
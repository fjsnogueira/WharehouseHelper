using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class DocCompra
    {

        public string TipoDoc
        {
            get;
            set;
        }

        public string NumDocExterno
        {
            get;
            set;
        }

        public string id
        {
            get;
            set;
        }

        public int NumDoc
        {
            get;
            set;
        }

        public string Entidade
        {
            get;
            set;
        }

        public DateTime DataEmissao
        {
            get;
            set;
        }

        public double TotalMerc
        {
            get;
            set;
        }

        public string Serie
        {
            get;
            set;
        }

        public DateTime DataVencimento { get; set; }

        public List<Model.LinhaDocCompra> LinhasDoc
        {
            get;
            set;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{

    public class LinhaDocCompra
    {

        public int NumLinha
        {
            get;
            set;
        }

        public string CodArtigo
        {
            get;
            set;
        }

        public double Quantidade
        {
            get;
            set;
        }

        public string Armazem
        {
            get;
            set;
        }

        public double Desconto
        {
            get;
            set;
        }

        public double PrecoUnitario
        {
            get;
            set;
        }

        public Model.LinhaDocCompraStatus Status
        {
            get;
            set;
        }
    }
}
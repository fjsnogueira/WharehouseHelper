using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Interop.ErpBS800;
using Interop.StdPlatBS800;
using Interop.StdBE800;
using Interop.GcpBE800;
using ADODB;
using Interop.IGcpBS800;
using System.Text.RegularExpressions;

namespace FirstREST.Lib_Primavera
{
    public class Comercial
    {
        private static String NomeEmpresa = "SINF";
        private static String UtilizadorEmpresa = "";
        private static String PasswordEmpresa = "";
        private static Dictionary<string, Lib_Primavera.Model.SessionModel> Session = new Dictionary<string, Model.SessionModel>();

        # region Cliente

        public static List<Model.Cliente> ListaClientes()
        {
            ErpBS objMotor = new ErpBS();

            StdBELista objList;

            Model.Cliente cli = new Model.Cliente();
            List<Model.Cliente> listClientes = new List<Model.Cliente>();


            if (PriEngine.InitializeCompany(NomeEmpresa, UtilizadorEmpresa, PasswordEmpresa) == true)
            {

                //objList = PriEngine.Engine.Comercial.Clientes.LstClientes();

                objList = PriEngine.Engine.Consulta("SELECT Cliente, Nome, Moeda, NumContrib as NumContribuinte FROM  CLIENTES");

                while (!objList.NoFim())
                {
                    cli = new Model.Cliente();
                    cli.CodCliente = objList.Valor("Cliente");
                    cli.NomeCliente = objList.Valor("Nome");
                    cli.Moeda = objList.Valor("Moeda");
                    cli.NumContribuinte = objList.Valor("NumContribuinte");

                    listClientes.Add(cli);
                    objList.Seguinte();

                }

                return listClientes;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.Cliente GetCliente(string codCliente)
        {
            ErpBS objMotor = new ErpBS();

            GcpBECliente objCli = new GcpBECliente();


            Model.Cliente myCli = new Model.Cliente();

            if (PriEngine.InitializeCompany(NomeEmpresa, UtilizadorEmpresa, PasswordEmpresa) == true)
            {

                if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == true)
                {
                    objCli = PriEngine.Engine.Comercial.Clientes.Edita(codCliente);
                    myCli.CodCliente = objCli.get_Cliente();
                    myCli.NomeCliente = objCli.get_Nome();
                    myCli.Moeda = objCli.get_Moeda();
                    myCli.NumContribuinte = objCli.get_NumContribuinte();
                    return myCli;
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.RespostaErro UpdCliente(Lib_Primavera.Model.Cliente cliente)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            ErpBS objMotor = new ErpBS();

            GcpBECliente objCli = new GcpBECliente();

            try
            {

                if (PriEngine.InitializeCompany(NomeEmpresa, UtilizadorEmpresa, PasswordEmpresa) == true)
                {

                    if (PriEngine.Engine.Comercial.Clientes.Existe(cliente.CodCliente) == false)
                    {
                        erro.Status = false;
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {

                        objCli = PriEngine.Engine.Comercial.Clientes.Edita(cliente.CodCliente);
                        objCli.set_EmModoEdicao(true);

                        objCli.set_Nome(cliente.NomeCliente);
                        objCli.set_NumContribuinte(cliente.NumContribuinte);
                        objCli.set_Moeda(cliente.Moeda);

                        PriEngine.Engine.Comercial.Clientes.Actualiza(objCli);

                        erro.Status = true;
                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }
                else
                {
                    erro.Status = false;
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;

                }

            }

            catch (Exception ex)
            {
                erro.Status = false;
                erro.Erro = 2;
                erro.Descricao = ex.Message;
                return erro;
            }

        }

        public static Lib_Primavera.Model.RespostaErro DelCliente(string codCliente)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBECliente objCli = new GcpBECliente();

            try
            {
                if (PriEngine.InitializeCompany(NomeEmpresa, UtilizadorEmpresa, PasswordEmpresa) == true)
                {
                    if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == false)
                    {
                        erro.Status = false;
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {
                        PriEngine.Engine.Comercial.Clientes.Remove(codCliente);
                        erro.Status = true;
                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }

                else
                {
                    erro.Status = false;
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Status = false;
                erro.Erro = 2;
                erro.Descricao = ex.Message;
                return erro;
            }

        }

        public static Lib_Primavera.Model.RespostaErro InsereClienteObj(Model.Cliente cli)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();

            GcpBECliente myCli = new GcpBECliente();

            try
            {
                if (PriEngine.InitializeCompany(NomeEmpresa, UtilizadorEmpresa, PasswordEmpresa) == true)
                {

                    myCli.set_Cliente(cli.CodCliente);
                    myCli.set_Nome(cli.NomeCliente);
                    myCli.set_NumContribuinte(cli.NumContribuinte);
                    myCli.set_Moeda(cli.Moeda);

                    PriEngine.Engine.Comercial.Clientes.Actualiza(myCli);

                    erro.Status = true;
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Status = false;
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Status = false;
                erro.Erro = 2;
                erro.Descricao = ex.Message;
                return erro;
            }


        }

        #endregion Cliente;   // -----------------------------  END   CLIENTE    -----------------------

        #region Artigos

        public static Lib_Primavera.Model.Artigo GetArtigo(string codArtigo)
        {
            GcpBEArtigo objArtigo = new GcpBEArtigo();
            Model.Artigo myArt = new Model.Artigo();

            if (PriEngine.InitializeCompany(NomeEmpresa, UtilizadorEmpresa, PasswordEmpresa) == true)
            {

                if (PriEngine.Engine.Comercial.Artigos.Existe(codArtigo) == false)
                {
                    return null;
                }
                else
                {
                    objArtigo = PriEngine.Engine.Comercial.Artigos.Edita(codArtigo);
                    myArt.CodArtigo = objArtigo.get_Artigo();
                    myArt.DescArtigo = objArtigo.get_Descricao();
                    myArt.CodBarras = objArtigo.get_CodBarras();

                    return myArt;
                }

            }
            else
            {
                return null;
            }

        }

        public static List<Model.Artigo> ListaArtigos()
        {
            ErpBS objMotor = new ErpBS();

            StdBELista objList;

            Model.Artigo art = new Model.Artigo();
            List<Model.Artigo> listArts = new List<Model.Artigo>();

            if (PriEngine.InitializeCompany(NomeEmpresa, UtilizadorEmpresa, PasswordEmpresa) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT dbo.Artigo.Artigo, dbo.Artigo.Descricao, dbo.Artigo.CodBarras FROM dbo.Artigo");

                while (!objList.NoFim())
                {
                    art = new Model.Artigo();
                    art.CodArtigo = objList.Valor("artigo");
                    art.DescArtigo = objList.Valor("descricao");
                    art.CodBarras = objList.Valor("CodBarras");
                    listArts.Add(art);
                    objList.Seguinte();
                }

                return listArts;

            }
            else
            {
                return null;

            }

        }

        #endregion Artigos

        #region Fornecedores

        public static Lib_Primavera.Model.Fornecedor GetFornecedor(string codFornecedor)
        {
            string query = "SELECT fornecedor, nome FROM dbo.Fornecedores WHERE dbo.Fornecedores.Fornecedor='" + codFornecedor + "'";
            ErpBS objMotor = new ErpBS();
            StdBELista objList;
            Model.Fornecedor fornecedor = new Model.Fornecedor();

            if (PriEngine.InitializeCompany(NomeEmpresa, UtilizadorEmpresa, PasswordEmpresa) == true)
            {
                objList = PriEngine.Engine.Consulta(query);
                if (!objList.NoFim())
                {
                    fornecedor.id = objList.Valor("fornecedor");
                    fornecedor.nome = objList.Valor("nome");
                }
            }

            return fornecedor;
        }

        #endregion Fornecedores

        #region Armazem

        public static Lib_Primavera.Model.Armazem GetArmazem(string codArmazem)
        {
            Model.Armazem armazem = new Model.Armazem();
            string query = "SELECT Armazem, Descricao, Morada, Localidade, Cp, CpLocalidade FROM dbo.Armazens WHERE dbo.Armazens.Armazem='" + codArmazem + "'";
            ErpBS objMotor = new ErpBS();
            StdBELista objList;

            if (PriEngine.InitializeCompany(NomeEmpresa, UtilizadorEmpresa, PasswordEmpresa) == true)
            {
                objList = PriEngine.Engine.Consulta(query);
                if (!objList.NoFim())
                {
                    armazem.id = objList.Valor("Armazem");
                    armazem.descricao = objList.Valor("Descricao");
                    armazem.morada = objList.Valor("Morada");
                    armazem.localidade = objList.Valor("Localidade");
                    armazem.Cp = objList.Valor("Cp");
                    armazem.CpLocalidade = objList.Valor("CpLocalidade");
                }
            }

            return armazem;
        }


        #endregion armazem

        #region DocumendosCompra

        public static List<Model.DocCompra> VGR_List()
        {
            string query = "SELECT PRISINF.dbo.CabecCompras.TipoDoc, PRISINF.dbo.CabecCompras.id, PRISINF.dbo.CabecCompras.NumDoc, PRISINF.dbo.CabecCompras.Entidade, PRISINF.dbo.CabecCompras.DataDoc,PRISINF.dbo.CabecCompras.NumDocExterno, PRISINF.dbo.CabecCompras.TotalMerc,PRISINF.dbo.CabecCompras.Serie, PRISINF.dbo.LinhasCompras.NumLinha, PRISINF.dbo.LinhasCompras.Artigo, PRISINF.dbo.LinhasCompras.Quantidade, PRISINF.dbo.LinhasCompras.Desconto1, PRISINF.dbo.LinhasCompras.PrecUnit, PRISINF.dbo.LinhasCompras.Armazem, PRISINF.dbo.LinhasComprasStatus.EstadoTrans, PRISINF.dbo.LinhasComprasStatus.QuantTrans FROM PRISINF.dbo.CabecCompras INNER JOIN PRISINF.dbo.LinhasCompras ON PRISINF.dbo.CabecCompras.Id = PRISINF.dbo.LinhasCompras.IdCabecCompras INNER JOIN PRISINF.dbo.LinhasComprasStatus ON PRISINF.dbo.LinhasCompras.Id = PRISINF.dbo.LinhasComprasStatus.IdLinhasCompras WHERE (PRISINF.dbo.CabecCompras.TipoDoc = N'ECF' AND PRISINF.dbo.LinhasComprasStatus.EstadoTrans = 'P') ORDER BY PRISINF.dbo.CabecCompras.NumDoc";
            ErpBS objMotor = new ErpBS();
            StdBELista objList;

            List<Model.DocCompra> listDocCompra = new List<Model.DocCompra>();
            Model.DocCompra docCompra;
            List<Model.LinhaDocCompra> listLinhasCompras;
            Model.LinhaDocCompra linhaDocCompra;
            Model.LinhaDocCompraStatus statusLinhaCompra;

            if (PriEngine.InitializeCompany(NomeEmpresa, UtilizadorEmpresa, PasswordEmpresa) == true)
            {
                objList = PriEngine.Engine.Consulta(query);

                if (!objList.NoFim()) //tem pelo menos 1 elemento
                {
                    docCompra = new Model.DocCompra();
                    docCompra.TipoDoc = objList.Valor("TipoDoc");
                    docCompra.id = objList.Valor("id");
                    docCompra.Entidade = objList.Valor("Entidade");
                    docCompra.NumDoc = objList.Valor("NumDoc");
                    docCompra.DataEmissao = objList.Valor("DataDoc");

                    docCompra.NumDocExterno = objList.Valor("NumDocExterno");
                    docCompra.TotalMerc = objList.Valor("TotalMerc");
                    docCompra.Serie = objList.Valor("Serie");

                    listLinhasCompras = new List<Model.LinhaDocCompra>();

                    //sacar linhaDoc associado + status
                    linhaDocCompra = new Model.LinhaDocCompra();
                    linhaDocCompra.NumLinha = objList.Valor("NumLinha");
                    linhaDocCompra.CodArtigo = objList.Valor("Artigo");
                    linhaDocCompra.Quantidade = objList.Valor("Quantidade");
                    linhaDocCompra.Armazem = objList.Valor("Armazem");
                    statusLinhaCompra = new Model.LinhaDocCompraStatus();
                    statusLinhaCompra.EstadoTrans = objList.Valor("EstadoTrans");
                    statusLinhaCompra.QuantTrans = objList.Valor("QuantTrans");
                    linhaDocCompra.Status = statusLinhaCompra;

                    listLinhasCompras.Add(linhaDocCompra);

                    objList.Seguinte();

                    while (!objList.NoFim()) //restantes elementos
                    {
                        if (docCompra.id != objList.Valor("id"))
                        {
                            docCompra.LinhasDoc = listLinhasCompras;
                            listDocCompra.Add(docCompra);

                            docCompra = new Model.DocCompra();
                            docCompra.TipoDoc = objList.Valor("TipoDoc");
                            docCompra.id = objList.Valor("id");
                            docCompra.Entidade = objList.Valor("Entidade");
                            docCompra.NumDoc = objList.Valor("NumDoc");
                            docCompra.DataEmissao = objList.Valor("DataDoc");
                            listLinhasCompras = new List<Model.LinhaDocCompra>();
                        }
                        //sacar linhas e status
                        linhaDocCompra = new Model.LinhaDocCompra();
                        linhaDocCompra.NumLinha = objList.Valor("NumLinha");
                        linhaDocCompra.CodArtigo = objList.Valor("Artigo");
                        linhaDocCompra.Quantidade = objList.Valor("Quantidade");
                        linhaDocCompra.Armazem = objList.Valor("Armazem");
                        statusLinhaCompra = new Model.LinhaDocCompraStatus();
                        statusLinhaCompra.EstadoTrans = objList.Valor("EstadoTrans");
                        statusLinhaCompra.QuantTrans = objList.Valor("QuantTrans");
                        linhaDocCompra.Status = statusLinhaCompra;

                        listLinhasCompras.Add(linhaDocCompra);

                        objList.Seguinte();
                    }
                    docCompra.LinhasDoc = listLinhasCompras;
                    listDocCompra.Add(docCompra);
                }
            }
            return listDocCompra;
        }

        public static Model.DocCompra getEncomenda(string codBarValue)
        {
            string[] substrings = Regex.Split(codBarValue, "ECF-");
            string query = "SELECT PRISINF.dbo.CabecCompras.TipoDoc, PRISINF.dbo.CabecCompras.id, PRISINF.dbo.CabecCompras.NumDoc, PRISINF.dbo.CabecCompras.Entidade, PRISINF.dbo.CabecCompras.DataDoc,PRISINF.dbo.CabecCompras.NumDocExterno, PRISINF.dbo.CabecCompras.TotalMerc,PRISINF.dbo.CabecCompras.Serie, PRISINF.dbo.LinhasCompras.NumLinha, PRISINF.dbo.LinhasCompras.Artigo, PRISINF.dbo.LinhasCompras.Quantidade, PRISINF.dbo.LinhasCompras.Desconto1, PRISINF.dbo.LinhasCompras.PrecUnit, PRISINF.dbo.LinhasCompras.Armazem, PRISINF.dbo.LinhasComprasStatus.EstadoTrans, PRISINF.dbo.LinhasComprasStatus.QuantTrans FROM PRISINF.dbo.CabecCompras INNER JOIN PRISINF.dbo.LinhasCompras ON PRISINF.dbo.CabecCompras.Id = PRISINF.dbo.LinhasCompras.IdCabecCompras INNER JOIN PRISINF.dbo.LinhasComprasStatus ON PRISINF.dbo.LinhasCompras.Id = PRISINF.dbo.LinhasComprasStatus.IdLinhasCompras WHERE (PRISINF.dbo.CabecCompras.TipoDoc = N'ECF' AND PRISINF.dbo.LinhasComprasStatus.EstadoTrans = 'P' AND dbo.CabecCompras.NumDoc like '" + substrings[1] + "') ORDER BY PRISINF.dbo.CabecCompras.NumDoc";
            ErpBS objMotor = new ErpBS();
            StdBELista objList;

            Model.DocCompra result = new Model.DocCompra();
            Model.LinhaDocCompra linhaDocCompra;
            Model.LinhaDocCompraStatus statusLinhaCompra;
            if (PriEngine.InitializeCompany(NomeEmpresa, UtilizadorEmpresa, PasswordEmpresa) == true)
            {
                objList = PriEngine.Engine.Consulta(query);

                if (!objList.NoFim()) //tem pelo menos 1 elemento
                {
                    result.TipoDoc = objList.Valor("TipoDoc");
                    result.id = objList.Valor("id");
                    result.Entidade = objList.Valor("Entidade");
                    result.NumDoc = objList.Valor("NumDoc");
                    result.DataEmissao = objList.Valor("DataDoc");

                    result.NumDocExterno = objList.Valor("NumDocExterno");
                    result.TotalMerc = objList.Valor("TotalMerc");
                    result.Serie = objList.Valor("Serie");

                    result.LinhasDoc = new List<Model.LinhaDocCompra>();

                    linhaDocCompra = new Model.LinhaDocCompra();
                    linhaDocCompra.NumLinha = objList.Valor("NumLinha");
                    linhaDocCompra.CodArtigo = objList.Valor("Artigo");
                    linhaDocCompra.Quantidade = objList.Valor("Quantidade");
                    linhaDocCompra.Armazem = objList.Valor("Armazem");
                    linhaDocCompra.Desconto = objList.Valor("Desconto1");
                    linhaDocCompra.PrecoUnitario = objList.Valor("PrecUnit");
                    statusLinhaCompra = new Model.LinhaDocCompraStatus();
                    statusLinhaCompra.EstadoTrans = objList.Valor("EstadoTrans");
                    statusLinhaCompra.QuantTrans = objList.Valor("QuantTrans");
                    linhaDocCompra.Status = statusLinhaCompra;

                    result.LinhasDoc.Add(linhaDocCompra);

                    objList.Seguinte();

                    while (!objList.NoFim()) //restantes elementos
                    {
                        linhaDocCompra = new Model.LinhaDocCompra();
                        linhaDocCompra.NumLinha = objList.Valor("NumLinha");
                        linhaDocCompra.CodArtigo = objList.Valor("Artigo");
                        linhaDocCompra.Quantidade = objList.Valor("Quantidade");
                        linhaDocCompra.Armazem = objList.Valor("Armazem");
                        linhaDocCompra.Desconto = objList.Valor("Desconto1");
                        linhaDocCompra.PrecoUnitario = objList.Valor("PrecUnit");
                        statusLinhaCompra = new Model.LinhaDocCompraStatus();
                        statusLinhaCompra.EstadoTrans = objList.Valor("EstadoTrans");
                        statusLinhaCompra.QuantTrans = objList.Valor("QuantTrans");
                        linhaDocCompra.Status = statusLinhaCompra;

                        result.LinhasDoc.Add(linhaDocCompra);

                        objList.Seguinte();
                    }
                }
            }

            return result;
        }

        public static void updateEncomenda(Model.DocCompra docCompra, Model.EncomendaRecepcionada encomendaRecebida)
        {

            bool found = false;
            foreach (Model.LinhaDocCompra linha in docCompra.LinhasDoc)
            {
                found = false;
                foreach (Model.ArtigosRecepcionados artigoRecebido in encomendaRecebida.artigos)
                {
                    if (artigoRecebido.idArtigo == linha.CodArtigo) // mesmo artigo
                    {
                        linha.Status.QuantTrans += artigoRecebido.quantidade;
                        if (linha.Status.QuantTrans == linha.Quantidade)
                            linha.Status.EstadoTrans = "T";
                        found = true;
                    }
                    if (found)
                        break;
                }
            }
            return;
        }

        public static Model.RespostaErro VGR_New(Model.DocCompra dc)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            GcpBEDocumentoCompra myGR = new GcpBEDocumentoCompra();
            GcpBELinhaDocumentoCompra myLin = new GcpBELinhaDocumentoCompra();
            GcpBELinhasDocumentoCompra myLinhas = new GcpBELinhasDocumentoCompra();

            PreencheRelacaoCompras rl = new PreencheRelacaoCompras();
            List<Model.LinhaDocCompra> lstlindv = new List<Model.LinhaDocCompra>();

            try
            {
                if (PriEngine.InitializeCompany(NomeEmpresa, UtilizadorEmpresa, PasswordEmpresa) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myGR.set_Entidade(dc.Entidade);
                    myGR.set_NumDocExterno(dc.NumDocExterno);
                    myGR.set_Serie(dc.Serie);
                    myGR.set_Tipodoc("VGR");
                    myGR.set_TipoEntidade("F");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dc.LinhasDoc;
                    PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(myGR, rl);
                    foreach (Model.LinhaDocCompra lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Compras.AdicionaLinha(myGR, lin.CodArtigo, lin.Quantidade, lin.Armazem, "", lin.PrecoUnitario, lin.Desconto);
                    }


                    PriEngine.Engine.IniciaTransaccao();
                    PriEngine.Engine.Comercial.Compras.Actualiza(myGR, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Status = true;
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Status = false;
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Status = false;
                erro.Erro = 2;
                erro.Descricao = ex.Message;
                return erro;
            }
        }

        #endregion DocumentosCompra

        #region DocumentosVenda

        public static Model.RespostaErro Encomendas_New(Model.DocVenda dv)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBEDocumentoVenda myEnc = new GcpBEDocumentoVenda();

            GcpBELinhaDocumentoVenda myLin = new GcpBELinhaDocumentoVenda();

            GcpBELinhasDocumentoVenda myLinhas = new GcpBELinhasDocumentoVenda();

            PreencheRelacaoVendas rl = new PreencheRelacaoVendas();
            List<Model.LinhaDocVenda> lstlindv = new List<Model.LinhaDocVenda>();

            try
            {
                if (PriEngine.InitializeCompany(NomeEmpresa, UtilizadorEmpresa, PasswordEmpresa) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myEnc.set_Entidade(dv.Entidade);
                    myEnc.set_Serie(dv.Serie);
                    myEnc.set_Tipodoc("ECL");
                    myEnc.set_TipoEntidade("C");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dv.LinhasDoc;
                    PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(myEnc, rl);
                    foreach (Model.LinhaDocVenda lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Vendas.AdicionaLinha(myEnc, lin.CodArtigo, lin.Quantidade, "", "", lin.PrecoUnitario, lin.Desconto);
                    }


                    // PriEngine.Engine.Comercial.Compras.TransformaDocumento(

                    PriEngine.Engine.IniciaTransaccao();
                    PriEngine.Engine.Comercial.Vendas.Actualiza(myEnc, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }

        public static List<Model.DocVenda> Encomendas_List()
        {
            ErpBS objMotor = new ErpBS();

            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new
            List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(NomeEmpresa, UtilizadorEmpresa, PasswordEmpresa) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL'");
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVenda();
                    dv.id = objListCab.Valor("id");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.NumDoc = objListCab.Valor("NumDoc");
                    dv.Data = objListCab.Valor("Data");
                    dv.TotalMerc = objListCab.Valor("TotalMerc");
                    dv.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                    listlindv = new List<Model.LinhaDocVenda>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocVenda();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        lindv.CodArtigo = objListLin.Valor("Artigo");
                        lindv.DescArtigo = objListLin.Valor("Descricao");
                        lindv.Quantidade = objListLin.Valor("Quantidade");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        lindv.Desconto = objListLin.Valor("Desconto1");
                        lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }
            return listdv;
        }

        public static Model.DocVenda Encomenda_Get(string numdoc)
        {
            ErpBS objMotor = new ErpBS();

            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(NomeEmpresa, UtilizadorEmpresa, PasswordEmpresa) == true)
            {

                string st = "SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL' and NumDoc='" + numdoc + "'";
                objListCab = PriEngine.Engine.Consulta(st);
                dv = new Model.DocVenda();
                dv.id = objListCab.Valor("id");
                dv.Entidade = objListCab.Valor("Entidade");
                dv.NumDoc = objListCab.Valor("NumDoc");
                dv.Data = objListCab.Valor("Data");
                dv.TotalMerc = objListCab.Valor("TotalMerc");
                dv.Serie = objListCab.Valor("Serie");
                objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                listlindv = new List<Model.LinhaDocVenda>();

                while (!objListLin.NoFim())
                {
                    lindv = new Model.LinhaDocVenda();
                    lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                    lindv.CodArtigo = objListLin.Valor("Artigo");
                    lindv.DescArtigo = objListLin.Valor("Descricao");
                    lindv.Quantidade = objListLin.Valor("Quantidade");
                    lindv.Unidade = objListLin.Valor("Unidade");
                    lindv.Desconto = objListLin.Valor("Desconto1");
                    lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                    lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                    lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");
                    listlindv.Add(lindv);
                    objListLin.Seguinte();
                }

                dv.LinhasDoc = listlindv;
                return dv;
            }
            return null;
        }

        #endregion DocumentosVenda

        #region User

        public static Lib_Primavera.Model.RespostaErro Login(Lib_Primavera.Model.Login user)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            StdBECamposChave chave = new StdBECamposChave();
            try
            {
                if (PriEngine.InitializeCompany(NomeEmpresa, UtilizadorEmpresa, PasswordEmpresa) == true)
                {
                    chave.AddCampoChave("CDU_Username", user.username);
                    chave.AddCampoChave("CDU_Password", user.password);

                    if (PriEngine.Engine.TabelasUtilizador.Existe("TDU_User", chave) == true)
                    {
                        Lib_Primavera.Model.SessionModel loggedUser = getUser(user.username);
                        if (loggedUser.Session_Val != "")
                        {
                            Session[loggedUser.Session_Val] = loggedUser;
                            erro.Status = true;
                            erro.Erro = 0;
                            erro.Descricao = loggedUser.Session_Val;
                        }
                        else
                        {
                            erro.Status = false;
                            erro.Erro = 1;
                            erro.Descricao = "Erro, par username/password não encontrado";
                        }
                        return erro;

                    }

                    erro.Status = false;
                    erro.Erro = 1;
                    erro.Descricao = "Erro, par username/password não encontrado";
                    return erro;
                }
                else
                {
                    erro.Status = false;
                    erro.Erro = 2;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;
                }
            }
            catch (Exception ex)
            {
                erro.Status = false;
                erro.Erro = 3;
                erro.Descricao = ex.Message;
                return erro;
            }
        }

        public static Lib_Primavera.Model.RespostaErro Logout(string sessionVal, Lib_Primavera.Model.Login user)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();

            if (Session.ContainsKey(sessionVal))
            {
                if (Session[sessionVal].UserName == user.username)
                {
                    Session.Remove(sessionVal);
                    erro.Status = true;
                    erro.Erro = 0;
                    erro.Descricao = "succesfully logged out user";
                }
                else
                {
                    erro.Status = false;
                    erro.Erro = 1;
                    erro.Descricao = "session value and username do not match";
                }
            }
            else
            {
                erro.Status = false;
                erro.Erro = 1;
                erro.Descricao = "session value not found in storage";
            }
            return erro;
        }

        private static Lib_Primavera.Model.SessionModel getUser(string username)
        {
            Model.SessionModel session = new Model.SessionModel();
            session.UserName = "";
            session.Armazem = "";
            session.Session_Val = "";
            string query = "SELECT CDU_Username, CDU_Armazem FROM PRISINF.dbo.TDU_User WHERE PRISINF.dbo.TDU_User.CDU_Username = '" + username + "'";
            ErpBS objMotor = new ErpBS();
            StdBELista objList;

            if (PriEngine.InitializeCompany(NomeEmpresa, UtilizadorEmpresa, PasswordEmpresa) == true)
            {
                objList = PriEngine.Engine.Consulta(query);
                if (!objList.NoFim())
                {
                    session.UserName = objList.Valor("CDU_Username");
                    session.Armazem = objList.Valor("CDU_Armazem");

                    //hash of username concatenated with julianData of current time
                    session.Session_Val = Base64Encode(session.UserName + (DateTime.Now.ToOADate() + 2415018.5).ToString());
                    return session;
                }

            }
            return session;
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        #endregion User

        #region search
        public static Lib_Primavera.Model.Search search(string valor)
        {
            Lib_Primavera.Model.Search procura = new Model.Search();
            procura.Artigos = new Dictionary<string, List<int>>();
            procura.Fornecedores = new Dictionary<string, List<int>>();
            procura.Armazens = new Dictionary<string, List<int>>();
            procura.Encomendas = new List<int>();
            string[] substrings = Regex.Split(valor, "ECF-");
            //string query = "SELECT PRISINF.dbo.CabecCompras.TipoDoc, PRISINF.dbo.CabecCompras.id, PRISINF.dbo.CabecCompras.NumDoc, PRISINF.dbo.CabecCompras.Entidade, PRISINF.dbo.LinhasCompras.Artigo, PRISINF.dbo.LinhasCompras.Armazem FROM PRISINF.dbo.CabecCompras INNER JOIN PRISINF.dbo.LinhasCompras ON PRISINF.dbo.CabecCompras.Id = PRISINF.dbo.LinhasCompras.IdCabecCompras INNER JOIN PRISINF.dbo.LinhasComprasStatus ON PRISINF.dbo.LinhasCompras.Id = PRISINF.dbo.LinhasComprasStatus.IdLinhasCompras WHERE (PRISINF.dbo.CabecCompras.TipoDoc = N'ECF' AND PRISINF.dbo.LinhasComprasStatus.EstadoTrans = 'P' AND (PRISINF.dbo.CabecCompras.NumDoc LIKE '" + substrings[substrings.Length - 1] + "' OR PRISINF.dbo.LinhasCompras.Artigo LIKE '%" + valor + "%' OR PRISINF.dbo.LinhasCompras.Armazem LIKE '%" + valor + "%' OR PRISINF.dbo.CabecCompras.Entidade LIKE '%" + valor + "%')) ORDER BY PRISINF.dbo.CabecCompras.NumDoc";
            string queryEncomendas = "SELECT DISTINCT PRISINF.dbo.CabecCompras.TipoDoc, PRISINF.dbo.CabecCompras.id, PRISINF.dbo.CabecCompras.NumDoc FROM PRISINF.dbo.CabecCompras INNER JOIN PRISINF.dbo.LinhasCompras ON PRISINF.dbo.CabecCompras.Id = PRISINF.dbo.LinhasCompras.IdCabecCompras INNER JOIN PRISINF.dbo.LinhasComprasStatus ON PRISINF.dbo.LinhasCompras.Id = PRISINF.dbo.LinhasComprasStatus.IdLinhasCompras WHERE (PRISINF.dbo.CabecCompras.TipoDoc = N'ECF' AND PRISINF.dbo.LinhasComprasStatus.EstadoTrans = 'P' AND (PRISINF.dbo.CabecCompras.NumDoc LIKE '" + substrings[substrings.Length - 1] + "' )) ORDER BY PRISINF.dbo.CabecCompras.NumDoc";
            string queryArtigos = "SELECT DISTINCT	PRISINF.dbo.CabecCompras.TipoDoc, PRISINF.dbo.CabecCompras.id, PRISINF.dbo.CabecCompras.NumDoc, PRISINF.dbo.LinhasCompras.Artigo FROM PRISINF.dbo.CabecCompras INNER JOIN PRISINF.dbo.LinhasCompras ON PRISINF.dbo.CabecCompras.Id = PRISINF.dbo.LinhasCompras.IdCabecCompras INNER JOIN PRISINF.dbo.LinhasComprasStatus ON PRISINF.dbo.LinhasCompras.Id = PRISINF.dbo.LinhasComprasStatus.IdLinhasCompras WHERE (PRISINF.dbo.CabecCompras.TipoDoc = N'ECF' AND PRISINF.dbo.LinhasComprasStatus.EstadoTrans = 'P' AND PRISINF.dbo.LinhasCompras.Artigo LIKE '%" + valor + "%' ) ORDER BY PRISINF.dbo.CabecCompras.NumDoc";
            string queryArmazens = "SELECT DISTINCT	PRISINF.dbo.CabecCompras.TipoDoc, PRISINF.dbo.CabecCompras.id, PRISINF.dbo.CabecCompras.NumDoc,PRISINF.dbo.LinhasCompras.Armazem FROM PRISINF.dbo.CabecCompras INNER JOIN PRISINF.dbo.LinhasCompras ON PRISINF.dbo.CabecCompras.Id = PRISINF.dbo.LinhasCompras.IdCabecCompras INNER JOIN PRISINF.dbo.LinhasComprasStatus ON PRISINF.dbo.LinhasCompras.Id = PRISINF.dbo.LinhasComprasStatus.IdLinhasCompras WHERE (PRISINF.dbo.CabecCompras.TipoDoc = N'ECF' AND PRISINF.dbo.LinhasComprasStatus.EstadoTrans = 'P' AND PRISINF.dbo.LinhasCompras.Armazem LIKE '%" + valor + "%') ORDER BY PRISINF.dbo.CabecCompras.NumDoc";
            string queryFornecedores = "SELECT DISTINCT	PRISINF.dbo.CabecCompras.TipoDoc, PRISINF.dbo.CabecCompras.id, PRISINF.dbo.CabecCompras.NumDoc, PRISINF.dbo.CabecCompras.Entidade FROM PRISINF.dbo.CabecCompras INNER JOIN PRISINF.dbo.LinhasCompras ON PRISINF.dbo.CabecCompras.Id = PRISINF.dbo.LinhasCompras.IdCabecCompras INNER JOIN PRISINF.dbo.LinhasComprasStatus ON PRISINF.dbo.LinhasCompras.Id = PRISINF.dbo.LinhasComprasStatus.IdLinhasCompras WHERE (PRISINF.dbo.CabecCompras.TipoDoc = N'ECF' AND PRISINF.dbo.LinhasComprasStatus.EstadoTrans = 'P' AND PRISINF.dbo.CabecCompras.Entidade LIKE '%" + valor + "%') ORDER BY PRISINF.dbo.CabecCompras.NumDoc";
            ErpBS objMotor = new ErpBS();
            StdBELista objList;

            if (PriEngine.InitializeCompany(NomeEmpresa, UtilizadorEmpresa, PasswordEmpresa) == true)
            {
                objList = PriEngine.Engine.Consulta(queryEncomendas);
                string chave;
                int parValor;

                while (!objList.NoFim())
                {
                    if (!procura.Encomendas.Contains(objList.Valor("NumDoc")))
                    {
                        procura.Encomendas.Add(objList.Valor("NumDoc"));
                    }
                    objList.Seguinte();
                }

                objList = PriEngine.Engine.Consulta(queryArtigos);

                while (!objList.NoFim())
                {
                    chave = objList.Valor("Artigo");
                    parValor = objList.Valor("NumDoc");
                    if (procura.Artigos.ContainsKey(chave))
                    {
                        List<int> listaEncomendas = procura.Artigos[chave];
                        if (!listaEncomendas.Contains(parValor))
                        {
                            listaEncomendas.Add(parValor);
                        }
                    }
                    else
                    {
                        List<int> lista = new List<int>();
                        lista.Add(parValor);
                        procura.Artigos.Add(chave, lista);
                    }
                    objList.Seguinte();
                }

                objList = PriEngine.Engine.Consulta(queryArmazens);

                while (!objList.NoFim())
                {
                    chave = objList.Valor("Armazem");
                    parValor = objList.Valor("NumDoc");
                    if (procura.Armazens.ContainsKey(chave))
                    {
                        List<int> listaEncomendas = procura.Armazens[chave];
                        if (!listaEncomendas.Contains(parValor))
                        {
                            listaEncomendas.Add(parValor);
                        }
                    }
                    else
                    {
                        List<int> lista = new List<int>();
                        lista.Add(parValor);
                        procura.Armazens.Add(chave, lista);
                    }
                    objList.Seguinte();
                }

                objList = PriEngine.Engine.Consulta(queryFornecedores);

                while (!objList.NoFim())
                {
                    chave = objList.Valor("Entidade");
                    parValor = objList.Valor("NumDoc");
                    if (procura.Fornecedores.ContainsKey(chave))
                    {
                        List<int> listaEncomendas = procura.Fornecedores[chave];
                        if (!listaEncomendas.Contains(parValor))
                        {
                            listaEncomendas.Add(parValor);
                        }
                    }
                    else
                    {
                        List<int> lista = new List<int>();
                        lista.Add(parValor);
                        procura.Fornecedores.Add(chave, lista);
                    }
                    objList.Seguinte();
                }

            }
            return procura;
        }
        #endregion search
    }
}
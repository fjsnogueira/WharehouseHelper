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

        #region DocumendosCompra

        public static List<Model.DocCompra> VGR_List()
        {
            string query = "SELECT dbo.CabecCompras.TipoDoc, dbo.CabecCompras.id, dbo.CabecCompras.NumDoc, dbo.CabecCompras.Entidade, dbo.CabecCompras.DataDoc, dbo.LinhasCompras.NumLinha, dbo.LinhasCompras.Artigo, dbo.LinhasCompras.Quantidade,dbo.LinhasCompras.Armazem, dbo.LinhasComprasStatus.EstadoTrans, dbo.LinhasComprasStatus.QuantTrans FROM dbo.CabecCompras INNER JOIN dbo.LinhasCompras ON dbo.CabecCompras.Id = dbo.LinhasCompras.IdCabecCompras INNER JOIN dbo.LinhasComprasStatus ON dbo.LinhasCompras.Id = dbo.LinhasComprasStatus.IdLinhasCompras WHERE (dbo.CabecCompras.TipoDoc = N'ECF' AND dbo.LinhasComprasStatus.EstadoTrans = 'P') ORDER BY dbo.CabecCompras.NumDoc";
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
                    myGR.set_Tipodoc("VGR");
                    myGR.set_TipoEntidade("F");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dc.LinhasDoc;
                    PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(myGR, rl);
                    foreach (Model.LinhaDocCompra lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Compras.AdicionaLinha(myGR, lin.CodArtigo, lin.Quantidade, lin.Armazem, "");
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

        public static Lib_Primavera.Model.RespostaErro isValid(Lib_Primavera.Model.Login user)
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
                        erro.Status = true;
                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
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

        public static bool createUser(Lib_Primavera.Model.Login user)
        {
            return false;
        }

        #endregion User

        #region search
        public static Lib_Primavera.Model.Search search(string valor)
        {

            Lib_Primavera.Model.Search procura = new Model.Search();
            procura.Artigos = new List<Model.Artigo>();
            procura.Fornecedores = new List<Model.Fornecedor>();
            procura.Encomendas = new List<Model.DocCompra>();
            string queryArtigos = "SELECT dbo.Artigo.Artigo, dbo.Artigo.Descricao, dbo.Artigo.CodBarras FROM dbo.Artigo WHERE dbo.Artigo.Artigo LIKE '%"+valor+"%'";
            string queryFornecedores = "SELECT dbo.Fornecedores.Fornecedor, dbo.Fornecedores.nome FROM dbo.Fornecedores WHERE dbo.Fornecedores.Fornecedor LIKE '%" + valor + "%' OR dbo.Fornecedores.nome LIKE '%" + valor + "%'";
            string[] substrings = Regex.Split(valor, "ECF-");
            string queryEncomendas = "SELECT DISTINCT dbo.CabecCompras.TipoDoc, dbo.CabecCompras.id, dbo.CabecCompras.NumDoc, dbo.CabecCompras.Entidade, dbo.CabecCompras.DataDoc FROM dbo.CabecCompras INNER JOIN dbo.LinhasCompras ON dbo.CabecCompras.Id = dbo.LinhasCompras.IdCabecCompras INNER JOIN dbo.LinhasComprasStatus ON dbo.LinhasCompras.Id = dbo.LinhasComprasStatus.IdLinhasCompras WHERE (dbo.CabecCompras.TipoDoc = N'ECF' AND dbo.LinhasComprasStatus.EstadoTrans = 'P' AND dbo.CabecCompras.NumDoc LIKE '" + substrings[1] + "') ORDER BY dbo.CabecCompras.NumDoc";
            ErpBS objMotor = new ErpBS();
            StdBELista objList;
            if (PriEngine.InitializeCompany(NomeEmpresa, UtilizadorEmpresa, PasswordEmpresa) == true)
            {
                objList = PriEngine.Engine.Consulta(queryArtigos);

                while (!objList.NoFim()) //inserir Artigos
                {
                    Lib_Primavera.Model.Artigo artigoEncontrado = new Model.Artigo();
                    artigoEncontrado.CodArtigo = objList.Valor("artigo");
                    artigoEncontrado.DescArtigo = objList.Valor("descricao");
                    artigoEncontrado.CodBarras = objList.Valor("CodBarras");
                    procura.Artigos.Add(artigoEncontrado);
                    objList.Seguinte();
                }

                objList = PriEngine.Engine.Consulta(queryFornecedores);

                while (!objList.NoFim()) //inserir Fornecedores
                {
                    Lib_Primavera.Model.Fornecedor fornecedorEncontrado = new Model.Fornecedor();
                    fornecedorEncontrado.id = objList.Valor("fornecedor");
                    fornecedorEncontrado.nome = objList.Valor("nome");
                    procura.Fornecedores.Add(fornecedorEncontrado);
                    objList.Seguinte();
                }

                objList = PriEngine.Engine.Consulta(queryEncomendas);

                while (!objList.NoFim()) //inserir Encomendas
                {
                    Lib_Primavera.Model.DocCompra encomendaEncontrada = new Model.DocCompra();
                    encomendaEncontrada.TipoDoc = objList.Valor("TipoDoc");
                    encomendaEncontrada.id = objList.Valor("id");
                    encomendaEncontrada.Entidade = objList.Valor("Entidade");
                    encomendaEncontrada.NumDoc = objList.Valor("NumDoc");
                    encomendaEncontrada.DataEmissao = objList.Valor("DataDoc");
                    encomendaEncontrada.LinhasDoc = new List<Model.LinhaDocCompra>();
                    procura.Encomendas.Add(encomendaEncontrada);
                    objList.Seguinte();
                }
            }
            return procura;
        }
        #endregion search
    }
}
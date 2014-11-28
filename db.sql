-- 		############################################################################
-- 		### ----------- ### ------- DOCUMENTOS DE COMPRA ------- ### ----------- ###
-- 		############################################################################
-- 		Nome da tabela: dbo.DocumentosCompra
			select * from PRISINF.dbo.DocumentosCompra ;

--		Atributos relevantes
--			(PK) Documento : string > "tag" do documento :: por exemplo, "ECF"
--			(--) Descricao : string > nome do tipo do documento :: por exemplo, "Encomenda a fornecedor"

-- 		############################################################################
-- 		### ------------ ### ------------- COMPRAS ------------ ### ------------ ###
-- 		############################################################################
-- CABEÇALHO
--		Nome da tabela: dbo.CabecCompras
			select * from PRISINF.dbo.CabecCompras;

--		Atributos relevantes
--			(PK) Id : string
--			(FK) TipoDoc : DocumentosCompra
--			(FK) Entidade : Fornecedores
--			(FK) CondPag : > condição de pagamento
				select * from PRISINF.dbo.CondPag;
--				(PK) CondPag : int > id
--				(--) Descricao : string > nome da condição de pagamento

--			(FK) ModoExp : > modo de expedição
				select * from PRISINF.dbo.ModosExp;
--				(PK) ModoExp : int > id
--				(--) Descricao : string > nome do mode de expedição 

--			(FK) ModoPag : > modo de pagamento
				select * from PRISINF.dbo.DocumentosBancos;
--				(PK) Movim : string > id do tipo de pagamento
--				(--) Descricao : string > nome do modo de pagamento

--			(--) NumContribuinte : int > número de contribuinte do fornecedor
--			(--) Nome : string > nome do fornecedor
--			(--) Morada : string > morada do fornecedor
--			(--) Localidade : string > localidade do fornecedor
--			(--) CodPostal : string > código postal do fornecedor

-- ARTIGOS
--		Nome da tabela: dbo.LinhasCompras
			select * from PRISINF.dbo.LinhasCompras;

-- 		Atributos relevantes
--			() NumLinha : int > número da linha no documento de compra
--			() Artigo : string > id do artigo
--			() TaxaIva = 23
--			() CodIva = 23
--			() Quantidade : int > quantidade do artigo a encomendar
--			() PrecUnit : floar > preço unitário de compra do artigo
--			() TipoLinha : int > "10" linha de artigos ; "60" linha converção de documento
--			(FK) Armazem : string > armazém onde entregar as encomendas
--			() DataEntrada
--			() DataEntrega
--			() PrecoLiquido : int > valor final da compra
--			() Descricao : string > descrição do artigo
--			() IdCabecCompras : string > id do documento de compra
--			(PK) Id : string > id da linha
--			() Localizacao : string > id do armazém

-- 		############################################################################
-- 		### ------------ ### ------------- ARIGOS ------------- ### ------------ ###
-- 		############################################################################
-- 		Nome da tabela: dbo.Fornecedores
			select * from PRISINF.dbo.Fornecedores ;
			
-- 		Atributos relevantes
--			(PK) Artigo : string > id do artigo :: por exemplo, "CYMBAL0001"
--			(--) Descricao : string > nome do artigo
--			(--) CodBarras : string > código de barras do artigo

-- 		############################################################################
-- 		### ------------ ### ---------- FORNECEDORES ---------- ### ------------ ###
-- 		############################################################################
-- 		Nome da tabela: dbo.Fornecedores
			select * from PRISINF.dbo.Fornecedores ;
			
-- 		Atributos relevantes
--			(PK) Fornecedore : string > id do fornecedor :: por exemplo, "F0001"
--			(--) Nome : string > nome do fornecedor
--			(--) NumContrib : int > número de contribuinte
--			(--) EncomendasPendentes : float > número de encomendas pendentes ?? 
--			(--) NomeFiscal : string > nome fiscal do fornecedor
--			(--) Morada : string > morada
--			(--) Local : string > localidade
--			(--) Cp : string > código postal 

-- 		############################################################################
-- 		### ------------ ### ------------ ARMAZENS ------------ ### ------------ ###
-- 		############################################################################
-- 		Nome da tabela: dbo.Armazem
			select * from PRISINF.dbo.Armazem ;
	
-- 		Atributos relevantes
--			(PK) Armazem : string > id do armazem
--			(--) Descricao : string > descrição/nome/identificação do armazém
--			(FK) Pais : Armazens_Pais
--			(FK) Distrito : Armazens_Distritos 
		
-- 		############################################################################
-- 		### ------------ ### ------------- PAISES ------------- ### ------------ ###
-- 		############################################################################
-- 		Nome da tabela: dbo.Paises
			select * from PRISINF.dbo.Paises ;
	
-- 		Atributos relevantes
--			(PK) Pais : string > id do pais :: por exemplo, "PT"
--			(--) Descricao : string > nome do pais :: por exemplo, "Portugal"
		
-- 		############################################################################
-- 		### ------------ ### ------------- PAISES ------------- ### ------------ ###
-- 		############################################################################
-- 		Nome da tabela: dbo.Distritos
			select * from PRISINF.dbo.Distritos ;
	
-- 		Atributos relevantes
--			(PK) Distrito : string > id do distrito :: por exemplo, "01"
--			(--) Descricao : string > nome do distrito :: por exemplo, "Aveiro"
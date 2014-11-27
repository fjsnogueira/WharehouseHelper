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
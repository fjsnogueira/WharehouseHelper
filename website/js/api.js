/******************************************************************************/
/***********************     		Globals     		***********************/
/******************************************************************************/

//var SERVER_BASE_URL 	= 	'http://sinf.ngrok.com/';	// Server base url
var SERVER_BASE_URL 	= 	'http://localhost:49822/';	// Server base url
var SERVER_API_URL		=	'api/';						// Server api url extension

var API					=	{
		login:			"login/",
		artigos:		"artigos/",
		docCompra:		"doccompra/",
		fornecedor:		"fornecedor/",
		armazem:		"armazem/",
		search:			"search/"
	};
		

/******************************************************************************/
/***********************     		Imports     		***********************/
/******************************************************************************/

// Needs jquery to be imported in order to execute properly
// Needs jquery.cookie to be imported in order to execute properly

/******************************************************************************/
/***************************     Login / Logout     ***************************/
/******************************************************************************/
/*
 * Logs a user into the system creating the cookies on completion
 * Cookies:
 *  ____________________________________________________________________________
 * |	  Name		|						Value								|
 * |________________|___________________________________________________________|
 * |    username	|	the username used to log into the system				|
 * |________________|___________________________________________________________|
 * |   wharehouse	|	the wharehouse to which the user is assigned			|
 * |________________|___________________________________________________________|
 * |     status		|	the cookie status: [ 'logged' | 'waiting' | 'error' ]	|
 * |________________|___________________________________________________________|
 */
function login(username, password){

	console.log("Preparing to log in...");

	// Setup status as waiting
	$.cookie('status', 'waiting');

	console.log("username: " + username);
	console.log("password: " + password);
	
	var loginData = {
		username:		username,
		password:		password
	};
	
	console.log("Requesting log in...");
	
	//deve esperar receber json
	$.ajax({
		url: 			SERVER_BASE_URL + SERVER_API_URL + API.login,
		type: 			"POST",
		data: 			loginData,
		headers: 		{ 'Content-Type': 'application/x-www-form-urlencoded' },
		beforeSend: 	function(){ $("#login").text('A ligar...');},
		success: 		loginSuccess,
		error: 			loginError
	});
}

// Creates all the needed cookies on a success login
function loginSuccess(data, testStatus, jqXHR){
	// Setup cookie information //
	$.cookie('username', data['username']);
	$.cookie('session', data['session']);
	$.cookie('status', 'success');
	
	window.location.href = "home.html";
}

function loginError(){
		// Creates the needed cookies on a login that failed
		// Setup cookie information //
		$.cookie('username', '');
		$.cookie('status', 'error');
		
		$("#login").text('Erro! Por favor tente mais tarde.');
}

// Logs out a user removing all cookies associated with him
function logout(){

	// Cleans current cookies
	$.removeCookie('username');
	$.removeCookie('session');
	$.removeCookie('status');
	
	// fazer aqui qualquer coisa //
	
}

/******************************************************************************/
/***********************     		QueryTime     		***********************/
/******************************************************************************/

// -----     getArtigo     ----- //

function getArtigo(artigoID){

	$.ajax({
		url: 		SERVER_BASE_URL + SERVER_API_URL + API["artigos"],
		type: 		GET,
		data: 		artigoID,
		success: 	artigoSuccess(data, textStatus, jqXHR),
		error: 		artigoError(jqXHR, textStatus, errorThrown)
	});
}

// retorna ok no caso do artigo ter sido encontrado
function artigoSuccess(data, testStatus, jqXHR){
	// fazer aqui qualquer coisa //
}

// retorna erro no caso do artigo nao ter sido encontrado
function artigoError(jqXHR, textStatus, errorThrown){
	// fazer aqui qualquer coisa //
}

// ----- getArtigos ----- //

function getArtigos(){
	$.ajax({
		url: 		SERVER_BASE_URL + SERVER_API_URL + API["artigos"],
		type: 		GET,
		data: 		"",
		success: 	artigosSuccess(data, textStatus, jqXHR),
		error: 		artigosError(jqXHR, textStatus, errorThrown)
	});
}

// retorna ok no caso dos artigos terem sido encontrados
function artigosSuccess(data, testStatus, jqXHR){
	// fazer aqui qualquer coisa //
}

// retorna erro no caso dos artigos nao terem sido encontrados
function artigosError(jqXHR, textStatus, errorThrown){
	// fazer aqui qualquer coisa //
}

// ----- getEncomendas ----- //

function getEncomendas(){
	$.ajax({
		url: 		SERVER_BASE_URL + SERVER_API_URL + API["docCompra"],
		type: 		GET,
		data: 		"",
		success: 	encomendasSuccess(data, textStatus, jqXHR),
		error: 		encomendasError(jqXHR, textStatus, errorThrown)
	});
}

// retorna ok no caso das encomendas terem sido encontradas
function encomendasSuccess(data, testStatus, jqXHR){
	// fazer aqui qualquer coisa //
}

// retorna erro no caso das encomendas nao terem sido encontradas
function encomendasError(jqXHR, textStatus, errorThrown){
	// fazer aqui qualquer coisa //
}

// ----- updateEncomenda ----- //

// TODO: encomenda tem de ser um modelo valido
function updateEncomenda(encomenda){
	//precisa de receber as cookies do user
	$.ajax({
		url: 		SERVER_BASE_URL + SERVER_API_URL + API["docCompra"],
		type: 		POST,
		data: 		encomenda,
		success: 	updateEncomendaSuccess(data, textStatus, jqXHR),
		error: 		updateEncomendaError(jqXHR, textStatus, errorThrown)
	});
}

// retorna ok no caso da encomenda ter sido actualizada com sucesso
function updateEncomendaSuccess(data, testStatus, jqXHR){
	// fazer aqui qualquer coisa //
}

// retorna erro no caso da encomenda nao ter sido actualizada com sucesso
function updateEncomendaError(jqXHR, textStatus, errorThrown){
	// fazer aqui qualquer coisa //
}

// -----     getFornecedor     ----- //

function getFornecedor(fornecedorID){

	$.ajax({
		url: 		SERVER_BASE_URL + SERVER_API_URL + API["fornecedor"],
		type: 		GET,
		data: 		fornecedorID,
		success: 	fornecedorSuccess(data, textStatus, jqXHR),
		error: 		fornecedorError(jqXHR, textStatus, errorThrown)
	});
}

// retorna ok no caso do fornecedor ter sido encontrado
function fornecedorSuccess(data, testStatus, jqXHR){
	// fazer aqui qualquer coisa //
}

// retorna erro no caso do fornecedor nao ter sido encontrado
function fornecedorError(jqXHR, textStatus, errorThrown){
	// fazer aqui qualquer coisa //
}

// -----     getArmazem     ----- //

function getArmazem(armazemID){

	$.ajax({
		url: 		SERVER_BASE_URL + SERVER_API_URL + API["armazem"],
		type: 		GET,
		data: 		armazemID,
		success: 	armazemSuccess(data, textStatus, jqXHR),
		error: 		armazemError(jqXHR, textStatus, errorThrown)
	});
}

// retorna ok no caso do armazem ter sido encontrado
function armazemSuccess(data, testStatus, jqXHR){
	// fazer aqui qualquer coisa //
}

// retorna erro no caso do armazem nao ter sido encontrado
function armazemError(jqXHR, textStatus, errorThrown){
	// fazer aqui qualquer coisa //
}

// -----     searchDB     ----- //

function searchDB(attribute){

	$.ajax({
		url: 		SERVER_BASE_URL + SERVER_API_URL + API["search"],
		type: 		GET,
		data: 		attribute,
		success: 	armazemSuccess(data, textStatus, jqXHR),
		error: 		armazemError(jqXHR, textStatus, errorThrown)
	});
}

// retorna ok no caso algo tenha sido encontrado
function armazemSuccess(data, testStatus, jqXHR){
	// fazer aqui qualquer coisa //
}

// retorna erro no caso de nada ter sido encontrado
function armazemError(jqXHR, textStatus, errorThrown){
	// fazer aqui qualquer coisa //
}
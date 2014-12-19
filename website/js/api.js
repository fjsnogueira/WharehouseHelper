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
	
var searches 			= 	0;	

function checkSearches(){
	searches++;
	if( searches == 4 ) {
		$('#loader').css('display', 'none');
		searches = 0;
	}
}

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

	if( data['Status'] != false ) {
		// Setup cookie information //
		$.cookie('username', data['username']);
		$.cookie('session', data['session']);
		$.cookie('status', 'success');
		
		window.location.href = "home.html";
	} else
		loginError();
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

// -----     "GET"Artigo     ----- //

function getArtigo(artigoID){
	$.ajax({
		url: 		SERVER_BASE_URL + SERVER_API_URL + API.artigos + artigoID,
		type: 		"GET",
		headers: 	{ 'Content-Type': 'text/plain; charset=utf-8' },
		success: 	artigoSuccess,
		error: 		artigoError
	});

}

// retorna ok no caso do artigo ter sido encontrado
function artigoSuccess(data, testStatus, jqXHR){
	if( data != null ) {
		// add the tr to tableProducts
		var desc = data['DescArtigo'];
		$('#tabProducts').addClass('tab-current');
		$('#section-bar-2').addClass('content-current');
		$('#tableProducts').empty();
		$('#tableProducts').append('<tr><td class="td_img"> <img src="img/artigo.jpg"></td><td class="td_desc">'+desc+'</td><td class="td_bt"> <button class="more_opt"> abrir opções </button> </td><td class="td_canvas"> <canvas id="canvas2_1"> </canvas> </td>');
		render('ean13',data['CodBarras'],'canvas2_1');
		$('#loader').css('display', 'none');
	} else
		artigoError();
}

// retorna erro no caso do artigo nao ter sido encontrado
function artigoError(){
	// fazer aqui qualquer coisa //
	$('#tableProducts').empty();
	$('#tabProducts').removeClass('tab-current');
	$('#section-bar-2').removeClass('content-current');
	
	checkSearches();
}

// ----- "GET"Artigos ----- //

function getArtigos(){
	$.ajax({
		url: 		SERVER_BASE_URL + SERVER_API_URL + API["artigos"],
		type: 		"GET",
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

// ----- "GET"Encomendas ----- //

function getEncomenda(encomendaID){
	$.ajax({
		url: 		SERVER_BASE_URL + SERVER_API_URL + API.docCompra + encomendaID,
		type: 		"GET",
		headers: 	{ 'Content-Type': 'text/plain; charset=utf-8' },
		success: 	encomendaSuccess,
		error: 		encomendaError
	});
}

// retorna ok no caso das encomendas terem sido encontradas
function encomendaSuccess(data, testStatus, jqXHR){
	if( data['id'] != null ) {
		// add the tr to tableProducts
		var desc = data['id'];
		$('#tabOrders').addClass('tab-current');
		$('#section-bar-1').addClass('content-current');
		$('#tableOrders').empty();
		$('#tableOrders').append('<tr><td class="td_img"> <img src="img/encomenda.jpg"></td><td class="td_desc">'+desc+'</td><td class="td_bt"> <button class="more_opt"> abrir opções </button> </td><td class="td_canvas"> <canvas id="canvas1_1"> </canvas> </td>');
		render('code128',data['TipoDoc']+'-'+data['NumDoc'],'canvas1_1');
		$('#loader').css('display', 'none');
	} else
		encomendaError();
}

// retorna erro no caso das encomendas nao terem sido encontradas
function encomendaError(jqXHR, textStatus, errorThrown){
	alert("ajksdhsa");
	// empty the table
	$('#tableOrders').empty();
	$('#tabOrders').removeClass('tab-current');
	$('#section-bar-1').removeClass('content-current');
	
	checkSearches();
}

// ----- updateEncomenda ----- //

// TODO: encomenda tem de ser um modelo valido
function updateEncomenda(encomenda){
	//precisa de receber as cookies do user
	$.ajax({
		url: 		SERVER_BASE_URL + SERVER_API_URL + API["docCompra"],
		type: 		"POST",
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

// -----     "GET"Fornecedor     ----- //

function getFornecedor(fornecedorID){

	$.ajax({
		url: 		SERVER_BASE_URL + SERVER_API_URL + API.fornecedor + fornecedorID,
		type: 		"GET",
		headers: 	{ 'Content-Type': 'text/plain; charset=utf-8' },
		success: 	fornecedorSuccess,
		error: 		fornecedorError
	});
}

// retorna ok no caso do fornecedor ter sido encontrado
function fornecedorSuccess(data, testStatus, jqXHR){
	if( data['id'] != null ) {
		// add the tr to tableProducts
		var desc = data['nome'];
		$('#tabSuppliers').addClass('tab-current');
		$('#section-bar-4').addClass('content-current');
		$('#tableSuppliers').empty();
		$('#tableSuppliers').append('<tr><td class="td_img"> <img src="img/fornecedor.jpg"></td><td class="td_desc">'+desc+'</td><td class="td_bt"> <button class="more_opt"> abrir opções </button> </td><td class="td_canvas"> <canvas id="canvas4_1"> </canvas> </td>');
		render('code128',data['id'],'canvas4_1');
		$('#loader').css('display', 'none');
	} else
		fornecedorError();
}

// retorna erro no caso do fornecedor nao ter sido encontrado
function fornecedorError(jqXHR, textStatus, errorThrown){
	// empty the table
	$('#tableSuppliers').empty();
	$('#tabSuppliers').removeClass('tab-current');
	$('#section-bar-4').removeClass('content-current');
	
	checkSearches();
}

// -----     "GET"Armazem     ----- //

function getArmazem(armazemID){
	$.ajax({
		url: 		SERVER_BASE_URL + SERVER_API_URL + API.armazem + armazemID,
		type: 		"GET",
		headers: 	{ 'Content-Type': 'text/plain; charset=utf-8' },
		success: 	armazemSuccess,
		error: 		armazemError
	});

}

// retorna ok no caso do artigo ter sido encontrado
function armazemSuccess(data, testStatus, jqXHR){
	if( data['id'] != null ) {
		// add the tr to tableProducts
		var desc = data['descricao'];
		$('#tabWarehouses').addClass('tab-current');
		$('#section-bar-3').addClass('content-current');
		$('#tableWarehouses').empty();
		$('#tableWarehouses').append('<tr><td class="td_img"> <img src="img/armazem.jpg"></td><td class="td_desc">'+desc+'</td><td class="td_bt"> <button class="more_opt"> abrir opções </button> </td><td class="td_canvas"> <canvas id="canvas3_1"> </canvas> </td>');
		render('code128',data['id'],'canvas3_1');
		$('#loader').css('display', 'none');
	} else
		armazemError();
}

// retorna erro no caso do artigo nao ter sido encontrado
function armazemError(){
	// empty the table
	$('#tableWarehouses').empty();
	$('#tabWarehouses').removeClass('tab-current');
	$('#section-bar-3').removeClass('content-current');
	
	checkSearches();
}

// -----     searchDB     ----- //

function searchDB(attribute){

	$.ajax({
		url: 			SERVER_BASE_URL + SERVER_API_URL + API.search,
		type: 			"GET",
		data: 			attribute,
		success: 		searchSuccess,
		error: 			searchError
	});
}

// retorna ok no caso algo tenha sido encontrado
function searchSuccess(data, testStatus, jqXHR){
	// fazer aqui qualquer coisa //
}

// retorna erro no caso de nada ter sido encontrado
function searchError(jqXHR, textStatus, errorThrown){
	// fazer aqui qualquer coisa //
}
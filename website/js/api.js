/******************************************************************************/
/***********************     		Globals     		***********************/
/******************************************************************************/

var SERVER_BASE_URL 	= 	'http://sinf.ngrok.com/';	// Server base url

/******************************************************************************/
/***********************     		Imports     		***********************/
/******************************************************************************/

// Needs jquery to be imported in order to execute properly //
// Needs jquery.cookie to be imported in order to execute properly //

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

	// Setup status as waiting
	$.cookie('status', 'waiting');

	var loginData;
	loginData['username'] = username;
	loginData['password'] = password;

	$.ajax({
		url: 		SERVER_BASE_URL + 'login',
		type: 		POST,
		data: 		loginData,
		success: 	loginSuccess(data, textStatus, jqXHR),
		error: 		loginError(jqXHR, textStatus, errorThrown)
	});
}

// Creates all the needed cookies on a success login
function loginSuccess(data, testStatus, jqXHR){
	// Setup cookie information //
	$.cookie('username', data['username']);
	$.cookie('wharehouse', data['wharehouse']);
	$.cookie('status', 'logged');
}

// Creates the needed cookies on a login that failed
function loginError(jqXHR, textStatus, errorThrown){
	// Setup cookie information //
	$.cookie('username', '');
	$.cookie('wharehouse', '');
	$.cookie('status', 'error');
}

// Logs out a user removing all cookies associated with him
function logout(){

	// Cleans current cookies
	$.removeCookie('username');
	$.removeCookie('wharehouse');
	$.removeCookie('status');
	
}

/******************************************************************************/
/***********************     		Sign Up     		***********************/
/******************************************************************************/



/******************************************************************************/
/***********************     		QueryTime     		***********************/
/******************************************************************************/




function doLogin(){
		console.log("user cliecked log in");
		var username=$("#username").val();
		var password=$("#password").val();
		
		login(username, password);
		return false;
}
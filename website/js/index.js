
$(document).ready(function() {

	console.log("creating click events");
	
	$('#login_form').submit(function() {
		console.log("user cliecked log in");
		var username=$("#username").val();
		var password=$("#password").val();
		
		login(username, password);
		return false;
	});
});
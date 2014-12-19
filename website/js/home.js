
function doSearch(){

	// get value to search in the db
	var searchText=$("#searchText").val();

	// Create loader
	$('#loader').html('<img src="img/loader.gif" id="imgLoader"><br/>&nbsp&nbsp&nbspLoading...');
	$('#imgLoader').css('margin-top', ($(window).height()/2.0) - 32);
	$('#loader').css('display', 'block');

	// call search ajax requests
	getArtigo(searchText);
	getEncomenda(searchText);
	getArmazem(searchText);
	getFornecedor(searchText);
}
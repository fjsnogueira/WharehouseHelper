
function doSearch(){
	// get value to search in the db
	var searchText=$("#searchText").val();
	
	// call search ajax request
	searchDB(searchText);
}
function load(){
	var order = JSON.parse(window.top.name);
	// title
    $('#orderTitle').text("Encomenda ECF-"+order['NumDoc']);
	
	// suplier and date
	$('#orderSuplier').append(order['Entidade']);
	$('#orderCreation').append(order['DataEmissao'].split('T')[0]);
	
	// render the order code
	render('code128','ECF-'+order['NumDoc'],'canvas0');
	
	// search results
	$.each(order['LinhasDoc'], function( index, value ) {
		$('#search_elements').append('<tr><td class="checkbox unchecked"><div class="checkbox"><input type="checkbox" value="'+index+'" id="checkbox'+index+'"><label for="checkbox'+index+'"></label></div></td><td class="td_desc">'+value['CodArtigo']+'</td><td class="td_qt"><button class="qt esq" type="button"> <span class="bt-icon icon-minus"></span> </button><input class="qt" type="text" value="'+value['Quantidade']+'"><button class="qt dir" type="button"> <span class="bt-icon icon-plus"></span> </button></td><td class="td_canvas"> <canvas id="canvas'+(index+1)+'"> </canvas> </td></tr>');
		render('code128',value['CodArtigo'],'canvas'+(index+1));
	});
}
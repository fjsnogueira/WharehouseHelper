function render(format, barcode, canvas_id) {

	var elt = format, text = barcode, opts = "includetext", bw = new BWIPJS;

	// Convert the options to a dictionary object, so we can pass alttext with
	// spaces.
	var tmp = opts.split(' ');
	opts = {};
	for (var i = 0; i < tmp.length; i++) {
		if (!tmp[i])
			continue;
		var eq = tmp[i].indexOf('=');
		if (eq == -1)
			opts[tmp[i]] = bw.value(true);
		else
			opts[tmp[i].substr(0, eq)] = bw.value(tmp[i].substr(eq+1));
	}

	bw.bitmap(new Bitmap);
	
	var scl = 1;
	bw.scale(scl,scl);

	bw.push(text);
	bw.push(opts);

	try {
		bw.call(elt) ;
		bw.bitmap().show( canvas_id , 'N') ;
	} catch(e) {
		var s = '';
		if (e.fileName)
			s += e.fileName + ' ';
		if (e.lineNumber)
			s += '[line ' + e.lineNumber + '] ';
		alert(s + (s ? ': ' : '') + e.message);
	}
}
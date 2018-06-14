var JqGridExt = {

	GetSelectedRows: function (selector) {
		let jqGrid = $(selector);
		let selectedIds$ = jqGrid.getGridParam('selarrrow');
		let rows = selectedIds$.map(function (id) { return jqGrid.getRowData(id); });
		return rows;
	}

}
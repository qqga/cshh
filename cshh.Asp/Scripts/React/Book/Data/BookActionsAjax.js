const BookActionsAjax = {
    delBookmark(id) {
        $.ajax("DeleteBookmark", { cache: false, async: false, type: 'POST', data: { id: id } })
            .done(data => { })
            .fail((xhr, err, text) => {
                alert(`${err}\r\n${text}`); throw new Error(text);
            });
    },

    addBookmark(title, note, position, bookId) {
        var res;
        $.ajax("AddBookmark", { cache: false, async: false, type: 'POST', data: { Title: title, Note: note, Position: position, ForeignText_Id: bookId } })
            .done(data => { res = data; })
            .fail((xhr, err, text) => {
                alert(`${err}\r\n${text}`); throw new Error(text);
            });
        return res;
    },

    editBookmark(id, title, note) {
        var res;
        $.ajax("EditBookmark", { cache: false, async: false, type: 'POST', data: { Id: id, Title: title, Note: note } })
            .done((data,ar2,ar3) => { res = data; })
            .fail((xhr, err, text) => {
                alert(`${err}\r\n${text}`); throw new Error(text);
            }).catch(() => {
                alert('catch some')
            });
        return res;
    }

}

module.exports = BookActionsAjax;
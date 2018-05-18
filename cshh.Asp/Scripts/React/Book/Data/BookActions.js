
const BookActionTypes = require('./BookActionTypes')
const BookDispatcher = require('./BookDispatcher') 

const BookActions = {
    addBookmark: function (title, note, position, bookId) {
        BookDispatcher.dispatch({
            type: BookActionTypes.ADD_BOOKMARK,
            title: title, note: note, position: position, bookId: bookId
        });
    },

    delBookmark: function (id) {
        BookDispatcher.dispatch({
            type: BookActionTypes.DEL_BOOKMARK,
            id: id,
        });
    },

    editBookmark: function (id, title, note) {
        BookDispatcher.dispatch({
            type: BookActionTypes.EDIT_BOOKMARK,
            id: id, title: title, note: note
        });
    },

}

module.exports = BookActions;
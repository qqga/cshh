var BookDispatcher = require('./BookDispatcher');
var BookActionTypes = require('./BookActionTypes');
var BookmarkTemplate = require('./BookmarkTemplate');

var Immutable = require('../../../immutable');
var FluxUtils = require('../../../FluxUtils');
var BookActionsAjax = require('./BookActionsAjax')


class BookStore extends FluxUtils.ReduceStore {

    constructor(data) {
        super(BookDispatcher);
        this.data = data        
        this['_state'] = Immutable.OrderedMap(this.data.Bookmarks.map(b => [b.Id.toString(), new BookmarkTemplate({ Id: b.Id, Title: b.Title, Note: b.Note, Position: b.Position })]));
    }

    setState(val) {
        this._state = val;
    }

    getInitialState() {        
        //return Immutable.OrderedMap(this.data.Bookmarks.map(b => [b.Id, b]));
        return Immutable.OrderedMap({
            "1": new BookmarkTemplate({
                Id: 1,
                Title: 'test',
                Note: 'test',
                Position: 10
            })
        });
    }

    //todo ajax requests and invoke only if status ok
    reduce(state, action) {
        switch (action.type) {
            case BookActionTypes.ADD_BOOKMARK:
                // Don't add todos with no text.
                //if (!action.text) {
                //    return state;
                //}

                var res = BookActionsAjax.addBookmark(action.title, action.note, action.position, action.bookId);

                let newBookmark = new BookmarkTemplate({
                    Id: res.Id,
                    Title: res.Title,
                    Note: res.Note,
                    Position: res.Position,
                })

                return state.set(newBookmark.Id.toString(), newBookmark);


            case BookActionTypes.DEL_BOOKMARK:
                BookActionsAjax.delBookmark(action.id);
                return state.delete(action.id.toString());

            case BookActionTypes.EDIT_BOOKMARK:

                var res = BookActionsAjax.editBookmark(action.id, action.title, action.note);

                return state.withMutations(map => {
                    map.setIn([action.id.toString(), 'Title'], res.Title).
                        setIn([action.id.toString(), 'Note'], res.Note)
                });


            default:
                return state;
        }
    }
}


module.exports = {
    BookStore,

}

//function bookContainer(props) {
//    return (
//        <div>
//            <Book {...props} />
//        </div>
//    );
//}

//class Book extends React.Component {

//    constructor(props) {
//        super(props);
//        this.prepareText = this.prepareText.bind(this);

//        this.newPosTmp = 10;
//    }

//    prepareText() {

//        this.text = this.props.text;
//        this.pointsCount = 0;

//        this.bookmarksMap = {};
//        this.props.bookmarks.map((bm) => { this.bookmarksMap[bm.Position] = bm });

//        //var dt1 = new Date();
//        this.replaceFunk = this.replaceFunk.bind(this);
//        this.resText = this.text.replace(/\n|\./g, this.replaceFunk);
//        //var dt2 = new Date();
//        //alert(dt2 - dt1);

//        this.bmCount = this.bmCount.bind(this);
//    }

//    bmCount() {
//        alert(this.props.bookmarks.length);
//    }

//    replaceFunk(replaceStr, offset) {
//        switch (replaceStr) {
//            case "\n": return "<br/>";
//            case ".":
//                {
//                    this.pointsCount++;
//                    let bm = this.bookmarksMap[this.pointsCount];
//                    var res = `<a href='' onClick='event.preventDefault();BookActions.addBookmark(new Date().toLocaleString(),null,${this.pointsCount},${this.props.bookId})' >.</a>`;
//                    if (bm)
//                        res = `.<span id='bookmark-${bm.Position}'></span>`;
//                    return res;
//                }
//            default: return replaceStr;
//        }
//    }

//    render() {
//        this.prepareText();
//        return (
//            <div>
//                <button onClick={this.bmCount}>bm</button>
//                <button onClick={() => { this.props.onAdd('asdf', 'fas', ++this.newPosTmp) }}>bm2Actions</button>
//                <BookmarksPan {...this.props} />
//                <div className="book-pages" dangerouslySetInnerHTML={{ __html: this.resText }} ></div>
//            </div>
//        );

//        //rawMarkup: function() {
//        //    var md = new Remarkable();
//        //    var rawMarkup = md.render(this.props.children.toString());
//        //    return { __html: rawMarkup };
//        //<span dangerouslySetInnerHTML={this.rawMarkup()} />
//    }

//}


//class BookmarksPan extends React.Component {
//    constructor(props) {
//        super(props);
//        //this.state = { bookmarks: props.bookmarks };
//        this.readMenuClick = this.readMenuClick.bind(this);
//        //this.inDrag = false;
//    }

//    readMenuClick(e) {
//        //if (this.inDrag) { this.inDrag = false; return; }

//        this.menuContent$.toggle("fold", {}, 500)
//        this.menuButtonIcon$.toggleClass("fa-angle-double-down").toggleClass("fa-angle-double-up");
//    }

//    renderItems() {
//        return this.props.bookmarks.map(bm => (<Bookmark key={bm.Id} model={bm} onDelete={this.props.onDelete} onEdit={this.props.onEdit} />));
//    }

//    render() {

//        return (
//            <div ref='menu' id="read-menu" >
//                <div id="read-menu-button" onClick={this.readMenuClick} >
//                    <span className="fa fa-angle-double-down" style={{ left: 50 + "%" }} ref="menuButonIcon"></span>
//                </div>
//                <div id="read-menu-contetn" ref="menuContent">
//                    <ul>
//                        {this.renderItems()}
//                    </ul>
//                </div>
//            </div>
//        );
//    }
//    componentDidUpdate() {

//        this.makeRefs();
//        this.renderBokmarks();
//    }
//    componentDidMount() {

//        this.makeRefs();
//        this.renderBokmarks();
//    }

//    makeRefs() {
//        this.$node = $(this.refs.menu);
//        this.menuContent$ = $(this.refs.menuContent);
//        this.menuButtonIcon$ = $(this.refs.menuButonIcon);

//        this.$node.draggable({ start: function () { /*inDrag = true;*/ } });
//    }
//    renderBokmarks() {
//        this.props.bookmarks.forEach((v, k) => {
//            let bm = v;
//            let el = document.getElementById('bookmark-' + bm.Position);
//            el.innerText = "";//virtual dom need change for upd, i think.
//            ReactDOM.render(React.createElement(BookmarkA, { model: bm }), el);
//        });
//    }

//}


//class Bookmark extends React.Component {
//    constructor(props) {
//        super(props);
//        //this.state = { model: props.model };

//        this.edit = this.edit.bind(this);
//        this.del = this.del.bind(this);
//    }
//    render() {
//        console.log('render Bookmark');
//        return (
//            <li id={"li-bookmark-" + this.props.model.Id}>
//                <a href={"#bookmark-" + this.props.model.Position} title={this.props.model.Note}>{this.props.model.Title}</a>
//                <div style={{ display: "none" }} ref="editDiv" >
//                    <input ref="editorTitle" type="text" defaultValue={this.props.model.Title} />
//                    <textarea ref="editorNote" type="text" defaultValue={this.props.model.Note} />
//                </div>
//                <a href="" ref="saveButton" style={{ display: "none" }} > <i className="fa fa-xs fa-save" onClick={(nativeEvenet) => { this.save(nativeEvenet, event, this.props.model); }} /></a>
//                <a ref="editButton" href="" title="Редактировать" onClick={(nativeEvent) => this.edit(nativeEvent, event, this.props.model)} > <i className="fas fa-edit fa-xs" ></i></a>
//                <a href="" title="Удалить" onClick={(nativeEvent) => this.del(nativeEvent, event, this.props.model)}> <i className="fas fa-trash fa-xs"  ></i></a>
//            </li>
//        );
//    }

//    save(nativeEvent, event, bookmark) {
//        nativeEvent.preventDefault();
//        $(this.refs.editDiv).hide();
//        $(this.refs.saveButton).hide();
//        $(this.refs.editButton).show();

//        this.props.onEdit(bookmark.Id, $(this.refs.editorTitle).val(), $(this.refs.editorNote).val());
//    }
//    edit(nativeEvent, event, bookmark) {
//        nativeEvent.preventDefault();
//        $(this.refs.editDiv).show();
//        $(this.refs.saveButton).show();
//        $(this.refs.editButton).hide();
//        //this.props.editClick(bookmark);
//        //this.setState((prev, props) => { return { model: { ...prev.model, Title: "a" } } });
//        //this.props.onEdit(bookmark.Id, Date.now().toLocaleString(), "edit");
//    }

//    del(nativeEvent, event, bookmark) {
//        nativeEvent.preventDefault();
//        this.props.onDelete(bookmark.Id);
//    }
//}

//class BookmarkA extends React.Component {
//    constructor(props) {
//        super(props);
//        this.model = props.model;
//    }
//    render() {
//        return (
//            <span className='fa fa-bookmark bookmarka' title={this.model.Note}>{this.model.Title}</span>
//        );
//    }
//}

////--------------------------------------------------------------------------------



//const BookmarkTemplate = Immutable.Record({
//    Id: -1,
//    Title: 'title',
//    Note: 'note',
//    Position: '-1',
//});

//const BookActionTypes = {
//    ADD_BOOKMARK: 'ADD_BOOKMARK',
//    DEL_BOOKMARK: 'DEL_BOOKMARK',
//    EDIT_BOOKMARK: 'EDIT_BOOKMARK',
//};

//const BookActions = {
//    addBookmark: function (title, note, position, bookId) {
//        BookDispatcher.dispatch({
//            type: BookActionTypes.ADD_BOOKMARK,
//            title: title, note: note, position: position, bookId: bookId
//        });
//    },

//    delBookmark: function (id) {
//        BookDispatcher.dispatch({
//            type: BookActionTypes.DEL_BOOKMARK,
//            id: id,
//        });
//    },

//    editBookmark: function (id, title, note) {
//        BookDispatcher.dispatch({
//            type: BookActionTypes.EDIT_BOOKMARK,
//            id: id, title: title, note: note
//        });
//    },

//}

//class BookStore extends FluxUtils.ReduceStore {

//    constructor(data) {
//        super(BookDispatcher);
//        this.data = data

//        this._state = Immutable.OrderedMap(this.data.Bookmarks.map(b => [b.Id.toString(), new BookmarkTemplate({ Id: b.Id, Title: b.Title, Note: b.Note, Position: b.Position })]));
//    }

//    getInitialState() {
//        //return Immutable.OrderedMap(this.data.Bookmarks.map(b => [b.Id, b]));
//        return Immutable.OrderedMap({
//            "1": new BookmarkTemplate({
//                Id: 1,
//                Title: 'test',
//                Note: 'test',
//                Position: 10
//            })
//        });
//    }

//    //todo ajax requests and invoke only if status ok
//    reduce(state, action) {
//        switch (action.type) {
//            case BookActionTypes.ADD_BOOKMARK:
//                // Don't add todos with no text.
//                //if (!action.text) {
//                //    return state;
//                //}
                              
//                var res = new BookmarkAjax().addBookmark(action.title, action.note, action.position, action.bookId);

//                let newBookmark  = new BookmarkTemplate({
//                    Id: res.Id,
//                    Title: res.Title,
//                    Note: res.Note,
//                    Position: res.Position,
//                })

//                return state.set(newBookmark.Id.toString(), newBookmark);


//            case BookActionTypes.DEL_BOOKMARK:
//                new BookmarkAjax().delBookmark(action.id);
//                return state.delete(action.id.toString());

//            case BookActionTypes.EDIT_BOOKMARK:

//                var res = new BookmarkAjax().editBookmark(action.id, action.title, action.note);

//                return state.withMutations(map => {
//                    map.setIn([action.id.toString(), 'Title'], res.Title).
//                        setIn([action.id.toString(), 'Note'], res.Note)
//                });


//            default:
//                return state;
//        }
//    }
//}


//function getStores() {
//    return [
//        BookEditStore
//    ];
//}

//function getState() {
//    return {
//        bookmarks: BookEditStore.getState(),
//        text: BookEditStore.data.Text,
//        bookId: BookEditStore.data.Id,

//        onAdd: BookActions.addBookmark,
//        onDelete: BookActions.delBookmark,
//        onEdit: BookActions.editBookmark,
//    };
//}

//var BookDispatcher = new Flux.Dispatcher();

//var BookContainer = FluxUtils.Container.createFunctional(bookContainer, getStores, getState);


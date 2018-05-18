const Bookmark = require('./Bookmark')
const BookmarkA = require('./BookmarkA')



class BookmarksPan extends React.Component {
    constructor(props) {
        super(props);
        //this.state = { bookmarks: props.bookmarks };
        this.readMenuClick = this.readMenuClick.bind(this);
        //this.inDrag = false;
    }

    readMenuClick(e) {
        //if (this.inDrag) { this.inDrag = false; return; }

        this.menuContent$.toggle("fold", {}, 500)
        this.menuButtonIcon$.toggleClass("fa-angle-double-down").toggleClass("fa-angle-double-up");
    }

    renderItems() {
        return this.props.bookmarks.map(bm => (<Bookmark key={bm.Id} model={bm} onDelete={this.props.onDelete} onEdit={this.props.onEdit} />));
    }

    render() {

        return (
            <div ref='menu' id="read-menu" >
                <div id="read-menu-button" onClick={this.readMenuClick} >
                    <span className="fa fa-angle-double-down" style={{ left: 50 + "%" }} ref="menuButonIcon"></span>
                </div>
                <div id="read-menu-contetn" ref="menuContent">
                    <ul>
                        {this.renderItems()}
                    </ul>
                </div>
            </div>
        );
    }
    componentDidUpdate() {

        this.makeRefs();
        this.renderBokmarks();
    }
    componentDidMount() {

        this.makeRefs();
        this.renderBokmarks();
    }

    makeRefs() {
        this.$node = $(this.refs.menu);
        this.menuContent$ = $(this.refs.menuContent);
        this.menuButtonIcon$ = $(this.refs.menuButonIcon);

        this.$node.draggable({ start: function () { /*inDrag = true;*/ } });
    }
    renderBokmarks() {
        this.props.bookmarks.forEach((v, k) => {
            let bm = v;
            let el = document.getElementById('bookmark-' + bm.Position);
            el.innerText = "";//virtual dom need change for upd, i think.
            ReactDOM.render(React.createElement(BookmarkA, { model: bm }), el);
        });
    }

}



module.exports = BookmarksPan;
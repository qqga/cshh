const BookmarksPan = require('./BookmarksPan');

function bookArea(props) {
    return (
        <div>
            <Book {...props} />
        </div>
    );
}

class Book extends React.Component {

    constructor(props) {
        super(props);
        this.prepareText = this.prepareText.bind(this);

        this.newPosTmp = 10;
    }

    prepareText() {

        this.text = this.props.text;
        this.pointsCount = 0;

        this.bookmarksMap = {};
        this.props.bookmarks.map((bm) => { this.bookmarksMap[bm.Position] = bm });

        //var dt1 = new Date();
        this.replaceFunk = this.replaceFunk.bind(this);
        this.resText = this.text.replace(/\n|\./g, this.replaceFunk);
        //var dt2 = new Date();
        //alert(dt2 - dt1);

        this.bmCount = this.bmCount.bind(this);
    }

    bmCount() {
        alert(this.props.bookmarks.length);
    }

    replaceFunk(replaceStr, offset) {
        switch (replaceStr) {
            case "\n": return "<br/>";
            case ".":
                {
                    this.pointsCount++;
                    let bm = this.bookmarksMap[this.pointsCount];

                    var res;
                    if (bm)
                        res = `.<span id='bookmark-${bm.Position}'></span>`;
                    else
                        res = `<a href='' onClick='event.preventDefault();pack.BookActions.addBookmark(new Date().toLocaleString(),null,${this.pointsCount},${this.props.bookId})' >.</a>`;
                    return res;
                }
            default: return replaceStr;
        }
    }

    render() {
        this.prepareText();
        return (
            <div>
                <BookmarksPan {...this.props} />
                <div className="book-pages" dangerouslySetInnerHTML={{ __html: this.resText }} ></div>
            </div>
        );

        //rawMarkup: function() {
        //    var md = new Remarkable();
        //    var rawMarkup = md.render(this.props.children.toString());
        //    return { __html: rawMarkup };
        //<span dangerouslySetInnerHTML={this.rawMarkup()} />
    }

}


module.exports =
    {
        Book, bookArea
    }
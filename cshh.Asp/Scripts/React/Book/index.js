const BookStoreModule = require('./Data/BookStore')
const BookViewModule = require('./Views/Book')
const BookActions = require('./Data/BookActions')

function initBook(data, elementId) {
    BookStore = new BookStoreModule.BookStore(data);
    
    var conatainer = FluxUtils.Container.createFunctional(BookViewModule.bookArea, getStores, getState)

    ReactDOM.render(
        //React.createElement(pack.BookContainer.BookContainer),
        React.createElement(conatainer),
        document.getElementById(elementId)
    );


    function getStores() {
        return [
            BookStore
        ];
    }

    function getState() {
        return {
            bookmarks: BookStore.getState(),
            text: BookStore.data.Text,
            bookId: BookStore.data.Id,

            onAdd: BookActions.addBookmark,
            onDelete: BookActions.delBookmark,
            onEdit: BookActions.editBookmark,
        };
    }
     

}


module.exports = {
    initBook, 
    BookActions
    //BookStore: require('./Data/BookStore.js'),    
    //BookContainer: require('./Containers/BookContainer'),

};
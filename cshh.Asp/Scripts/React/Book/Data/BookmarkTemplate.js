const Immutable = require('../../../immutable');

const BookmarkTemplate = Immutable.Record({
    Id: -1,
    Title: 'title',
    Note: 'note',
    Position: '-1',
});

module.exports = BookmarkTemplate;

class BookmarkA extends React.Component {
    constructor(props) {
        super(props);
        this.model = props.model;
    }
    render() {
        return (
            <span className='fa fa-bookmark bookmarka' title={this.model.Note}>{this.model.Title}</span>
        );
    }
}

module.exports = BookmarkA;
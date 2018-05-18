class Bookmark extends React.Component {
    constructor(props) {
        super(props);
        //this.state = { model: props.model };

        this.edit = this.edit.bind(this);
        this.del = this.del.bind(this);
    }
    render() {
        console.log('render Bookmark');
        return (
            <li id={"li-bookmark-" + this.props.model.Id}>
                <a href={"#bookmark-" + this.props.model.Position} title={this.props.model.Note}>{this.props.model.Title}</a>
                <div style={{ display: "none" }} ref="editDiv" >
                    <input ref="editorTitle" type="text" defaultValue={this.props.model.Title} />
                    <textarea ref="editorNote" type="text" defaultValue={this.props.model.Note} />
                </div>
                <a href="" ref="saveButton" style={{ display: "none" }} > <i className="fa fa-xs fa-save" onClick={(nativeEvenet) => { this.save(nativeEvenet, event, this.props.model); }} /></a>
                <a ref="editButton" href="" title="Редактировать" onClick={(nativeEvent) => this.edit(nativeEvent, event, this.props.model)} > <i className="fas fa-edit fa-xs" ></i></a>
                <a href="" title="Удалить" onClick={(nativeEvent) => this.del(nativeEvent, event, this.props.model)}> <i className="fas fa-trash fa-xs"  ></i></a>
            </li>
        );
    }

    save(nativeEvent, event, bookmark) {
        nativeEvent.preventDefault();
        $(this.refs.editDiv).hide();
        $(this.refs.saveButton).hide();
        $(this.refs.editButton).show();

        this.props.onEdit(bookmark.Id, $(this.refs.editorTitle).val(), $(this.refs.editorNote).val());
    }
    edit(nativeEvent, event, bookmark) {
        nativeEvent.preventDefault();
        $(this.refs.editDiv).show();
        $(this.refs.saveButton).show();
        $(this.refs.editButton).hide();
        //this.props.editClick(bookmark);
        //this.setState((prev, props) => { return { model: { ...prev.model, Title: "a" } } });
        //this.props.onEdit(bookmark.Id, Date.now().toLocaleString(), "edit");
    }

    del(nativeEvent, event, bookmark) {
        nativeEvent.preventDefault();
        this.props.onDelete(bookmark.Id);
    }
}

module.exports = Bookmark;
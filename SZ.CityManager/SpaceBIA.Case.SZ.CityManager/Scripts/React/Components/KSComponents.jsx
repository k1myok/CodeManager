var GroupItem = React.createClass({
    getInitialState: function () {
        return {
        };
    },
    getDefaultProps: function () {
        return {
        };
    },
    createItems: function () {
        var source = this.props.dataSource;
        if(!source)
            return null;
        return source.Children.map(function (item) {
            return <span>{item.Title}</span>
        });
    },
    render: function () {
        return (
            <div className='groupItem'>
                <div  className='title'>{this.props.dataSource.Title}</div>
                <div className='items'>
                    {this.createItems()}
                </div>
            </div>
        );
    }
});
var GroupPanel = React.createClass({
    getInitialState: function () {
        return {
        };
    },
    getDefaultProps: function () {
        return {
        };
    },
    createItems: function () {
        var source = this.props.dataSource;
        if(!source)
            return null;
        return source.Children.map(function (item) {
            return <GroupItem dataSource={item}/>
        });
    },
    render: function () {
        if (!this.props.dataSource)
            return null;
        return (
            <div className='groupPanel'>
                <div className='title'>{this.props.dataSource.Title}</div>
                <div className='items'>
                    {this.createItems()}
                </div>
            </div>
        );
    }
});

var DataCenterHome = React.createClass({
    getInitialState: function () {
        return {
        };
    },
    getDefaultProps: function () {
        return {
        };
    },
    componentDidMount: function () {
        $.get('../DataCenter/DataSource', function (result) {
            if (this.isMounted()) {
                this.setState({
                    dataSource: result
                });
            }
        }.bind(this));

    },
    createItems: function () {
        var source = this.state.dataSource;
        if(!source)
            return null;
        return source.map(function (item) {
            return <GroupPanel dataSource={item}/>
            });
    },
    render: function () {
        if (!this.state.dataSource)
            return null;
        return (
            <div className='dataCenter'>
                {this.createItems()}
            </div>
        );
    }
});

var Timeline_MigrationSample = React.createClass({
    getInitialState: function () {
        return {
        };
    },
    getDefaultProps: function () {
        return {
        };
    },
    map: null,
    drawTool:null,
    componentDidMount: function () {
        loadEMLayer('timeline_Migration1', 'Timeline_MigrationOption', 'http://localhost/SpaceBIA.Web.EasyPortal/Map/GetDefaultTimeline_MigrationDataSource', 'Timeline_Migration');
    },
    componentDidUpdate: function () {
        this.componentDidMount();
    },
    onClick: function () {
        this.props.onClick();
    },
    render: function () {
        var imgStyle = {
            width: '100%',
            height:'100%'
        };

        return (
            <div id="timeline_Migration1" style={imgStyle}>

            </div>
        );
}
});

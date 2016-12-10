var MigrationSample = React.createClass({
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
        loadEMLayer('migration1', 'MigrationOption', 'http://localhost/SpaceBIA.Web.EasyPortal/Map/GetDefaultMigrationDataSource', 'Migration');
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
            <div id="migration1" style={imgStyle}>

            </div>
        );
}
});

var MarkPointSample = React.createClass({
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
        loadEMLayer('markPoint1', 'MarkPointOption', 'http://localhost/SpaceBIA.Web.EasyPortal/Map/GetDefaultMarkPointDataSource', 'MarkPoint');;
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
            <div id="markPoint1" style={imgStyle}>

            </div>
        );
}
});

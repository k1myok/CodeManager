var HeatMapSample = React.createClass({
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
        loadEMLayer('heatMap1', 'HeatMapOption', 'http://localhost/SpaceBIA.Web.EasyPortal/Map/GetDefaultHeatMapSource', 'HeatMap');
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
            <div id="heatMap1" style={imgStyle}>

            </div>
        );
}
});

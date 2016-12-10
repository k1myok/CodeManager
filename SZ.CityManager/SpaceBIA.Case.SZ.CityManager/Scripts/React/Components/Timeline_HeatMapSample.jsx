var Timeline_HeatMapSample = React.createClass({
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
        loadEMLayer('timeline_HeatMap1', 'Timeline_HeatMapOption', 'http://localhost/SpaceBIA.Web.EasyPortal/Map/GetDefaultTimeline_HeatMapSource', 'Timeline_HeatMap');
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
            <div id="timeline_HeatMap1" style={imgStyle}>

            </div>
        );
}
});

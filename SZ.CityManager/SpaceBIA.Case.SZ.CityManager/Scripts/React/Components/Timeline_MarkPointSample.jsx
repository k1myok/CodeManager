var Timeline_MarkPointSample = React.createClass({
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
        loadEMLayer('timeline_MarkPoint1', 'Timeline_MarkPointOption', 'http://localhost/SpaceBIA.Web.EasyPortal/Map/GetDefaultTimeline_MarkPointDataSource', 'Timeline_MarkPoint');
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
            <div id="timeline_MarkPoint1" style={imgStyle}>

            </div>
        );
}
});

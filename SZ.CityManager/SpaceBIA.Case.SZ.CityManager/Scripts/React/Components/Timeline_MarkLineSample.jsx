var Timeline_MarkLineSample = React.createClass({
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
        loadEMLayer('timeline_MarkLine1', 'Timeline_MarkLineOption', 'http://localhost/SpaceBIA.Web.EasyPortal/Map/GetDefaultTimeline_MarkLineDataSource', 'Timeline_MarkLine');
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
            <div id="timeline_MarkLine1" style={imgStyle}>

            </div>
        );
}
});

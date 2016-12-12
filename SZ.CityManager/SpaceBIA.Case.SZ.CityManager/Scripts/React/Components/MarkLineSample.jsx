var MarkLineSample = React.createClass({
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
        loadEMLayer('markLine1', 'MarkLineOption', 'http://localhost/SpaceBIA.Web.EasyPortal/Map/GetDefaultMarkLineDataSource', 'MarkLine');
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
            <div id="markLine1" style={imgStyle}>

            </div>
        );
}
});

var Timeline_DensityMapSample = React.createClass({
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
        loadEMLayer('timeline_DensityMap1', 'Timeline_DensityMapOption', rootURL + '/Map/GetDefaultTimeline_DensityMapSource', 'Timeline_Density');
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
            <div id="timeline_DensityMap1" style={imgStyle}>

            </div>
        );
}
});

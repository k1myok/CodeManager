var DensityMapSample = React.createClass({
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
        loadEMLayer('densityMap1', 'DensityMapOption', 'http://localhost/SpaceBIA.Web.EasyPortal/Map/GetDefaultDensityMapSource', 'Density');
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
            <div id="densityMap1" style={imgStyle}>

            </div>
        );
}
});

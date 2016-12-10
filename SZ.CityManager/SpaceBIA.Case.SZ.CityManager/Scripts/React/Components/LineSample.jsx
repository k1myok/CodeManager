var LineSample = React.createClass({
    getInitialState: function () {
        return {
        };
    },
    getDefaultProps: function () {
        return {
        };
    },
    componentDidMount: function () {
        console.log('loading LineSample');
        loadLineSample('LineSample');
    },
    componentDidUpdate: function () {
        console.log('update LineSample dom complete');
        loadLineSample('LineSample');
    },

    render: function () {
        var imgStyle = {
            width: '100%',
            height:'100%'
        };

        return (
            <div id="LineSample" style={imgStyle}>

            </div>
        );
}
});

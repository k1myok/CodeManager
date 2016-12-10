var PieSample1 = React.createClass({
    getInitialState: function () {
        return {
        };
    },
    getDefaultProps: function () {
        return {
        };
    },
    componentDidMount: function () {
        console.log('loading PieSample1');
        loadPieSample('PieSample1');
    },
    componentDidUpdate: function () {
        console.log('update PieSample1 dom complete');
        loadPieSample('PieSample1');
    },

    render: function () {
        var imgStyle = {
            width: '100%',
            height:'100%'
        };

        return (
            <div id="PieSample1" style={imgStyle}>

            </div>
        );
}
});

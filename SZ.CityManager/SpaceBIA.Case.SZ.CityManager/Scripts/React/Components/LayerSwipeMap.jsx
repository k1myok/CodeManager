  var LayerSwipeMap = React.createClass({
      getInitialState: function () {
          return {
          };
      },
      getDefaultProps: function () {
          return {
          };
      },
      map: null,
      drawTool: null,
      componentDidMount: function () {
          //console.log("layerSwipeDiv");
          LayerSwipeMapFactory("layerSwipeDiv", 'SwipeDiv', 'http://content.china-ccw.com:6080/arcgis/rest/services/KS/KGYD/MapServer', 'http://content.china-ccw.com:6080/arcgis/rest/services/KS/ksdzdt/MapServer');
          //LayerSwipeMapFactory(LayerSwipeMap);
      },
      componentDidUpdate: function () {
          this.componentDidMount();
      },
      //onClick: function () {
      //    this.props.onClick();
      //},
      render: function () {
          var imgStyle = {
              width: '100%',
              height: '100%'
          };

          return (
              <div style={imgStyle}>
                  <div id="SwipeDiv" >
                  </div>
                  <div id="layerSwipeDiv" style={imgStyle}>
                  </div>   
              </div>
             
        );
  }
  });

 

!function(){function t(t,a){t.beginPath();var n=a[0],o=a[1];t.moveTo(n[0],n[1]);for(var c=1;c<a.length;c++){var i=e(n,o);t.quadraticCurveTo(n[0],n[1],i[0],i[1]),n=a[c],o=a[c+1]}var u=t.createPattern(r(),"repeat");t.strokeStyle=u,t.stroke()}function e(t,e){return[t[0]+(e[0]-t[0])/2,t[1]+(e[1]-t[1])/2]}function r(){var t=document.createElement("canvas"),e=10,r=5,a=t.getContext("2d");return t.width=t.height=e+r,a.fillStyle="red",a.beginPath(),a.arc(e/2,e/2,e/2,0,2*Math.PI,!1),a.closePath(),a.fill(),t}brushes.dotsPattern=t}();
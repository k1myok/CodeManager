/**
 * Created by JasonYang on 15-08-07.
 */
dojoConfig = {
    //forceGfxRenderer:"canvas",
    parseOnLoad: true,
    locale: 'zh-cn',
    serverIp: window.location.host + "/arcgis_js_api/library/3.16/3.16/",
    packages: [{
        "name": "widgets",
        "location": location.pathname.replace(/\/[^/]+$/, "").replace(/Home/,"") + "/Scripts/DataVisComponent/widgets"
        //"location": "http://localhost/SpaceBIA.Case.SZ.CityManager/Scripts/widgets"
    }]
};
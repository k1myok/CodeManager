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
        //"location": location.pathname.replace(/\/[^/]+$/, "") + "/widgets"
        "location": "http://10.36.172.234:8011/SpaceBIA/Scripts/EMC/widgets/"
    }]
};
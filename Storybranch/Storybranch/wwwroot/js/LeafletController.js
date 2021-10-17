function initLeaflet(divTargetName)
{
    var mapMinZoom = 2;
    var mapMaxZoom = 12;
    var mapMaxResolution = 1.00000000;
    var mapMinResolution = Math.pow(2, mapMaxZoom) * mapMaxResolution;
    var tileExtent = [0.00000000, -7680.00000000, 4320.00000000, 0.00000000];
    var crs = L.CRS.Simple;
    crs.transformation = new L.Transformation(1, -tileExtent[0], -1, tileExtent[3]);
    crs.scale = function (zoom) {
        return Math.pow(2, zoom) / mapMinResolution;
    };
    crs.zoom = function (scale) {
        return Math.log(scale * mapMinResolution) / Math.LN2;
    };
    var map = new L.Map(divTargetName, {
        maxZoom: mapMaxZoom,
        minZoom: mapMinZoom,
        crs: crs
    });

    var layer = L.tileLayer('http://battosai.de/jedaya/map/tiles/{z}/{x}/{y}.jpg', {
        minZoom: mapMinZoom,
        maxZoom: mapMaxZoom,
        attribution: 'Kangaroos are great',
        noWrap: true,
        tms: false
    }).addTo(map);

    map.setView(
        [-535000, 500000],
        3
    );
    L.control.mousePosition().addTo(map);
}

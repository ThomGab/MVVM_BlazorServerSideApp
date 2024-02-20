let map;
let markers = [];
const london_coord = { lat: 51.5072, lng: 0.1276 };

async function initialize() {
    const { Map } = await google.maps.importLibrary("maps");

    const position = london_coord;

    map = new Map(document.getElementById("map"), {
        center: position,
        zoom: 8,
        mapId: "TestDemoMap"
    });
} 

async function centreMapTo(latitude, longitude) {

    var centre = new google.maps.LatLng(latitude, longitude);

    map.panTo(centre);
}

async function zoomMapToFit() {

    zoomLevel = 13;

    map.setZoom(zoomLevel);
}

async function addMarker(latitude, longitude, category, colour) {

    var pinBackground;
    const { AdvancedMarkerElement } = await google.maps.importLibrary("marker");
    const { PinElement } = await google.maps.importLibrary("marker");

    pinBackground = new PinElement({
        background: colour,
        glyphColor: colour,
    });

    var latLng = new google.maps.LatLng(latitude, longitude, category);

    var marker = new AdvancedMarkerElement({
        map: map,
        position: latLng,
        title: category,
        content: pinBackground.element,
    });

    markers.push(marker);
}

// Sets the map on all markers in the array.
async function displayAllMarkers()
{
    for (let i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
    }
}

async function clearClusterMap()
{
    markerCluster.clearMarkers()
    markerCluster.setMap(null)
    new markerClusterer.MarkerClusterer({ markers, map })
}

async function clearAllMarkers() {
    for (let i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    markers = [];
}
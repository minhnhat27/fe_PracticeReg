function showAlert(data) {
    $("#error").fadeIn();
    $("#error").addClass("alert alert-danger").text(data);
    setTimeout(function () {
        $("#error").fadeOut('slow')
    }, 1000);
}



$(document).ready(function () {
    var lists = $(".item")
    var left = $("#list")
    var boxes = $(".box")
    var selected = null;

    boxes.on('dragover', function (e) {
        var box = $(this)
        if (box.children().length !== 0 || box.hasClass("dayoff")) {
            e.preventDefault = null;
        }
        else {
            e.preventDefault();
        }
    })
    boxes.on('drop', function () {
        var box = $(this)
        if (box.children().length === 0 && selected !== null) {
            box.append(selected)

            var param = {
                "ngaythuchanh": box.data("ngay"),
                "buoi": box.data("buoi"),
                "sotuan": parseInt(box.data("tuan")),
                "hknk": $("#schedule").data("hknh"),
                "mscb": $("#schedule").data("mscb"),
                "sttbuoithuchanh": parseInt(box.children(".item").children().data("sttbuoi")),
                "manhomhp": box.children(".item").children().data("manhom")
            };

            $.ajax({
                url: "/Home/CreateSchedule",
                type: 'POST',
                data: param,
                success: function (data) {
                    if (data)
                        showAlert("Thêm lịch thành công");
                    else showAlert("Thêm lịch thất bại. Có lỗi xảy ra!");
                }
            });

            //var headerParams = { 'Authorization': 'bearer ' + $.cookie("AuthToken") };
            //$.ajax({
            //    url: "https://localhost:44304/api/Schedule/saveSchedule",
            //    type: 'POST',
            //    headers: headerParams,
            //    data: JSON.stringify(param),
            //    contentType: 'application/json',
            //    success: function () {
            //        showAlert("Thêm lịch thành công");
            //    },
            //    error: function (data) {
            //        showAlert("Thêm lịch thất bại. Có lỗi xảy ra!");
            //    }
            //});
        }
        selected = null;
    })

    left.on('dragover', function (e) {
        e.preventDefault();
    })
    left.on('drop', function () {
        if (selected !== null) {
            left.append(selected);

            var param = {
                "ngaythuchanh": "01/01/2000",
                "buoi": "Morning",
                "sotuan": 1,
                "hknk": $("#schedule").data("hknh"),
                "mscb": $("#schedule").data("mscb"),
                "sttbuoithuchanh": parseInt(selected.children().data("sttbuoi")),
                "manhomhp": selected.children().data("manhom")
            };

            $.ajax({
                url: "/Home/updateOnSchedule",
                type: 'POST',
                data: param,
                success: function (data) {
                    if (data)
                        showAlert("Xóa lịch thành công");
                    else showAlert("Xóa lịch thất bại. Có lỗi xảy ra!");
                }
            });

        }
        selected = null;
    })

    lists.on('drag', function () {
        selected =  $(this)
    })

    //mobile
    var selectedItem = null;
    lists.on('touchend', function () {
        var touchedItem = $(this);
        if (selectedItem !== null && selectedItem[0] !== touchedItem[0]) {
            selectedItem.children().removeClass("bg-primary-subtle");
        }
        touchedItem.children().addClass("bg-primary-subtle");
        selectedItem = touchedItem;
    })
    boxes.on('touchend', function () {
        var box = $(this)
        if (box.children().length === 0 && selectedItem !== null) {
            box.append(selectedItem)    
            selectedItem.children().removeClass("bg-primary-subtle");

            var param = {
                "ngaythuchanh": box.data("ngay"),
                "buoi": box.data("buoi"),
                "sotuan": parseInt(box.data("tuan")),
                "hknk": $("#schedule").data("hknh"),
                "mscb": $("#schedule").data("mscb"),
                "sttbuoithuchanh": parseInt(box.children(".item").children().data("sttbuoi")),
                "manhomhp": box.children(".item").children().data("manhom")
            };

            $.ajax({
                url: "/Home/CreateSchedule",
                type: 'POST',
                data: param,
                success: function (data) {
                    if (data)
                        showAlert("Thêm lịch thành công");
                    else showAlert("Thêm lịch thất bại. Có lỗi xảy ra!");
                }
            });
        }
        selectedItem = null;
    })
    //left.on('touchend', function () {
    //    if (selectedItem !== null) {
    //        left.append(selectedItem);
    //        selectedItem.children().removeClass("bg-primary-subtle");

    //        var param = {
    //            "ngaythuchanh": "01/01/2000",
    //            "buoi": "Morning",
    //            "sotuan": 1,
    //            "hknk": $("#schedule").data("hknh"),
    //            "mscb": $("#schedule").data("mscb"),
    //            "sttbuoithuchanh": parseInt(selectedItem.children().data("sttbuoi")),
    //            "manhomhp": selectedItem.children().data("manhom")
    //        };

    //        $.ajax({
    //            url: "/Home/updateOnSchedule",
    //            type: 'POST',
    //            data: param,
    //            success: function (data) {
    //                if (data)
    //                    showAlert("Xóa lịch thành công");
    //                else showAlert("Xóa lịch thất bại. Có lỗi xảy ra!");
    //            }
    //        });

    //    }
    //    selectedItem = null;
    //})
})

$(document).ready(function () {
    var currentLocation = window.location.pathname;
    if (currentLocation == '/') {
        $(".index-item").addClass('selected');
    }
    $("a.sidebar-link").each(function () {
        var linkHref = $(this).attr("href");
        if (currentLocation.includes(linkHref)) {
            $(this).closest('.sidebar-item').addClass('selected');
        }
    });
});

var mapObject = L.map("map", { center: [10.031014, 105.769094], zoom: 15, doubleClickZoom: false });
L.tileLayer(
    "http://mt0.google.com/vt/lyrs=y&hl=en&x={x}&y={y}&z={z}",
    {
        attribution: '&copy; <a href="https://www.google.com/help/legalnotices_maps/" target="_blank">GoogleStreetMap</a>'
    }
).addTo(mapObject);
L.marker([10.030773, 105.768888], {title: "Trường CNTT&TT"}).addTo(mapObject);

var myLocationButton = L.control({ position: 'topleft' });
myLocationButton.onAdd = function () {
    var container = L.DomUtil.create('div', 'leaflet-bar leaflet-control');
    var button = L.DomUtil.create('a', 'leaflet-control-my-location', container);
    button.href = 'javascript:void(0)';
    button.title = 'Tìm trường CNTT&TT';
    button.innerHTML = '<i class="bi bi-cursor"></i>';
    L.DomEvent.on(button, 'click', function () {
        mapObject.setView([10.031014, 105.769094], 17)
    });
    return container;
};
myLocationButton.addTo(mapObject);

//var url = "https://data.vietnam.opendevelopmentmekong.net/geoserver/ODVietnam/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=ODVietnam%3A0fe4de63-d3da-4a10-b471-087b85ea28a9&outputFormat=application%2Fjson";
//$.getJSON(url, function (data) {
//    L.geoJSON(data).addTo(mapObject);
//})

//$.getJSON('data/thunhapbinhquan.json', function(data){
//    //L.geoJSON(data).addTo(mapObject);
//    L.geoJSON(data).bindPopup((f) => '<h2>' + f.feature.properties.Name_VI +
//        '</h2><p>Thu nhập bình quân: ' + f.feature.properties.Income + '000VND</p>').addTo(mapObject);
//})

// const scrollingElement = document.getElementById("Course");

// window.addEventListener("scroll", () => {
//   const scrollPercentage = (window.scrollY / (document.body.scrollHeight - window.innerHeight)) * 100;
//   const translateY = -(scrollPercentage * 12);
//   if(scrollPercentage < 14){
//     scrollingElement.style.transform = `translateY(${translateY}px)`;
//   }
// });

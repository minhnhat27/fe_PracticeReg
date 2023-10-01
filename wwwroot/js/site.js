function showAlert(data) {
    $("#error").fadeIn();
    $('#error').addClass("alert alert-danger").text(data);
    setTimeout(function () {
        $("#error").fadeOut('slow')
    }, 1000);
}
setTimeout(function () {
    $("#error-google").fadeOut('slow')
}, 2000);

$(document).ready(function () {
    var lists = $(".item")
    var left = $("#list")
    var boxes = $(".box")
    var selected = null;

    boxes.on('dragover', function (e) {
        var box = $(this)
        if (box.children().length !== 0) {
            e.preventDefault = null;
        }
        else {
            e.preventDefault();
        }
    })
    boxes.on('drop', function (e) {
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
    left.on('drop', function (e) {
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

    lists.on('drag', function (e) {
        selected =  $(this)
    })
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

//var mapObject = L.map("map", { center: [10.031014, 105.769094], zoom: 18 });
//L.tileLayer(
//    "http://mt0.google.com/vt/lyrs=y&hl=en&x={x}&y={y}&z={z}",
//    {
//        attribution: '&copy; <a href="http://' +
//            'www.openstreetmap.org/copyright">OpenStreetMap</a>'
//    }
//).addTo(mapObject);


// const scrollingElement = document.getElementById("Course");

// window.addEventListener("scroll", () => {
//   const scrollPercentage = (window.scrollY / (document.body.scrollHeight - window.innerHeight)) * 100;
//   const translateY = -(scrollPercentage * 12);
//   if(scrollPercentage < 14){
//     scrollingElement.style.transform = `translateY(${translateY}px)`;
//   }
// });



// var lists = document.getElementsByClassName("item");
// var left = document.getElementById("list");
// var boxes = document.getElementsByClassName("box");
// var selected = null;

// function preventDefaultDrag(e) {
//     e.preventDefault();
// }

// for (let box of boxes) {
//     box.addEventListener('dragover', preventDefaultDrag);
//     box.addEventListener('drop', function (e) {
//         if (box.children.length === 0 && selected !== null) {
//             box.appendChild(selected);
//         }
//         selected = null;
//     });
// }

// left.addEventListener('dragover', preventDefaultDrag);
// left.addEventListener('drop', function (e) {
//     if (selected !== null) {
//         left.appendChild(selected);
//     }
//     selected = null;
// });

// for (let item of lists) {
//     item.addEventListener('drag', function (e) {
//         selected = e.target;
//     });
// }

// function ChangeCourse() {
//     var id = document.getElementById("selectCourse").value;

//     $.ajax({
//         url: "Home/ChangeCourse",
//         data: { id: id },
//         success: function (response) {
//             $("#list").append(response);
//         }
//     })
// }
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
})

//$(document).ready(function () {
//    var lists = $(".item")
//    var left = $("#list")
//    var boxes = $(".box")
//    //mobile
//    var selectedItem = null;

//    lists.on('touchend', function () {
//        var touchedItem = $(this);
//        if (selectedItem !== null && selectedItem[0] !== touchedItem[0]) {
//            selectedItem.children().removeClass("bg-primary-subtle");
//        }
//        touchedItem.children().addClass("bg-primary-subtle");
//        selectedItem = touchedItem;
//    })
//    boxes.on('touchend', function () {
//        var box = $(this)
//        if (box.children().length === 0 && selectedItem !== null) {
//            box.append(selectedItem)
//            selectedItem.children().removeClass("bg-primary-subtle");

//            var param = {
//                "ngaythuchanh": box.data("ngay"),
//                "buoi": box.data("buoi"),
//                "sotuan": parseInt(box.data("tuan")),
//                "hknk": $("#schedule").data("hknh"),
//                "mscb": $("#schedule").data("mscb"),
//                "sttbuoithuchanh": parseInt(box.children().data("sttbuoi")),
//                "manhomhp": box.children().data("manhom")
//            };

//            $.ajax({
//                url: "/Home/CreateSchedule",
//                type: 'POST',
//                data: param,
//                success: function (data) {
//                    if (data)
//                        showAlert("Thêm lịch thành công");
//                    else showAlert("Thêm lịch thất bại. Có lỗi xảy ra!");
//                }
//            });
//        }
//        selectedItem = null;
//    })
//    left.on('touchend', function () {
//        if (selectedItem !== null) {
//            left.append(selectedItem);
//            selectedItem.children().removeClass("bg-primary-subtle");

//            var param = {
//                "ngaythuchanh": "01/01/2000",
//                "buoi": "Morning",
//                "sotuan": 1,
//                "hknk": $("#schedule").data("hknh"),
//                "mscb": $("#schedule").data("mscb"),
//                "sttbuoithuchanh": parseInt(selectedItem.children().data("sttbuoi")),
//                "manhomhp": selectedItem.children().data("manhom")
//            };

//            $.ajax({
//                url: "/Home/updateOnSchedule",
//                type: 'POST',
//                data: param,
//                success: function (data) {
//                    if (data)
//                        showAlert("Xóa lịch thành công");
//                    else showAlert("Xóa lịch thất bại. Có lỗi xảy ra!");
//                }
//            });

//        }
//        selectedItem = null;
//    })
//}

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

// const scrollingElement = document.getElementById("Course");

// window.addEventListener("scroll", () => {
//   const scrollPercentage = (window.scrollY / (document.body.scrollHeight - window.innerHeight)) * 100;
//   const translateY = -(scrollPercentage * 12);
//   if(scrollPercentage < 14){
//     scrollingElement.style.transform = `translateY(${translateY}px)`;
//   }
// });

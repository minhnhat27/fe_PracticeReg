// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const scrollingElement = document.getElementById("Course");

window.addEventListener("scroll", () => {
    const scrollPercentage = (window.scrollY / (document.body.scrollHeight - window.innerHeight)) * 100;
    const translateY = -(scrollPercentage * 20);
    if(scrollPercentage < 8){  
    scrollingElement.style.transform = `translateY(${translateY}px)`;
    }
})

function DragAndDrop() {
    var lists = $(".item")
    var left = $("#list")
    var boxes = $(".box")
    var selected = null

    function preventDefaultDrag(e) {
        e.preventDefault()
    }

    boxes.on('dragover', preventDefaultDrag)
    boxes.on('drop', function (e) {
        var box = $(this)
        if (box.children().length === 0 && selected !== null) {
            box.append(selected)
        }
        selected = null
    })

    left.on('dragover', preventDefaultDrag)
    left.on('drop', function (e) {
        if (selected !== null) {
            left.append(selected);
        }
        selected = null;
    })

    lists.on('drag', function (e) {
        selected =  $(this)
    })
}

$(document).ready(DragAndDrop)

//function ChangeCourse() {
//    var id = $("#selectCourse").val();

//    $.ajax({
//        url: "Home/ChangeCourse",
//        data: { id: id },
//        success: function (response) {
//            $("#list").html(response);
//            DragAndDrop();
//        }
//    })
//}

//$(document).ready(ChangeCourse)




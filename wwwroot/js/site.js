$(document).ready(function () {
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
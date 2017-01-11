$(document).ready(function () {
    $.ajax({
        url: "/Account/GetAll",
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response[3] != null) 
                document.getElementById("techMenu").style.display = "block";
            if (response[3] == "professor")
                document.getElementById("profQ").style.display = "block";
            if (response[3] == "student")
                document.getElementById("studQ").style.display = "block";
            if (response[3] == "admin") {
                document.getElementById("profQ").style.display = "block";
                document.getElementById("studQ").style.display = "block";
            }

            
        }
    });

    $.ajax({
        url: "/Technologies/GetAll",
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            f(response)
        }
    });

    function f(list){
        var ul = document.getElementById('technologiesList');
        ul.setAttribute('class', 'dropdown-menu');

            for (var i = 0; i < list.length; i++) {
                var item = document.createElement('li');
                var a = document.createElement('a');
                a.appendChild(document.createTextNode(list[i]["name"]));
                a.setAttribute("href", "/Home/Learn?technology=" + list[i]["id"]);
                item.appendChild(a)
                ul.appendChild(item);
            }
    }
});

function homeButton() {
    $.ajax({
        url: "/Account/GetAll",
        type: 'GET',
        dataType: 'json',
        success: function(response) {
            if (response[0] == null) 
                window.location.replace("/");
            else
                window.location.replace("/Home/Index");
        }
    });
}
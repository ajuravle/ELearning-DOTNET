$(document).ready(function () {
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
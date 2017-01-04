$(document).ready(function () {
    $.ajax({
        url: "/Topics/GetTopicsByTechnologyID/" + location.search.split("=")[1],
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            f(response)
        }
    });

    function f(list) {
        var div = document.getElementById('topicsList');
        div.setAttribute('class', 'list-group');
        div.setAttribute('style', 'margin-top: 10%');

        if (list.length == 0)
            myFunction("00000000-0000-0000-0000-000000000000");

        for (var i = 0; i < list.length; i++) {
            if (i == 0) {
                myFunction(list[i]["id"]);
            }
            var item = document.createElement('button');
            item.setAttribute('type', 'button');
            item.setAttribute('class', 'list-group-item');
            item.setAttribute('onclick', 'myFunction("'+list[i]["id"]+'")');
            item.appendChild(document.createTextNode(list[i]["topicName"]));
            div.appendChild(item);
        }
     }
});

$(document).ready(function () {
    $.ajax({
        url: "/Technologies/GetAll",
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            f(response)
        }
    });

    function f(list) {
        var div = document.getElementById('technologiesListImg');
        div.setAttribute('class', 'row')

        for (var i = 0; i < list.length; i++) {
            var item = document.createElement('div');
            item.setAttribute('class', 'col-md-3');

            var tumb = document.createElement('div');
            tumb.setAttribute('class', 'thumbnail');

            var a = document.createElement('a');
            a.setAttribute("href", "/Home/Learn?technology=" + list[i]["id"]);

            var img = document.createElement('img');
            img.setAttribute('src', list[i]["urlImage"]);
            img.setAttribute('style', "width:100%");
            img.setAttribute('alt', 'technology');

            a.appendChild(img);
            tumb.appendChild(a);
            item.appendChild(tumb);
            div.appendChild(item);

        }
    }
});


function myFunction(idTopic) {
        $.ajax({
            url: "/Materials/GetMaterialsByTopicID/" + idTopic,
            type: 'GET',
            dataType: 'json',
            success: function(response) {
                f(response)
            }
        });
    
    function f(list) {
        var div = document.getElementById('materialsList');
        div.setAttribute('class', 'carousel-inner');
        div.setAttribute('role', 'listbox');

        while (div.firstChild) {
            div.removeChild(div.firstChild);
        }

        if (list.length == 0) {
            var item = document.createElement('div');
            item.setAttribute('class', 'item active');
            var img = document.createElement('img');
            img.setAttribute('src', '/materials/nothing.jpg');
            img.setAttribute('width', "460");
            img.setAttribute('height', "345");
            img.setAttribute('style', "margin: 0 auto;");

            item.appendChild(img);
            div.appendChild(item);
        }

        for (var i = 0; i <= list.length; i++) {
            var item = document.createElement('div');
            if (i==0)
                item.setAttribute('class', 'item active');
            else
                item.setAttribute('class', 'item');
            var img = document.createElement('img');
            img.setAttribute('src', list[i]["urlMaterial"]);
            img.setAttribute('width', "460");
            img.setAttribute('height', "345");
            img.setAttribute('style', "margin: 0 auto;");

            item.appendChild(img);
            div.appendChild(item);
        }
    }
}

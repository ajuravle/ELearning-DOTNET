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

        for (var i = 0; i < list.length; i++) {
            var item = document.createElement('button');
            item.setAttribute('type', 'button');
            item.setAttribute('class', 'list-group-item');
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
        div.setAttribute('class', 'row');

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
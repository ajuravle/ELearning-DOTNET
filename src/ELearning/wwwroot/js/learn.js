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
            item.appendChild(document.createTextNode(list[i]["technologyName"]));
            div.appendChild(item);
        }

        console.log("aaa", location.search.split("=")[1]);
    }
});
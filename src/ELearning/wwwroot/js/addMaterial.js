$(document).ready(function () {
    $('#IdTechnology').on('change', function () {
        var technologyId = $('#IdTechnology').val();
        getTopicByTechnology(technologyId);
    });

    function getTopicByTechnology(technologyId) {
        function onSuccess(topics) {
            var selectTopics = $('#IdTopic');
            selectTopics.empty();
            
            topics.forEach(function (topic) {
                selectTopics.append($("<option></option>")
                   .attr("value", topic.id).text(topic.topicName));
            });
        }

        function onError(err) {
            console.log('error get topics by technology id');
        }

        $.ajax({
            type: 'GET',
            url: '/Topics/GetTopicsByTechnologyID?id=' + technologyId,
            dataType: 'json',
            contentType: 'application/json',
            success: onSuccess,
            error: onError
        });
    }
});
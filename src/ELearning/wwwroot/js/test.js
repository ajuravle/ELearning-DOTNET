function createQuestionsList() {
    var id = document.getElementById("testButton").getAttribute("value");
    $.ajax({
        url: "/Questions/GetQuestionsByTopicID/" + id,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            test = document.getElementById("tests");
            if (response == null || response.length == 0) {
                while (test.firstChild) {
                    test.removeChild(test.firstChild);
                }
                var h = document.createElement("h4");
                h.appendChild(document.createTextNode("No test yet"));
                test.appendChild(h);
            } else {
                f(response);
                var button = document.createElement("button");
                button.setAttribute("id", "submitTestButton");
                button.appendChild(document.createTextNode("Finish test"));
                button.setAttribute("class", "btn btn-primary");
                button.setAttribute("style", "margin-left: 30px");
                button.setAttribute("onclick", "submit_test()");
                test.appendChild(button);
                var testRes = document.createElement("h4");
                testRes.setAttribute("id", "testResult");
                testRes.setAttribute("style", "margin-left: 35px; margin-top: 20px");
                test.appendChild(testRes);

            }
        }
    });

    function f(response) {
        
        var test = document.getElementById("tests");
        while (test.firstChild) {
            test.removeChild(test.firstChild);
        }
        var ol = document.createElement("ol");
        for (var i = 0; i < response.length; i++) {
            
            var rr = response[i];
            (function(rr) {
                $.ajax({
                    url: "/Answers/GetByQuestionID/" + response[i]["id"],
                    type: 'GET',
                    dataType: 'json',
                    success: function(res) {
                        ol.appendChild(f2(rr, res));
                    }
                });
            }(rr));
        }
        test.appendChild(ol);
    }

    function f2(res1, res2) {
        var li = document.createElement("li");
        var qh4 = document.createElement("h4");
        qh4.appendChild(document.createTextNode(res1["questionText"]));
        li.appendChild(qh4);
        var answers = document.createElement("div");

        for (var j = 0; j < res2.length; j++) {
            var ck = document.createElement('input');
            ck.setAttribute('type', "checkbox");
            ck.setAttribute('name', "testAns");
            ck.setAttribute('style', "vertical-align:middle; float: left");
            ck.setAttribute('value', res2[j]["correct"]);
            var p = document.createElement("h4");

            p.setAttribute("style", "padding-right: 15px; padding-left: 5px; margin-left: 15px;");
            p.appendChild(document.createTextNode(res2[j]["answerText"]));
            p.setAttribute('name', "testAnsText");
            answers.appendChild(ck);
            answers.appendChild(p);
        }
        li.appendChild(answers);
        return li;
    }
}

function submit_test() {
    var testResult = 0;
    var answers = document.getElementsByName("testAns");
    var answersText = document.getElementsByName("testAnsText");
    for (var i = 0; i < answers.length; i++) {
        answers[i].setAttribute("disabled", "true");
        if (answers[i].value == "true") {
            if (answers[i].checked == true) testResult++;
            answersText[i].setAttribute("class", "text-success");
        } else {
            if (answers[i].checked == true) testResult--;
            answersText[i].setAttribute("class", "text-danger");
        }
    }
    document.getElementById("submitTestButton").style.display = 'none';
    document.getElementById("testResult").appendChild(document.createTextNode("Result: " + testResult));

}
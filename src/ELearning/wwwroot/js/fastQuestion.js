$(document).ready(function () {
    $("input[type=radio][name=radioA]").change(function () {
        if (this.value == 'choice') {
            document.getElementById("nrCharactersAnswerDiv").style.display = 'none';
            document.getElementById("answers").style.display = 'block';
            document.getElementById("buttonAddAnswer").style.display = 'block';
        }
        else if (this.value == 'text') {
            document.getElementById("nrCharactersAnswerDiv").style.display = 'block';
            document.getElementById("answers").style.display = 'none';
            document.getElementById("buttonAddAnswer").style.display = 'none';
        }
    });
});

function addAnswer() {
    var answers = document.getElementById('answers');
    var div = document.createElement('div');
    div.setAttribute('class', "form-group row");

    var label = document.createElement('label');
    label.setAttribute('class', "col-sm-2 col-form-label");
    label.appendChild(document.createTextNode("Answer option " + (answers.childElementCount+1)+":"));

    var div2 = document.createElement('div');
    div2.setAttribute('class', "col-sm-10");

    var input = document.createElement('input');
    input.setAttribute('class', "form-control");
    input.setAttribute('type', "text");
    input.setAttribute('placeholder', "Enter answer");
    input.setAttribute('style', "float: left; margin-right: 15px");
    input.setAttribute('id', "ans" + answers.childElementCount);

    var input2 = document.createElement('input');
    input2.setAttribute('type', "checkbox");
    input2.setAttribute('style', "vertical-align:middle");
    input2.setAttribute('id', "check" + answers.childElementCount);

    div2.appendChild(input);
    div2.appendChild(input2);
    div.appendChild(label);
    div.appendChild(div2);
    answers.appendChild(div);
}

function validateQ() {
    var obj1 = document.getElementById('fastQuestion');
    if (obj1.value == '') {
        document.getElementById('err_q').appendChild(document.createTextNode("The Question field is required"));
        obj1.focus();
        return false;
    } else {
        if (document.getElementById('err_q').firstChild)
        document.getElementById('err_q').removeChild(document.getElementById('err_q').firstChild);
        submitQ()
        return true;
    }
}

function submitQ() {
    var quesion = document.getElementById('fastQuestion');
    var types = document.getElementsByName('radioA');
    var sel_type;
    for (var i = 0; i < types.length; i++) {
        if (types[i].checked) {
            if (types[i].value == 'text')
                sel_type = 0;
            else {
                sel_type = 1;
            }
        }
    }
    var nr;
    if (sel_type == 0) {
        nr = document.getElementById('nrCharactersAnswer').value;
    } else {
        nr = document.getElementById('answers').childElementCount;
    }


    var data = {
        QuestionText: quesion.value,
        Type: sel_type,
        Number: nr,
        Active: 1
    }

    $.ajax({
        type: 'POST',
        url: '/FastQuestion/Create',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: function(result) {
            
            post_ans(result);
        }
    });

    function post_ans(result) {
        var id_q = result["id"];
        if (sel_type == 1) {
            for (var i = 0; i < nr; i++) {
                var ans = document.getElementById("ans" + i).value
                var ischecked = document.getElementById("check" + i).checked

                var dataA = {
                    QuestionId: id_q,
                    AnswerText: ans,
                    Correct: ischecked
                }

                $.ajax({
                    type: 'POST',
                    url: '/Answers/AddFastAnswer',
                    dataType: 'json',
                    contentType: 'application/json',
                    data: JSON.stringify(dataA),
                    success: function (result) {
                        console.log('Data received: ');
                        console.log(result);
                    }
                });
            }

        }
        window.location.replace("/Home/FastQuestionResponse");
    }
}

$(document).ready(function () {
    var question = document.getElementById("fastQuestionR");
    $.ajax({
        url: "/FastQuestion/GetActive",
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response != null) idR = response["id"];
            else idR = "00000000-0000-0000-0000-000000000000";
            $.ajax({
                url: "/FastAnswer/GetUserByQuestion/" + idR,
                type: 'GET',
                dataType: 'json',
                success: function (res) {
                    if (response == null) {
                        if (document
                                .getElementById("fastQuestionR")
                        )
                            document.getElementById("fastQuestionR")
                                .appendChild(document.createTextNode("No active question"));
                        if (document.getElementById("buttonAnswer")) {
                            document.getElementById("buttonAnswer").style.display = 'none';
                        }
                    } else {
                        question.appendChild(document.createTextNode(response["questionText"]));
                        if (response["type"] == 0) {
                            if (document
                                    .getElementById("answersResp")
                            ) document.getElementById("answersResp").style.display = 'none';
                            if (document
                                    .getElementById("answersStud")
                            ) document.getElementById("answersStud").style.display = 'none';
                            if (document.getElementById("textAreaStud")) {
                                document.getElementById("textAreaStud").style.display = 'block';
                                document
                                    .getElementById("fastAnswer")
                                    .setAttribute("placeholder", "Max characters number: " + response["number"]);
                            }
                        } else {
                            if (document
                                    .getElementById("textAreaStud")
                            ) document.getElementById("textAreaStud").style.display = 'none';
                            if (document
                                    .getElementById("answersStud")
                            ) document.getElementById("answersStud").style.display = 'block';

                        }
                        get_question_id(response["id"],res);
                    }
                }
            });
        }
    });

        function get_question_id(qID,res) {
        $.ajax({
            url: "/Answers/GetByQuestionID/" + qID,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (document.getElementById("answersR")) {
                    get_answers(response);
                }
                if (document.getElementById("answersS")) {
                    get_answers2(response,res);
                }
            }
        });
    }

    function get_answers(response) {
        var answers = document.getElementById("answersR");
        
        for (var i = 0; i < response.length; i++) {
            var p = document.createElement("h4");
            if (response[i]["correct"])
                p.setAttribute("class", "text-success");
            else
                p.setAttribute("class", "text-danger");
            p.setAttribute("style", "padding-right: 15px; padding-left: 5px; margin-left: 15px;");
            p.appendChild(document.createTextNode(response[i]["answerText"]));
            answers.appendChild(p);
        }
    }

    function get_answers2(response, res) {
        if (res == null) {
            console.log(res);
            var answers = document.getElementById("answersS");

            for (var i = 0; i < response.length; i++) {
                var ck = document.createElement('input');
                ck.setAttribute('type', "checkbox");
                ck.setAttribute('style', "vertical-align:middle; float: left");
                ck.setAttribute('id', "ck" + i);
                ck.setAttribute('value', response[i]["id"]);
                ck.setAttribute('data-valuetwo', response[i]["correct"]);
                ck.setAttribute('name', "ck_name");

                var p = document.createElement("h4");

                p.setAttribute("style", "padding-right: 15px; padding-left: 5px; margin-left: 15px;");
                p.appendChild(document.createTextNode(response[i]["answerText"]));
                p.setAttribute('id', "ckaa" + i);
                answers.appendChild(ck);
                answers.appendChild(p);
            }
        } else {
            document.getElementById("activeAnswer").style.display = 'none';
            document.getElementById("fastQuestionR").removeChild(document.getElementById("fastQuestionR").firstChild);

            document.getElementById("fastQuestionR")
                                .appendChild(document.createTextNode("No active question"));
        }
    }

});

function post_answerStudent() {
    $.ajax({
        url: "/FastQuestion/GetActive",
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            postUserQA(response["id"]);
            if (response["type"]==0)
                f(response);
            else {
                f2(response);
            }
            document.getElementById("activeAnswer").style.display = 'none';
        }
    });

    //---------------------------------------------------------
    function postUserQA(questionID) {
        $.ajax({
            url: "/Account/GetAll",
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response[0] != null) {
                    var data = {
                        "idUser": response[4],
                        "idQA": questionID
                    }
                    $.ajax({
                        type: 'POST',
                        url: '/FastAnswer/AddUserResponse',
                        dataType: 'json',
                        contentType: 'application/json',
                        data: JSON.stringify(data),
                        success: function (result) {
                         
                        }
                    });

                }
            }
        });
    }
    //---------------------------------------------------------

    function f(response) {
        text = document.getElementById("fastAnswer").value;
        if (document.getElementById('err_a').firstChild)
            document.getElementById('err_a').removeChild(document.getElementById('err_a').firstChild);

        if (response["number"] < text.length) {
            document.getElementById('err_a').appendChild(document.createTextNode("Max characters number: "+response["number"]));

        } else {
        
            var dataA = {
                "questionID": response["id"],
                "answer": text
            }

            $.ajax({
                type: 'POST',
                url: '/FastAnswer/Create',
                dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify(dataA),
                success: function(result) {
                   
                }
            });
            document.getElementById("inactiveAnswer").appendChild(document.createTextNode("Your answer:\xa0\xa0\xa0\xa0" + text));
        }
    }

    function f2(response) {
        var rasp = [];
        var correct = [];
        for (var i = 0; i < response["number"]; i++) {
            var ck = document.getElementById("ck" + i.toString());
            if (ck.checked) {
                var dataA = {
                    "questionID": response["id"],
                    "answer": ck.value
                }
                var ckaa = document.getElementById("ckaa" + i.toString()).textContent;
                rasp.push(ckaa);
                correct.push(ck.getAttribute("data-valuetwo"));

                $.ajax({
                    type: 'POST',
                    url: '/FastAnswer/Create',
                    dataType: 'json',
                    contentType: 'application/json',
                    data: JSON.stringify(dataA),
                    success: function (result) {
                       
                    }
                });
            }
        }
        var inactiveAnswer = document.getElementById("inactiveAnswer")
        inactiveAnswer.appendChild(document.createTextNode("Your answers:"));
        for (var i = 0; i < rasp.length; i++) {
            var p = document.createElement("h4");
            p.setAttribute("style", "padding-right: 15px; padding-left: 5px; margin-left: 15px;");
            if (correct[i]=="true")
                p.setAttribute("class", "text-success");
            else
                p.setAttribute("class", "text-danger");
            p.appendChild(document.createTextNode(rasp[i]));
            inactiveAnswer.appendChild(p);
        }

    }
}

function get_answerStudent() {
    $.ajax({
        url: "/FastQuestion/GetActive",
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            document.getElementById("getAnswersButton").style.display = 'none';
            document.getElementById("studentsAnswers").style.display = 'block';
            f(response);
        }
    });

    function f(question) {
        $.ajax({
            url: "/FastQuestion/StopTest",
            type: 'POST'
        });
        $.ajax({
            url: "/FastAnswer/GetByQuestionID/" + question["id"],
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                f2(res, question);
            }
        });
    }

    function f2(answers, question) {
        if (question["type"] == 0) {
            var table = document.getElementById("answersTable");
            for (var i = 0; i < answers.length; i++) {
                var tr = document.createElement("tr");
                var td = document.createElement("td");
                td.appendChild(document.createTextNode(answers[i]["answer"]));
                tr.appendChild(td);
                table.appendChild(tr);
            }
        } else {

            $.ajax({
                url: "/Answers/GetByQuestionID/" + question["id"],
                type: 'GET',
                dataType: 'json',
                success: function (res) {
                    var table = document.getElementById("answersTable");
                    for (var i = 0; i < res.length; i++) {
                        var tr = document.createElement("tr");
                        var td1 = document.createElement("td");
                        var nr = 0;
                        for (var j = 0; j < answers.length; j++) {
                            
                            if (answers[j]["answer"] == res[i]["id"])
                                nr++;
                        }
                        td1.appendChild(document.createTextNode(nr + " (" + (nr*100 / answers.length).toFixed(2) + "%)"));
                        td1.setAttribute("class","col-md-1");

                        var td2 = document.createElement("td");
                        td2.appendChild(document.createTextNode(res[i]["answerText"]));
                        td2.setAttribute("class", "col-md-11");

                        tr.appendChild(td1);
                        tr.appendChild(td2);
                        table.appendChild(tr);
                    }

                }
            });
        }
    }

}
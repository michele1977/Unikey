
//This forces page to reload when user goes back with browser arrows
$(document).ready(function () {     
    window.addEventListener("pageshow", function (event) {
        var historyTraversal = event.persisted ||
        (typeof window.performance != "undefined" &&
            window.performance.navigation.type === 2);
        if (historyTraversal) {
            // Handle page restore.
            window.location.reload();
        }
    });
});

//$('#delIcolink').click(showLoadingSpinner());

function showLoadingSpinner() {
    $('#cover-spin').show();
}

function hideLoadingSpinner() {
    $('#cover-spin').hide();
}


function deleteConfirmation(id, pageNumber, pageSize) {

    let deletionData = { "Id": id, "PageNumber": pageNumber, "PageSize": pageSize };

    if (confirm("Are you sure you want to delete this test?")) {
        showLoadingSpinner();
        $.ajax({
            url: "/Test/DeleteTest",
            data: deletionData,
            method: "POST"
        }).then(
            function (response) {
                hideLoadingSpinner();
                window.location.href = response.redirectUrl;
            }, 
            function() {
                var label = document.getElementById("ErrorAlert");
                if (label.style.display === "none") {
                    label.style.display = "block";
                    window.scrollTo({ top: 0, behavior: 'smooth' });
                }
            }
        );
    }
}

function resizePage(size, pageNumber, url) {

    showLoadingSpinner();

    $.ajax({
        url: url + size + "&PageNumber=" + pageNumber,
        method: "GET",
        dataType: 'html',
    }).then(
        function (response) {
            hideLoadingSpinner();
            $('#renderDiv').empty();
            $('#renderDiv').html(response);
        }
    );

}

function send() {

    let data = { "email": document.getElementById("email-address").value, "name": document.getElementById("subject-name").value, "Id": document.getElementById("selectedTest").value };
    let myurl = "/Test/SendMail";
    showLoadingSpinner();
    $.ajax({ url: myurl, data: data, method:"POST" }).then(
        function (response) {
            hideLoadingSpinner();
            if (response.result == true) {
                document.getElementById("false").setAttribute("hidden", "hidden");
                document.getElementById("true").removeAttribute("hidden");
                document.getElementById("subject-name").value = null;
                document.getElementById("email-address").value = null;
            } else {
                document.getElementById("true").setAttribute("hidden", "hidden");
                document.getElementById("false").removeAttribute("hidden");
                document.getElementById("subject-name").value = null;
                document.getElementById("email-address").value = null;
            }
        });
}
function getDetailsTablePartial(id) {
    function functionOk(resp) {
        $("#myrender").html(resp);
        $('#myModal').modal('show');
        $('#cover-spin').hide();
    }
    function functionKo() {
        alert('ko');
    }
    let myurl = "/ExTest/DetailsTablePartial?testId=" + id;
    //$('#myModal').modal('show');
    //alert($('#myModal'));
    $('#cover-spin').show();
    $.ajax({ url: myurl, method: "GET" }).then(functionOk,functionKo);
}

function closeErrorAlert() {
        var label = document.getElementById("ErrorAlert");
        if (label.style.display === "block") {
            label.style.display = "none";
        }
}
//function getAnswer(Answers) {
//    function functionOk(resp) {
//        alert('ok');
//    }
//    function functionKo() {
//        alert('ko');
//    }
//    let myurl = "/Test/EditQuestions";

//    $.ajax({ url: myurl, method: "POST", data: Answers }).then(functionOk, functionKo);
//}



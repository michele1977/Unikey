function deleteConfirmation(id, pageNumber, pageSize) {

    let deletionData = { "Id": id, "PageNumber": pageNumber, "PageSize": pageSize };

    if (confirm("Are you sure you want to delete this test?")) {
        $.ajax({
            url: "/Test/DeleteTest",
            data: deletionData,
            method: "POST"
        }).then(
            function (response) {
                window.location.href = response.redirectUrl;
            });
    }
}
function resizePage(size, pageNumber, url) {

    window.location.href = url + size + "&PageNumber=" + pageNumber;

}

function send() {

    let data = { "email": document.getElementById("email-address").value, "name": document.getElementById("subject-name").value, "Id": document.getElementById("selectedTest").value };
    let myurl = "/Test/SendMail";
    $.ajax({ url: myurl, data: data, method:"POST" }).then(
        function (response) {
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
    }
    function functionKo() {
        alert('ko');
    }
    let myurl = "/ExTest/DetailsTablePartial?testId=" + id;
    //$('#myModal').modal('show');
    //alert($('#myModal'));
    $.ajax({ url: myurl, method: "GET" }).then(functionOk,functionKo);
}


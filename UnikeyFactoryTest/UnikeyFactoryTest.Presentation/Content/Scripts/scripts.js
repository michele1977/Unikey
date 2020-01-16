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

    var recipient;

    let data = { "email": document.getElementById("email-address").value, "name": document.getElementById("subject-name").value, "Id": document.getElementById("ID").value };
    let myurl = "/Test/SendMail";
    $.ajax({ url: myurl, data: data, method:"POST" }).then(
        function (response) {
            if (response.result == true) {
                document.getElementById("true").removeAttribute("hidden");
                document.getElementById("subject-name").value = null;
                document.getElementById("email-address").value = null;
            } else {
                document.getElementById("false").removeAttribute("hidden");
                document.getElementById("subject-name").value = null;
                document.getElementById("email-address").value = null;
            }
        });
}


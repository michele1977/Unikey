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

function closeErrorAlert() {
        var label = document.getElementById("ErrorAlert");
        if (label.style.display === "block") {
            label.style.display = "none";
        }
}


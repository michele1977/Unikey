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

function resizePage(size, pageNumber) {

    window.location.href = "/Test/TestsList?PageSize=" + size + "&PageNumber=" + pageNumber;

}
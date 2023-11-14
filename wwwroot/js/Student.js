function confirmDelete(studentId) {
    if (confirm("Are you sure you want to delete this Student")) {
        window.location.href = "/Students/" + studentId + "/Delete"
    }
}
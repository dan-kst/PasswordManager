// wwwroot/js/table-filter.js
function filterTableByName() {
    var input = document.getElementById("jsFilterInput");
    var filter = input.value.toLowerCase();
    var table = document.getElementById("secretsTable");
    var trs = table.getElementsByTagName("tr");

    // Start from 1 to skip the header row
    for (var i = 1; i < trs.length; i++) {
        var td = trs[i].getElementsByTagName("td")[0]; // Name column
        if (td) {
            var txtValue = td.textContent || td.innerText;
            trs[i].style.display = txtValue.toLowerCase().indexOf(filter) > -1 ? "" : "none";
        }
    }
}
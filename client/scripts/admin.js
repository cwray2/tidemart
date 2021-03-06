const baseUrl = "https://localhost:6969/api/users";

function deleteFunction(id) {
  const deleteUserApiUrl = baseUrl + "/" + id;
  fetch(deleteUserApiUrl, {
    method: "DELETE",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
  }).then((response) => {
    //populateList();
  });
}

$(document).ready(function () {
  var columnDefs = [
    {
      data: "userID",
      title: "ID #",
      type: "readonly",
    },
    {
      data: "firstName",
      title: "First Name",
    },
    {
      data: "lastName",
      title: "Last Name",
    },
    {
      data: "username",
      title: "Username",
    },
    {
      data: "phone",
      title: "Phone #",
    },
    {
      data: "startDate",
      title: "Start date",
    },
    {
      data: "salary",
      title: "Salary",
    },
  ];

  var myTable;

  var url_ws_mock_get = "./mock_svc_load.json";
  var url_ws_mock_ok = "./mock_svc_ok.json";
  if (location.href.startsWith("file://")) {
    // local URL's are not allowed
    url_ws_mock_get = "https://localhost:6969/api/users";
    url_ws_mock_delete = "https://localhost:6969/api/users";
    url_ws_mock_ok =
      "https://luca-vercelli.github.io/DataTable-AltEditor/example/03_ajax_objects/mock_svc_ok.json";
  }

  myTable = $("#example").DataTable({
    sPaginationType: "full_numbers",
    ajax: {
      url: url_ws_mock_get,
      // our data is an array of objects, in the root node instead of /data node, so we need 'dataSrc' parameter
      dataSrc: "",
    },
    columns: columnDefs,
    dom: "Bfrtip", // Needs button container
    select: "single",
    responsive: true,
    altEditor: true, // Enable altEditor
    buttons: [
      {
        text: "Add",
        name: "add", // do not change name
      },
      {
        extend: "selected", // Bind to Selected row
        text: "Edit",
        name: "edit", // do not change name
      },
      {
        extend: "selected", // Bind to Selected row
        text: "Delete",
        name: "delete", // do not change name
      },
      {
        text: "Refresh",
        name: "refresh", // do not change name
      },
      {
        text: "excel",
        name: "excel"
      }
    ],
    onAddRow: function (datatable, rowdata, success, error) {
      $.ajax({
        // a tipycal url would be / with type='PUT'
        url: url_ws_mock_get,
        type: "GET",
        data: rowdata,
        success: success,
        error: error,
      });
    },
    onDeleteRow: function (datatable, rowdata, success, error) {
      deleteFunction(rowdata[0].userID);
      $.ajax({
        // a tipycal url would be /{id} with type='DELETE'
        url: url_ws_mock_get,
        type: "GET",
        data: rowdata,
        success: success,
        error: error,
      });
    },
    onEditRow: function (datatable, rowdata, success, error) {
      $.ajax({
        // a tipycal url would be /{id} with type='POST'
        url: url_ws_mock_ok,
        type: "GET",
        data: rowdata,
        success: success,
        error: error,
      });
    },
  });
});

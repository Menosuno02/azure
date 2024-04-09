let urlApi =
    "https://apicorecruddepartamentos2024alejo.azurewebsites.net/";

function deleteDepartamentoAsync(id, callBack) {
    let request = "api/departamentos/" + id;
    $.ajax({
        url: urlApi + request,
        type: "DELETE",
        success: function () {
            callBack();
        }
    });
}

function getJsonDepartamentosAsync(callBack) {
    let request = "api/departamentos";
    $.ajax({
        url: urlApi + request,
        type: "GET",
        dataType: "json",
        success: function (data) {
            callBack(data);
        }
    });
}

function insertDepartamentoAsync(id, nombre, localidad, callBack) {
    let dataJSON = convertDeptToJson(id, nombre, localidad);
    let request = "api/departamentos";
    $.ajax({
        url: urlApi + request,
        type: "POST",
        data: dataJSON,
        contentType: "application/json",
        success: function () {
            callBack();
        }
    });
}

function updateDepartamentoAsync(id, nombre, localidad, callBack) {
    let dataJson = convertDeptToJson(id, nombre, localidad);
    let request = "api/departamentos";
    $.ajax({
        url: urlApi + request,
        type: "PUT",
        data: dataJson,
        contentType: "application/json",
        success: function () {
            callBack();
        }
    });
}

function convertDeptToJson(id, nombre, localidad) {
    let dept = new Object();
    dept.deptNo = id;
    dept.nombre = nombre;
    dept.localidad = localidad;
    let dataJSON = JSON.stringify(dept);
    return dataJSON;
}
app.controller("CargoControllerIndex", function ($scope, $http, $window) {
    $scope.Cargos = [];
    $http.get("/Cargo/GetCargo").success(function (dados) {
        $scope.Cargos = dados;
    });

    $scope.Excluir = function(id) {
        swal({
            title: "Alerta!",
            text: "Tem certeza que deseja excluir o Cargo?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Sim",
            cancelButtonText: "Não",
            closeOnConfirm: false
        }, function () {
            $http.post("/Cargo/ExcluirCargo/", { ID: id }).success(function (data) {
                if (data.retorno) {
                    //swal({
                    //    title: "Aviso!",
                    //    text: data.mensagem,
                    //    type: "success",
                    //    showConfirmButton: false,
                    //    closeOnConfirm: true,
                    //    timer: 1500
                    //}, function () {
                    //    $window.location.href = "/Cargo";
                    //});
                    $window.location.href = "/Cargo";
                } else {
                    swal("Alerta", data.mensagem, "warning");
                }
            }).error(function (data) {
                swal("Falha: ", data.mensagem, "error");
            });
        });
    };

    $scope.Novo = function () {
        $window.location.href = "/Cargo/Novo";
    };

    $scope.Editar = function (id) {
        $window.location.href = "/Cargo/Editar/"+id;
    };
});

app.controller("CargoControllerNovo_Editar", function ($scope, $http, $window) {
    $scope.cargo = new Object();
    var parametro = GetParametro();
   
    $scope.Salvar = function (cargo) {
        $http.post("/Cargo/Salvar", cargo).success(function(dados) {
            if (dados.retorno) {
                swal({
                    title: "Aviso!",
                    text: dados.mensagem,
                    type: "success",
                    showConfirmButton: false,
                    closeOnConfirm: false,
                    timer: 1500
                }, function () {
                    $scope.Voltar();
                });
            } else {
                swal("Falha", dados.mensagem, "error");
            }
        }).error(function(erro) {
            swal("Falha", erro.mensagem, "error");
        });
    };

    if (!angular.equals("Novo", parametro)) {
        $http.get("/Cargo/GetCargoObj", { params: { ID: parametro } }).success(function (dados) {
            $scope.cargo = dados;
        });
    };

    $scope.Voltar = function () {
        $window.location.href = "/Cargo";
    };
});

function GetParametro() {
    var url = document.location.href.split("/");
    var parametro = url[url.length - 1];
    return parametro;
}
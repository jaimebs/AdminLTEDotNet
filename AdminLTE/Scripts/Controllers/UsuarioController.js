app.controller("UsuarioControllerIndex", function ($scope, $http, $window, Upload) {
    $scope.Usuarios = [];

    $scope.Novo = function() {
        $window.location.href = "/Usuario/Novo";
    };

    $scope.Editar = function (id) {
        $window.location.href = "/Usuario/Editar/"+id;
    };

    $scope.Logar = function(usuario) {
        $http.post("/Login/Logar", usuario).success(function(dados) {
            if (dados.retorno) {
                $window.location.href = "/Home";
            } else {
                swal("Alerta", dados.mensagem, "warning");
            }
        });
    };

    $scope.Excluir = function (id) {
        swal({
            title: "Alerta!",
            text: "Tem certeza que deseja excluir o Usuário?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Sim",
            cancelButtonText: "Não",
            closeOnConfirm: false
        }, function () {
            $http.post("/Usuario/ExcluirUsuario/", { ID: id }).success(function (data) {
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
                    $window.location.href = "/Usuario";
                } else {
                    swal("Alerta", data.mensagem, "warning");
                }
            }).error(function (data) {
                swal("Falha: ", data.mensagem, "error");
            });
        });
    };

    $http.get("/Usuario/GetUsuario").success(function(dados) {
        $scope.Usuarios = dados;
    });

    $scope.submit = function () {
        if ($scope.form.file.$valid && $scope.file) {
            $scope.upload($scope.file);
        }
    };

    // upload on file select or drop
    $scope.upload = function (file) {
       Upload.upload({
            url: 'Usuario/Upload',
            data: { arquivo: file }
        }).then(function (resp) {
            //console.log(resp.data.retorno);
            if (resp.data.retorno) {
                swal("Alerta", "Arquivo feiro Upload com sucesso", "success");
            };
       }, function (resp) {
            //console.log('Error status: ' + resp.status);
        }, function (evt) {
            //var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
            //console.log('progress: ' + progressPercentage + '% ' + evt.config.data.file.name);
        });
    };


});

app.controller("UsuarioControllerNovo_Editar", function ($scope, $http, $window) {
    $scope.usuario = {};
    var paramentro = GetParametro();
    $scope.Cargos = [];
    $scope.Telefones = [];
   
    $http.get("/Cargo/GetCargo").success(function (dados) {
        $scope.Cargos = dados;
    });

    $scope.Incluir = function(telefone)
    {
        $scope.Telefones.push({
            Telefone: telefone,
            UsuarioID : $scope.usuario.ID
        });
        $scope.usuario.Telefone = "";
    };

    $scope.Salvar = function (usuario) {
        usuario.Telefones = $scope.Telefones;

        if($scope.ValidarUsuarioTelefone(usuario.Telefones))
        {
            swal("Alerta", "Digite um Telefone para salvar!", "warning");
            return;
        }

        $http.post("/Usuario/Salvar", usuario).success(function (dados) {
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
        }).error(function (erro) {
            swal("Falha", erro.mensagem, "error");
        });
    };

    if (!angular.equals("Novo",paramentro)) {
        $http.get("/Usuario/GetUsuarioObj/", { params: { ID: paramentro } }).success(function(dados) {
            $scope.usuario = dados;
            //usuario.Telefones = dados.Telefones;
            angular.forEach(dados.Telefones, function(valor,chave) {
                $scope.Telefones.push({
                    ID:valor.ID,
                    Telefone: valor.Telefone,
                    UsuarioID : valor.UsuarioID
                });
            });
        });
    }

    $scope.ExcluirUsuarioTelefone = function (id,index) {
        swal({
            title: "Alerta!",
            text: "Tem certeza que deseja excluir o Telefone?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Sim",
            cancelButtonText: "Não",
            closeOnConfirm: true
        }, function () {
            $http.post("/Usuario/ExcluirUsuarioTelefone/", { ID: id }).success(function (data) {
                if (data.retorno) {
                    $scope.Telefones.splice(index, 1);
                } else {
                    swal("Alerta", data.mensagem, "warning");
                }
            }).error(function (data) {
                swal("Falha: ", data.mensagem, "error");
            });
        });
    };

    $scope.ValidarUsuarioTelefone = function (lista) {
        var flag = false;

        angular.forEach(lista, function (valor) {
            if (!valor.Telefone) {
                flag = true;
            }
        });

        return flag;
    };
    
    $scope.Voltar = function () {
        $window.location.href = "/Usuario";
    };
});

function GetParametro() {
    var url = document.location.href.split("/");
    var parametro = url[url.length - 1];
    return parametro;
}
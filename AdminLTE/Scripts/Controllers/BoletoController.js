app.controller("BoletoControllerIndex", function ($scope, $http, $window, mascaras) {
    $scope.boleto = new Object();
    $scope.boletos = [];

    $scope.GerarBoleto = function (codBanco) {
        $http.post("/Boleto/VisualizarBoleto", { id: codBanco }).success(function () {
            var url = "/Boleto/VisualizarBoleto/" + codBanco;
            //window.open(url, "WindowPopup", 'width=750,height=780');
            window.open(url, "_blank");
        });
    };

    $http.get("/Boleto/GetBoleto").success(function(dados) {
        $scope.boletos = dados;
    });

    $scope.Salvar = function (boleto) {
        boleto.CnpjCedente = mascaras.MascaraCnpj(boleto.CnpjCedente);
        boleto.CpfSacado = mascaras.MascaraCpf(boleto.CpfSacado);
        $http.post("/Boleto/Salvar", boleto).success(function (dados) {
            if (dados.retorno) {
                swal({
                    title: "Aviso!",
                    text: dados.mensagem,
                    type: "success",
                    showConfirmButton: false,
                    closeOnConfirm: false,
                    timer: 1500
                }, function() {
                    $scope.Voltar();
                });
            } else {
                swal("Falha", dados.mensagem, "error");
            }
        }).error(function(erro) {
            swal("Falha", erro.mensagem, "error");
        });
    };

    $scope.Excluir = function (id) {
        swal({
            title: "Alerta!",
            text: "Tem certeza que deseja excluir o Boleto?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Sim",
            cancelButtonText: "Não",
            closeOnConfirm: false
        }, function () {
            $http.post("/Boleto/ExcluirBoleto/", { ID: id }).success(function (data) {
                if (data.retorno) {
                    $window.location.href = "/Boleto";
                } else {
                    swal("Alerta", data.mensagem, "warning");
                }
            }).error(function (data) {
                swal("Falha: ", data.mensagem, "error");
            });
        });
    };

    $scope.ConsultaCep = function (cep) {
        $http.get("https://viacep.com.br/ws/"+cep+"/json/").success(function (dados) {
            $scope.boleto.Endereco = dados.logradouro;
            $scope.boleto.Bairro = dados.bairro;
            $scope.boleto.Cidade = dados.localidade;
            $scope.boleto.Uf = dados.uf;
        });
    };

    $scope.Novo = function () {
        $window.location.href = "/Boleto/Novo";
    };

    $scope.Editar = function (id) {
        $window.location.href = "/Boleto/Editar/"+id;
    };

    $scope.Voltar = function () {
        $window.location.href = "/Boleto";
    };
});

app.controller("BoletoControllerNovo_Editar", function ($scope, $http, $window, mascaras) {
    $scope.boleto = new Object();
    var parametro = GetParametro();
  
    $http.get("/Boleto/GetBoleto").success(function (dados) {
        $scope.boletos = dados;
    });

    if (!angular.equals("Novo", parametro)) {
        $http.get("/Boleto/GetBoletoObj", { params: { ID: parametro } }).success(function (dados) {
            $scope.boleto = dados;
            $scope.boleto.Vencimento = dados.VencimentoFormatado;
        });
    }

    $scope.Salvar = function (boleto) {
        boleto.CnpjCedente = mascaras.MascaraCnpj(boleto.CnpjCedente);
        boleto.CpfSacado = mascaras.MascaraCpf(boleto.CpfSacado);
        $http.post("/Boleto/Salvar", boleto).success(function (dados) {
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

    $scope.ConsultaCep = function (cep) {
        $http.get("https://viacep.com.br/ws/" + cep + "/json/").success(function (dados) {
            $scope.boleto.Endereco = dados.logradouro;
            $scope.boleto.Bairro = dados.bairro;
            $scope.boleto.Cidade = dados.localidade;
            $scope.boleto.Uf = dados.uf;
        });
    };

    $scope.Voltar = function () {
        $window.location.href = "/Boleto";
    };
});

function GetParametro() {
    var url = document.location.href.split("/");
    var parametro = url[url.length - 1];
    return parametro;
}
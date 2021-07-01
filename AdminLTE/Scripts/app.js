var app = angular.module("app", ["angularUtils.directives.dirPagination", "ui.utils.masks", "ui.mask", "ngFileUpload"]);
//Filter para quando retorna para a view ja ficar no formato de cpf. Uso: ng-bind="variavel | cpf"//
app.filter('cpf', function () {
    return function (cpf) {
        if (!!cpf) {
            return cpf.slice(0, 3) + '.' + cpf.slice(3, 6) + '.' + cpf.slice(6, 9) + '-' + cpf.slice(9, 12);
        }
        return cpf;
    };
});
//Filter para quando retorna para a view ja ficar no formato de cnpj. Uso: ng-bind="variavel | cnpj"//
app.filter('cnpj', function () {
    return function (cnpj) {
        if (!!cnpj) {
            return cnpj.slice(0, 2) + '.' + cnpj.slice(2, 5) + '.' + cnpj.slice(5, 8) + '/' + cnpj.slice(8, 12) + '-' + cnpj.slice(12, 14);
        }
        return cnpj;
    };
});
//Filter para quando retorna para a view ja ficar no formato de telefone. Uso: ng-bind="variavel | telefone"//
app.filter('telefone', function () {
    return function (telefone) {
        if (!!telefone) {
            if (telefone.length === 10) {
                return telefone.slice(0, 0) + '(' + telefone.slice(0, 2) + ') ' + telefone.slice(2, 6) + '-' + telefone.slice(6, 12);
            } else {
                return telefone.slice(0, 0) + '(' + telefone.slice(0, 2) + ') ' + telefone.slice(2, 7) + '-' + telefone.slice(7, 13);
            }
        }
        return telefone;
    };
});
//Diretiva para limitar o tamanho de caracteres em um campo do tipo number. Uso: maxlength="6" aw-limit-length="6"//
app.directive('awLimitLength', function () {
    return {
        restrict: "A",
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            attrs.$set("ngTrim", "false");
            var limitLength = parseInt(attrs.awLimitLength, 10);
            scope.$watch(attrs.ngModel, function (newValue) {
                if (ngModel.$viewValue.length > limitLength) {
                    ngModel.$setViewValue(ngModel.$viewValue.substring(0, limitLength));
                    ngModel.$render();
                }
            });
        }
    };
});
//Servicos criados para validação dos dados.//
app.service("validacoes", function () {
    this.ValidarCnpj = function (_cnpj) {
        var cnpj = _cnpj;

        cnpj = cnpj.replace(/[^\d]+/g, '');
        if (cnpj == '') return false;
        if (cnpj.length != 14)
            return false;
        //Elimina CNPJs invalidos conhecidos
        if (cnpj == "00000000000000" ||
            cnpj == "11111111111111" ||
            cnpj == "22222222222222" ||
            cnpj == "33333333333333" ||
            cnpj == "44444444444444" ||
            cnpj == "55555555555555" ||
            cnpj == "66666666666666" ||
            cnpj == "77777777777777" ||
            cnpj == "88888888888888" ||
            cnpj == "99999999999999")
            return false;

        tamanho = cnpj.length - 2;
        numeros = cnpj.substring(0, tamanho);
        digitos = cnpj.substring(tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(0))
            return false;

        tamanho = tamanho + 1;
        numeros = cnpj.substring(0, tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(1))
            return false;

        return true;
    };
});
//Servicos criados para enviar os dados com mascara para o servidor.//
app.service("mascaras", function () {
    this.MascaraCep = function (cep) {
        cep = cep.replace("-", "");
        if (!!cep) {
            return cep.slice(0, 5) + '-' + cep.slice(5, 8);
        }
        return cep;
    };

    this.MascaraCpf = function (cpf) {
        cpf = cpf.replace(".", "").replace("-", "");
        if (!!cpf) {
            return cpf.slice(0, 3) + '.' + cpf.slice(3, 6) + '.' + cpf.slice(6, 9) + '-' + cpf.slice(9, 11);
        }
        return cpf;
    };

    this.MascaraCnpj = function(cnpj) {
        cnpj = cnpj.replace(".", "").replace("/", "").replace("-", "");
        if (!!cnpj) {
            if (cnpj.length === 14) {
                return cnpj.slice(0, 2) + '.' + cnpj.slice(2, 5) + '.' + cnpj.slice(5, 8) + '/' + cnpj.slice(8, 12) + '-' + cnpj.slice(12, 14);
            }
        }
        return cnpj;
    };

    this.MascaraTelefone = function(telefone) {
        telefone = telefone.replace("(", "").replace(")", "").replace("-", "");
        if (!!telefone) {
            if (telefone.length === 10) {
                return telefone.slice(0, 0) + '(' + telefone.slice(0, 2) + ')' + telefone.slice(2, 6) + '-' + telefone.slice(6, 12);
            } else {
                return telefone.slice(0, 0) + '(' + telefone.slice(0, 2) + ')' + telefone.slice(2, 7) + '-' + telefone.slice(7, 13);
            }
        }
        return telefone;
    };

    this.MascaraData = function (data) {
        if (!!data) {
            return moment().locale('pt-br').format('L', data);
        }
        return data;
    }
});
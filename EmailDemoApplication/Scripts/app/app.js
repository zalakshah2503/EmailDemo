var EmailApp = angular.module('EmailApp', ["ngRoute","ui.bootstrap.modal"]);

EmailApp.config(["$routeProvider", function config($routeProvider) {
    $routeProvider
        .when('/', {
            template: '<emailcontent-list></emailcontent-list>'
        });
}
]);
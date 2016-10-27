'use strict';

var appLogin = angular.module("DMS", ["ui.router","ngResource"]);

appLogin.config(function ($stateProvider, $urlRouterProvider, $resourceProvider, $locationProvider) {

   // $locationProvider.html5mode(true);
    var DocumentDetail = {
        name: 'DocumentDetail',
        url: '/DocumentDetail',
        templateUrl: 'Templates/DocumentDetail.html',
    };        
   $stateProvider.state(DocumentDetail);
   //$urlRouterProvider.otherwise('/DocumentDetail');
   //$locationProvider.html5Mode({
   //    enabled: true,
   //    requireBase: false,
   //    rewriteLinks: false
   //});
   //$resourceProvider.defaults.stripTrailingSlashes = false;
});

toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-bottom-center",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}
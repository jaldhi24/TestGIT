var app = angular.module("DMS", ["ui.router", "ngSanitize", "LocalStorageModule"]);

app.config(function (localStorageServiceProvider) {
    localStorageServiceProvider
      .setStorageType('localStorage');
});

app.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {

    var DocumentDetail = {
        name: 'DocumentDetail',
        url: '/DocumentDetail',
        templateUrl: 'Templates/DocumentDetail.html',
    },
   EditDocument = {
       name: 'EditDocument',
       url: '/EditDocument',
       templateUrl: 'Templates/EditDocument.html',
   },
   homeIndex = {
       name: 'Home',
       url: '/Home',
       templateUrl: 'Templates/index.html',
   },
   Login = {
       name: 'Login',
       url: '/Login',
       templateUrl: 'Templates/Login/Login.html',
   },
   Registration = {
       name: 'Registration',
       url: '/Registration',
       templateUrl: 'Templates/Registration/Registration.html',
   },
   ShowSkills = {
       name: 'ShowSkills',
       url: '/ShowSkills',
       templateUrl: 'Templates/Skills/SkillDetails.html',
   },
   UsersSkills = {
       name: 'ShowUsersSkill',
       url: '/UserSkills',
       templateUrl: 'Templates/UsersSkills/UsersSkills.html',
   };


    $stateProvider.state(Login);
    $stateProvider.state(homeIndex);
    $stateProvider.state(DocumentDetail);
    $stateProvider.state(EditDocument);
    $stateProvider.state(Registration);
    $stateProvider.state(ShowSkills);
    $stateProvider.state(UsersSkills);
    //$stateProvider.state(newPosition);

    //$urlRouterProvider.otherwise({ redirectTo: 'Templates/DocumentDetail.html' });
    $urlRouterProvider.otherwise('/Home');

    //$locationProvider.html5Mode(true);
})

.run(function ($rootScope, $state, $http, $browser, localStorageService) {
    $rootScope.ViewSidebar = true;

    if (localStorageService.isSupported) {

        var headerToken = localStorageService.get("ApiToken");

        if (headerToken != null) {

            $http.defaults.headers.common['Token'] = headerToken;
        }

    }

    $state.transitionTo('DocumentDetail');
});



function BindToolTip() {
    $("[data-toggle=tooltip]").tooltip();
}
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
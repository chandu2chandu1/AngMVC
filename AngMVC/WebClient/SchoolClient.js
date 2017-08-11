var SchoolApp = angular.module("SchoolApp", []);

SchoolApp.controller("SchoolController", function ($scope, SchoolService) {

    SchoolService.getSchools()
    .then(function (response) {
        $scope.schools = response.data;
    });

    $scope.editSchool = function (school) {
        $scope.selectedSchool = school;
        $('#editSchoolForm').modal('show');
    }

    $scope.addSchool = function () {
        $('#createSchoolForm').modal('show');
    }

    $scope.removeSchool = function (school) {
        var index = $scope.schools.indexOf(school);
        $('#myModal').modal('show');
        $('#removeMessage').text("You are removing the school " + school.SchoolName);
        $scope.schools.splice(index, 1);
    }

    $scope.updateSchool = function (school) {
        SchoolService.updateSchool(school).then(function (data) {
            console.log("Posted the data and returned now..");
        });
    }

    $scope.createSchool = function (school) {
        SchoolService.createSchool(school).then(function (data) {
            console.log("Created a new school", data);
            SchoolService.getSchools()
                .then(function (response) {
                    $scope.schools = response.data;
                });
        });
    }

});

SchoolApp.factory("SchoolService", ['$http', function ($http) {
    var SchoolService = {};
    SchoolService.getSchools = function () {
        //return $http.get('GetSchools');
        return $http.get('api/School');
    };

    SchoolService.updateSchool = function (school) {
        //return $http.post('UpdateSchool', school);
        return $http.put('api/School/'+school.ID, school);
    };

    SchoolService.createSchool = function (school) {
        return $http.post('api/School', school);
    };

    return SchoolService;
}]);

SchoolApp.directive("createSchoolForm", function () {
    return {
        restrict: 'E',
        templateUrl: "../WebClient/CreateSchool.html"
    };
});

SchoolApp.directive("editSchoolForm", function () {
    return {
        restrict: 'E',
        templateUrl: "../WebClient/EditSchool.html",
        scope: {
            school: '='
        }
    }
})
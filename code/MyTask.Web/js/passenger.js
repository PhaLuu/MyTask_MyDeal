function escapeRegExp(string) {
    return string.replace(/([.+?^=!:${}()|\[\]\/\\])/g, "\\$1");
}
function getParameter() {
    var vars = {};
    var parts = window.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi,
    function (m, key, value) {
        vars[key] = value;
    });
    return vars;
}

(function () {
    'use strict';

    angular.module('managePassenger', [])
        .controller('mainPassenger', MainPassenger)
        .controller('addPassenger', AddPassenger)
        .controller('editPassenger', EditPassenger)
        .factory('PassengerService', PassengerService);

    /* Define Main Passenger Controller  */
    MainPassenger.$inject = ['$scope', 'PassengerService'];
    function MainPassenger($scope, PassengerService) {
        PassengerService.getall(function (result) {
            //console.log(result);
            $scope.locators = result;
            $scope.isShow = true;
        });

        //-----------
        $scope.search = '';
        var regex;
        $scope.$watch('search', function (value) {
            var escaped = escapeRegExp(value);
            var formatted = escaped.replace('*', '.*')

            if (formatted.indexOf('*') === -1) {
                formatted = '.*' + formatted + '.*'
            }

            regex = new RegExp('^' + formatted + '$', 'im');
        });

        $scope.filterBySearch = function (locator) {
            if (!$scope.search) return true;
            if (regex.test(locator.Name))
                return true;
            else {
                console.log(locator.Passengers);
                for (var i = 0; i < locator.Passengers.length; i++ )
                {
                    console.log(locator.Passengers[i]);
                    if (regex.test(locator.Passengers[i].Name))
                        return true;
                }
                return false;
            }
        };
    };

    /* Define Edit Passenger Controller  */
    EditPassenger.$inject = ['$scope', 'PassengerService'];
    function EditPassenger($scope, PassengerService) {
        $scope.LocatorName = getParameter()["id"];

        $scope.submitForm = function (isValid) {
            console.log("I am working");
            if ($scope.isUpdate) {
                if (isValid) {
                    var data;
                    if ($scope.LocatorName.length >= 3 && $scope.PassengerName.length >= 3) {
                        data = {
                            LocatorName: $scope.LocatorName,
                            PassengerName: $scope.PassengerName,
                        };
                        console.log(data.PassengerName);
                        PassengerService.update(data, function (result) {
                            if (result == 0)
                                alert(errorMessage.addPassengersFailed);
                            else
                            {
                                UIkit.modal.alert("Successfully! Added passenger to booking");
                            }
                        });
                    }   
                }
            }
        }
    };


    /* Define Add Passenger Controller  */
    AddPassenger.$inject = ['$scope', 'PassengerService'];
    function AddPassenger($scope, PassengerService) {
        $scope.submitForm = function (isValid) {
            console.log("I am working");
            if ($scope.isUpdate) {
                if (isValid) {
                    var data;
                    if ($scope.LocatorName.length>=3 && $scope.PassengerName.length>=3) {
                        data = {
                            LocatorName: $scope.LocatorName,
                            PassengerName: $scope.PassengerName,
                        };
                        console.log(data.PassengerName);
                        PassengerService.update(data, function (result) {
                            if (result == 0)
                                alert(errorMessage.addPassengersFailed);
                            else
                            {  
                                UIkit.modal.alert("Successfully! Added a booking");                             
                            }
                                
                        });
                    }
                }
            }
        }
    };

    /* Passenger Service */
    PassengerService.$inject = ['$http'];
    function PassengerService($http) {
        var service = {};

        service.getall = function ( callback) {
            $http.get(UrlAPI + "api/pnl/getList").
              success(function (data, status, headers, config) {
                  //data = JSON.stringify(data);
                  callback && callback(data);
              }).
              error(function (data, status, headers, config) {
                  console.log(data);
              });
        }

        service.update = function (data, callback) {
            $http.put(UrlAPI + 'api/pnl', data).
                success(function (data, status, headers, config) {
                    callback && callback(data);
                }).
                error(function (data, status, headers, config) {
                    console.log(data);
                });
        }

        return service;
    }

})();

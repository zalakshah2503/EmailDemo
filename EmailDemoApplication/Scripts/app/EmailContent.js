angular.module('EmailApp', ['ngMaterial']).
    controller('EmailContent', function ($scope, $mdDialog, $http) {

        $scope.Config = $http.get('api/MailConfig');
        $scope.Config.then(function (response) {
            if (response.data[0] != undefined) {
                $scope.ConfigData = [
                    {
                        email: response.data[0].Email,
                        password: response.data[0].Password,
                        hostname: response.data[0].HostName,
                        portno: response.data[0].PortNo,
                        Id: response.data[0].Id,
                        enableSSl: response.data[0].enableSSl
                    }
                ];
            }
            else {
                $scope.ConfigData = null;
            }
        });

        $scope.submit = function () {
            var IsValidate = true;
            var content = tinymce.activeEditor.getContent();

            $('.required').each(function () {
                if ($(this).val() == undefined || $(this).val() == "") {
                    if ($(this).attr('id') == "divtextarea") {
                        if (content == "") {
                            $(this).css('border', '1px solid red');
                            IsValidate = false;
                        }
                        else
                            $(this).css('border', '0px solid #cccccc');
                    }
                    else {
                        $(this).css('border', '1px solid red');
                        IsValidate = false;
                    }
                }
                else {
                    $(this).css('border', '1px solid #cccccc');
                }
            });

            if (IsValidate && content != "") {
                if ($scope.ConfigData != undefined && $scope.ConfigData[0] != undefined) {
                    var status = {
                        email: $scope.ConfigData[0].email,
                        password: $scope.ConfigData[0].password,
                        hostname: $scope.ConfigData[0].hostname,
                        portno: $scope.ConfigData[0].portno,
                        enableSSl: $scope.ConfigData[0].enableSSl,
                        to: $('#txtName').val(),
                        content: content
                    }

                    var uri = '/api/EmailConfig';
                    $scope.ISSend = $http.post(uri, status);
                    $scope.ISSend.then(function (response) {
                        console.log(response.data);
                        //if (response.data == true) {
                        //    alert("Mail Sent Successfully...");
                        //}
                        //else {
                        //    alert("Mail Sent Failed...");
                        //}
                    });
                }
                else {
                    alert("Please Fill the Mail Configuration...");
                }
            }
        };

        $scope.ConfigPopup = function (ev) {
            $mdDialog.show({
                locals: { ConfigData: ConfigData },
                controller: DialogController,
                templateUrl: 'Scripts/app/Dashboard.html',
                parent: angular.element(document.body),
                targetEvent: ev,
            }).then(function (answer) {
                if (answer != undefined)
                    $scope.status = answer;
            });
        }

        function DialogController($scope, $mdDialog, ConfigData) {

            var Id = 0;
            if (ConfigData != undefined && ConfigData[0] != undefined) {
                $scope.dataToPass = ConfigData[0];
                Id = ConfigData[0].Id;
            }

            $scope.cancel = function () {
                $mdDialog.hide(undefined);
            };

            $scope.save = function () {
                var IsValidate = true;
                $('.configrequired').each(function () {
                    if ($(this).val() == undefined || $(this).val() == "") {
                        $(this).css('border', '1px solid red');
                        IsValidate = false;
                    }
                    else {
                        $(this).css('border', '1px solid #cccccc');
                    }
                });

                if (IsValidate) {

                    var MailConfig = {
                        email: $('#txtEmail').val(),
                        password: $('#txtPassword').val(),
                        hostname: $('#txtHostName').val(),
                        portno: $('#txtPort').val(),
                        enableSSl: $('#chkEnableSSL').is(':checked'),
                        Id: Id
                    }
                    if ($scope.dataToPass != undefined) {
                        var uri = '/api/MailConfig/' + $scope.dataToPass.Id;
                        $scope.ISSend = $http.put(uri, MailConfig);
                    }
                    else {
                        var uri = '/api/MailConfig';
                        $scope.ISSend = $http.post(uri, MailConfig);
                    }
                    $scope.ISSend.then(function (response) {
                        console.log(response.data);
                        MailConfig = response.data;
                    });
                    $mdDialog.hide(MailConfig);
                }
            };
        }
    });
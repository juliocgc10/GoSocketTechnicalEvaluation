GoSocket = window.GoSocket || {};

GoSocket.HomeIndex = (function ($, window, document, navigator, localStorage, sessionStorage, undefined) {

    var defaults = null;    

    var npMainFunction = function (args) {

        defaults = args;

        Initialize_Application(defaults);
        Initialization_Events(defaults);

    }
    

    function Initialize_Application(defaults) {

    }

    function Initialization_Events(defaults) {

        try {
            $("#btnNumberAreas").click(function () {
                getNumberAreas(defaults);
            });
        } catch (e) {
            alert("Element:btnNumberAreas Event:click \nException: " + e.message);
        }

        try {
            $("#btnNumberAreasEmployee").click(function () {
                getNumberAreasemployee(defaults);
            });
        } catch (e) {
            alert("Element:btnNumberAreasEmployee Event:click \nException: " + e.message);
        }

        try {
            $("#btnInformationSalary").click(function () {
                getInformationSalary(defaults);
            });
        } catch (e) {
            alert("Element:btnNumberAreasEmployee Event:click \nException: " + e.message);
        }



    }

    //#region Functions Initialization_Events

    function getNumberAreas(defaults) {

        var settings = {
            url: defaults.UrlWepApi + "/api/WorkXML/GetNumberAreas",
            async: true,
            type: 'GET',
            dataType: 'json'
        };


        $.ajax(settings).done(function (response) {
            if (response.IsSuccess) {
                Swal.fire("El número de áreas son: " + response.Data);
            }
            else {
                Swal.fire('Oops...', response.Message, 'error');
            }

        }).fail(function (jqXHR, textStatus, err) {
            Swal.fire('Oops...', err, 'error');
            console.log(err);
            console.log(textStatus);
        });
    }

    function getNumberAreasemployee(defaults) {

        var settings = {
            url: defaults.UrlWepApi + "/api/WorkXML/GetAreasPerEmployee?nodesPerEmployee=2",
            async: true,
            type: 'GET',
            dataType: 'json'
        };


        $.ajax(settings).done(function (response) {
            if (response.IsSuccess) {
                Swal.fire("El número de áreas que tienen más de 2 empelados son: " + response.Data);
            }
            else {
                Swal.fire('Oops...', response.Message, 'error');
            }

        }).fail(function (jqXHR, textStatus, err) {
            Swal.fire('Oops...', err, 'error');
            console.log(err);
            console.log(textStatus);
        });
    }

    function getInformationSalary(defaults) {

        var settings = {
            url: defaults.UrlWepApi + "/api/WorkXML/GetInformationSalary",
            async: true,
            type: 'GET',
            dataType: 'json'
        };


        $.ajax(settings).done(function (response) {
            if (response.IsSuccess) {
                var responseHtml = '<ul>';
                $.each(response.Data, function (index, value) {
                    responseHtml += '<li>' + value.AreaName + '|' + value.TotalSalary +'</li>'
                });
                responseHtml += '<ul>';

                Swal.fire({
                    title: '<strong>Total de Salario por área</strong>',
                    icon: 'info',
                    html: responseHtml
                });
            }
            else {
                Swal.fire('Oops...', response.Message, 'error');
            }

        }).fail(function (jqXHR, textStatus, err) {
            Swal.fire('Oops...', err, 'error');
            console.log(err);
            console.log(textStatus);
        });
    }


    //#endregion



    return {
        Config: defaults,
        npMainFunction: npMainFunction
    }
}(jQuery, window, document, navigator, localStorage, sessionStorage, undefined));


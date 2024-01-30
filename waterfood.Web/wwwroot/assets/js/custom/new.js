

function removePhoto(path, image) {

    alert(path);
    alert(image);
}


$(function () {

    $("#RoleRef").select2({
        ajax: {
            url: '/Admin/Select/SelectAllRoles',
            dataType: 'json',
            type: "Get",
            data: function (params) {
                return {
                    term: params.term
                };
            },
            processResults: function (response) {
                return {
                    results: response
                };
            },
            cache: true
        }
    });

    $("#CategoryRef").select2({
        ajax: {
            url: '/Admin/Select/SelectAllCategories',
            dataType: 'json',
            type: "Get",
            data: function (params) {
                return {
                    term: params.term
                };
            },
            processResults: function (response) {
                return {
                    results: response
                };
            },
            cache: true
        }
    });

    $("#Parent").select2({
        ajax: {
            url: '/Admin/Select/SelectAllCategories',
            dataType: 'json',
            type: "Get",
            data: function (params) {
                return {
                    term: params.term
                };
            },
            processResults: function (response) {
                return {
                    results: response
                };
            },
            cache: true
        }
    });

    $("#LevelRef").select2({
        ajax: {
            url: '/Admin/Select/SelectAllLevels',
            dataType: 'json',
            type: "Get",
            data: function (params) {
                return {
                    term: params.term
                };
            },
            processResults: function (response) {
                return {
                    results: response
                };
            },
            cache: true
        }
    });

    $("#UserRef").select2({
        ajax: {
            url: '/Admin/Select/SelectAllUsers',
            dataType: 'json',
            type: "Get",
            data: function (params) {
                return {
                    term: params.term
                };
            },
            processResults: function (response) {
                return {
                    results: response
                };
            },
            cache: true
        }
    });

    $("#TeacherRef").select2({
        ajax: {
            url: '/Admin/Select/SelectAllTeachers',
            dataType: 'json',
            type: "Get",
            data: function (params) {
                return {
                    term: params.term
                };
            },
            processResults: function (response) {
                return {
                    results: response
                };
            },
            cache: true
        }
    });

});


function notify(type, text) {
   
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "100",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "positionClass": "toast-bottom-center",
    };

    if (type === 'success') {
        toastr.success(text, 'لرنوفن');
    }

    if (type === 'error') {
        toastr.error(text, 'لرنوفن');
    }
}
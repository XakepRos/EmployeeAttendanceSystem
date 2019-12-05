if (!window.webapp) {
    window.webapp = {};
}


window.webapp.employee = (function ($) {
    var table;
    var frmDialogCreate = $("#ModalCreate");
    var frmDialogEdit = $("#ModalEdit");
    var viewtable = $("#employeeGrid");
    var buttonCreat = $("#btnSubmit");
    var buttonEdit = $("#btnEdit");
    var reload = $('.btn_reload');

    var onDocumentReady = function () {
        initUI();
        initEvent();
    },
        initUI = function () {
        },
      
        initEvent = function () {
            $(".create").on('click', addEntity);
            viewtable.on('click', '.btn_edit', getEditEnity);
            viewtable.on('click', '.btn_delete', deleteEntity);
            $("#btnCreate").on('click', saveEmployee);
            $('#btnModalEdit').on('click', updateEmployee);
            $('#btnClose').on('click', closePopup);
            $('#btnCloseEdit').on('click', closePopupEdit);



        },

        closePopup = function (event) {
            event.preventDefault();
            $('.ModalCreatePopUp').removeClass('show');

        },
        closePopupEdit = function (event) {
            event.preventDefault();
            $('.ModalEditPopUp').removeClass('show');
        },
        addEntity = function (event) {
            event.preventDefault();
            var url = $(this).attr('data-href');
            $.get(url, function (data) {
                frmDialogEdit.find('div.modal-body').html('');
                frmDialogCreate.find('div.modal-body').html(data);
                frmDialogCreate.find('.heading ').html('Create Employee');
                $('.ModalCreatePopUp').addClass('show');
                $('body').addClass('scroll-hide');
                $('input:text:visible:first').focus();
            });


        },

        getEditEnity = function (event) {
            var $d = $.Deferred();
            var id = $(this).parent().attr("data-id");
            var url = "/Employee/EditEmployee" + "?id=" + id;
            $.ajax(url).done(function (data) {
                $d.resolve(data);
                frmDialogCreate.find('div.modal-body').html();
                frmDialogEdit.find('div.modal-body').html(data);
                frmDialogEdit.find('.heading ').html('Edit Employee');
                $('.ModalEditPopUp').addClass('show');
                $('body').addClass('scroll-hide');
                $('input:text:visible:first').focus();

            }).fail(function (data) {
                $d.reject(data);
            });
            return $d.promise();
        },
        updateEmployee = function (event) {
            var model = {
                Id: '',
                Name: '',
                DesignationId: '',
                DateOfBirth:''
            }
            model.Id = $("#Id").val();
            model.Name = $("#Name").val();
            model.DesignationId = $("#DesignationId").val();
            model.DateOfBirth = $("#DateOfBirth").val();
         
            var $form = frmDialogEdit.find('#EditEmployeeForm');

            event.preventDefault();
            $.ajax({
                url: '/Employee/EditEmployee',
                type: "Post",
                data: JSON.stringify(model),
                dataType: "json",
                contentType: "application/json"

            }).done(function (response) {
                if (response.success === true) {
                    $('.ModalEditPopUp').removeClass('show');
                    location.reload();
                    alert("success");
                }
                else if (response.success === false) {
                    alert("error");
                }
                else {
                    alert("error");
                }
            }).fail(function (response) {
                alert("Error occured while processing your request!!");
            });

        },
        saveEmployee = function (event) {
            var $d = $.Deferred();
            var model = {
                Id: '',
                Name: '',
                DesignationId: '',
                DateOfBirth: ''
            }
            model.Id = $("#Id").val();
            model.Name = $("#Name").val();
            model.DesignationId = $("#DesignationId").val();
            model.DateOfBirth = $("#DateOfBirth").val();
            debugger;
            var $form = frmDialogCreate.find('#CreateEmployeeForm');

            event.preventDefault();
            $.ajax({
                url: '/Employee/CreateEmployee',
                type: "POST",
                data: JSON.stringify(model),
                dataType: "json",
                contentType: "application/json; charset=utf-8"
            }).done(function (response) {
                $d.resolve(response);
                if (response.success === true) {
                    $('.ModalCreatePopUp').removeClass('show');
                    $('body').addClass('scroll-hide');
                    location.reload();
                    alert("success");
                }
                else if (response.success === false) {
                    alert("error");
                }
                else {
                    alert("error");
                }

            }).fail(function (response) {
                $d.reject(response);
                alert("error");
            });


            return $d.promise();
        },
        deleteEntity = function (e) {
            var $d = $.Deferred();
            e.preventDefault();
            var id = $(this).parent().attr("data-id");
            if (confirm("Are you sure you want to delete this employee")) {
                $.ajax({
                    type: "POST",
                    url: "/Employee/Delete",
                    dataType: "JSON",
                    data: { id: id }
                }).done(function (response) {
                    if (response.success === true) {
                        $d.resolve(response);
                        location.reload();
                        alert("success");
                    } else if (response.result === false) {
                        $d.resolve(response);
                        alert(response.message);
                    } else {
                        $d.resolve(response);
                        alert(response.message);
                    }
                }).fail(function (response) {
                    alert(response.message);
                });
            }
            return $d.promise;
        };

    return {
        onDocumentReady: onDocumentReady
    };
}(jQuery));

jQuery(webapp.employee.onDocumentReady);


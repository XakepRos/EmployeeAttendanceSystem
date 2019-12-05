if (!window.webapp) {
    window.webapp = {};
}


window.webapp.designation = (function ($) {
    var table;
    var frmDialogCreate = $("#ModalCreate");
    var frmDialogEdit = $("#ModalEdit");
    var viewtable = $("#designationGrid");
    var buttonCreate = $("#btnSubmit");
    var buttonEdit = $("#btnEdit");
    var reload = $('.btn_reload');

    var onDocumentReady = function () {
        initUI();
        initEvent();
    },
        initUI = function () {
        },

        reloadData = function () {
            table.fnDraw();
        },

        initEvent = function () {
            $(".create").on('click', addEntity);
            viewtable.on('click', '.btn_edit', getEditEnity);
            viewtable.on('click', '.btn_delete', deleteEntity);
            $("#btnCreate").on('click', saveDesignation);
            $('#btnModalEdit').on('click', updateDesignation);
            $('#btnClose').on('click', closePopup);
            $('#btnCloseEdit').on('click', closePopupEdit);

            reload.on('click', reloadData);
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
            debugger;
            $.get(url, function (data) {
                frmDialogEdit.find('div.modal-body').html('');
                frmDialogCreate.find('div.modal-body').html(data);
                frmDialogCreate.find('.heading ').html('Create Designation');
                $('.ModalCreatePopUp').addClass('show');
                $('body').addClass('scroll-hide');
                $('input:text:visible:first').focus();
            });
        },

        getEditEnity = function (event) {
            var $d = $.Deferred();
            debugger;
            var id = $(this).parent().attr("data-id");
            var url = "/Designation/EditDesignation" + "?id=" + id;
            $.ajax(url).done(function (data) {
                $d.resolve(data);
                frmDialogCreate.find('div.modal-body').html();
                frmDialogEdit.find('div.modal-body').html(data);
                frmDialogEdit.find('.heading ').html('Edit Designation');
                $('.ModalEditPopUp').addClass('show');
                $('body').addClass('scroll-hide');
                $('input:text:visible:first').focus();

            }).fail(function (data) {
                $d.reject(data);
            });
            return $d.promise();
        },

        updateDesignation = function (event) {
            var model = {
                Id: '',
                DesignationName: '',
            }
            model.Id = $("#Id").val();
            model.DesignationName = $("#DesignationName").val();
            var $form = frmDialogEdit.find('#EditDesignationForm');

            event.preventDefault();
            $.ajax({
                url: '/Designation/EditDesignation',
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

        saveDesignation = function (event) {
            var $d = $.Deferred();
            var model = {
                DesignationName: ''
            };
            model.DesignationName = $("#DesignationName").val();
            debugger;
            var $form = frmDialogCreate.find('#CreateDesignationForm');

            event.preventDefault();
            $.ajax({
                url: '/Designation/CreateDesignation',
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
                else if (response.success === false)
                {
                      alert("error");
                }
                     else
                     {
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
            if (confirm("Are you sure you want to delete this designation")) {
                $.ajax({
                    type: "POST",
                    url: "/Designation/DeleteDesignation",
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

jQuery(webapp.designation.onDocumentReady);


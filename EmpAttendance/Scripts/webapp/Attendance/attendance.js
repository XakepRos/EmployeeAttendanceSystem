if (!window.webapp) {
    window.webapp = {};
}


window.webapp.attendance = (function ($) {
    var table;
    var frmDialogCreate = $("#ModalCreate");
    var frmDialogEdit = $("#ModalEdit");
    var viewtable = $("#attendanceGrid");
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
            $("#btnCreate").on('click', saveAttendance);
            $('#btnModalEdit').on('click', updateAttendance);
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
                frmDialogCreate.find('.heading ').html('Create Attendance');
                $('.ModalCreatePopUp').addClass('show');
                $('body').addClass('scroll-hide');
                $('input:text:visible:first').focus();
            });


        },

        getEditEnity = function (event) {
            var $d = $.Deferred();
            var id = $(this).parent().attr("data-id");
            var url = "/Attendance/EditAttendance" + "?id=" + id;
            $.ajax(url).done(function (data) {
                $d.resolve(data);
                frmDialogCreate.find('div.modal-body').html();
                frmDialogEdit.find('div.modal-body').html(data);
                frmDialogEdit.find('.heading ').html('Edit Attendance');
                $('.ModalEditPopUp').addClass('show');
                $('body').addClass('scroll-hide');
                $('input:text:visible:first').focus();

            }).fail(function (data) {
                $d.reject(data);
            });
            return $d.promise();
        },
        updateAttendance = function (event) {
            var model = {
                Id: '',
                EmpId: '',
                InTime: '',
                OutTime:''
            }
            model.Id = $("#Id").val();
            model.EmpId = $("#EmpId").val();
            model.InTime = $("#InTime").val();
            model.OutTime = $("#OutTime").val();
         
            var $form = frmDialogEdit.find('#EditAttendanceForm');

            event.preventDefault();
            $.ajax({
                url: '/Attendance/EditAttendance',
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
        saveAttendance = function (event) {
            var $d = $.Deferred();
            var model = {
                Id: '',
                EmpId: '',
                InTime: '',
                OutTime: ''
            }
            model.Id = $("#Id").val();
            model.EmpId = $("#EmpId").val();
            model.InTime = $("#InTime").val();
            model.OutTime = $("#OutTime").val();
            debugger;
            var $form = frmDialogCreate.find('#CreateAttendanceForm');

            event.preventDefault();
            $.ajax({
                url: '/Attendance/CreateAttendance',
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
            if (confirm("Are you sure you want to delete this attendance")) {
                $.ajax({
                    type: "POST",
                    url: "/Attendance/Delete",
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

jQuery(webapp.attendance.onDocumentReady);


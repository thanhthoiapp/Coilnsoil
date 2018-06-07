var SettingController = function () {
    this.initialize = function () {
        loadData();
        registerEvents();
        //registerControls();
    }

    function registerEvents() {
        $('#txtValue4').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
        //Init validation
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                txtNameM: { required: true }
            }
        });

        $('#txt-search-keyword').keypress(function (e) {
            if (e.which === 13) {
                e.preventDefault();
                loadData();
            }
        });
        $("#btn-search").on('click', function () {
            loadData();
        });

        $("#ddl-show-page").on('change', function () {
            tedu.configs.pageSize = $(this).val();
            tedu.configs.pageIndex = 1;
            loadData(true);
        });

        $("#btn-create").on('click', function () {
            resetFormMaintainance();
            $('#modal-add-edit').modal('show');

        });

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            $.ajax({
                type: "GET",
                url: "/Admin/setting/GetById",
                data: { id: that },
                dataType: "json",
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function (response) {
                    var data = response;
                    $('#hidIdM').val(data.Id);
                    $('#txtNameM').val(data.Name);
                    $('#ckStatusM').prop('checked', data.Status == 1);
                    $('#txtValue1').val(data.Value1);
                    $('#txtValue2').val(data.Value2);
                    $('#ckValue3').prop('checked', true);
                    $('#txtValue4').val(data.Value4);
                    $('#txtValue5').val(data.Value5);
                    $('#modal-add-edit').modal('show');
                    tedu.stopLoading();
                },
                error: function () {
                    tedu.notify('Có lỗi xảy ra', 'error');
                    tedu.stopLoading();
                }
            });
        });

        $('#btnSaveM').on('click', function (e) {
            if ($('#frmMaintainance').valid()) {
                e.preventDefault();
                var id = $('#hidIdM').val();
                var name = $('#txtNameM').val();
                var status = $('#ckStatusM').prop('checked') == true ? 1 : 0;;
                var value1 = $('#txtValue1').val();
                var value2 = $('#txtValue2').val();
                var value3 = $('#ckValue3').prop('checked');
                var value4 = $('#txtValue4').val();
                var value5 = $('#txtValue5').val();

                $.ajax({
                    type: "POST",
                    url: "/Admin/setting/SaveEntity",
                    data: {
                        Id: id,
                        Status: status,
                        Name: name,
                        Value1: value1,
                        Value2: value2,
                        Value3: value3,
                        Value4: value4,
                        Value5: value5
                    },
                    dataType: "json",
                    beforeSend: function () {
                        tedu.startLoading();
                    },
                    success: function () {
                        tedu.notify('Update page successful', 'success');
                        $('#modal-add-edit').modal('hide');
                        resetFormMaintainance();

                        tedu.stopLoading();
                        loadData(true);
                    },
                    error: function () {
                        tedu.notify('Have an error in progress', 'error');
                        tedu.stopLoading();
                    }
                });
                return false;
            }
            return false;
        });

        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            tedu.confirm('Are you sure to delete?', function () {
                $.ajax({
                    type: "POST",
                    url: "/Admin/setting/Delete",
                    data: { id: that },
                    dataType: "json",
                    beforeSend: function () {
                        tedu.startLoading();
                    },
                    success: function () {
                        tedu.notify('Delete page successful', 'success');
                        tedu.stopLoading();
                        loadData();
                    },
                    error: function () {
                        tedu.notify('Have an error in progress', 'error');
                        tedu.stopLoading();
                    }
                });
            });
        });
    };


    function loadData(isPageChanged) {
        $.ajax({
            type: "GET",
            url: "/admin/setting/GetAllPaging",
            data: {
                keyword: $('#txt-search-keyword').val(),
                page: tedu.configs.pageIndex,
                pageSize: tedu.configs.pageSize
            },
            dataType: "json",
            beforeSend: function () {
                tedu.startLoading();
            },
            success: function (response) {
                var template = $('#table-template').html();
                var render = "";
                if (response.RowCount > 0) {
                    $.each(response.Results, function (i, item) {
                        render += Mustache.render(template, {
                            Status: tedu.getStatus(item.Status),
                            Id: item.Id,
                            Name: item.Name,
                            Value1: item.Value1,
                            Value2: item.Value2,
                            Value3: item.Value3,
                            Value4: item.Value4,
                            Value5: item.Value5
                        });
                    });
                    $("#lbl-total-records").text(response.RowCount);
                    if (render != undefined) {
                        $('#tbl-content').html(render);

                    }
                    wrapPaging(response.RowCount, function () {
                        loadData();
                    }, isPageChanged);


                }
                else {
                    $('#tbl-content').html('');
                }
                tedu.stopLoading();
            },
            error: function (status) {
                console.log(status);
            }
        });
    };
    function resetFormMaintainance() {
        $('#hidIdM').val('');
        $('#txtNameM').val('');
        $('#ckStatusM').prop('checked', true);
        $('#txtValue1').val('');
        $('#txtValue2').val('');
        $('#ckValue3').prop('checked', true);
        $('#txtValue4').val('');
        $('#txtValue5').val('');
    }
    //function registerControls() {
    //    CKEDITOR.replace('txtContentM', {});

    //    //Fix: cannot click on element ck in modal
    //    $.fn.modal.Constructor.prototype.enforceFocus = function () {
    //        $(document)
    //            .off('focusin.bs.modal') // guard against infinite focus loop
    //            .on('focusin.bs.modal', $.proxy(function (e) {
    //                if (
    //                    this.$element[0] !== e.target && !this.$element.has(e.target).length
    //                    // CKEditor compatibility fix start.
    //                    && !$(e.target).closest('.cke_dialog, .cke').length
    //                    // CKEditor compatibility fix end.
    //                ) {
    //                    this.$element.trigger('focus');
    //                }
    //            }, this));
    //    };

    //}
    function wrapPaging(recordCount, callBack, changePageSize) {
        var totalsize = Math.ceil(recordCount / tedu.configs.pageSize);
        //Unbind pagination if it existed or click change pagesize
        if ($('#paginationUL a').length === 0 || changePageSize === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData("twbs-pagination");
            $('#paginationUL').unbind("page");
        }
        //Bind Pagination Event
        $('#paginationUL').twbsPagination({
            totalPages: totalsize,
            visiblePages: 7,
            first: 'Đầu',
            prev: 'Trước',
            next: 'Tiếp',
            last: 'Cuối',
            onPageClick: function (event, p) {
                tedu.configs.pageIndex = p;
                setTimeout(callBack(), 200);
            }
        });
    }
}
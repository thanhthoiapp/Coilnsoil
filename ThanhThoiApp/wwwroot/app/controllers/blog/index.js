var BlogController = function () {
    this.initialize = function () {
        loadCategory();
        loadData();
        registerEvents();
        registerControls();
    }

    function registerEvents() {
        //Init validation
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'vi',
            rules: {
                txtNameM: { required: true },
                ddlCategoryIdM: { required: true },
                txtAliasM: { required: true }
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
        $('#btnSelectImg').on('click', function () {
            $('#fileInputImage').click();
        });
        $("#fileInputImage").on('change', function () {
            var fileUpload = $(this).get(0);
            var files = fileUpload.files;
            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }
            $.ajax({
                type: "POST",
                url: "/Admin/Upload/UploadImage",
                contentType: false,
                processData: false,
                data: data,
                success: function (path) {
                    $('#txtImageM').val(path);
                    tedu.notify('Upload image succesful!', 'success');

                },
                error: function () {
                    tedu.notify('There was error uploading files!', 'error');
                }
            });
        });
        $("#btn-create").on('click', function () {
            resetFormMaintainance();
            initTreeDropDownCategory();
            $('#modal-add-edit').modal('show');

        });

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            $.ajax({
                type: "GET",
                url: "/Admin/Blog/GetById",
                data: { id: that },
                dataType: "json",
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function (response) {
                    var data = response;
                    $('#hidIdM').val(data.Id);
                    $('#txtNameM').val(data.Name);
                    initTreeDropDownCategory(data.CategoryId);
                    $('#txtAliasM').val(data.SeoAlias);
                    CKEDITOR.instances.txtContentM.setData(data.Content);
                    $('#txtDescripM').val(data.Description);

                    $('#ckHomeFlagM').prop('checked', data.HomeFlag);
                    $('#ckHotFlagM').prop('checked', data.HotFlag);

                    $('#txtImageM').val(data.Image);

                    $('#txtSeoDesM').val(data.SeoDescription);
                    $('#txtKeyM').val(data.SeoKeywords);
                    $('#txtPageTitleM').val(data.SeoPageTitle);
                    $('#txtTagM').val(data.Tags);
                    $('#ckStatusM').prop('checked', data.Status === 1);

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
                var categoryId = $('#ddlCategoryIdM').combotree('getValue');
                var seoAlias = $('#txtAliasM').val();
                var content = CKEDITOR.instances.txtContentM.getData();
                var description = $('#txtDescripM').val();
                var homeFlag = $('#ckHomeFlagM').prop('checked');
                var hotFlag = $('#ckHotFlagM').prop('checked');
                var thumbnailImage = $('#txtImageM').val();
                var seoMetaDescription = $('#txtSeoDesM').val();
                var seoMetaKeyword = $('#txtKeyM').val();
                var seoPageTitle = $('#txtPageTitleM').val();
                var tags = $('#txtTagM').val();
                var status = $('#ckStatusM').prop('checked') == true ? 1 : 0;

                $.ajax({
                    type: "POST",
                    url: "/Admin/Blog/SaveEntity",
                    data: {
                        Id: id,
                        CategoryId: categoryId,
                        Name: name,
                        SeoAlias: seoAlias,
                        Content: content,
                        Description: description,
                        HomeFlag: homeFlag,
                        HotFlag: hotFlag,
                        Image: thumbnailImage,
                        SeoDescription: seoMetaDescription,
                        SeoKeywords: seoMetaKeyword,
                        SeoPageTitle: seoPageTitle,
                        Tags: tags,
                        Status: status
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
                    url: "/Admin/Blog/Delete",
                    data: { id: that },
                    dataType: "json",
                    beforeSend: function () {
                        tedu.startLoading();
                    },
                    success: function () {
                        tedu.notify('Delete blog successful', 'success');
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

    function resetFormMaintainance() {
        $('#hidIdM').val(0);
        initTreeDropDownCategory('');
        $('#txtNameM').val('');
        $('#txtAliasM').val('');
        CKEDITOR.instances.txtContentM.setData('');
        $('#txtDescripM').val('');
        $('#txtImageM').val('');
        $('#ckHomeFlagM').prop('checked', true);
        $('#ckHotFlagM').prop('checked', true);
        $('#txtSeoDesM').val('');
        $('#txtKeyM').val('');
        $('#txtPageTitleM').val('');
        $('#txtTagM').val('');
        $('#ckStatusM').prop('checked', true);
    }

    function registerControls() {
        var editorConfig = {
            filebrowserImageUploadUrl: '/Admin/Upload/UploadImageForCKEditor?type=Images'
        }
        CKEDITOR.replace('txtContentM', editorConfig);
        //Fix: cannot click on element ck in modal
        $.fn.modal.Constructor.prototype.enforceFocus = function () {
            $(document)
                .off('focusin.bs.modal') // guard against infinite focus loop
                .on('focusin.bs.modal', $.proxy(function (e) {
                    if (
                        this.$element[0] !== e.target && !this.$element.has(e.target).length
                        // CKEditor compatibility fix start.
                        && !$(e.target).closest('.cke_dialog, .cke').length
                        // CKEditor compatibility fix end.
                    ) {
                        this.$element.trigger('focus');
                    }
                }, this));
        };
    }
    function loadCategory() {
        $.ajax({
            type: 'GET',
            url: '/admin/blog/GetAllCategory',
            dataType: 'json',
            success: function (response) {
                var render = "<option value=''>--Select category--</option>";
                $.each(response, function (i, item) {
                    render += "<option value='" + item.Id + "'>" + item.Name + "</option>"
                });
                $('#ddlCategoryearch').html(render);
            },
            error: function (status) {
                console.log(status);
                tedu.notify('Cannot loading product category data', 'error');
            }
        });
    }
    function loadData(isPageChanged) {
        $.ajax({
            type: "GET",
            url: "/admin/blog/GetAllPaging",
            data: {
                categoryId: $('#ddlCategoryearch').val(),
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
                            Id: item.Id,
                            CategoryId: item.Category.Name,
                            Name: item.Name,
                            Image: item.Image == null ? '<img src="/admin-side/images/user.png" width=50' : '<img src="' + item.Image + '" width=50 />',
                            Status: tedu.getStatus(item.Status)
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
    function initTreeDropDownCategory(selectedId) {
        $.ajax({
            url: "/Admin/Category/GetAllBlog",
            type: 'GET',
            dataType: 'json',
            async: false,
            success: function (response) {
                var data = [];
                $.each(response, function (i, item) {
                    data.push({
                        id: item.Id,
                        text: item.Name,
                        parentId: item.ParentId,
                        sortOrder: item.SortOrder
                    });
                });
                var arr = tedu.unflattern(data);
                $('#ddlCategoryIdM').combotree({
                    data: arr
                });
                if (selectedId != undefined) {
                    $('#ddlCategoryIdM').combotree('setValue', selectedId);
                }
            }
        });
    }
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
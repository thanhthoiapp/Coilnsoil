var FunctionController = function () {
    this.initialize = function () {
        loadData();
        registerEvents();
    }
    function registerEvents() {
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                txtNameM: { required: true },
                txtSortOrderM: { number: true }
            }
        });

        $('#btnCreate').off('click').on('click', function () {
            resetFormMaintainance();
            $('#modal-add-edit').modal('show');
        });


        $('body').on('click', '#btnEdit', function (e) {
            e.preventDefault();
            var that = $('#hidIdM').val();

            $.ajax({
                type: "GET",
                url: "/Admin/function/GetById",
                data: { id: that },
                dataType: "json",
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function (response) {
                    var data = response;

                    $('#hidIdM').val(data.Id);
                    $('#txtNameM').val(data.Name);
                    initTreeDropDownCategory(data.ParentId);
                    $('#txtIconCssM').val(data.IconCss);
                    $('#txtSortOrderM').val(data.SortOrder);
                    $('#txtUrlM').val(data.URL);
                    $('#ckStatusM').prop('checked', data.Status == 1);

                    $('#modal-add-edit').modal('show');
                    tedu.stopLoading();

                },
                error: function (status) {
                    tedu.notify('Có lỗi xảy ra', 'error');
                    tedu.stopLoading();
                }
            });

        });

        $('body').on('click', '#btnDelete', function (e) {
            e.preventDefault();
            var that = $('#hidIdM').val();
            tedu.confirm('Are you sure to delete?', function () {
                $.ajax({
                    type: "POST",
                    url: "/Admin/function/Delete",
                    data: { id: that },
                    dataType: "json",
                    beforeSend: function () {
                        tedu.startLoading();
                    },
                    success: function (response) {
                        tedu.notify('Deleted success', 'success');
                        tedu.stopLoading();
                        loadData();
                    },
                    error: function (status) {
                        tedu.notify('Has an error in deleting progress', 'error');
                        tedu.stopLoading();
                    }
                });
            });
        });

        $('#btnSave').on('click', function (e) {
            e.preventDefault();
            if ($('#frmMaintainance').valid()) {
                var id = $('#hidIdM').val();
                var name = $('#txtNameM').val();
                var parentId = $('#ddlFunctionIdM').combotree('getValue');
                var iconCss = $('#txtIconCssM').val();
                var order = parseInt($('#txtSortOrderM').val());
                var url = $('#txtUrlM').val();
                var status = $('#ckStatusM').prop('checked') == true ? 1 : 0;

                $.ajax({
                    type: "POST",
                    url: "/Admin/function/SaveEntity",
                    data: {
                        Id: id,
                        Name: name,
                        ParentId: parentId,
                        IconCss: iconCss,
                        SortOrder: order,
                        URL: url,
                        Status: status
                    },
                    dataType: "json",
                    beforeSend: function () {
                        tedu.startLoading();
                    },
                    success: function (response) {
                        tedu.notify('Cập nhật thành công', 'success');
                        $('#modal-add-edit').modal('hide');

                        resetFormMaintainance();

                        tedu.stopLoading();
                        loadData(true);
                    },
                    error: function () {
                        tedu.notify('Có lỗi, cập nhật không thành công', 'error');
                        tedu.stopLoading();
                    }
                });
            }
            return false;

        });
    }
    function resetFormMaintainance() {
        $('#hidIdM').val('');
        $('#txtNameM').val('');
        initTreeDropDownCategory();
        $('#txtIconCssM').val('');
        $('#txtSortOrderM').val('1');
        $('#txtUrlM').val('');
        $('#ckStatusM').prop('checked', true);

    }
    function initTreeDropDownCategory(selectedId) {
        $.ajax({
            url: "/Admin/Function/GetAll",
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
                $('#ddlFunctionIdM').combotree({
                    data: arr
                });
                if (selectedId != undefined) {
                    $('#ddlFunctionIdM').combotree('setValue', selectedId);
                }
            }
        });
    }
    function loadData() {
        $.ajax({
            url: '/Admin/Function/GetAll',
            dataType: 'json',
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
                var treeArr = tedu.unflattern(data);
                treeArr.sort(function (a, b) {
                    return a.sortOrder - b.sortOrder;
                });
                //var $tree = $('#treeFunction');

                $('#treefunction').tree({
                    data: treeArr,
                    dnd: true,
                    onContextMenu: function (e, node) {
                        e.preventDefault();
                        // select the node
                        //$('#tt').tree('select', node.target);
                        $('#hidIdM').val(node.id);
                        // display context menu
                        $('#contextmenu').menu('show', {
                            left: e.pageX,
                            top: e.pageY
                        });
                    },
                    onDrop: function (target, source, point) {
                        console.log(target);
                        console.log(source);
                        console.log(point);
                        var targetNode = $(this).tree('getNode', target);
                        if (point === 'append') {
                            var children = [];
                            $.each(targetNode.children, function (i, item) {
                                children.push({
                                    key: item.id,
                                    value: i
                                });
                            });

                            //Update to database
                            $.ajax({
                                url: '/Admin/function/UpdateParentId',
                                type: 'post',
                                dataType: 'json',
                                data: {
                                    sourceId: source.id,
                                    targetId: targetNode.id,
                                    items: children
                                },
                                success: function (res) {
                                    loadData();
                                }
                            });
                        }
                        else if (point === 'top' || point === 'bottom') {
                            $.ajax({
                                url: '/Admin/Function/ReOrder',
                                type: 'post',
                                dataType: 'json',
                                data: {
                                    sourceId: source.id,
                                    targetId: targetNode.id
                                },
                                success: function (res) {
                                    loadData();
                                }
                            });
                        }
                    }
                });

            }
        });
    }
}

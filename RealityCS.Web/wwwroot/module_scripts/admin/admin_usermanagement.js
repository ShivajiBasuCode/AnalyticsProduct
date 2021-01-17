var admin_usermanagement = {
    /** */
    init: function () {

        this.fillcomboRole()
        this.fillcomboManager()
    },
    /** */
    getSearchCriteria: function () {

        return {
            Name: $('#txtSearchname').val(),
            Email: $('#txtSearchemail').val(),
            IsActive: $('#chkIsactive').is(":checked") ? true : false
        }

    },
    /** */   
    fillcomboRole: function () {

        apiCall.ajaxCallWithReturnData(undefined, 'GET', 'admin/Usermanagement/Roles')
            .then(res => {

                let data = res.Data
                if (typeof data != typeof undefined) {

                    let selectedValue = $("#ddlRole").attr("data-selected-value")
                    // console.log(data)
                    $.map(data, function (obj) {

                        obj.id = obj.Id;//adding select to required property
                        obj.text = obj.Name;//adding select to required property
                        if (selectedValue == obj.id) {
                            obj.selected = true
                        }
                        delete obj.Id;//
                        delete obj.Name
                    });



                    $("#ddlRole").empty();
                    $("#ddlRole").append($("<option style='font-size:14px;'></option>").attr("value", "").text('Select'));
                    $("#ddlRole").off('select2:select');
                    $("#ddlRole").off('select2:unselect');
                    $('#ddlRole').select2({
                        data: data,
                        //theme: "default",
                        //width: "250px",
                        containerCss: { "font-size": "12px" },
                        dropdownCss: { "font-size": "12px" },
                    })

                }
            })
    },
    /** */
    fillcomboManager: function () {

        apiCall.ajaxCallWithReturnData({ RoleId: 3 }, 'GET', 'admin/Usermanagement/GetUsers')
            .then(res => {

                let data = res.Data

                let selectedValue = $("#ddlRepotsTo").attr("data-selected-value")

                $.map(data, function (obj) {

                    if (selectedValue == obj.id) {
                        obj.selected = true
                    }

                });

                $("#ddlRepotsTo").empty();
                $("#ddlRepotsTo").append($("<option style='font-size:12px;'></option>").attr("value", "").text('Select'));
                $("#ddlRepotsTo").off('select2:select');
                $("#ddlRepotsTo").off('select2:unselect');
                $('#ddlRepotsTo').select2({
                    data: data,
                    //theme: "default",
                    //width: "200px",                    
                    containerCss: { "font-size": "12px" },
                    dropdownCss: { "font-size": "12px" },
                })

            })
    },
    /** */
    ListUsers: function () {
        commonFunctions.clearDatatable('dtUserlist')
        let userSearchCriteria = admin_usermanagement.getSearchCriteria();

        apiCall.ajaxCallWithReturnData(userSearchCriteria, 'POST', 'admin/UserManagement/ListUsers')
            .then(res => {
                admin_usermanagement.onSuccess_ListUsers(res.Data)
            })
    },
    /**
     * 
     * @param {any} data
     */
    onSuccess_ListUsers: function (data) {

        var dtUserlist = $('#dtUserlist').DataTable({
            bServerSide: false,
            bDestroy: true,
            paging: true,
            autoWidth: false,
            bStateSave: false,
            searching: false,
            data: data,
            info: false,
            aaSorting: [],
            language: {
                paginate: {
                    previous: "<",
                    next: ">"
                },
                info: "Showing _START_ - _END_ of _TOTAL_ readings",
            },
            aoColumns: [

                {
                    mData: null, defaultContent: "", sTitle: "#", sClass: "head1 text-left", bSortable: true,
                    mRender: function (data,type,full,meta) {

                        return (meta.row+1)
                    }
                },
                {
                    mData: "Name", sTitle: "Name", sClass: "head1 text-left", bSortable: true,
                },
                {
                    mData: "Email", sTitle: "Email", sClass: "head1 text-left", bSortable: true,
                },
                {
                    mData: "Role", sTitle: "Role", sClass: "head1 text-left", bSortable: true,
                },
                //{
                //    mData: "Manager", sTitle: "Manager", sClass: "head1 text-left", bSortable: true,
                //},
                {
                    mData: "IsActive", sTitle: "Status", sClass: "head1 text-center", bSortable: true,
                    mRender: function (data) {
                        if (data == true) {
                            return 'Active'
                        }
                        else {
                            return 'Inactive'
                        }
                    }
                },
                {
                    mData: "Id", defaultContent: "", sTitle: "", sClass: "head1 text-right", bSortable: false,
                    mRender: function (data, type, full) {

                        var url = config.webhostingdomain + 'admin/Usermanagement/Add/' + data

                        var markup = '<a  href="' + url + '">'
                        markup += '      <i class="fas fa-edit" aria-hidden="true"></i>'
                        markup += '   </a>'
                        return markup;
                    }
                },


            ],

            bUseRendered: true,
            sPaginationType: "simple_numbers",
            aaSorting: [],
            "dom": '<<"float-right"l>f<t><"#df"<"float-left" i><"float-right pagination pagination-sm p-1"p>>>',
            "lengthChange": true,
            "lengthMenu": [[10, 20, 30, -1], [10, 20, 30, "All"]],
            pageLength:20,
            initComplete: function (settings) {
                // 
                //$('#dtweeklyScorecardList > thead').addClass('table-head-bgcolor');
                $(settings.nTHead).addClass('table-head-bgcolor');
                //
            },

        });

    },
    /**
     * 
     * @param {any} response
     */
    addUserAjaxComplete: function (response) {

        let res = response.responseJSON

        console.log(res)

        switch (response.status) {
            case 200:
                if (res.Data.StatusCode == 2) {
                    $('#spanPassword').text(res.Data.Description)
                    return false;
                }
                if (parseInt($('#hdnId').val()) <= 0) {
                    commonFunctions.resetControls('frmAdduser')
                }
                bootbox.alert(res.Data.Description)
                break;
        }

    }

}


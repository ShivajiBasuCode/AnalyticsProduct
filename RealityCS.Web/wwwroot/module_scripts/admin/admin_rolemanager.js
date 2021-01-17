var admin_rolemanager = {
    /**
     * 
     * */
    ListRoles: function () {
        clearDatatable('dtRolelist')
       

        apiCall.ajaxCallWithReturnData(undefined, 'GET', 'admin/RoleManager/Roles')
            .then(res => {
                admin_rolemanager.onSuccess_ListRoles(res.Data)
            })
    },
    /**
     * 
     * @param {any} data
     */
    onSuccess_ListRoles: function (data) {

        var dtRolelist = $('#dtRolelist').DataTable({
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
                    mRender: function (data, type, full, meta) {

                        return (meta.row + 1)
                    }
                },
                {
                    mData: "Name", sTitle: "Name", sClass: "head1 text-left", bSortable: true,
                },                              
                {
                    mData: "Id", defaultContent: "", sTitle: "", sClass: "head1 text-right", bSortable: false,
                    mRender: function (data, type, full) {

                        var url = config.webhostingdomain + 'admin/RoleManager/Add/' + data

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
            pageLength: 20,
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
    addRoleAjaxComplete: function (response) {

        //let res = response.responseJSON

        //console.log(res)

        //switch (response.status) {
        //    case 200:
        //        if (res.Data.StatusCode == 2) {
        //            $('#spanPassword').text(res.Data.Description)
        //            return false;
        //        }
        //        if (parseInt($('#hdnId').val()) <= 0) {
        //            commonFunctions.resetControls('frmAdduser')
        //        }
        //        bootbox.alert(res.Data.Description)
        //        break;
        //}

    }

}
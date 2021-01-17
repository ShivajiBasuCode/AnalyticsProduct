
var admin_clientmanagement = {

    controllerurl: "admin/ClientManagement/",
    /**
     * 
     * */
    initlistpage: function () {
        $(document).ready(() => {
            admin_clientmanagement.List();
            admin_clientmanagement.FillSearchParameters();
        })

    },
    /**
     * 
     * */
    initaddpage: function () {
        $(document).ready(() => {
            commonFunctions.createDatePicker()
            commonFunctions.createTimePicker()
            admin_clientmanagement.FillDropdown();
            admin_clientmanagement.FillMultiselect();
        })
    },
    /**
     * */
    getSearchCriteria: function () {

        return {
            Field: $("#Field").val(),
            Query: $("#Query").val(),
            //ClientName: $('#txtClientName').val(),
            IncludeInactive: $('#IncludeInactive').is(":checked") ? true : false,
            IncludeDeleted: $('#IncludeDeleted').is(":checked") ? true : false
        }

    },
    //#region Fill Dropdowns

    FillDropdown: function () {

        let selectedvalue = $('#DropDownCol').attr('data-selectedvalue')

        fillcombo.DropdownData(selectedvalue).then(data => {
            commonFunctions.fillCombo('DropDownCol', data);
        })
    },
    FillMultiselect: function () {
        let selectedvalue = $('#MultiSelect').attr('data-selectedvalue')
        fillcombo.DropdownData(selectedvalue).then(data => {
            commonFunctions.fillCombo('MultiSelect', data, true);
        })
    },
    FillSearchParameters: function () {

        let payload = { Table: "ClientInformation" }
        fillcombo.SearchParameterData(payload).then(data => {
            //console.log(data)
            commonFunctions.fillCombo('Field', data, false);
        })
    },

    //#endregion

    /**
     * return a list of clients based on search criteria.
     * */
    List: function () {
        commonFunctions.clearDatatable('dtclientlist')

        let clientSearchCriteria = admin_clientmanagement.getSearchCriteria();

        apiCall.ajaxCallWithReturnData(clientSearchCriteria, 'POST', admin_clientmanagement.controllerurl + 'List')
            .then(res => {
                //console.log(res)               
                admin_clientmanagement.onsuccess_List(res.Data)
            })

    },   
    /**
     *Callback method of List function 
     * @param {any} response
     */
    onsuccess_List: function (data) {

        var dtclientlist = $('#dtclientlist').DataTable({
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
                    mData: null, defaultContent: "", sTitle: "#", sClass: "head1 text-left", bSortable: false,
                    mRender: function (data, type, full, meta) {

                        return (meta.row + 1)
                    }
                },
                {
                    mData: "CompanyName", sTitle: "Company Name", sClass: "head1 text-left", bSortable: false,
                },
                {
                    mData: "CityName", sTitle: "City Name", sClass: "head1 text-left", bSortable: false,
                },
                {
                    mData: "ContactPersonName", sTitle: "Contact Person", sClass: "head1 text-left", bSortable: false,
                },
                {
                    mData: "ContactEmail", sTitle: "Contact Email", sClass: "head1 text-left", bSortable: false,
                },
                {
                    mData: "ContactMobileNo", sTitle: "Mobile No", sClass: "head1 text-left", bSortable: false,
                },
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
                    mData: null, defaultContent: "", sTitle: "Edit", sClass: "head1 text-right", bSortable: false, sWidth: "5px",
                    mRender: function (data, type, full) {

                        var url = config.webhostingdomain + admin_clientmanagement.controllerurl + 'Add/' + full.ClientID

                        var markup = '<a  href="' + url + '">'
                        markup += '      <i class="fas fa-edit" aria-hidden="true"></i>'
                        markup += '   </a>'
                        return markup;
                    }
                },

                {
                    mData: null, defaultContent: "", sTitle: "Change Status", sClass: "head1 text-center", bSortable: false, sWidth: "10px",
                    mRender: function (data, type, full) {

                        var markup = '<a  href="javascript:void(0)">'
                        markup += '      <i class="fas fa-exchange-alt" aria-hidden="true" onclick="commonFunctions.confirmation(&apos;Are you sure to change status &apos;,admin_clientmanagement.ChangeStatus.bind({ClientID:' + full.ClientID + ',Status:' + !full.IsActive +'}))"></i>'
                        markup += '   </a>'
                        return markup;
                    }
                },

                {
                    mData: null, defaultContent: "", sTitle: "Delete", sClass: "head1 text-center", bSortable: false,sWidth:"8px",
                    mRender: function (data, type, full) {

                        var markup = '<a  href="javascript:void(0)">'
                        markup += '      <i class="far fa-trash-alt" aria-hidden="true" onclick="commonFunctions.confirmation(&apos;Are you sure to delete this record?&apos;,admin_clientmanagement.Delete.bind({ClientID:' + full.ClientID + '}))"></i>'
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
    * @param {any} callbackResult
    */
    ChangeStatus: function (callbackResult) {
        if (callbackResult == true) {
            apiCall.ajaxCall(undefined, 'GET', admin_clientmanagement.controllerurl + 'ChangeStatus', { ClientID: this.ClientID, IsActive: this.Status })
                .then(res => {
                    admin_clientmanagement.List();
                })
        }
    },
    /**
     * Delete client's record .
     * @param {any} callbackResult
     */
    Delete: function (callbackResult) {

        if (callbackResult == true) {
            apiCall.ajaxCall(undefined, 'GET', admin_clientmanagement.controllerurl + 'Delete', { ClientID: this.ClientID })
                .then(res => {
                    admin_clientmanagement.List();
                })
        }

    },
    /**
     * 
     * @param {any} response
     */
    addUserAjaxComplete: function (response) {

        let data = response.responseJSON

        //console.log(data)

        if (data.IsSuccess == true && data.ReturnMessage == "Client updated") {
            bootbox.alert("Record updated successfully.");
        }
        if (data.IsSuccess == true && data.ReturnMessage == "Client Created") {
            commonFunctions.resetControls('frmAddclient')
            bootbox.alert("Record created successfully.");
        }



    }

}

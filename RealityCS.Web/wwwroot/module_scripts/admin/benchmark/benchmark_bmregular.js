var benchmark_bmregular = {

    initialize: function (kpicid, operation) {
        apiCall.ajaxCallWithReturnData({ kpicid: kpicid, operation: operation }, "GET", "admin/Benchmark/PopulateGrid")
            .then(res => {
                let data = res.Data
                benchmark_bmregular.loadTale(data)
            })
    },

    loadTale: function (data) {
        var dtgrdvRegBm = $('#dtgrdvRegBm').DataTable({
            bServerSide: false,
            bDestroy: true,
            paging: false,
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
                    mData: "TimeLines", sTitle: "Time Lines", sClass: "head1 text-left", bSortable: false,
                },

                {
                    mData: "LowerValue", sTitle: "Lower Value", sClass: "head1 text-left", bSortable: false,
                    mRender: function (data, type, full, meta) {

                        return '<input type="text" value="' + data + '" class="form-control form-control-sm"  onkeyup="benchmark_bmregular.updateTableValue(this,' + meta.row + ',&apos;LowerValue&apos;)" />'
                    }
                },
                {
                    mData: "MidValue", sTitle: "Mid Value", sClass: "head1 text-left", bSortable: false,
                    mRender: function (data, type, full, meta) {

                        return '<input type="text" value="' + data + '" class="form-control form-control-sm" onkeyup="benchmark_bmregular.updateTableValue(this,' + meta.row + ',&apos;MidValue&apos;)" />'
                    }
                },
                {
                    mData: "UpperValue", sTitle: "Upper Value", sClass: "head1 text-left", bSortable: false,
                    mRender: function (data, type, full, meta) {

                        return '<input type="text" value="' + data + '" class="form-control form-control-sm" onkeyup="benchmark_bmregular.updateTableValue(this,' + meta.row + ',&apos;UpperValue&apos;)" />'
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

    updateTableValue: function (control, row, field) {       

        //if (event.which != 8 && event.which != 0 && event.which != 46 && (event.which < 48 || event.which > 57)) {
        //    console.log(event.which)
        //    return false;
        //}
        
        var tabledata = $("#dtgrdvRegBm").dataTable().fnGetData()
        tabledata[row][field]=parseFloat($(control).val())
     },

    fillComboTimeline: function (kpicid) {
        apiCall.ajaxCallWithReturnData({ kpicid: kpicid }, "GET", "admin/Benchmark/Timelines")
            .then(res => {
                return $('#ddlTimelines').select2({
                    data: res.Data,
                    theme: "bootstrap",
                    multiple: false
                })
            }).done((ddl) => {
                ddl.on('select2:select', function (e) { console.log(e.params.data) })
            })
    },

    saveBenchmark: function () {
        let data = $('#dtgrdvRegBm').dataTable().fnGetData()

        console.log(data)
       
        apiCall.ajaxCall(undefined, 'POST', 'admin/BenchMark/SaveBenchMark', data)
    },


}

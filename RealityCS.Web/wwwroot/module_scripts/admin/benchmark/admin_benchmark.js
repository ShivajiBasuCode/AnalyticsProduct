var admin_benchmark = {

    setBenchMarkMenuByRole: function () {

        
        apiCall.ajaxCallWithReturnData({role:'CORPORATE'}, 'GET', 'admin/Benchmark/SetBenchMarkMenuByRole')
            .then(res => {
                //admin_rolemanager.onSuccess_ListRoles(res.Data)
               // console.log(res)
                //$.jstree.defaults.core.themes.variant='small'

                

                $('#divListBenchmark').jstree({
                    core: {
                        multiple:false,
                        data: res.Data,
                        themes: {
                            icons: false,
                            dots: true,
                            stripes: false,
                            variant: false,
                            responsive:true
                        },
                    }
                });


               

                $('#divListBenchmark').on('ready.jstree', function () {
                    
                    //$("#divListBenchmark").jstree("open_all");
                });

                $('#divListBenchmark').on('select_node.jstree', function (node, selected, ev) {
                    
                   // console.log(node)
                    //console.log(selected.node)
                   // console.log(selected.node.data)
                    let data = selected.node.data;

                    //var r = data.kpiname//$(this).parent().attr('rel');
                    //if (r && r.length) {
                    //    var q = data.cntrl// $.address.parameter('q');
                    //    if ($.address.parameter('f') === r) {
                    //        $.address.value('/' + (q ? '?q=' + q : ''));
                    //    } else {
                    //        $.address.value('/?' + (q ? 'q=' + q + '&' : '') + 'f=' + r);
                    //    }
                    //}

                    apiCall.loadPartial({ KPI_TYPE: data.cntrl, kpiname: data.kpiname, kpicd: data.kpicd, operation: data.operation }, "GET", "admin/Benchmark/GetUserControl")
                        .then(res => {
                            $("#divPVContent").html(res)                           


                        })

                    //console.log(ev)
                    // $("#jstree_demo_div").jstree("open_all");
                });

            })
    }
    

}
var fillcombo = {
    controllerUrl: 'common/FillCombo/',
    /**
     * pass the <select> element ,which need to fill .
     * and the function return a select2 object to add
     * required events .
     * to attach an event use bellow code
     * .on('select2:select', function (e) { console.log(e.params.data) })
     * @param {any} element
     */
    kpiIndustries: function (element) {
        element = $('#' + element)
        element.append($("<option></option>").attr("value", "0").text("Select"))
        apiCall.ajaxCallWithReturnData(undefined, "GET", "common/FillCombo/KpiIndustries")
            .then(res => {
                element.select2({
                    data: res.Data,
                    theme: "bootstrap",
                    multiple: false
                })
            })

        return element;
    },
    /**
     * pass the <select> element ,which need to fill .
     * and the function return a select2 object to add
     * required events .
     * to attach an event use bellow code
     * .on('select2:select', function (e) { console.log(e.params.data) })
     * @param {any} element
     */
    kpiTypes: function (element) {
        element = $('#' + element)
        element.append($("<option></option>").attr("value", "0").text("Select"))
        apiCall.ajaxCallWithReturnData(undefined, "GET", "common/FillCombo/KpiTypes")
            .then(res => {
                element.select2({
                    data: res.Data,
                    theme: "bootstrap",
                    multiple: false
                })
            })

        return element;
    },
    /**
     * return data to fill dromdown
     * @param {any} selectedvalue
     */
    DropdownData: function (selectedvalue) {

        let selectedvaluearr = (typeof selectedvalue!=typeof undefined)? selectedvalue.split(','):[]

        return apiCall.ajaxCallWithReturnData(undefined, 'GET', fillcombo.controllerUrl + 'Dropdown')
            .then(response => {

                let data = ((typeof response.Data !== typeof undefined && response.Data != null) && response.Data.length > 0) ? response.Data : []

                if (data.length > 0) {
                    $.map(data, function (obj) {

                        $.map(selectedvaluearr, function (selected) {
                            if (selected == obj.id) {
                                obj.selected = true
                            }
                        })



                    });

                }
                return data
            })
    },
    /**
     * 
     * @param {any} payload payload example {Table:''}
     */
    SearchParameterData: function (payload) {       

        return apiCall.ajaxCallWithReturnData(payload, 'GET', fillcombo.controllerUrl + 'SearchParameters')
            .then(response => {                
                return response.Data
            })
    }

}
var commonFunctions = {
/**
 * clear form's element
 * @param {any} formid form id without #
 */
    resetControls: function (formid/*, objFieldsValue*/) {

        /*
            assign objFieldsValue with some value when need to set value into field after reset
            checkboxValue => true/false ; default =>true
            dropdownValue => selectedvalue [any numeric] ;  default =>0
        */
        var fieldDefaultValue = {
            checkboxValue: true,
            dropdownValue: 0,
            selectedIndex: 0
        }

        /* if ('undefined' !== typeof objFieldsValue) {
             for (var i in objFieldsValue) {
                 if ('undefined' !== typeof objFieldsValue[i]) {
                     fieldDefaultValue[i] = objFieldsValue[i];
                 }
             }
         }*/
        $('.validationerror').remove()
        $('#' + formid).find(':input').each(function (index, element) {
            /*
            In future We can bellow values
            console.log(element)
            console.log($(element).is("input"))
            console.log($(element).get(0).tagName)
            console.log($(element).attr('id'))
            console.log($(element).attr('type'))            
           */

            //if ($(element).attr('type') != 'hidden') {
            switch ($(element).get(0).tagName) {
                case 'INPUT':
                    if (typeof ($(element).attr('type')) == 'undefined' || $(element).attr('type') == 'text'
                        || $(element).attr('type') == 'email' || $(element).attr('type') == 'tel'
                        || $(element).attr('type') == 'password') {
                        $(element).val('');
                        $(element).removeClass('is-invalid')
                    }
                    else {
                        switch ($(element).attr('type')) {
                            case 'checkbox':
                                $(element).prop('checked', false)
                                break;
                            case 'hidden':
                                $(element).val('0');

                                break;
                        }
                    }
                    break;
                case 'SELECT':

                    $(element).val(fieldDefaultValue.dropdownValue).trigger('change');
                    /*if (fieldDefaultValue.selectedIndex != "") {
                        $(element)[0].selectedIndex = fieldDefaultValue.selectedIndex;
                    }
                    else {
                        $(element).val(fieldDefaultValue.dropdownValue).trigger('change');
                    }
    
                    if ('undefined' !== typeof $(element).attr('combodefaultvalue')) {
                        $(element).val($(element).attr('combodefaultvalue')).trigger('change');
                    }*/

                    break;
                case 'TEXTAREA':
                    if (typeof summernote !== typeof undefined) {
                        $('.editor').summernote('reset');
                    }                    
                    $(element).val('');
                    break;
            }
            //}
        })
    },

    /**
 * creating datepicker .
 * ElementID => on which elemt datepicker is needed.
 */
    createDatePickerByID: function (ElementID) {

        $('#' + ElementID).datetimepicker({
            format: 'DD/MM/YYYY',
            useCurrent: false,
            allowInputToggle: true,
        })
    },

    /**
     * 
     * */
    createDatePicker: function () {


        $('div.date').each(function (index, element) {
            // console.log($(element))
            if (!$(element).hasClass('time')) {
                $(element).datetimepicker({
                    defaultDate: moment(new Date(), "DD/MM/YYYY"),
                    format: $(element).hasClass('time') ? 'LT' : 'DD/MM/YYYY'
                }).on('dp.change', function (e) {
                    $(this).find(":input").val(moment(e.date).format('DD/MM/YYYY'))
                    $(this).find(":input").trigger("change");

                });
            }
        })

    },
    /**
     * 
     * */
    createTimePicker: function () {
        //console.log($('div.date.time'))
        $('div.date.time').datetimepicker({
            format: 'LT'
        });
    },

    /**
     * 
     * @param {any} url
     */
    goTo: function (url) {
        window.location.href = config.webhostingdomain + url;
    },
    /**
     * 
     * @param {any} formid
     */
    validateForm: function (formid) {

        $form = $("#" + formid);

        $.validator.unobtrusive.parse($form);
        $form.validate();

        if (!($form.valid())) {
            $.each($form.validate().errorList, function (key, value) {      // show form validation message if not valid
                $errorSpan = $("span[data-valmsg-for='" + value.element.id + "']");
                $errorSpan.html("<span class='text-danger'>" + value.message + "</span>");
                $errorSpan.show();
            });
        }

        if ($form.validate().errorList.length == 0) {
            return true
        } else {
            return false
        }
    },

    /**
     * check a datatable instance exists or not
     * @param {any} dataTableID
     */
    isDataTableExits: function (dataTableID) {
        return $.fn.DataTable.isDataTable("#" + dataTableID + "");
    },

    /**
     * empty datatable instance
     * @param {any} dataTableID
     */
    emptyDataTable: function (dataTableID) {
        $("#" + dataTableID + "").empty();
    },

    /**
     * destroy datatable instance
     * @param {any} dataTableID
     */
    destroyDataTable: function (dataTableID) {
        //  $("#" + dataTableID + "").dataTable().fnDestroy();
        //https://datatables.net/forums/discussion/comment/93067/#Comment_93067
        $("#" + dataTableID + "").DataTable().destroy();
    },
    /**
     * 
     * @param {any} dataTableID
     */
    clearDatatable: function (dataTableID) {
        if (commonFunctions.isDataTableExits(dataTableID) == true) {
            commonFunctions.destroyDataTable(dataTableID)
            commonFunctions.emptyDataTable(dataTableID)
        }
    },
    /**
    *  pass the <select> element ,which need to fill .
    * and the function return a select2 object to add
    * required events .
    * to attach an event use bellow code
    * .on('select2:select', function (e) { console.log(e.params.data) })
    * @param {any} element element The HTML select element's ID on which select2 needed
    * @param {any} data data    data ned to load
    * @param {any} multiple for multi select 
    */
    fillCombo: function (element, data, multiple) {
        // alert(1)
        element = $('#' + element)
        
        if (data.findIndex(x => x.selected == true) <0) {
            element.prepend($("<option selected='true'></option>").attr("value", "0").text("Select"))
        }
        if (data.findIndex(x => x.selected == true) >= 0) {
            element.prepend($("<option ></option>").attr("value", "0").text("Select"))
        }
        

        if ((typeof data !== typeof undefined && data != null) && data.length > 0) {
            if ((typeof multiple == typeof undefined || multiple==false)) {
                element.select2({
                    data: data,
                    theme: "classic",
                    multiple:  false,
                    width: "100%",
                })
            }
            else {
                element.select2({
                    data: data,
                    theme: "classic",
                    multiple: true,
                    width: "100%",
                })

            }
        }
        else {
            element.select2({
                theme: "classic",
                multiple: (typeof multiple == typeof undefined) ? false : true,
                width: "100%"
            })
        }

        return element;
    },
    confirmation: function (message,callback) {
        
        bootbox.confirm({
            message: message,
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-danger'
                }
            },
            callback: callback
            //callback: function (result) {
            //    console.log('This was logged in the callback: ' + result);
            //}
        });


    }

}
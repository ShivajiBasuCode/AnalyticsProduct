/**
 * Ref reading
 * https://stackoverflow.com/a/6394826
 * [Reason: CORS request not HTTP](https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS/Errors/CORSRequestNotHttp)
 * 
 * 
 */

/*
 var apiEndpoint = "http://localhost:5000/api/"
var apiEndpoint = "http://ec2-18-218-30-23.us-east-2.compute.amazonaws.com:81/api/"
*/




var apiEndpoint = config.webhostingdomain

var apiCall = {

    /**
     * 
     * 
     * 
     * 
     *
     */
    ajaxCall: function (ParentForm, RequestType, Url, AjaxData, headers) {

        var queryParams = ''
        var formatedData = {}
        var xhr
        formatedData = typeof AjaxData === typeof undefined ? this.prepareAjaxCallData(ParentForm) : AjaxData

        if (RequestType == "GET" && typeof ParentForm !== typeof undefined || ParentForm !== null) {

            $.each(formatedData, function (key, value) {
                queryParams += key + '=' + value + '&'
            });

            queryParams = queryParams.slice(0, -1);
        }

        switch (RequestType) {
            case 'GET':

                xhr = $.ajax({
                    type: 'GET',
                    url: queryParams == '' ? apiEndpoint + Url : apiEndpoint + Url + '?' + queryParams,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    //context: this,
                    headers: (typeof headers === typeof undefined) ? null : headers
                });

                break;

            case 'POST':

                xhr = $.ajax({
                    type: 'POST',
                    url: apiEndpoint + Url,
                    data: JSON.stringify(formatedData),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    //context: this,
                    headers: (typeof headers === typeof undefined) ? null : headers
                });

                break;
        }
        return xhr;

    },
/**
*
* */
    ajaxCallWithReturnData: function (ApiParameter, RequestType, Url, headers,async) {

        var queryParams = ''
        var xhr

        if (RequestType == "GET" && typeof ApiParameter !== typeof undefined || ApiParameter !== null) {

            $.each(ApiParameter, function (key, value) {
                queryParams += key + '=' + value + '&'
            });

            queryParams = queryParams.slice(0, -1);
        }
       
        //console.log(DataFormat == 'object' ? JSON.stringify(formatedData) : formatedData)
        switch (RequestType) {
            case 'GET':

                xhr = $.ajax({
                    type: 'GET',
                    async:(typeof async===typeof undefined)?true:async,
                    url: queryParams == '' ? apiEndpoint + Url : apiEndpoint + Url + '?' + queryParams,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    headers: (typeof headers===typeof undefined)?null:headers
                })

                break;

            case 'POST':

                xhr = $.ajax({
                    type: 'POST',
                    url: apiEndpoint + Url,
                    data: JSON.stringify(ApiParameter),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    headers: (typeof headers === typeof undefined) ? null : headers
                })
                break;
        }

        return xhr;

    },

/**
*
* */
    ajaxFileUpload: function (fileuploadelement, Url) {


        if (window.FormData !== undefined) {

            var fileUpload = $("#" + fileuploadelement).get(0);
            var files = fileUpload.files;

            if (files.length <= 0) {
                return alert("PLease select a file.")
            }

            // Create FormData object
            var fileData = new FormData();

            // Looping over all files and add it to FormData object
            for (var i = 0; i < files.length; i++) {
                // Adding one more key to FormData object   
                fileData.append(files[i].name, files[i]);
            }            

            return $.ajax({
                url: apiEndpoint + Url,
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                data: fileData
            })

        } else {
            return alert("FormData is not supported.");
        }
    },

/**
*
* */
    prepareAjaxCallData: function (parentForm) {

        var formatedData = {}

        $('#' + parentForm).find(':input').each(function (index, element) {

            /*
              console.log(element)
              console.log($(element).is("input"))
              console.log($(element).get(0).tagName)
              console.log($(element).attr('id'))
              console.log($(element).attr('type'))             
             */

            if ($(element).attr('data-ParameterName') !== undefined) {
                switch ($(element).get(0).tagName) {
                    case 'INPUT':
                        if ($(element).attr('type') == 'checkbox') {
                            formatedData[$(element).attr('data-ParameterName')] = $(element).is(":checked") ? true : false;
                        }
                        else if ($(element).attr('type') == 'radio') {
                            $('[name="' + $(element).attr('name') + '"]').each(function (ri, radio) {

                                if (typeof $(radio).attr('data-ParameterName') != typeof undefined) {

                                    if ($(radio).is(":checked")) {
                                        formatedData[$(radio).attr('data-ParameterName')] = true
                                    }
                                    else {
                                        formatedData[$(radio).attr('data-ParameterName')] = false
                                    }
                                }
                            })

                        }
                        else {
                            formatedData[$(element).attr('data-ParameterName')] = $(element).val()
                        }
                        break;
                    case 'SELECT':
                        if ($.isArray($(element).val())) {

                            var multiselectvalue = $(element).val()
                            formatedData[$(element).attr('data-ParameterName')] = multiselectvalue.join(',')
                        }
                        else {
                            formatedData[$(element).attr('data-ParameterName')] = $(element).val()
                        }

                        break;
                    case 'TEXTAREA':
                        if ($(element).hasClass('editor')) {
                            formatedData[$(element).attr('data-ParameterName')] = $(element).summernote('code')
                        }
                        else {
                            formatedData[$(element).attr('data-ParameterName')] = $(element).val()
                        }

                        break;
                }
            }

        })
        //console.log(formatedData)
        return formatedData;

    },

/**
*
* */
    bindModel: function (parentForm, data) {
        //console.log(data)
        $.each(data, function (key, value) {
            $('#' + parentForm).find("[data-ParameterName='" + key + "']").each(function (index, element) {
                /*
                  console.log(element)
                  console.log($(element).is("input"))
                  console.log($(element).get(0).tagName)
                  console.log($(element).attr('id'))
                  console.log($(element).attr('type'))             
                 */
                if ($(element).attr('data-ParameterName') !== undefined) {
                    switch ($(element).get(0).tagName) {
                        case 'INPUT':
                            if ($(element).attr('type') == 'checkbox') {

                                var checkboxValue = value == 1 ? true : false;
                                $(element).prop('checked', checkboxValue)
                            }
                            /* else if ($(element).attr('type') == 'radio') {
                                 $('[name="' + $(element).attr('name') + '"]').each(function (ri, radio) {
 
                                     if (typeof $(radio).attr('data-ParameterName') != typeof undefined) {
 
                                         if ($(radio).is(":checked")) {
                                             formatedData[$(radio).attr('data-ParameterName')] = true
                                         }
                                         else {
                                             formatedData[$(radio).attr('data-ParameterName')] = false
                                         }
                                     }
                                 })
 
                             }*/
                            else {
                                $(element).val(value)
                            }
                            break;

                        case 'SELECT':
                            value = value == null ? 0 : value
                            var multiple = $(element).prop('multiple');

                            // For some browsers, `attr` is undefined; for others,
                            // `attr` is false.  Check for both.
                            if (typeof multiple !== typeof undefined && multiple === true) {

                                if (value.split(',').length <= 0) {
                                    $(element).val(0).trigger('change');
                                }
                                else {
                                    $(element).val(value.split(',')).trigger('change');
                                }
                            }
                            else {
                                $(element).val(value).trigger('change');
                            }
                            break;

                        case 'TEXTAREA':
                            if ($(element).hasClass('editor')) {
                                $(element).summernote('code', value)
                            }
                            else {
                                $(element).val(value)
                            }

                            break;
                    }
                }
            })
        });


    },

    loadPartial: function (Parameter, RequestType, Url) {


        var queryParams = ''
        var xhr

        if (RequestType == "GET" && typeof Parameter !== typeof undefined || Parameter !== null) {

            $.each(Parameter, function (key, value) {
                queryParams += key + '=' + value + '&'
            });

            queryParams = queryParams.slice(0, -1);
        }

        //console.log(DataFormat == 'object' ? JSON.stringify(formatedData) : formatedData)
        switch (RequestType) {
            case 'GET':

                xhr = $.ajax({
                    type: 'GET',  
                    cache: false,
                    url: queryParams == '' ? apiEndpoint + Url : apiEndpoint + Url + '?' + queryParams,
                    dataType: "html",
                })

                break;

            case 'POST':

                xhr = $.ajax({
                    type: 'POST',
                    cache: false,
                    url: apiEndpoint + Url,
                    data: JSON.stringify(Parameter),
                    dataType: "html",
                   
                })
                break;
        }

        return xhr;
    },

}
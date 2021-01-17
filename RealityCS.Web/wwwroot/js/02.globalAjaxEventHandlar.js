/**
 * 
 * @param {any} e
 */
function getEventSource(e) {
    if (typeof event != typeof undefined) {

        let eventType = event.type;
        let element;
        switch (eventType) {
            case 'submit':
                element = $(event.submitter)
                break;
            case 'click':
                element = $(event.target)
                break;
        }

        return element
    }
    else {
        return undefined
    }
}
/**
 * 
 * */
$(document).ajaxSend(function (ajaxevent, jqxhr, settings) {

    let element = getEventSource(event);
    //console.log(element)
    if (element != undefined) {
        if ($(element).hasClass('btn')) {
            let existingbtnid = $(element).attr('id')
            let existingbtnicon = $(element).find('[data-fa-i2svg]')
            let iconprefix = existingbtnicon.attr('data-prefix');
            let icon = existingbtnicon.attr('data-icon');
            
            $(element).prop('disabled',true);
            $(existingbtnicon).remove()

            let id = 'loader_' + Math.random().toString(36).substr(2, 9);
            jqxhr.setRequestHeader('X-Frmwk-Fa-Sync-Id', id)
            jqxhr.setRequestHeader('X-Frmwk-Exist-Icon-Cls', iconprefix + " fa-" + icon)
            jqxhr.setRequestHeader('X-Frmwk-Exist-Btn-Id', existingbtnid)
            $(element).append('<i id="' + id + '" class="fas fa-sync-alt fa-spin"  aria-hidden="true"></i>')
        }
    }
})
/**
 *
 * */
$(document).ajaxComplete(function (ajaxevent, xhr, settings) {

    var fasyncid = xhr.getResponseHeader('X-Frmwk-Fa-Sync-Id');
    var existiconcls = xhr.getResponseHeader('X-Frmwk-Exist-Icon-Cls');
    var clickedbtnid = xhr.getResponseHeader('X-Frmwk-Exist-Btn-Id');

    if (typeof headers !== typeof undefined) {
        $('body').find('#' + fasyncid).remove()
    }

    if ((typeof clickedbtnid !== typeof undefined) && (typeof existiconcls !== typeof undefined)) {
        let clickedbtnbutton = $("#" + clickedbtnid);

        $(clickedbtnbutton).prop('disabled',false);
        $(clickedbtnbutton).find('[data-fa-i2svg]').remove()
        $(clickedbtnbutton).append('<i  class="' + existiconcls + '"  aria-hidden="true"></i>')
    }


})





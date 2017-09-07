/***
**Common Javascript Framework
***/

function CommonJS() {
}

/***
*ajaxUrl - Url where the call is to be made
*callType - POST,GET,PUT,DELETE
*postData - Post Params
*successCallback - function to be called when ajax call succeeds
*failureCallBack - function to be called when ajax call fails
***/
function ajaxCall(ajaxUrl, postData, callType, successCallBack, failureCallBack, dataType, contentType) {

    if (callType == undefined || callType == null) callType = CommonJS.Type.POST;
    if (successCallBack == undefined || successCallBack == null) successCallBack = defaultSuccessCallBack;
    if (failureCallBack == undefined || failureCallBack == null) failureCallBack = defaultFailureCallBack;
    $("#spinner").fadeIn();
    $.ajax(
    {
        url: ajaxUrl,
        type: callType,
        data: postData,
        datatype: dataType,
        contentType: contentType,
        cache: false,
        success: function (data) {
            if (checkTimeout(data)) {
                $("#spinner").fadeOut();
                successCallBack(data);
            }
        },
        error: function (result) {
            if (checkTimeout(result)) {
                $("#spinner").fadeOut();
                failureCallBack(result);

            }
        }
    });
}

CommonJS.ajaxCall = ajaxCall;
CommonJS.Type = { GET: "GET", POST: "POST", PUT: "PUT", DELETE: "DELETE" };
CommonJS.State = { ERROR: "ERROR", SUCCESS: "SUCCESS", WARNING: "WARNING", ALERT: "ALERT", INFO: "INFO" };


function defaultFailureCallBack(result) {
    var message = result.statusText;
    if (result.responseText != undefined && result.responseText != null) {
        var errorObj = $.parseJSON(result.responseText);
        if (errorObj.error) {
            message = errorObj.message;
        }

    }
    CommonJS.showMessage(CommonJS.State.ERROR, message);
}

function showMessage(state, msg) {

    var msgData = {
        text: msg,
        type: 'information',
        dismissQueue: true,
        layout: 'topCenter',
        theme: 'defaultTheme',
        timeout: 8000,
        closeWith: ['button'],
        textAlign:'left'
        
    };
    switch (state) {
        case CommonJS.State.SUCCESS:
            msgData.type = 'success';
            break;
        case CommonJS.State.ERROR:
            msgData.type = 'error';
            break;
        case CommonJS.State.WARNING:
            msgData.type = 'warning';
            break;
        case CommonJS.State.ALERT:
            msgData.type = 'alert';
            break;
        case CommonJS.State.INFO:
            msgData.type = 'information';
            break;

    }
    var n = noty(msgData);
}
CommonJS.showMessage = showMessage;

CommonJS.OnSuccessComment = function (e)
{
    $('#spinner').hide();
     if(e.Success) {
         if (e.Message != undefined && e.Message != "" && e.Message != null) {
             CommonJS.showMessage(CommonJS.State.SUCCESS, e.Message);
         }
         if (e.ReturnUrl != null) {
           window.location.assign(e.ReturnUrl);
        }
        return;
    }
    CommonJS.showMessage(CommonJS.State.ERROR, e.Message);
}

CommonJS.OnFailure = function () {
    $('#spinner').hide();
    CommonJS.showMessage(FestPlannerHelper.State.ERROR, "Try after some time.");
};




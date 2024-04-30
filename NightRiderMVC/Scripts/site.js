/**
 * Created at 2024-04-06
 * @author Michael Springer
 * @summary script for handling general js across the site
 * UPDATED: 2024-04-27: alert for account registration,
 *      modal for profile update
 *      modal for account delete
 */
"use strict";

$(document).ready(function () {

    /**
     * MODAL AND ALERT HANDLING SECTION
     */

    // cookie notice
    showCookieNoticeForSession();

    // account already registered notice
    showAccountAlreadyRegisteredNotice();

    // client profile update notice
    showProfileUpdateNotice();

    showDeleteAccountNotice();

})


/**
 * Shows the cooke notice modal on the home index view at the beginning of a session
 */
function showCookieNoticeForSession() {
    var showCookieNotice = $("[data-show-cookie-notice").data("show-cookie-notice");
    if (showCookieNotice) {
        $('#mdl-cookie').modal('show');
    }
}

/**
 * Shows the account already registered notice modal on the register view
  */
function showAccountAlreadyRegisteredNotice() {
    var registrationMessage = $('#modal-message').text();
    if (registrationMessage != '' && registrationMessage != null) {
        $('#mdl-register-error').modal('show');
    }
}

function showProfileUpdateNotice() {
    var profileUpdateMessage = $('#mdl-update-result').text();
    if (profileUpdateMessage != '' && profileUpdateMessage != null) {
        $('#mdl-profile-update').modal('show');
    }
}

function showDeleteAccountNotice() {
    $('#btn-client-delete').on('click', function () {
        $('#mdl-profile-delete').modal('show');
    });

}
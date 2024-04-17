/**
 * Created at 2024-04-06
 * @author Michael Springer
 * @summary script for handling general js across the site
 * 
 */
"use strict";

$(document).ready(function () {

    /**
     * MODAL HANDLING SECTION
     */

    // cookie notice
    showCookieNoticeForSession();

    




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
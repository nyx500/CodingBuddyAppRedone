﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Attribution: https://stackoverflow.com/questions/15944559/form-submit-using-jquery-and-mvc

// Store the toggle button on the navbar
const toggleButton = document.getElementById("toggle-button");
// Store the lists of links inside the navbar
const mainNavList = document.getElementById("navbar-list");
const loginList = document.getElementById("login-list");

// Add an event listener to the toggle (tri-bar) menu button that toggles the link visibility on and off
toggleButton.addEventListener("click", () => {
    mainNavList.classList.toggle("active");
    loginList.classList.toggle("active");
})


// Attribution: https://bbbootstrap.com/snippets/multi-step-form-wizard-30467045
$(document).ready(function () {

    var current_fs, next_fs, previous_fs; //fieldsets
    var opacity;
    // Regex for SlackID --> alphanumeric chars only + underscore
    var usernameRegex = /^[A-Za-z0-9_]+/;
    var firstPageErrors;
    var secondPageErrors;
    var thirdPageErrors;
    var slackIdInputField;
    var slackIdValue;
    var usernameInputField;
    var usernameValue;
    var passwordInputField;
    var passwordValue;
    var confirmPasswordInputField;
    var confirmPasswordValue;


    $("#first").click(function () {

        // CLIENT-SIDE VALIDATION FOR ALL THE INPUT FIELDS ON THE FIRST PAGE (to put in separate functions later)
        firstPageErrors = 0;

        // Get slackId input value and check for validity (slackId client-side validation)
        slackIdInputField = $("#slackId");
        slackIdValue = $("#slackId").val();
        usernameInputField = $("#username-input");
        usernameValue = $("#username-input").val();
        passwordInputField = $("#password-input");
        passwordValue = $("#password-input").val();
        confirmPasswordInputField = $("#confirm-password-input");
        confirmPasswordValue = $("#confirm-password-input").val();

        // Highlight SlackId input field in red by adding error CSS class if invalid (not alphanumeric
        // or does not contain at least one digit, or is less than 5 characters), and do not
        // continue on the form
        // Regex for SlackID --> alphanumeric chars only
        if (slackIdValue.length < 5 || slackIdValue.length > 50 || !slackIdValue.match(/^[0-9a-zA-Z]+$/) || !/\d/.test(slackIdValue)) {
            // Add error CSS class to SlackId input field
            firstPageErrors += 1;
            slackIdInputField.addClass("invalid-input");
        }
        else {
            if (slackIdInputField.hasClass("invalid-input")) {
                slackIdInputField.removeClass("invalid-input");
            }
        }

        // Validate username
        if (usernameValue.length < 6 || !usernameValue.match(/^[0-9a-zA-Z]+$/) || usernameValue > 70) {
            firstPageErrors += 1;
            usernameInputField.addClass("invalid-input");

        }
        else {
            if (usernameInputField.hasClass("invalid-input")) {
                usernameInputField.removeClass("invalid-input");
            }
        }

        // Validate password
        if (passwordValue.length < 10) {
            firstPageErrors += 1;
            passwordInputField.addClass("invalid-input");
        }
        else {
            if (passwordInputField.hasClass("invalid-input")) {
                passwordInputField.removeClass("invalid-input");
            }
        }

        // Validation of passwords matching
        if (passwordValue != confirmPasswordValue) {
            firstPageErrors += 1;
            confirmPasswordInputField.addClass("invalid-input");
        }
        else {
            if (confirmPasswordInputField.hasClass("invalid-input")) {
                confirmPasswordInputField.removeClass("invalid-input");
            }
        }


        if (firstPageErrors == 0) {

            console.log(firstPageErrors);
            current_fs = $(this).parent();
            next_fs = $(this).parent().next();

            //Add Class Active
            $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");
            //show the next fieldset
            next_fs.show();

            //hide the current fieldset with style
            current_fs.animate({ opacity: 0 }, {
                step: function (now) {
                    // for making fielset appear animation
                    opacity = 1 - now;

                    current_fs.css({
                        'display': 'none',
                        'position': 'relative'
                    });
                    next_fs.css({ 'opacity': opacity });
                },
                duration: 600
            });
        }
    });


    $("#second").click(function () {

        secondPageErrors = 0;

        // Validate career-phase selected
        if ($("#career-phases-dropdown").val() === "") {
            secondPageErrors += 1;
            $("#career-phases-dropdown").addClass("invalid-input");
        }
        else {
            if ($("#career-phases-dropdown").hasClass("invalid-input")) {
                $("#career-phases-dropdown").removeClass("invalid-input");
            }
        }

        //Validate experience-level selected
        if ($("#experience-levels-dropdown").val() === "") {
            secondPageErrors += 1;
            $("#experience-levels-dropdown").addClass("invalid-input");
        }
        else {
            if ($("#experience-levels-dropdown").hasClass("invalid-input")) {
                $("#experience-levels-dropdown").removeClass("invalid-input");
            }
        }

        if (secondPageErrors == 0) {
            current_fs = $(this).parent();
            next_fs = $(this).parent().next();

            //Add Class Active
            $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");
            //show the next fieldset
            next_fs.show();

            //hide the current fieldset with style
            current_fs.animate({ opacity: 0 }, {
                step: function (now) {
                    // for making fielset appear animation
                    opacity = 1 - now;

                    current_fs.css({
                        'display': 'none',
                        'position': 'relative'
                    });
                    next_fs.css({ 'opacity': opacity });
                },
                duration: 600
            });
        }
    }
    )

    $("#confirmation").click(function () {

        thirdPageErrors = 0;

        var progLangCount = 0;

        // Attribution: https://stackoverflow.com/questions/4735342/jquery-to-loop-through-elements-with-the-same-class
        $(".prog-lang-checkbox").each(function (i, obj) {
            // Attribution: https://stackoverflow.com/questions/901712/how-do-i-check-whether-a-checkbox-is-checked-in-jquery
            if (obj.checked) {
                progLangCount++;
            }
        });

        if (progLangCount < 1) {
            $("#progLangsLabel").addClass("invalid-input");
            thirdPageErrors++;
        }
        else {
            $("#progLangsLabel").removeClass("invalid-input");
        }
    });


    $(".previous").click(function () {

        current_fs = $(this).parent();
        previous_fs = $(this).parent().prev();

        //Remove class active
        $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

        //show the previous fieldset
        previous_fs.show();

        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                previous_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });
    });
});

    //    $("#second").click(function () {
    //        secondPageErrors = 0;
    //        // Validate career-phase selected
    //        if ($("#career-phases-dropdown").val() === "") {
    //            secondPageErrors += 1;
    //            $("#career-phases-dropdown").addClass("invalid-input");
    //        }
    //        else {
    //            if ($("#career-phases-dropdown").hasClass("invalid-input")) {
    //                $("#career-phases-dropdown").removeClass("invalid-input");
    //            }
    //        }

    //        // Validate experience-level selected
    //        if ($("#experience-levels-dropdown").val() === "") {
    //            secondPageErrors += 1;
    //            $("#experience-levels-dropdown").addClass("invalid-input");
    //        }
    //        else {
    //            if ($("#experience-levels-dropdown").hasClass("invalid-input")) {
    //                $("#experience-levels-dropdown").removeClass("invalid-input");
    //            }
    //        }

    //        if (secondPageErrors == 0) {
    //            current_fs = $(this).parent();
    //            next_fs = $(this).parent().next();

    //            //Add Class Active
    //            $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");
    //            //show the next fieldset
    //            next_fs.show();

    //            //hide the current fieldset with style
    //            current_fs.animate({ opacity: 0 }, {
    //                step: function (now) {
    //                    // for making fielset appear animation
    //                    opacity = 1 - now;

    //                    current_fs.css({
    //                        'display': 'none',
    //                        'position': 'relative'
    //                    });
    //                    next_fs.css({ 'opacity': opacity });
    //                },
    //                duration: 600
    //            });
    //        }

    //    }
    //    else {
    //        current_fs = $(this).parent();
    //        next_fs = $(this).parent().next();

    //        //Add Class Active
    //        $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");
    //        //show the next fieldset
    //        next_fs.show();

    //        //hide the current fieldset with style
    //        current_fs.animate({ opacity: 0 }, {
    //            step: function (now) {
    //                // for making fielset appear animation
    //                opacity = 1 - now;

    //                current_fs.css({
    //                    'display': 'none',
    //                    'position': 'relative'
    //                });
    //                next_fs.css({ 'opacity': opacity });
    //            },
    //            duration: 600
    //        });
    //    }
    //});


    //$(".submit").click(function () {
    //    return false;
    //})

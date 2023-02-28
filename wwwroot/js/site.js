// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Attribution: https://stackoverflow.com/questions/15944559/form-submit-using-jquery-and-mvc


// Counts the errors for registration form to decide whether to move forwards to next step
var firstPageErrors;
var secondPageErrors;
var thirdPageErrors;

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

    // Hides form welcome screen on Find-a-Buddy View when the Start button is pressed
    hideWelcomeScreen();
    // On Find-a-Buddy form: go to next page when next-buttons are clicked
    goToNextPageOnFindABuddyForm($("#find-buddy-first"));
    goToNextPageOnFindABuddyForm($("#find-buddy-second"));
    goToNextPageOnFindABuddyForm($("#find-buddy-third"));
    // Functions to like/pass users when browsing through filter results (potential matches)
    likeUsers();
    passUsers();
    // Calls functions to like/unlike user when actually viewing their profile page
    toggleLikeUnlikeOnProfile();
    // On Matches page, remove a match
    matchPageRemoveUser();
    // On Matches page, like a user who liked you
    matchPageLikeUser();
    // Go to the last-visited panel
    getTheRightPanel();

    var current_fs, next_fs, previous_fs; //fieldsets
    var opacity;
    // Regex for SlackID --> alphanumeric chars only + underscore
    var usernameRegex = /^[a-zA-Z0-9_]+$/;
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

            $("#client-side-error-slack-id").css("display", "block");
            $("#slack-id-label").css("display", "none");
        }
        else {

            if (slackIdInputField.hasClass("invalid-input")) {
                slackIdInputField.removeClass("invalid-input");
            }

            $("#client-side-error-slack-id").css("display", "none");
            $("#slack-id-label").css("display", "block");

            var slackIdNotAvailable = CheckIfSlackIdAvailable(slackIdValue);
            if (slackIdNotAvailable) {
                firstPageErrors++;
            }
        }


        // Validate username
        if (usernameValue.length < 6 || !usernameValue.match(usernameRegex) || usernameValue > 70) {
            firstPageErrors += 1;
            usernameInputField.addClass("invalid-input");


            $("#client-side-error-username").css("display", "block");
            $("#username-label").css("display", "none");

        }
        else {
            if (usernameInputField.hasClass("invalid-input")) {
                usernameInputField.removeClass("invalid-input");
            }

            $("#client-side-error-username").css("display", "none");
            $("#username-label").css("display", "block");


            var usernameNotAvailable = (CheckIfUsernameAvailable(usernameValue));

            if (usernameNotAvailable) {
                firstPageErrors++;
            }

        }



        // Validate password
        if (passwordValue.length < 10) {
            firstPageErrors += 1;
            passwordInputField.addClass("invalid-input");
            $("#client-side-error-password").css("display", "block");
            $("#password-label").css("display", "none");
        }
        else {
            if (passwordInputField.hasClass("invalid-input")) {
                passwordInputField.removeClass("invalid-input");
            }
            $("#client-side-error-password").css("display", "none");
            $("#password-label").css("display", "block");
        }

        // Validation of passwords matching
        if (passwordValue.length > 10 && passwordValue != confirmPasswordValue) {
            firstPageErrors += 1;
            confirmPasswordInputField.addClass("invalid-input");
            $("#client-side-error-confirm-password").css("display", "block");
            $("#confirm-password-label").css("display", "none");
        }
        else {
            if (confirmPasswordInputField.hasClass("invalid-input")) {
                confirmPasswordInputField.removeClass("invalid-input");
            }
            $("#client-side-error-confirm-password").css("display", "none");
            $("#confirm-password-label").css("display", "block");
        }

        // Only let user continue if there are no errors
        if (firstPageErrors == 0)
        {

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
            $("#career-phase-label").css("color", "red");
        }
        else {
            if ($("#career-phases-dropdown").hasClass("invalid-input")) {
                $("#career-phases-dropdown").removeClass("invalid-input");
            }
            $("#career-phase-label").css("color", "black");
        }

        //Validate experience-level selected
        if ($("#experience-levels-dropdown").val() === "") {
            secondPageErrors += 1;
            $("#experience-levels-dropdown").addClass("invalid-input");
            $("#experience-level-label").css("color", "red");
        }
        else {
            if ($("#experience-levels-dropdown").hasClass("invalid-input")) {
                $("#experience-levels-dropdown").removeClass("invalid-input");
            }
            $("#experience-level-label").css("color", "black");
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
            $("#progLangsLabel").css("color", "red");
            thirdPageErrors++;
        }
        else {
            $("#progLangsLabel").removeClass("invalid-input");
            $("#progLangsLabel").css("color", "black");
        }


        var csInterestCount = 0;

        $(".cs-interest-checkbox").each(function (i, obj) {
            if (obj.checked) {
                csInterestCount++;
            }
        });

        if (csInterestCount < 1) {
            $("#csInterestsLabel").addClass("invalid-input");
            $("#csInterestsLabel").css("color", "red");
            thirdPageErrors++;
        }
        else {
            $("#csInterestsLabel").removeClass("invalid-input");
            $("#csInterestsLabel").css("color", "black");
        }


        var hobbyCount = 0;
        $(".hobby-checkbox").each(function (i, obj) {
           
            if (obj.checked) {
                hobbyCount++;
            }
        });

        if (hobbyCount < 3 || hobbyCount > 10) {
            $("#hobbiesLabel").addClass("invalid-input");
            $("#hobbiesLabel").css("color", "red");
            thirdPageErrors++;
        }
        else {
            $("#hobbiesLabel").removeClass("invalid-input");
            $("#hobbiesLabel").css("color", "black");
        }

        // If there are no errors, print Success message in JS console
        if (thirdPageErrors == 0) {
            $("#registration-form").submit();
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



// Sends the username using Ajax to the CheckUsername method in the ActionController
// The response is 'false' if username already exists in database and 'true' otherwise
function CheckIfUsernameAvailable(usernameValue) {

    var username_data = { username: usernameValue };

    var result = false;

    // Why it was necessary to use "async:false" which is generally BAD practice...
    // There is no other way to block the user from continuing to the next page unless the firstPageErrors
    // variable is incremented --> it is impossible to increment it for the form to validate properly
    // umless async is set to false.
    // Attribution: "Below is one case where one have to set async to false, for the code to work properly." on https://stackoverflow.com/questions/1478295/what-does-async-false-do-in-jquery-ajax
    $.ajax({
        type: "POST",
        url: "/Account/CheckUsername",
        data: username_data,
        async: false, 
        success: function (response) {
            // Response from controller is 'true': username is taken already
            if (response) {
                // Display the 'username already taken' error-message in the view
                $("#client-side-error-username-taken").css("display", "block");
                result = true;
            }
            // Response from controller is 'false': can accept the username
            else {
                // Hide the 'username already taken' error-message in the view
                $("#client-side-error-username-taken").css("display", "none");
            }
        }
    });

    return result;
};

    
// Same logic for SlackId
function CheckIfSlackIdAvailable(slackIdValue) {

    var slackId_data = { slackId: slackIdValue };

    var result = false;


    $.ajax({
        type: "POST",
        url: "/Account/CheckSlackId",
        data: slackId_data,
        async: false, 
        success: function (response) {
            // Response from controller is 'true': can accept this username because it is unique
            if (response) {
                // Hide the 'username already taken' error-message in the view
                $("#client-side-error-slack_id-taken").css("display", "block");
                result = true;
            }
            // Response from controller is 'false': can accept the username
            else {
                // Display the 'username already taken' error-message in the view
                $("#client-side-error-slack_id-taken").css("display", "none");
            }
        }
    });

    return result;
}

function hideWelcomeScreen() {
    // Hide the welcome screen and display the form when start button is clicked on the "Find-a-Buddy" view
    $("#start-matching").click(function () {
        $("#find-a-buddy-welcome-page-container").css("display", "none");
        // Display the hidden form
        $("#find-a-buddy-form-container").removeClass("hidden");
    })
}

function goToNextPageOnFindABuddyForm(nextButton) {
    nextButton.click(function () {

        console.log('clicked next');
        current_fs = $(this).parent();
        next_fs = $(this).parent().next();

        //Add Class Active
        $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");
        console.log(current_fs);

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
    });
}

function likeUsers() {
    $(".like-button").click(function () {
        // post user id from hidden input field before the button
        var user_data = { id: $(this).prev().val() };

        // get container id
        var divId = $(this).next().val();
        divId = "#" + divId;

        $.ajax({
            type: "POST",
            url: "/Matches/LikeUser",
            data: user_data,
            success: function (response) {
                if (response) {
                    console.log("like: worked");
                    // Show the next match
                    if ($(divId).next().attr("id") != undefined && $(divId).next().attr("id").includes("container")) {
                        $(divId).next().addClass("potential-match-container");
                        $(divId).next().removeClass("hidden");
                    }
                    else {
                        $("#no-matches-left").removeClass("hidden");
                    }
                    // Remove the match
                    $(divId).remove();
                }
                else {
                    console.log("like: didn't work");
                }
            }
        });

    });
}


function passUsers() {
    $(".pass-button").click(function () {

        // post user id from hidden input field before the button
        var user_data = { id: $(this).prev().val() };

        // get container id
        var divId = $(this).next().val();
        divId = "#" + divId;

        $.ajax({
            type: "POST",
            url: "/Matches/PassUser",
            data: user_data,
            success: function (response) {
                if (response) {
                    console.log("rejection: worked");
                    // Show the next match
                    if ($(divId).next().attr("id") != undefined && $(divId).next().attr("id").includes("container")) {
                        $(divId).next().addClass("potential-match-container");
                        $(divId).next().removeClass("hidden");
                    }
                    else {
                        $("#no-matches-left").removeClass("hidden");
                    }
                    // Remove the match
                    $(divId).remove();
                }
                else {
                    console.log("rejection: didn't work");
                }
            }
        });
    });
}

// Allows logged-in user to like/unlike a user when viewing their profile page
function toggleLikeUnlikeOnProfile() {

    var likeToggleButton = $("#view-profile-like-user-button");
    var likeToggleIcon = $("#like-icon");

    $(likeToggleButton).click(function () {
        console.log("clicked LIKE user");


        // Data from hidden field (target user's unique ID)
        // get container id
        var targetId = $("#hidden-user-id").val();
        console.log("User Id: " + targetId);

        // If button is blue, means logged-in user likes the viewed user but wants to "unlike"
        if (likeToggleIcon.hasClass("fas")) {
            console.log("Heart icon is solid");

            // Send request to Controller to unlike the user
            unlikeUserOnTheirProfile(likeToggleIcon, targetId);
        }
        // If button is black, means logged-in user does not like the viewed user but wants to "like"
        else {
            console.log("button is just an outline");
            // Send request to Controller to like the user
            likeUserOnTheirProfile(likeToggleIcon, targetId);
        }
    });
}


function likeUserOnTheirProfile(likeToggleIcon, targetUserId) {

    var user_data = { id: targetUserId };

    $.ajax({
            type: "POST",
            url: "/Matches/LikeUser",
            data: user_data,
            success: function (response) {
                if (response) {
                    console.log("like: it worked!");

                    // Change the heart colour from black to blue if the user managed to like the viewed user
                    likeToggleIcon.removeClass("far");
                    likeToggleIcon.addClass("fas");
                    // Change the text from Like to Unlike
                    $("#like-unlike-text").text("Unlike");
                }
                else {
                    // Display error message
                    $("#could-not-like-error").removeClass("hidden");
                }
            }
        });
}

function unlikeUserOnTheirProfile(likeToggleIcon, targetUserId) {

    var user_data = { id: targetUserId };

    $.ajax({
        type: "POST",
        url: "/Matches/UnlikeUser",
        data: user_data,
        success: function (response) {
            if (response) {
                console.log("unlike: it worked!");

                // Change the heart colour from blue to black if the user managed to unlike the viewed user
                likeToggleIcon.removeClass("fas");
                likeToggleIcon.addClass("far");

                // Change the text from Unlike to Like
                $("#like-unlike-text").text("Like");
            }
            else {
                // Display error message
                $("#could-not-like-error").removeClass("hidden");
            }
        }
    });
}


function matchPageRemoveUser() {
    $(".remove-button").each(function () {
        $(this).on("click", function (e) {

            var user_data = { id: $(this).prev().val() };

            var panelNumber = $(this).next().val();

            $.ajax({
                type: "POST",
                url: "/Matches/UnlikeUser",
                data: user_data,
                success: function (response) {
                    if (response) {

                        setPanelSession(panelNumber);

                        window.location.replace("/Matches/Matches");       
                    }
                }
            });
        });
    });
}

function matchPageLikeUser() {
    $(".heart-button").each(function () {
        $(this).on("click", function (e) {

            var user_data = { id: $(this).prev().val() };

            var panelNumber = $(this).next().val();

            $.ajax({
                type: "POST",
                url: "/Matches/LikeUser",
                data: user_data,
                success: function (response) {
                    if (response) {

                        window.location.replace("/Matches/Matches");
                    }
                }
            });
        });
    });
}

// Uses session storage to redirect to the correct panel when the page is reloaded
function setPanelSession(panelNumber) {

    if (panelNumber == "panel-one") {
        sessionStorage.setItem("matches-panel", 1);
    } else if (panelNumber == "panel-two") {
        sessionStorage.setItem("matches-panel", 2);
    } else {
        sessionStorage.setItem("matches-panel", 3);
    }
    sessionStorage.setItem("redirectToMatchesPage", true);
}

// Makes the correct panel visible
function getTheRightPanel() {
    // Useful hint on how to check location:
    // Attribution: https://linuxhint.com/check-if-current-url-contains-string-javascript/#:~:text=Conclusion-,To%20check%20if%20the%20current%20URL%20contains%20a%20string%20in,value%20in%20the%20specified%20string.
    if (window.location.href.indexOf("Matches/Matches") > -1)
    {
        if (sessionStorage.getItem("redirectToMatchesPage"))
        {
            var panelNum =sessionStorage.getItem("matches-panel")

            if (panelNum == 1) {
                $("#radio-one").attr('checked', 'checked');
            }
            else if (panelNum == 2) {
                $("#radio-two").attr('checked', 'checked');
            }
            else {
                $("#radio-three").attr('checked', 'checked');
            }


            sessionStorage.setItem("redirectToMatchesPage", false);

        }
    }

}
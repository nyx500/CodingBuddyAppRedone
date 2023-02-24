// Start code running when DOM is fully-loaded:
$(document).ready(function () {

    // Profile picture uploading process...
    var profilePictureInput = $("#profile-picture-input");

    profilePictureInput.change(function (event) {

        // event.preventDefault() prevents empty form from being submitted!
        event.preventDefault();
        uploadImageFile(profilePictureInput);
    });

    updateUsernameFunctionality();
    updateBioFunctionality();
    updateCareerPhaseFunctionality();
        
});

function uploadImageFile(inputId) {


    var formData = new FormData();
    formData.append("file", $("#profile-picture-input")[0].files[0]);

    $.ajax({
        url: "/Account/UploadPicture",
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            if (result.success) {
                window.location.replace("/account/editprofile");
            }
            else {
                alert(result.error);
            }
        }
    });

}


function updateUsernameFunctionality() {
    // Edit Username stuff
    var editUsernameButton = $("#edit-username");
    // Display Edit Username form when button is clicked
    editUsernameButton.click(function () {
        // Remove hidden class for form when corresponding button clicked
        if ($("#edit-username-form").hasClass("hidden")) {
            $("#edit-username-form").removeClass("hidden");
        }

        if (!$("#username-title").hasClass("hidden")) {
            $("#username-title").addClass("hidden");
        }

        // Hide the button (submit button replaces it)
        editUsernameButton.addClass("hidden");
    });

    // Close username form if cancelled
    var cancelUsernameInputButton = $("#cancel-update-username");
    cancelUsernameInputButton.click(function () {
        $('#edit-username-form').addClass("hidden");
        $("#username-title").removeClass("hidden")
    });

    var saveUsernameButton = $("#save-username-input-button");
    var updateNewUsername = false;
    saveUsernameButton.click(function (event) {
        event.preventDefault();
        var usernameInput = $("#edit-username-input");
        // Validate username input
        // Check length
        if (usernameInput.val().length < 6 || usernameInput.val().length > 70) {
            $("#username-input-label").text("Invalid: Bad length!");
            $("#username-input-label").css("color", "red");
        }
        // Check chars
        else if (!usernameInput.val().match(/^[a-zA-Z0-9_]+$/)) {
            $("#username-input-label").text("Invalid characters!");
            $("#username-input-label").css("color", "red");
        }
        // Check if username is available
        else {
            var username_data = { username: usernameInput.val() };
            $.ajax({
                type: "POST",
                url: "/Account/CheckUsername",
                data: username_data,
                async: false,
                success: function (response) {
                    // Response from controller is 'true': username is taken already
                    if (response) {
                        // Display the 'username already taken' error-message in the view
                        $("#username-input-label").text("Username taken!");
                        $("#username-input-label").css("color", "red");
                    }
                    else {
                        updateNewUsername = true;
                    }
                }
            });

            // Async AJAX request if username available
            if (updateNewUsername) {
                var username_data = { newUsername: usernameInput.val() };
                $.ajax({
                    type: "POST",
                    url: "/Account/UpdateUsername",
                    data: username_data,
                    success: function (response) {
                        // Response from controller is 'true': have to login again
                        if (response == "updated") {
                            window.location.replace("/account/EditProfile");
                        }
                        else {
                            window.location.replace("/account/EditProfile");
                        }
                    }
                });

            }
        }
    });
}

function updateBioFunctionality() {
    var editBioButton = $("#update-bio-button");

    // Display Edit Bio form when button is clicked
    editBioButton.click(function () {
        // Remove the hidden class for the form when corresponding button clicked
        if ($("#edit-bio-form").hasClass("hidden")) {
            $("#edit-bio-form").removeClass("hidden");
            $("#bio-buttons").removeClass("hidden");
        }

        // Place cursor on top left-corner of textarea input
        // Attribution: https://stackoverflow.com/questions/17158802/using-jquery-selector-and-setselectionrange-is-not-a-function
        var textarea = $('#edit-bio-input')[0];
        $('#edit-bio-input').focus(function () {
            textarea.setSelectionRange(0, 0);
        });
        textarea.focus();

        if (!$("#bio-section").hasClass("hidden")) {
            $("#bio-section").addClass("hidden");
        }
    });


    var saveButton = $("#save-bio-button");

    saveButton.click(function () {
        console.log($("#edit-bio-input").val().length);
        if ($("#edit-bio-input").val().length > 500) {
            $("#bio-input-label").text("Invalid: Too long!");
            $("#bio-input-label").css("color", "red");
        }
        // Bio is valid --> upload with Ajax request
        else {
            var bio_data = { bio: $("#edit-bio-input").val() };
            $.ajax({
                type: "POST",
                url: "/Account/UpdateBio",
                data: bio_data,
                success: function (response) {
                    if (response == "updated") {
                        window.location.replace("/account/EditProfile");
                    }
                    else {
                        $("#bio-input-label").text("An error occurred when trying to update the database.");
                        $("#bio-input-label").css("color", "red");
                    }
                }
            });
}
    });


    //Close the Bio form if cancel button is clicked
    var cancelBioInputButton = $("#cancel-bio-button");

    cancelBioInputButton.click(function () {
        $('#edit-bio-form').addClass("hidden");
        $('#bio-buttons').addClass("hidden");
        $("#bio-section").removeClass("hidden")
    });
}

function updateCareerPhaseFunctionality() {

    var editButton = $("#edit-career-button");

    editButton.click(function () {
        // For some reason, adding class called "hidden" to
        // element with Id "career-dropdown" doesn't work,
        // so act directly on CSS here:
        $("#career-dropdown").css("display", "block");
        // hide the data + edit button
        $("#career-phase-wrapper").addClass("hidden");
    })

    var cancelButton = $("#cancel-career-button");
    cancelButton.click(function () {
        console.log('clicked');
        // Hide the dropdown
        $("#career-dropdown").css("display", "none");
        // Show the data + edit button again
        $("#career-phase-wrapper").removeClass("hidden");
    });

    var saveButton = $("#save-career-button");
    saveButton.click(function () {
        // Validate career-phase selected
        if ($("#career-phases-dropdown").val() === "") {
            console.log('invalid input');
        }
        // If career-phase IS selected, do Ajax call to Controller
        else {
            var career_phase_data = { id: $("#career-phases-dropdown").val() };
            $.ajax({
                type: "POST",
                url: "/Account/UpdateCareerPhase",
                data: career_phase_data,
                success: function (response) {
                    if (response == "updated") {
                        window.location.replace("/account/EditProfile");
                    }
                    else {
                        $("#career-error-label").text("An error occurred when trying to update the database.");
                        $("#career-error-label").css("color", "red");
                    }
                }
            });
        }
    })
}
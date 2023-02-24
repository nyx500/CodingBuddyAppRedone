// Start code running when DOM is fully-loaded:
$(document).ready(function () {

    // Profile picture uploading process...
    var profilePictureInput = $("#profile-picture-input");

    profilePictureInput.change(function (event) {

        // event.preventDefault() prevents empty form from being submitted!
        event.preventDefault();
        uploadImageFile(profilePictureInput);
    });


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


function validateUsername(usernameValue) {
    var username_data = { username: usernameValue };

    // Upload the client-side validated username to DB
    $.ajax({
        type: "POST",
        url: "/Account/UpdateUsername",
        data: username_data,
        async: false,
        success: function (response) {
            // Response from controller is 'true': username is taken already
            if (response == "updated") {
                // Reload the page to display new username
                window.location.replace("/account/editprofile");
            }
        }
    });
}
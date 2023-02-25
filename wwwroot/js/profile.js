// Start code running when DOM is fully-loaded:
$(document).ready(function () {

    // NB - CLEAN THIS CODE UP WITH A SWITCH STATEMENT LATER IF TIME!!!!s
    // If document is reloading after a save, go to the last-seen panel
    if (sessionStorage.panel == 2)
    {
    
        if (sessionStorage.redirect) {

            $("#radio-two").attr('checked', 'checked');

            sessionStorage.setItem("redirect", false);
            sessionStorage.setItem("panel", 1);
        }
    };
    // If document is reloading after a save, go to the last-seen panel
    if (sessionStorage.panel == 3) {

        if (sessionStorage.redirect) {

            $("#radio-three").attr('checked', 'checked');

            sessionStorage.setItem("redirect", false);
            sessionStorage.setItem("panel", 1);
        }
    };
    // If document is reloading after a save, go to the last-seen panel
    if (sessionStorage.panel == 4) {

        if (sessionStorage.redirect) {

            $("#radio-four").attr('checked', 'checked');

            sessionStorage.setItem("redirect", false);
            sessionStorage.setItem("panel", 1);
        }
    };

    $(".lang-checkbox").each(function () {
        $(this).on("change", function (e) {
            if ($(this).is(":checked"))
                console.log("true");
            else
                console.log("false");
        })
    });


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
    updateExperienceLevelFunctionality();
    updateLanguages();
    updateProgrammingLanguages();
    updateCSInterests();
    updateHobbies();
    generateRandomQuestion();
    uploadAnswer();
    deleteQuestion();
    editQuestion();
        
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
        // Hide the dropdown
        $("#career-dropdown").css("display", "none");
        // Show the data + edit button again
        $("#career-phase-wrapper").removeClass("hidden");
    });

    var saveButton = $("#save-career-button");
    saveButton.click(function () {
        // Validate career-phase selected
        if ($("#career-phases-dropdown").val() === "") {
            $("#career-error-label").text("Please select a choice!");
            $("#career-error-label").text("Please select a choice!").css("color", "red");
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

function updateExperienceLevelFunctionality() {

    var editButton = $("#edit-experience-level");

    editButton.click(function () {
        // For some reason, adding class called "hidden" to
        // element with Id "career-dropdown" doesn't work,
        // so act directly on CSS here:
        $("#experience-dropdown").css("display", "block");
        // hide the data + edit button
        $("#experience-level-wrapper").addClass("hidden");
    })

    var cancelButton = $("#cancel-experience-button");
    cancelButton.click(function () {
        // Hide the dropdown
        $("#experience-dropdown").css("display", "none");
        // Show the data + edit button again
        $("#experience-level-wrapper").removeClass("hidden");
    });

    var saveButton = $("#save-experience-button");
    saveButton.click(function () {
        // Validate exp-level is selected
        if ($("#experience-levels-dropdown").val() === "") {
            $("#experience-error-label").text("Please select a choice!");
            $("#experience-error-label").text("Please select a choice!").css("color", "red");
        }
        // If exp-level IS selected, do Ajax call to Controller
        else {
            var experience_level_data = { id: $("#experience-levels-dropdown").val() };
            $.ajax({
                type: "POST",
                url: "/Account/UpdateExperienceLevel",
                data: experience_level_data,
                success: function (response) {
                    if (response == "updated") {
                        window.location.replace("/account/EditProfile");
                    }
                    // Did not manage to update: display error above dropdown in red
                    else {
                        $("#experience-error-label").text("An error occurred when trying to update the database.");
                        $("#experience-error-label").css("color", "red");
                    }
                }
            });
        }
    })
}

function updateLanguages() {

    // Toggle data storing languages spoken and the editing form for the languages
    $("#edit-languages-spoken").click(function () {
        $("#language-selection-container").css("display", "flex");

        $("#languages-wrapper").css("display", "none");
    })

    $("#cancel-lang-button").click(function () {
        $("#language-selection-container").css("display", "none");
        $("#languages-wrapper").css("display", "flex");
    })

    $("#save-lang-button").click(function () {

        var selectedIds = [];

        $(".edit-lang-checkbox").each(function (i, obj) {
            // Attribution: https://stackoverflow.com/questions/901712/how-do-i-check-whether-a-checkbox-is-checked-in-jquery
            if (obj.checked) {
                selectedIds.push(i + 1);
            }
        });

        // Do Ajax Upload
        var langData = { ids: selectedIds };
        
        $.ajax({
            type: "POST",
            url: "/Account/UpdateLanguages",
            data: langData,
            success: function (response) {
                if (response== "updated") {
                    window.location.replace("/account/EditProfile");
                }
                // Did not manage to update: display error above dropdown in red
                else {
                    $("#lang-label").text("An error occurred when trying to update the database.");
                    $("#lang-label").css("color", "red");
                }
            }
        });
    })
}


function updateProgrammingLanguages() {

    // Toggle data storing languages spoken and the editing form for the languages
    $("#edit-programming-languages").click(function () {

        $("#programming-language-selection-container").css("display", "flex");
        $("#programming-language-selection-container").removeClass("hidden");
        $("#programming-languages-data").addClass("hidden");
        
    })

    $("#cancel-programming-lang-button").click(function () {
        $("#programming-language-selection-container").css("display", "none");
        $("#programming-language-selection-container").addClass("hidden");
        $("#programming-languages-data").removeClass("hidden");
    })

    $("#save-programming-lang-button").click(function () {

        var selectedIds = [];

        $(".edit-programming-lang-checkbox").each(function (i, obj) {
            // Attribution: https://stackoverflow.com/questions/901712/how-do-i-check-whether-a-checkbox-is-checked-in-jquery
            if (obj.checked) {
                selectedIds.push(i + 1);
            }
        });

        if (selectedIds.length < 1) {
            $("#programming-lang-label").text("You have to select at least one programming language!");
            $("#programming-lang-label").addClass("error-options-label");
        }
        else {
            // Do Ajax Upload for new programming languages
            var programmingLangData = { ids: selectedIds };

            // Set session to go to correct tab/panel when the data reloads after updating
            sessionStorage.setItem("panel", 2);
            sessionStorage.setItem("redirect", true);


            $.ajax({
                type: "POST",
                url: "/Account/UpdateProgrammingLanguages",
                data: programmingLangData,
                success: function (response) {
                    if (response == "updated") {
                        window.location.replace("/account/EditProfile");
                    }
                    // Did not manage to update: display error above dropdown in red
                    else {
                        $("#programming-lang-label").text("An error occurred when trying to update the database.");
                        $("#programming-lang-label").addClass("error-options-label");
                    }
                }
            });
        }

        
    })
}

function updateCSInterests() {

    // Toggle data storing languages spoken and the editing form for the languages
    $("#edit-cs-interests").click(function () {

        $("#cs-interest-selection-container").css("display", "flex");
        $("#cs-interest-selection-container").removeClass("hidden");
        $("#cs-interests-data").addClass("hidden");

    })

    $("#cancel-cs-interest-button").click(function () {
        $("#cs-interest-selection-container").css("display", "none");
        $("#cs-interest-selection-container").addClass("hidden");
        $("#cs-interests-data").removeClass("hidden");
    })

    $("#save-cs-interest-button").click(function () {

        var selectedIds = [];

        $(".edit-cs-interest-checkbox").each(function (i, obj) {
           
            if (obj.checked) {
                selectedIds.push(i + 1);
            }
        });

        if (selectedIds.length < 1) {
            $("#cs-interest-label").text("You have to select at least one Computer Science interest!");
            $("#cs-interest-label").addClass("error-options-label");
        }
        else {
            // Do Ajax Upload
            var csInterestData = { ids: selectedIds };

            sessionStorage.setItem("panel", 2);
            sessionStorage.setItem("redirect", true);


            $.ajax({
                type: "POST",
                url: "/Account/UpdateCSInterests",
                data: csInterestData,
                success: function (response) {
                    if (response == "updated") {
                        window.location.replace("/Account/EditProfile");
                    }
                    // Did not manage to update: display error above dropdown in red
                    else {
                        $("#cs-interest-label").text("An error occurred when trying to update the database.");
                        $("#cs-interest-label").addClass("error-options-label");
                    }
                }
            });
        }
       
    })
}

function updateHobbies() {

    // Toggle data storing languages spoken and the editing form for the languages
    $("#edit-hobbies").click(function () {

        $("#hobbies-selection-container").css("display", "flex");
        $("#hobbies-selection-container").removeClass("hidden");
        $("#hobbies-data").addClass("hidden");

    })

    $("#cancel-hobby-button").click(function () {
        $("#hobbies-selection-container").css("display", "none");
        $("#hobbies-selection-container").addClass("hidden");
        $("#hobbies-data").removeClass("hidden");
    })

    $("#save-hobby-button").click(function () {

        var selectedIds = [];

        $(".edit-hobby-checkbox").each(function (i, obj) {

            if (obj.checked) {
                selectedIds.push(i + 1);
            }
        });

        if (selectedIds.length < 3 || selectedIds.length > 10) {
            $("#hobby-label").text("Please select between 3-10 hobbies and interests!");
            $("#hobby-label").addClass("error-options-label");
        }
        else {
            // Do Ajax Upload to update hobbies and interests
            var hobbyData = { ids: selectedIds };

            sessionStorage.setItem("panel", 3);
            sessionStorage.setItem("redirect", true);


            $.ajax({
                type: "POST",
                url: "/Account/UpdateHobbies",
                data: hobbyData,
                success: function (response) {
                    if (response == "updated") {
                        window.location.replace("/Account/EditProfile");
                    }
                    // Did not manage to update: display error above dropdown in red
                    else {
                        $("#hobby-label").text("An error occurred when trying to update the database.");
                        $("#hobby-label").addClass("error-options-label");
                    }
                }
            });
        }
    })
}

function generateRandomQuestion() {
    $("#random-question-generator").click(function () {
        $.ajax({
            type: "GET",
            url: "/Account/GenerateRandomQuestion",
            success: function (response) {
                if (response.output.length > 0) {

                    // Attribution: https://mkyong.com/javascript/how-to-access-json-object-in-javascript/
                    var json = JSON.parse(response.output);
                    var questionId = json["QuestionId"];
                    var questionString = json["QuestionString"];

                    var questionElement = $("#random-question");
                    questionElement.text(questionString);


                    // Show the input field for the answer
                    $("#answer-inputs").removeClass("hidden");

                    var hiddenInput = $("#hidden-input");
                    hiddenInput.val(questionId);

                }
                // Did not manage to update: display error above dropdown in red
                else {
                    console.log('nope');
                }
            }
        });
    });
}

function uploadAnswer() {
    $("#save-answer-button").click(function () {

        // Display and highlight an error msg if the answer is over 200 chars
        if ($("#answer-input").val().length > 250 || $("#answer-input").val().length < 1) {
            $("#error-label-for-answer").addClass("error-options-label");
            $("#error-label-for-answer").text("Answer must be more than 1 and less than 200 chars.");
        }
        else
        {
            var qaData = { id: $("#hidden-input").val(), answer: $("#answer-input").val() };

            sessionStorage.setItem("panel", 4);
            sessionStorage.setItem("redirect", true);

            $.ajax({
                type: "POST",
                url: "/Account/UpdateRandomQuestion",
                data: qaData,
                success: function (response) {
                    if (response == "updated") {
                        console.log('success');
                        console.log(response);
                        window.location.replace("/Account/EditProfile");
                    }
                    // Did not manage to update: display error above dropdown in red
                    else {
                        console.log('fail');
                        console.log(response);
                        $("#error-label-for-answer").addClass("error-options-label");
                        $("#error-label-for-answer").text("An error occurred - could not update database.");
                    }
                },
                error: function () {
                    console.log('ERROR');
                    $("#error-label-for-answer").addClass("error-options-label");
                    $("#error-label-for-answer").text("An error occurred - could not update database.");
                }
            });
        }
    });
}

function deleteQuestion() {
    $(".delete-button").each(function () {
        $(this).on("click", function (e) {

            // Get last number/char of id of the delete button
            var id = $(this).attr('id');
            id = id.charAt(id.length - 1);

            // Concatenate the hidden input corresponding to that delete button
            // Stores the ID of the question-answer block as a value
            var hiddenInputId = "#hidden-" + id;
            var hiddenInput = $(hiddenInputId);

            var errorLabelId = "#error-label-" + id;
            var errorLabel = $(errorLabelId);

            // Get the ID of the question answer block to delete
            var questionAnswerBlockId = hiddenInput.val();

            var qaData = { id: questionAnswerBlockId };

            // Redirect to same panel
            sessionStorage.setItem("panel", 4);
            sessionStorage.setItem("redirect", true);

            $.ajax({
                type: "POST",
                url: "/Account/DeleteRandomQuestion",
                data: qaData,
                success: function (response) {
                    if (response == "updated") {
                        console.log('success');
                        console.log(response);
                        window.location.replace("/Account/EditProfile");
                    }
                    // Did not manage to update: display error above dropdown in red
                    else {
                        console.log('fail');
                        console.log(response);
                    }
                },
                error: function () {    
                    console.log('ERROR');
                }
            });


        })
    })
}

function editQuestion() {

    $(".edit-answer-button").each(function () {
        $(this).on("click", function (e) {

            // Get last number/char of id of the editbutton
            var id = $(this).attr('id');
            id = id.charAt(id.length - 1);

            // Corresponding text input block
            var editInputWrapperId = "#edit-input-wrapper-" + id;
            var editInputWrapper = $(editInputWrapperId);
            editInputWrapper.removeClass("hidden");


            $(".save-answer-button").each(function () {
                $(this).on("click", function (e) {

                    // Get the last number/char/id
                    var id = $(this).attr('id');
                    id = id.charAt(id.length - 1);

                    // Concatenate the hidden input corresponding to that delete button
                    // Stores the ID of the question-answer block as a value
                    var hiddenInputId = "#hidden-" + id;
                    var hiddenInput = $(hiddenInputId);
                    var qaId = hiddenInput.val();

                    var inputId = "#edit-input-" + id;
                    var input = $(inputId);
                    var inputString = input.val();

                    var qaData = { questionAnswerBlockId: qaId, newAnswer: inputString };

                    // Redirect to same panel
                    sessionStorage.setItem("panel", 4);
                    sessionStorage.setItem("redirect", true);


                    $.ajax({
                        type: "POST",
                        url: "/Account/EditRandomQuestion",
                        data: qaData,
                        success: function (response) {
                            if (response == "updated") {
                                console.log('success');
                                console.log(response);
                                window.location.replace("/Account/EditProfile");
                            }
                            // Did not manage to update: display error above dropdown in red
                            else {
                                console.log('failllll');
                                console.log(response);
                            }
                        },
                        error: function () {
                            console.log('ERROR');
                        }
                    });

                });
            });



        });
    });


   
}
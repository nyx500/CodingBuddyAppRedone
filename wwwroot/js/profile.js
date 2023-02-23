// Start code running when DOM is fully-loaded:
$(document).ready(function () {

    // Profile picture uploading process...
    var profilePictureInput = $("#profile-picture-input");

    profilePictureInput.change(function (event) {

        // event.preventDefault() prevents empty form from being submitted!
        event.preventDefault();
        uploadImageFile(profilePictureInput);
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
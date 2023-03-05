// Start code running when DOM is fully-loaded:
$(document).ready(function () {

    var colorChangerButton = $("#change-color-button");
    // Set default color scheme if session is empty (user first accesses site)
    if (!sessionStorage.getItem("colorScheme")) {
        sessionStorage.setItem("colorScheme", "coding-picture");
    }
    else if (sessionStorage.getItem("colorScheme") == "coding-picture") {
        $('body').addClass("body-coding-background");
    }
    else {
        $('body').removeClass("body-coding-background");

    }


    colorChangerButton.click(function () {

        // Change to minimalist
        if (sessionStorage.getItem("colorScheme") == "coding-picture") {
            sessionStorage.setItem("colorScheme", "minimalist");
            $('body').removeClass("body-coding-background");
        }
        // Change to coding-picture
        else {
            sessionStorage.setItem("colorScheme", "coding-picture");
            $('body').addClass("body-coding-background");
        }
    });

});
$(() => {
    $('form').submit(onSubmitFormDisablePostButton);

    function onSubmitFormDisablePostButton() {
        const form = $(this);
        const button = form.find("button[type='submit']");

        if (!button.hasClass("js-dont-diasble-on-submit")) {
            button.prop("disabled", true);
        }
    }
});

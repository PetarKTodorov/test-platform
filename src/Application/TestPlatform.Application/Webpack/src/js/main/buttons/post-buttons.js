$(() => {
    $('form').submit(onSubmitFormDisablePostButton);

    function onSubmitFormDisablePostButton() {
        const form = $(this);
        const button = form.find("button[type='submit']");
        button.prop("disabled", true);
    }
});

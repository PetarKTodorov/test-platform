$(() => {
    const deleteForms = $(".js-delete-form");

    deleteForms.on("submit", removeDisabledAttributeFromFields);

    function removeDisabledAttributeFromFields() {
        const form = $(this);

        form.find(":input").removeAttr("disabled");
    }
});

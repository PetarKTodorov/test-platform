$(() => {
    const conductTestContainer = $(".js-block-site-modal");

    conductTestContainer.addClass("d-block");
    conductTestContainer.on("click", showModal);

    const closeLeaveTestModalButton = $(".js-close-leave-test-modal-button");
    closeLeaveTestModalButton.on("click", hideModal);

    function showModal() {
        const modalButton = $(".js-test-leave-modal-button");
        conductTestContainer.removeClass("d-block");

        modalButton.trigger("click");
    }

    function hideModal() {
        conductTestContainer.addClass("d-block");
    }
});

$(() => {
    const restoreModalButtons = $(".js-restore-modal-button");

    restoreModalButtons.on("click", onClick);

    function onClick() {
        const restoreButton = $(this);
        const modalId = restoreButton.attr("data-bs-target");
        const modelId = restoreButton.attr("data-model-id");
        const restoreModal = $(modalId);

        const confirmRestoreButton = restoreModal.find(".js-confirm-restore");
        const hrefWithId = confirmRestoreButton.attr("href") + "/" + modelId;
        confirmRestoreButton.attr("href", hrefWithId);
    }
});

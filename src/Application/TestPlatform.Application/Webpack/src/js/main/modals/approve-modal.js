$(() => {
    const approveModalButtons = $(".js-approve-modal-button");

    approveModalButtons.on("click", onClick);

    function onClick() {
        const restoreButton = $(this);
        const modalId = restoreButton.attr("data-bs-target");
        const modelId = restoreButton.attr("data-model-id");
        const approveModal = $(modalId);

        const confirmApproveButton = approveModal.find(".js-confirm-approve");
        const hrefWithId = confirmApproveButton.attr("href") + "/" + modelId;
        confirmApproveButton.attr("href", hrefWithId);
    }
});

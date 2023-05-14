$(() => {
    // initialize globally select2
    $('.js-select2').select2();

    $('.js-select2-modal').select2({
        dropdownParent: $('.js-modal-parent')
    });
});

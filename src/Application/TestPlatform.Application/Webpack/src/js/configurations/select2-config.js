$(() => {
    configSelect2();
    $(document).on('form-reload', configSelect2);

    function configSelect2() {
        // initialize globally select2
        $('.js-select2').select2();

        $('.js-select2-modal').select2({
            dropdownParent: $('.js-modal-parent')
        });
    }
});

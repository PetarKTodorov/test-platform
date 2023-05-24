$(() => {
    const select = $(".js-search-select");

    select.on("change", onChangeSearchSelect);
    onStartSearchSelect();

    function onChangeSearchSelect() {
        const select = $(this);
        const rangeOption = select.attr("data-range-option");
        const selectedOption = select.find(":selected").val();
        const otherSearchTermInput = select.parent().parent().find(".js-other-search-term");
        const toggleClassName = "d-none";

        otherSearchTermInput.addClass(toggleClassName);

        if (selectedOption == rangeOption) {
            otherSearchTermInput.removeClass(toggleClassName);
        }
    }

    function onStartSearchSelect() {
        $(".js-search-select").each(function () {
            const select = $(this);
            select.trigger("change");
        });
    }
});

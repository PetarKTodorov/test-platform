$(() => {
    const conductTestForm = $(".js-conduct-test-form");

    conductTestForm.on("submit", generateCheckboxesInputs)

    function generateCheckboxesInputs() {
        const conductFormCheckboxes = $(".js-test-conduct-form-checkbox:checked");
        const questionsIndexes = [];
        let answerIndex = 0;

        conductFormCheckboxes.each(function() {
            const checkbox = $(this);

            const currentQuestionIndex = checkbox.attr("data-question-index");

            if (questionsIndexes.includes(currentQuestionIndex) == false) {
                questionsIndexes.push(currentQuestionIndex);
                answerIndex = 0;
            } else {
                answerIndex++;
            }

            const inputName = checkbox.attr("data-answer-name") + `[${answerIndex}]`;
            const inputValue = checkbox.attr("data-answer-id");

            const hiddenInput = $("<input>").attr("type", "hidden");
            hiddenInput.attr("name", inputName)
                .val(inputValue)
                .appendTo(conductTestForm);
        });
    }
});

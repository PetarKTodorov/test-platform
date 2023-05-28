(() => {
    const addAnswerButton = $(".js-add-answer");
    const removeAnswerButton = $(".js-remove-answer");

    removeAnswerButton.on("click", removeAnswer);
    addAnswerButton.on("click", addAnswer);

    function removeAnswer() {
        const removeButton = $(this);
        const parentContainer = removeButton
            .parent()
            .parent();

        const lastAnswer = parentContainer
            .find(".js-answer-container")
            .last();

        if (lastAnswer) {
            lastAnswer.remove();

            const answers = parentContainer.find(".js-answer-container");
            if (answers.length == 0) {
                const index = 0;
                const answerHtml = generateAnswerHtml(index);

                const newAnswer = $(answerHtml);
                const buttonsContainer = removeButton
                    .parent();
                newAnswer.insertBefore(buttonsContainer);
            }
        }
    };

    function addAnswer() {
        const addButton = $(this);

        const parentContainer = addButton
            .parent()
            .parent();

        const index = parentContainer
            .find(".js-answer-container")
            .length;

        const lastAnswer = parentContainer
            .find(".js-answer-container")
            .last();

        const answerHtml = generateAnswerHtml(index);

        const newAnswer = $(answerHtml);
        const buttonsContainer = addButton
            .parent();
        newAnswer.insertBefore(buttonsContainer);
    };

    function generateAnswerHtml(index) {
        const idNameAttribute = `Answers[${index}].Id`;
        const answerIdNameAttribute = `Answers[${index}].AnswerId`;
        const isCorrectNameAttribute = `Answers[${index}].IsCorrect`;
        const isCorrectInputId = `Answers_${index}__.IsCorrect`;
        const answerContentNameAttribute = `Answers[${index}].AnswerContent`;

        const answerHtml = `
            <div class="my-2 js-answer-container">
                <input hidden type="text" name="${idNameAttribute}" value="" />
                <input hidden type="text" name="${answerIdNameAttribute}" value="" />
                <div class="d-flex gap-2">
                    <input id="${isCorrectInputId}" name="${isCorrectNameAttribute}" type="checkbox" value="true">
                    <input id="${isCorrectInputId}" name="${isCorrectNameAttribute}" type="checkbox" value="false" hidden>
                    <input class="form-control" type="text" name="${answerContentNameAttribute}" value="" />
                </div>
            </div>`;

        return answerHtml;
    }
})();

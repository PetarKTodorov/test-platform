(() => {
    const addAnswerButton = $(".js-add-answer");
    const removeAnswerButton = $(".js-remove-answer");
    const questionForm = $(".js-question-form");
    const questionTypeIdSelect = $("#QuestionTypeId");

    const mutlipleChoiceQuestionTypeId = "AE825D9C-EF76-45E9-A31C-561976E6387E";
    const singleChoiceQuestionTypeId = "00F1EA00-27B4-4894-8AFE-877C36B9E41D";

    questionTypeIdSelect.on("change", onChangeQuestionType);
    questionForm.one("submit", updateSubmitQuestionForm);
    removeAnswerButton.on("click", removeAnswer);
    addAnswerButton.on("click", addAnswer);

    function onChangeQuestionType() {
        addAnswerButton.parent().removeClass("d-none");

        const questionTypeId = questionTypeIdSelect.val();
        if (questionTypeId.toLowerCase() == singleChoiceQuestionTypeId.toLowerCase()) {
            const answerCheckboxes = $('.js-answer-checkbox');

            answerCheckboxes.each(function () {
                const checkboxElement = $(this);
                const checkboxName = checkboxElement.attr("name");

                const checkBoxParent = checkboxElement.parent();

                checkboxElement.remove();
                checkBoxParent.prepend(generateInputBasedOnQuestionType(questionTypeId, checkboxName, checkboxName));
            });
        } else {
            const answerRadios = $('.js-answer-radio');

            answerRadios.each(function () {
                const radioElement = $(this);
                const radioValue = radioElement.attr("value");

                const radioParent = radioElement.parent();

                radioElement.remove();
                radioParent.prepend(generateInputBasedOnQuestionType(questionTypeId, radioValue, radioValue));
            });
        }
    }

    function updateSubmitQuestionForm(event) {
        event.preventDefault();

        const questionTypeId = questionTypeIdSelect.val();
        if (questionTypeId.toLowerCase() == singleChoiceQuestionTypeId.toLowerCase()) {
            const selectedRadioButtonValue = $('input[name="IsCorrect"]:checked').val();
            const trueCheckboxHtml = $(`
                <input id="${selectedRadioButtonValue}" checked="checked" name="${selectedRadioButtonValue}" type="checkbox" value="true" hidden />
            `);

            trueCheckboxHtml.insertBefore(addAnswerButton.parent());
        }

        questionForm.trigger("submit");

        return true;
    }

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

        if (!questionTypeIdSelect.val()) {
            return;
        }

        const questionTypeId = questionTypeIdSelect.val();
        const answerHtml = generateAnswerHtml(index, questionTypeId);

        const newAnswer = $(answerHtml);
        const buttonsContainer = addButton
            .parent();
        newAnswer.insertBefore(buttonsContainer);
    };

    function generateAnswerHtml(index, typeId) {
        const idNameAttribute = `Answers[${index}].Id`;
        const answerIdNameAttribute = `Answers[${index}].AnswerId`;
        const isCorrectInputId = `Answers_${index}__.IsCorrect`;
        const isCorrectNameAttribute = `Answers[${index}].IsCorrect`;
        const answerContentNameAttribute = `Answers[${index}].AnswerContent`;

        const answerHtml = `
            <div class="my-2 js-answer-container">
                <input hidden type="text" name="${idNameAttribute}" value="" />
                <input hidden type="text" name="${answerIdNameAttribute}" value="" />
                <div class="d-flex gap-2">
                    ${generateInputBasedOnQuestionType(typeId, isCorrectNameAttribute, isCorrectInputId)}
                    <input class="form-control" type="text" name="${answerContentNameAttribute}" value="" />
                </div>
            </div>`;

        return answerHtml;
    }

    function generateInputBasedOnQuestionType(questionTypeId, isCorrectNameAttribute, isCorrectInputId) {
        let inputHtml = "";

        if (questionTypeId.toLowerCase() == mutlipleChoiceQuestionTypeId.toLowerCase()) {
            inputHtml = `
                <input class="js-answer-checkbox" id="${isCorrectInputId}" name="${isCorrectNameAttribute}" type="checkbox" value="true">
                <input id="${isCorrectInputId}" name="${isCorrectNameAttribute}" type="checkbox" value="false" hidden>
            `;

        } else if (questionTypeId.toLowerCase() == singleChoiceQuestionTypeId.toLowerCase()) {
            inputHtml = `<input class="js-answer-radio" id="${isCorrectInputId}" name="IsCorrect" type="radio" value="${isCorrectNameAttribute}" />`;
        }

        return inputHtml;
    }
})();

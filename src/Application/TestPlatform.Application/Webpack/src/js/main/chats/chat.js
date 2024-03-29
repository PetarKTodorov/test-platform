(() => {
    const escapeSymbolsMap = {
        '&': '&amp;',
        '<': '&lt;',
        '>': '&gt;',
        '"': '&quot;',
        "'": '&#39;',
        '/': '&#x2F;',
        '`': '&#x60;',
        '=': '&#x3D;'
    };

    const room = $(".js-chat-room-input").val();
    if (!room) {
        return;
    }

    const connection = new signalR.HubConnectionBuilder()
        .withUrl(`/test-chat?roomId=${room}`)
        .build();

    const chatSubmitButton = $(".js-chat-submit-button");

    chatSubmitButton.attr("disabled", true);

    connection.start()
        .then(function () {
            chatSubmitButton.removeAttr("disabled");
        })
        .catch(function (err) { });

    connection.on("ReceiveMessageAsync", function (userEmail, message, time) {
        const currentUserEmail = $(".js-chat-user-input").val();

        let isCurrentUser = currentUserEmail === userEmail;

        const currentUserMessageClass = isCurrentUser ? "chat__message--current-user" : null;
        const chatMessageTime = isCurrentUser ? time : `${userEmail} - ${time}`;

        const chatMessageTemplate = `
            <div class="chat__message ${currentUserMessageClass}">
                <p class="chat__message-text">${escapeHtml(message)}</p>
                <div class="chat__message-time">${chatMessageTime}</div>
            </div>
        `;

        const inputChatText = $(".js-chat-message-input");
        inputChatText.val(null);

        const chatContainer = $(".chat");
        chatContainer.append(chatMessageTemplate);

        chatContainer.scrollTop(chatContainer[0].scrollHeight);
    });

    chatSubmitButton.on("click", function (event) {
        event.preventDefault();

        const userEmail = $(".js-chat-user-input").val();
        const message = $(".js-chat-message-input").val().trim();

        if (message && userEmail) {
            connection
                .invoke("SendMessageAsync", userEmail, message)
                .catch(function (err) { });
        }
    });

    function escapeHtml(string) {
        const result = String(string).replace(/[&<>"'`=\/]/g, function (s) {
            return escapeSymbolsMap[s];
        });

        return result;
    }
})();

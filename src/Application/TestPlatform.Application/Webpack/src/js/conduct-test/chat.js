(() => {
    var room = document.querySelector(".js-chat-room-input").value;
    var connection = new signalR.HubConnectionBuilder().withUrl(`/test-chat?roomId=${room}`).build();

    const chatSubmitButton = document.querySelector(".js-chat-submit-button");

    //Disable the send button until connection is established.
    chatSubmitButton.disabled = true;

    connection.start()
        .then(function () {
            chatSubmitButton.disabled = false;
        })
        .catch(function (err) {
            return console.error(err.toString());
        });

    connection.on("ReceiveMessageAsync", function (user, message) {
        const date = new Date();
        const chatMessage = `
            <div class="chat__message">
                <p class="chat__message-text">${message}</p>
                <div class="chat__message-time">${user} - ${date.getHours()}:${date.getMinutes()}</div>
            </div>
        `;

        const chatContainer = document.querySelector(".chat");
        chatContainer.innerHTML += chatMessage;

        document.querySelector(".js-chat-message-input").value = "";
    });

    chatSubmitButton.addEventListener("click", function (event) {
        event.preventDefault();

        var user = document.querySelector(".js-chat-user-input").value;
        var message = document.querySelector(".js-chat-message-input").value;

        connection
            .invoke("SendMessageAsync", user, message)
            .catch(function (err) {
                return console.error(err.toString());
            });
    });
})();

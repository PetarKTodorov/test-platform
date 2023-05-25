(() => {
    const deleteUserButtons = document.querySelectorAll(".delete-user-button");
    const restoreUserButtons = document.querySelectorAll(".restore-user-button");

    deleteUserButtons.forEach(button => button.addEventListener("click", (e) => clickUserButton(e, '#deleteUserModal')))
    restoreUserButtons.forEach(button => button.addEventListener("click", (e) => clickUserButton(e, '#restoreUserModal')))

    function clickUserButton (e, modalSelector) {
        const url = e.currentTarget.dataset.url;
        fetch(url)
            .then(response => response.json())
            .then(data => {
                const userModal = document.querySelector(modalSelector);

                const userIdInput = userModal.querySelector('.user-id');
                const fullNameInput = userModal.querySelector('.full-name');
                const emailInput = userModal.querySelector('.email');
                const createdDateInput = userModal.querySelector('.created-date');

                userIdInput.value = data.id;
                fullNameInput.value = data.fullName;
                emailInput.value = data.email;
                createdDateInput.value = data.createdDate;
            });
    }
})();

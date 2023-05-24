(() => {
    const modifyUserRolesButtons = document.querySelectorAll(".delete-user-button");

    modifyUserRolesButtons.forEach(button => {
        button.addEventListener("click", function (e) {
            const rolesMultiSelect = $(".js-select2-modal.roles");
            rolesMultiSelect.empty();

            const url = this.dataset.url;
            fetch(url)
                .then(response => response.json())
                .then(data => {
                    const userIdInput = document.querySelector('.delete-user-id');
                    const fullNameInput = document.querySelector('.full-name');
                    const emailInput = document.querySelector('.email');
                    const createdDateInput = document.querySelector('.created-date');

                    userIdInput.value = data.id;
                    fullNameInput.value = data.fullName;
                    emailInput.value = data.email;
                    createdDateInput.value = data.createdDate;

                    console.log(data);
                })
        })
    })
})();

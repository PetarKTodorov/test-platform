(() => {
    $(".js-select2-modal.roles").select2({
        dropdownParent: $(".js-modal-parent"),
        multiple: true,
    });

    const modifyUserRolesButtons = document.querySelectorAll(".modify-user-roles-button");

    modifyUserRolesButtons.forEach(button => {
        button.addEventListener("click", function (e) {
            const rolesMultiSelect = $(".js-select2-modal.roles");
            rolesMultiSelect.empty();

            const userId = this.dataset.userId;
            const url = this.dataset.url;
            fetch(url)
                .then(response => response.json())
                .then(data => {
                    const userIdInput = document.querySelector('.user-id');
                    userIdInput.value = userId;

                    for (role of data.allRoles) {
                        const optionElement = document.createElement("option");
                        optionElement.value = role.id;
                        optionElement.innerText = role.name;

                        let isRoleSelected = false;
                        const userRolesIds = data.userRoles.map(ur => ur.roleId);
                        if (userRolesIds.includes(role.id)) {
                            isRoleSelected = true;
                        }

                        var newOption = new Option(role.name, role.id, false, isRoleSelected);
                        rolesMultiSelect.append(newOption);
                    }
                    rolesMultiSelect.trigger("change");
                })
        })
    })
})();

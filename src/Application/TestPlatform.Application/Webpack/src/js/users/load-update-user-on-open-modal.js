(() => {
    const selects = [
        $(".js-select2-modal.js-roles"),
        $(".js-select2-modal.js-subject-tags"),
    ];

    selects.forEach(select => select.select2({
        dropdownParent: $(".js-modal-parent"),
        multiple: true,
    }));

    const updateUserButtons = document.querySelectorAll(".js-update-user-button");

    updateUserButtons.forEach(button => {
        button.addEventListener("click", function (e) {
            selects.forEach(select => select.empty());

            const userId = this.dataset.userId;
            const url = this.dataset.url;
            fetch(url)
                .then(response => response.json())
                .then(data => {
                    const userIdInput = document.querySelector('.user-id');
                    userIdInput.value = userId;

                    const rolesMultiSelect = selects[0];
                    for (role of data.allRoles) {
                        let isRoleSelected = false;
                        const userRolesIds = data.userRoles.map(ur => ur.roleId.toLowerCase());
                        if (userRolesIds.includes(role.id.toLowerCase())) {
                            isRoleSelected = true;
                        }

                        var newOption = new Option(role.name, role.id, false, isRoleSelected);
                        rolesMultiSelect.append(newOption);
                    }

                    const subjectTagsSelect = selects[1];
                    for (subjectTag of data.allSubjectTags) {
                        let isSubjectTagSelected = false;
                        const userSubjectTagsIds = data.userSubjectTags.map(ur => ur.subjectTagId.toLowerCase());
                        if (userSubjectTagsIds.includes(subjectTag.id.toLowerCase())) {
                            isSubjectTagSelected = true;
                        }

                        var newOption = new Option(subjectTag.name, subjectTag.id, false, isSubjectTagSelected);
                        subjectTagsSelect.append(newOption);
                    }
                    selects.forEach(select => select.trigger("change"));
                })
        })
    })
})();

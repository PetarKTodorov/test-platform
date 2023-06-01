namespace TestPlatform.Application.Areas.Teacher.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using TestPlatform.Application.Infrastructures.Searcher.Types;
    using TestPlatform.Common.Constants;
    using TestPlatform.Common.Enums;
    using TestPlatform.Common.Extensions;
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.DTOs.BindingModels.Rooms;
    using TestPlatform.DTOs.BindingModels.Tests;
    using TestPlatform.DTOs.ViewModels.Rooms;
    using TestPlatform.DTOs.ViewModels.Tests;
    using TestPlatform.Services.Database.Authorization.Interfaces;
    using TestPlatform.Services.Database.Rooms.Interfaces;
    using TestPlatform.Services.Database.Test.Interfaces;
    using TestPlatform.Services.Managers.Interfaces;

    public class RoomController : BaseTeacherController
    {
        private readonly IRoomService roomService;
        private readonly ITestService testService;
        private readonly IUserService userService;
        private readonly ISearchPageableMananager searchPageableMananager;

        public RoomController(IRoomService roomService,
            ITestService testService,
            IUserService userService,
            ISearchPageableMananager searchPageableMananager)
        {
            this.roomService = roomService;
            this.testService = testService;
            this.userService = userService;
            this.searchPageableMananager = searchPageableMananager;
        }

        [HttpGet]
        public async Task<IActionResult> List(ICollection<AbstractSearch> searchCriteria, int? page = 1)
        {
            var dataQuery = this.roomService
                .FindAllAsQueryable<ListRoomsVM>()
                .Where(r => r.CreatedBy == this.CurrentUserId);

            var model = this.searchPageableMananager.CreateSearchFilterModelWithPaging(dataQuery, searchCriteria, page.Value);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create(Guid testId)
        {
            var test = await this.testService.FindByIdAsync<Test>(testId);

            if (test == null)
            {
                return this.RedirectToAction(nameof(List));
            }

            var createRoomBM = new CreateRoomBM()
            {
                TestId = testId,
                TestTitle = test.Title
            };

            this.ViewData["AllStudents"] = (await this.userService.FindAllByRoleIdAsync<List<SelectListItem>>(Roles.Student.GetUid())).ToList();

            return this.View(createRoomBM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoomBM model)
        {
            if (this.ModelState.IsValid == false)
            {
                this.ViewData["AllStudents"] = (await this.userService.FindAllByRoleIdAsync<List<SelectListItem>>(Roles.Student.GetUid())).ToList();

                return this.View(model);
            }

            var room = await this.roomService.CreateAsync<Room, CreateRoomBM>(model, this.CurrentUserId);

            await this.roomService.UpdateParticipantsAsync(room.Id, model.ParticipantsIds, this.CurrentUserId);

            return this.RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id, bool isDeleted = false)
        {
            var room = await this.roomService.FindByIdAsync<DetailsRoomVM>(id, isDeleted);

            return this.View(room);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var room = await this.roomService.FindByIdAsync<UpdateRoomBM>(id);

            if (room.CreatedBy != this.CurrentUserId)
            {
                return this.NotFound();
            }

            if (room.StartDateTime <= DateTime.Now && DateTime.Now <= room.EndDateTime)
            {
                this.ViewData["RoomError"] = "The room is now live, any changes will not be accepted, till the end date comes.";
            }

            this.ViewData["AllStudents"] = (await this.userService.FindAllByRoleIdAsync<List<SelectListItem>>(Roles.Student.GetUid())).ToList();

            return this.View(room);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateRoomBM model)
        {
            if (this.ModelState.IsValid == false)
            {
                this.ViewData["AllStudents"] = (await this.userService.FindAllByRoleIdAsync<List<SelectListItem>>(Roles.Student.GetUid())).ToList();

                return this.View(model);
            }

            if (model.CreatedBy != this.CurrentUserId)
            {
                return this.NotFound();
            }

            var room = await this.roomService.FindByIdAsync<UpdateRoomBM>(model.Id);
            if (room.StartDateTime <= DateTime.Now && DateTime.Now <= room.EndDateTime)
            {
                this.ViewData["RoomError"] = "The room is now live, any changes will not be accepted, till the end date comes.";
                this.ViewData["AllStudents"] = (await this.userService.FindAllByRoleIdAsync<List<SelectListItem>>(Roles.Student.GetUid())).ToList();
                return this.View(model);
            }

            await this.roomService.UpdateAsync<Room, UpdateRoomBM>(model.Id, model, this.CurrentUserId);
            await this.roomService.UpdateParticipantsAsync(model.Id, model.ParticipantsIds, this.CurrentUserId);

            return this.RedirectToAction(nameof(Details), new { id = model.Id });
        }
    }
}

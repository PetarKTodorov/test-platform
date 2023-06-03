namespace TestPlatform.Application.Areas.Student.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TestPlatform.Application.Infrastructures.Searcher.Types;
    using TestPlatform.DTOs.ViewModels.Rooms;
    using TestPlatform.DTOs.ViewModels.Users;
    using TestPlatform.Services.Database.Rooms.Interfaces;
    using TestPlatform.Services.Database.Test.Interfaces;
    using TestPlatform.Services.Managers.Interfaces;

    public class RoomsController : BaseStudentController
    {
        private readonly IRoomService roomService;
        private readonly ISearchPageableMananager searchPageableMananager;
        private readonly ITestUserMapService testUserMapService;

        public RoomsController(IRoomService roomService,
            ISearchPageableMananager searchPageableMananager,
            ITestUserMapService testUserMapService)
        {
            this.roomService = roomService;
            this.searchPageableMananager = searchPageableMananager;
            this.testUserMapService = testUserMapService;
        }

        [HttpGet]
        public IActionResult List(ICollection<AbstractSearch> searchCriteria, int? page = 1)
        {
            var dataQuery = this.roomService.FindAllRoomsByUserIdAsQueryable<ListStudentRoomVM>(this.CurrentUserId);
            var model = this.searchPageableMananager.CreateSearchFilterModelWithPaging(dataQuery, searchCriteria, page.Value);

            foreach (var data in model.Data)
            {
                data.Grade = this.testUserMapService.FindUserGradeForTest(data.TestId, this.CurrentUserId);
            }

            return this.View(model);
        }
    }
}

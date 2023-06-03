namespace TestPlatform.Application.Areas.Student.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TestPlatform.Application.Infrastructures.Searcher.Types;
    using TestPlatform.DTOs.ViewModels.Rooms;
    using TestPlatform.Services.Database.Rooms.Interfaces;
    using TestPlatform.Services.Managers.Interfaces;

    public class RoomsController : BaseStudentController
    {
        private readonly IRoomService roomService;
        private readonly ISearchPageableMananager searchPageableMananager;

        public RoomsController(IRoomService roomService,
            ISearchPageableMananager searchPageableMananager)
        {
            this.roomService = roomService;
            this.searchPageableMananager = searchPageableMananager;
        }

        [HttpGet]
        public IActionResult List(ICollection<AbstractSearch> searchCriteria, int? page = 1)
        {
            var dataQuery = this.roomService.FindAllRoomsByUserIdAsQueryable<ListStudentRoomVM>(this.CurrentUserId);

            // TODO: fix Grade set

            var model = this.searchPageableMananager.CreateSearchFilterModelWithPaging(dataQuery, searchCriteria, page.Value);

            return this.View(model);
        }
    }
}

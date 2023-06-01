namespace TestPlatform.Application.Areas.Student.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TestPlatform.DTOs.BindingModels.Common;
    using TestPlatform.DTOs.BindingModels.Tests;
    using TestPlatform.DTOs.ViewModels.Tests;
    using TestPlatform.Services.Database.Rooms.Interfaces;
    using TestPlatform.Services.Database.Test.Interfaces;

    public class TestsController : BaseStudentController
    {
        private readonly IRoomService roomService;
        private readonly ITestService testService;
        private readonly ITestUserMapService testUserMapService;

        public TestsController(IRoomService roomService,
            ITestService testService,
            ITestUserMapService testUserMapService)
        {
            this.roomService = roomService;
            this.testService = testService;
            this.testUserMapService = testUserMapService;
        }

        [HttpGet]
        public async Task<IActionResult> StartTest(Guid roomId)
        {
            var room = await this.roomService.FindByIdAsync<ConductTestVM>(roomId);

            if (room == null || !(room.StartDateTime <= DateTime.Now && DateTime.Now <= room.EndDateTime))
            {
                return this.NotFound();
            }

            var testUserMap = await this.testUserMapService.FindByTestIdAndRoomIdAsync<BaseBM>(room.TestId, this.CurrentUserId);

            if (testUserMap == null)
            {
                var newTestUserMap = new CreateTestUserMapBM()
                {
                    TestId = room.TestId,
                    UserId = this.CurrentUserId
                };

                await this.testUserMapService.CreateAsync<BaseBM, CreateTestUserMapBM>(newTestUserMap, this.CurrentUserId);
            }

            return this.View(room);
        }

        [HttpPost]
        public async Task<IActionResult> FinishTest(ConductTestVM model)
        {


            return this.RedirectToAction(nameof(StartTest), new { roomId = model.Id });
        }
    }
}

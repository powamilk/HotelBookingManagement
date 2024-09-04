using HotelBookingManagement.Models;
using HotelBookingManagement.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingManagement.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // Hiển thị danh sách các phòng
        public async Task<IActionResult> Index()
        {
            var rooms = await _roomService.GetAllRooms();
            return View(rooms);
        }

        // Hiển thị chi tiết phòng
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var room = await _roomService.GetRoomById(id.Value);
            if (room == null) return NotFound();

            return View(room);
        }

        // Tạo mới phòng
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Price,Description,Status,Capacity")] Room room)
        {
            if (ModelState.IsValid)
            {
                await _roomService.CreateRoom(room);
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // Sửa phòng
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var room = await _roomService.GetRoomById(id.Value);
            if (room == null) return NotFound();

            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Price,Description,Status,Capacity")] Room room)
        {
            if (id != room.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _roomService.UpdateRoom(room);
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // Xóa phòng
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var room = await _roomService.GetRoomById(id.Value);
            if (room == null) return NotFound();

            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _roomService.DeleteRoom(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

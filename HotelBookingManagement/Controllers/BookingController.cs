using Microsoft.AspNetCore.Mvc;
using HotelBookingManagement.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelBookingManagement.Service.Interface;

public class BookingController : Controller
{
    private readonly IBookingService _bookingService;
    private readonly ICustomerService _customerService;
    private readonly IRoomService _roomService;

    public BookingController(IBookingService bookingService, ICustomerService customerService, IRoomService roomService)
    {
        _bookingService = bookingService;
        _customerService = customerService;
        _roomService = roomService;
    }

    // Hiển thị danh sách đặt phòng
    public async Task<IActionResult> Index()
    {
        var bookings = await _bookingService.GetAllBookings();
        return View(bookings);
    }

    // Hiển thị chi tiết đặt phòng
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var booking = await _bookingService.GetBookingById(id.Value);
        if (booking == null) return NotFound();

        return View(booking);
    }

    // Hiển thị form tạo mới đặt phòng
    public async Task<IActionResult> Create()
    {
        ViewData["CustomerId"] = new SelectList(await _customerService.GetAllCustomers(), "Id", "Name");
        ViewData["RoomId"] = new SelectList(await _roomService.GetAllRooms(), "Id", "Type");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,CustomerId,RoomId,CheckInDate,CheckOutDate,Status")] Booking booking)
    {
        if (ModelState.IsValid)
        {
            await _bookingService.CreateBooking(booking);
            return RedirectToAction(nameof(Index));
        }
        ViewData["CustomerId"] = new SelectList(await _customerService.GetAllCustomers(), "Id", "Name", booking.CustomerId);
        ViewData["RoomId"] = new SelectList(await _roomService.GetAllRooms(), "Id", "Type", booking.RoomId);
        return View(booking);
    }

    // Hiển thị form chỉnh sửa đặt phòng
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var booking = await _bookingService.GetBookingById(id.Value);
        if (booking == null) return NotFound();

        ViewData["CustomerId"] = new SelectList(await _customerService.GetAllCustomers(), "Id", "Name", booking.CustomerId);
        ViewData["RoomId"] = new SelectList(await _roomService.GetAllRooms(), "Id", "Type", booking.RoomId);
        return View(booking);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,RoomId,CheckInDate,CheckOutDate,Status")] Booking booking)
    {
        if (id != booking.Id) return NotFound();

        if (ModelState.IsValid)
        {
            await _bookingService.UpdateBooking(booking);
            return RedirectToAction(nameof(Index));
        }
        ViewData["CustomerId"] = new SelectList(await _customerService.GetAllCustomers(), "Id", "Name", booking.CustomerId);
        ViewData["RoomId"] = new SelectList(await _roomService.GetAllRooms(), "Id", "Type", booking.RoomId);
        return View(booking);
    }

    // Xóa đặt phòng
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var booking = await _bookingService.GetBookingById(id.Value);
        if (booking == null) return NotFound();

        return View(booking);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var booking = await _bookingService.GetBookingById(id);
        if (booking != null)
        {
            await _bookingService.DeleteBooking(id);
        }
        return RedirectToAction(nameof(Index));
    }

}

using DataBaseService.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Portal.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService orderService;


        public OrderController(OrderService orderService)
        {
            this.orderService = orderService;
        }

        /// <summary>
        /// Скачка файла по id
        /// </summary>
        [HttpGet]
        [Route("DownloadLayoutElementsFile/{id}")]
        [ResponseCache(Duration = int.MaxValue)]
        public async Task<IActionResult> Download(Guid id)
        {
            (byte[] Data, string FileName, string Extension) file = await orderService.GetFileById(id);
            return File(file.Data, "application/pdf", file.FileName + file.Extension);
        }
        
        ///// <summary>
        ///// Скачка файла по id
        ///// </summary>
        //[HttpGet]
        //[Route("PayOrder/{id}")]
        //[ResponseCache(Duration = int.MaxValue)]
        //public async Task<IActionResult> PayOrder(Guid id)
        //{
        //    var order = await orderService.GetOrderById(id);
        //    this.NavigationManager.NavigateTo($"/order/PayemntConfirmed/{order.OrderNumber}");

        //    return Ok();
        //}

    }
}

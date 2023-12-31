﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IHMS.Models;
using IHMS.ViewModel.DTO;

namespace IHMS.Controllers.APIcontrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsDTOController : ControllerBase
    {
        private readonly IhmsContext _context;

        public OrderDetailsDTOController(IhmsContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetailsDTO
        //得到
        [HttpGet]
        public IEnumerable<OrderDetail> GetOrderDetails()
        {
          
            return  _context.OrderDetails;
        }

        // GET: api/OrderDetailsDTO/5
        [HttpGet("GetOrderDetailByOrderid/{orderId}")]
        public async Task<List<OrderDetail>> GetOrderDetailByOrderid(int OrderId)
        {
            List<OrderDetail> orderDetail = await _context.OrderDetails
               .Where(od => od.OrderId == OrderId).ToListAsync();

            //var orderDetail = await _context.OrderDetails.FindAsync(OrderId);

            return orderDetail;
        }

        // PUT: api/OrderDetailsDTO/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetail(int id, OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderdetailId)
            {
                return BadRequest();
            }

            _context.Entry(orderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrderDetailsDTO
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<string> PostOrderDetail(OrderDetail orderDetail , IhmsContext _context)
        {
            if (_context.Orders == null || _context.Members == null)
            {
                return "新增失敗";
            }

            Schedule targeSchedule = _context.Schedules.FirstOrDefault(s => s.ScheduleId == orderDetail.ScheduleId);
            Order targeOrder = _context.Orders.FirstOrDefault(o => o.OrderId == orderDetail.OrderId);

            OrderDetail OrderDe= new OrderDetail
            {

                Order = targeOrder,
                Schedule = targeSchedule,
            };
            _context.OrderDetails.Add(OrderDe);
            await _context.SaveChangesAsync();

            return "新增訂單成功";
        }

        // DELETE: api/OrderDetailsDTO/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            if (_context.OrderDetails == null)
            {
                return NotFound();
            }
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderDetailExists(int id)
        {
            return (_context.OrderDetails?.Any(e => e.OrderdetailId == id)).GetValueOrDefault();
        }
    }
}

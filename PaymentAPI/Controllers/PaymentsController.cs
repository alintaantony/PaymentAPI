using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Models;
using PaymentAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PaymentsController));
        private readonly IPayRepos _context;
        public PaymentsController(IPayRepos context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Payments> GetAllPayments()
        {
            _log4net.Info("Get All Payments Was Called !!");
            return _context.GetAllPayments();
        }
        [HttpGet("{id}")]
        public IActionResult GetPaymentsById(int id)
        {
            _log4net.Info("Get Payments By ID Was Called !!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var payment = _context.GetPaymentsbyId(id);
                _log4net.Info("Payment of Id " + id + " Was called");
                if (payment == null)
                {
                    return NotFound();
                }
                return Ok(payment);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostPayments(Payments item)
        {
            _log4net.Info("Post Payments Was called !!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var addPayment = await _context.PostPayments(item);
                return Ok(addPayment);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdatePayment(Payments item, int id)
        {
            _log4net.Info("Update Payment Was Called !!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var updatePayment = await _context.UpdatePayment(item, id);
                _log4net.Info("Update Payment By Id " + id + " Was Called !!");
                return Ok(updatePayment);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}

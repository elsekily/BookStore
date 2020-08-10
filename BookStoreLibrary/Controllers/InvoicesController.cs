using AutoMapper;
using BookStoreLibrary.Core;
using BookStoreLibrary.Entities.Models;
using BookStoreLibrary.Entities.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookStoreLibrary.Controllers
{
    [Route("api/[controller]")]
    public class InvoicesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IInvoiceRepository repository;
        private readonly UserManager<User> userManager;
        public InvoicesController(IMapper mapper, IUnitOfWork unitOfWork,
            IInvoiceRepository repository, UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.userManager = userManager;
        }
        [Authorize(Policy = Policies.Moderator)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoice(int id)
        {
            var invoice = await repository.GetInvoice(id);
            return Ok(mapper.Map<Invoice, InvoiceResource>(invoice));
        }
        [Authorize(Policy = Policies.Moderator)]
        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceSaveResource invoiceResource)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var invoice = mapper.Map<InvoiceSaveResource, Invoice>(invoiceResource);
            invoice.User = userManager.GetUserAsync(HttpContext.User).Result;
            invoice.TimeIssued = DateTime.Now;

            try
            {
                repository.Add(invoice);
                await unitOfWork.CompleteAsync();

                invoice = await repository.GetInvoice(invoice.ID);
                var result = mapper.Map<Invoice, InvoiceResource>(invoice);
                return Created(nameof(GetInvoice), result);
            }
            catch
            {
                return BadRequest();
            } 
        }
        [Authorize(Policy = Policies.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await repository.GetInvoice(id);

            if (invoice == null)
                return NotFound();

            repository.Remove(invoice);
            await unitOfWork.CompleteAsync();

            return Accepted();
        }
    }
}

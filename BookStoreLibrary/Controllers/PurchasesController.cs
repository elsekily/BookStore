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
    public class PurchasesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPurchaseRepository repository;
        private readonly UserManager<User> userManager;
        public PurchasesController(IMapper mapper, IUnitOfWork unitOfWork,
            IPurchaseRepository repository, UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.userManager = userManager;
        }
        [Authorize(Policy = Policies.Moderator)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchase(int id)
        {
            var purchase = await repository.GetPurchase(id);
            return Ok(mapper.Map<Purchase, PurchaseResource>(purchase));
        }
        [Authorize(Policy = Policies.Moderator)]
        [HttpPost]
        public async Task<IActionResult> CreatePurchase([FromBody] PurchaseSaveResource purchaseResource)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var purchase = mapper.Map<PurchaseSaveResource, Purchase>(purchaseResource);
            purchase.User = userManager.GetUserAsync(HttpContext.User).Result;
            purchase.TimeIssued = DateTime.Now;
            repository.Add(purchase);
            await unitOfWork.CompleteAsync();

            purchase = await repository.GetPurchase(purchase.ID);
            var result = mapper.Map<Purchase, PurchaseSaveResource>(purchase);
            return Created(nameof(GetPurchase), result);
        }
        [Authorize(Policy = Policies.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(int id)
        {
            var purchase = await repository.GetPurchase(id);

            if (purchase == null)
                return NotFound();

            repository.Remove(purchase);
            await unitOfWork.CompleteAsync();

            return Accepted();
        }
    }
}

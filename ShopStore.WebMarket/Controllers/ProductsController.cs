namespace ShopStore.WebMarket.Controllers
{
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    

    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;

        private readonly IUserHelper userHelper;

        public ProductsController(
            IProductRepository productRepository, IUserHelper userHelper)
        {            
            this.productRepository = productRepository;

            this.userHelper = userHelper;

        }

        public IActionResult Index()
        {
            return View(productRepository.GetAll());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = productRepository.
                GetByIdAsync(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                //TODO Change for the logged user....
                product.User =
                    await userHelper.
                    GetUserByEmailAsync("luedgova1967@gmail.com");

                await productRepository.
                    CreateAsync(product);                

                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = 
                productRepository.
                GetByIdAsync(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //TODO Change for the logged user....
                    product.User =
                        await userHelper.
                        GetUserByEmailAsync("luedgova1967@gmail.com");

                    await productRepository.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await productRepository.ExistAsync(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await productRepository.
                GetByIdAsync(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {            
            var product = await productRepository.
                GetByIdAsync(id);

            await productRepository.
                DeleteAsync(product);

            return RedirectToAction(nameof(Index));
        }
    }
}
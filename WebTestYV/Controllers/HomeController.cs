using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTestYV.Calc;
using WebTestYV.Data;
using WebTestYV.Models;

namespace WebTestYV.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("AccountId,Period")] InputData model)
        {
            if (ModelState.IsValid)
            {
                List<Balance> balances = await DataJson.GetBalances();
                if (balances == null)
                {
                    ModelState.AddModelError("", "Не найден файл баланса");
                    return View(model);
                }
                if (balances.FirstOrDefault(b => b.Account_id == model.AccountId) != null)
                {

                    return RedirectToAction("GetBalances", "Home", (object)model);
                }
                else
                    ModelState.AddModelError("", "Укажите AccountId, введенного нет в балансе");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetBalances(InputData input)
        {
            List<Balance> balances = await DataJson.GetBalances();
            if (balances == null)
                return RedirectToAction("Error", "Не найден файл баланса");
            List<Payment> payments = await DataJson.GetPayment();
            if (payments == null)
                return RedirectToAction("Error", "Не найден файл оплаты");
            if (balances.FirstOrDefault(b => b.Account_id == input.AccountId) == null)
                return RedirectToAction("Error", "Укажите AccountId, введенного нет в балансе");

            return View(CalcFunction.GetBalance(balances, payments, input));
        }
        public IActionResult Error(string error)
        {
            return View(new ErrorViewModel { RequestId = error });
        }


    }
}

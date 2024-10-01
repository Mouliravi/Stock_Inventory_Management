using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SIM_Application.BO;
using SIM_Application.Controllers.api;
using SIM_Application.Models;
using SIM_Application.Repository;

namespace SIM_Application.Controllers
{
    [Authorize]
    public class BankDetailsController : Controller
    {
        private readonly IBankDetailRepository _bankDetailRepository;
        private readonly BankDetailBo _bankDetailBo;
        private readonly ILogger<UserDetailAPIController> _logger;
        public BankDetailsController(IBankDetailRepository bankDetailRepository, ILogger<UserDetailAPIController> logger)
        {
            _bankDetailRepository = bankDetailRepository;
            _bankDetailBo = new BankDetailBo(_bankDetailRepository);
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation($"Trying to fetch all Bank Details");
            var bankDetailsList = _bankDetailBo.FetchAllBankDetails();
            if (bankDetailsList == null)
            {
                TempData["Alert"] = "failure";
                TempData["AlertMessage"] = "Due to some issue cannot fetch the Bank Details";
                _logger.LogInformation($"Due to some issue cannot fetch all the Bank Details");
            }
            _logger.LogInformation($"All Bank Details are fetched successfully");
            return View( bankDetailsList );
        }
       
        public IActionResult Details(int id)
        {
            _logger.LogInformation($"Trying to fetch Bank Detail for Id:{id}");
            var bankDetail = _bankDetailBo.FetchBankDetailsById(id);
            if (bankDetail == null)
            {
                TempData["Alert"] = "failure";
                TempData["AlertMessage"] = $"Bank Detail is not found for Id:{id}";
                _logger.LogInformation($"Bank Detail is not found for Id:{id}");
                return RedirectToAction(nameof(Index));
            }
            _logger.LogInformation($"Bank Detail is fetched successfully for Id:{id}");
            return View(bankDetail);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("BankDetailsId,BankName,Ifsc,Mcir,AccountNumber,AccountBalance,CreatedAt,UpdatedAt")] BankDetail bankDetail)
        {
            _logger.LogInformation($"Trying to add Bank Detail wiht Bank Name:{bankDetail.BankName}");
            bankDetail.CreatedAt = DateTime.Now;
            bankDetail.UpdatedAt = DateTime.Now;
            if (ModelState.IsValid)
            {
               int id = _bankDetailBo.AddBankDetails(bankDetail);
                if(id > 0)
                {
                    TempData["Alert"] = "success";
                    TempData["AlertMessage"] = $"Bank Detail is successfully added";
                    _logger.LogInformation($"Bank Detail with Bank Name:{bankDetail.BankName} is added successfully");
                    return RedirectToAction("CreateUser", "UserDetails", new { id = bankDetail.BankDetailsId });
                }
            }
            TempData["Alert"] = "failure";
            TempData["AlertMessage"] = $"Bank Detail is not added successfully";
            _logger.LogInformation($"Bank Detail with Bank Name:{bankDetail.BankName} is not added successfully");
            return View(bankDetail);
        }

        public IActionResult Edit(int id)
        {
            _logger.LogInformation($"Trying to fetch Bank Detail for Id:{id}");
            var bankDetail = _bankDetailBo.FetchBankDetailsById(id);
            if (bankDetail == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"Bank Detail is fetched successfully for Id:{id}");
            return View(bankDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("BankDetailsId,BankName,Ifsc,Mcir,AccountNumber,AccountBalance,CreatedAt,UpdatedAt")] BankDetail bankDetail)
        {
            _logger.LogInformation($"Trying to update Bank Detail for Id:{id}");
            if (id != bankDetail.BankDetailsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    int i = _bankDetailBo.UpdateBankDetails(bankDetail);
                    if (i > 0)
                    {
                        _logger.LogInformation($"Bank Detail is updated successfully for Id:{id}");
                        TempData["Alert"] = "success";
                        TempData["AlertMessage"] = $"Bank Detail is updated successfully for Id:{bankDetail.BankDetailsId}";
                    }
                    else
                    {
                        _logger.LogInformation($"Bank Detail is not updated successfully for Id:{bankDetail.BankDetailsId}");
                        TempData["Alert"] = "failure";
                        TempData["AlertMessage"] = $"Bank Detail is not updated successfully for Id:{bankDetail.BankDetailsId}";
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankDetailExists(bankDetail.BankDetailsId))
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
            return View(bankDetail);
        }

        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"Trying to fetch Bank Detail for Id:{id}");
            var bankDetail = _bankDetailBo.FetchBankDetailsById(id);
            if (bankDetail == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"Bank Detail is fetched is successfully for Id:{id}");
            return View(bankDetail);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _logger.LogInformation($"Trying to delete Bank Detail for Id:{id}");
            var bankDetail = _bankDetailBo.FetchBankDetailsById(id);
            int i = -1;
            if (bankDetail != null)
            {
               i = _bankDetailBo.DeleteBankDetails(bankDetail);
            }
            if (i == -1)
            {
                _logger.LogInformation($"Bank Detail is not deleted successfully for Id:{id}");
                TempData["Alert"] = "failure";
                TempData["AlertMessage"] = $"Bank Detail is not deleted successfully for Id:{id}";
            }
            else
            {
                _logger.LogInformation($"Bank Detail is deleted successfully for Id:{id}");
                TempData["Alert"] = "success";
                TempData["AlertMessage"] = $"Bank Detail is deleted successfully for Id:{id}";
            }
            return RedirectToAction(nameof(Index));
        }
        private bool BankDetailExists(int id)
        {
            return _bankDetailBo.BankDetailExists(id);
        }
    }
}

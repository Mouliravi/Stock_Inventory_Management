using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Newtonsoft.Json;
using SIM_Application.BO;
using SIM_Application.Controllers.api;
using SIM_Application.Models;
using SIM_Application.Repository;

namespace SIM_Application.Controllers
{
    [Authorize]
    public class UserDetailsController : Controller
    {
        private readonly UserDetailBo _userDetailBo;
        private readonly BankDetailBo _bankDetailBo;
        private readonly IUserDetailRepository _userDetailRepository;
        private readonly ILogger<UserDetailAPIController> _logger;

        public UserDetailsController(IUserDetailRepository userDetailRepository, IBankDetailRepository _bankDetailRepository, ILogger<UserDetailAPIController> logger)
        {
            _userDetailRepository = userDetailRepository;
            _userDetailBo = new UserDetailBo(userDetailRepository);
            _bankDetailBo = new BankDetailBo(_bankDetailRepository);
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            _logger.LogInformation($"Trying to fetch all User Details");
            IEnumerable<UserDetail> userDetailList = _userDetailBo.FetchAllUserDetails();
            if(userDetailList == null)
            {
                TempData["Alert"] = "failure";
                TempData["AlertMessage"] = "Due to some issue cannot fetch all the User Details";
                _logger.LogInformation($"Due to some issue cannot fetch all the User Details");
            }
            else
                _logger.LogInformation($"All User Details are fetched successfully");
            return View(userDetailList);
        }

        public IActionResult FetchById()
        {
            return View();
        }
        public IActionResult FetchUserDetail(int id)
        {
            _logger.LogInformation($"Trying to fetch User Detail for User Id:{id}");
            var userDetail = _userDetailBo.FetchUserDetailsById(id);

            if (userDetail == null)
            {
                TempData["Alert"] = "failure";
                TempData["AlertMessage"] = $"User Detail is not found for User Id:{id}";
                _logger.LogInformation($"User Detail is not found for User Id:{id}");
                return RedirectToAction(nameof(FetchById));
            }
            _logger.LogInformation($"User Detail is fetched is successfully for User Id:{id}");
            return View(userDetail);
        }
        public IActionResult Details(int id)
        {
            _logger.LogInformation($"Trying to fetch User Detail for User Id:{id}");
            var userDetail = _userDetailBo.FetchUserDetailsById(id);
            _logger.LogInformation($"Trying to fetch Order Details for User Id:{id}");
            var orderDetails = _userDetailBo.fetchOrderDetails(id);
            _logger.LogInformation($"Order Details are fetched for User Id:{id}");
            UserDetailsWithOrderDetails userDetailsWithOrderDetails = new UserDetailsWithOrderDetails();
            userDetailsWithOrderDetails.OrderDetails = orderDetails;
            userDetailsWithOrderDetails.UserDetail = userDetail;
            
            if (userDetail == null)
            {
                TempData["Alert"] = "failure";
                TempData["AlertMessage"] = $"User Detail is not found for User Id:{id}";
                _logger.LogInformation($"User Detail is not found for User Id:{id}");
                return RedirectToAction(nameof(Index));
            }
            _logger.LogInformation($"User Detail is fetched is successfully for User Id:{id}");
            return View(userDetailsWithOrderDetails);
        }

        public IActionResult Create()
        {
            ViewData["BankDetailsId"] = new SelectList(_bankDetailBo.FetchAllBankDetails(), "BankDetailsId", "BankDetailsId");
            ViewData["CityId"] = new SelectList(_userDetailBo.CityTables(), "CityId", "City");
            ViewData["CountryId"] = new SelectList(_userDetailBo.CountryTables(), "CountryId", "Country");
            ViewData["GenderId"] = new SelectList(_userDetailBo.GenderTables(), "GenderId", "Gender");
            ViewData["MaritalStatusId"] = new SelectList(_userDetailBo.MaritalStatusTables(), "MaritalStatusId", "MaritalStatus");
            ViewData["NationalityId"] = new SelectList(_userDetailBo.NationalityTables(), "NationalityId", "Nationality");
            ViewData["RoleId"] = new SelectList(_userDetailBo.RoleTables(), "RoleId", "Role");
            ViewData["StateId"] = new SelectList(_userDetailBo.StateTables(), "StateId", "State");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("UserId,UserName,DateOfBirth,RoleId,NationalityId,GenderId,MaritalStatusId,AddressLine1,CityId,StateId,CountryId,MobileNumber,Occupation,Email,AnnualIncome,BalanceAmount,BankDetailsId,CreatedAt,UpdatedAt")] UserDetail userDetail)
        {
            _logger.LogInformation($"Trying to add User Detail wiht User Name:{userDetail.UserName}");
            userDetail.CreatedAt = DateTime.Now;
            userDetail.UpdatedAt = DateTime.Now;
            var serializedUserDetail = JsonConvert.SerializeObject(userDetail);
            TempData["userDetail"] = serializedUserDetail;
			string dateOfBirthError = userDetail.ValidateDateOfBirth();
			if (!string.IsNullOrEmpty(dateOfBirthError))
			{
				// If validation fails, return error message
				ModelState.AddModelError(nameof(UserDetail.DateOfBirth), dateOfBirthError);
			}
			if (ModelState.IsValid)
            {
                return RedirectToAction("Create", "BankDetails");
            }
            ViewData["BankDetailsId"] = new SelectList(_bankDetailBo.FetchAllBankDetails(), "BankDetailsId", "BankDetailsId", userDetail.BankDetailsId);
            ViewData["CityId"] = new SelectList(_userDetailBo.CityTables(), "CityId", "City", userDetail.BankDetailsId);
            ViewData["CountryId"] = new SelectList(_userDetailBo.CountryTables(), "CountryId", "Country", userDetail.BankDetailsId);
            ViewData["GenderId"] = new SelectList(_userDetailBo.GenderTables(), "GenderId", "Gender", userDetail.BankDetailsId);
            ViewData["MaritalStatusId"] = new SelectList(_userDetailBo.MaritalStatusTables(), "MaritalStatusId", "MaritalStatus", userDetail.BankDetailsId);
            ViewData["NationalityId"] = new SelectList(_userDetailBo.NationalityTables(), "NationalityId", "Nationality", userDetail.BankDetailsId);
            ViewData["RoleId"] = new SelectList(_userDetailBo.RoleTables(), "RoleId", "Role", userDetail.BankDetailsId);
            ViewData["StateId"] = new SelectList(_userDetailBo.StateTables(), "StateId", "State", userDetail.BankDetailsId);
            return View(userDetail);
        }
        public IActionResult CreateUser(int id)
        {
            var serializedUserDetail = TempData["userDetail"] as string;
            var userDetail = JsonConvert.DeserializeObject<UserDetail>(serializedUserDetail);
            if (userDetail != null)
            {
                userDetail.BankDetailsId = id;
            }

            if (ModelState.IsValid)
            {
                int i = _userDetailBo.AddUserDetails(userDetail);
                if(i > 0)
                {

                    TempData["Alert"] = "success";
                    TempData["AlertMessage"] = $"User Detail is successfully added";
                    _logger.LogInformation($"User Detail wiht User Name:{userDetail.UserName} is added succesfully");
                    return RedirectToAction(nameof(Details), new { id = userDetail.UserId });
                }
            }
            TempData["Alert"] = "failure";
            TempData["AlertMessage"] = $"User Detail is not added successfully";
            _logger.LogInformation($"User Detail wiht User Name:{userDetail.UserName} is not added successfully");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            _logger.LogInformation($"Trying to fetch User Detail for User Id:{id}");
            var userDetail = _userDetailRepository.FetchUserDetailsById(id);
            if (userDetail == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"User Detail is fetched successfully for User Id:{id}");
            ViewData["BankDetailsId"] = new SelectList(_bankDetailBo.FetchAllBankDetails(), "BankDetailsId", "BankDetailsId", userDetail.BankDetailsId);
            ViewData["CityId"] = new SelectList(_userDetailBo.CityTables(), "CityId", "City", userDetail.BankDetailsId);
            ViewData["CountryId"] = new SelectList(_userDetailBo.CountryTables(), "CountryId", "Country", userDetail.BankDetailsId);
            ViewData["GenderId"] = new SelectList(_userDetailBo.GenderTables(), "GenderId", "Gender", userDetail.BankDetailsId);
            ViewData["MaritalStatusId"] = new SelectList(_userDetailBo.MaritalStatusTables(), "MaritalStatusId", "MaritalStatus", userDetail.BankDetailsId);
            ViewData["NationalityId"] = new SelectList(_userDetailBo.NationalityTables(), "NationalityId", "Nationality", userDetail.BankDetailsId);
            ViewData["RoleId"] = new SelectList(_userDetailBo.RoleTables(), "RoleId", "Role", userDetail.BankDetailsId);
            ViewData["StateId"] = new SelectList(_userDetailBo.StateTables(), "StateId", "State", userDetail.BankDetailsId);
            return View(userDetail);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("UserId,UserName,DateOfBirth,RoleId,NationalityId,GenderId,MaritalStatusId,AddressLine1,CityId,StateId,CountryId,MobileNumber,Occupation,Email,AnnualIncome,BalanceAmount,BankDetailsId,CreatedAt,UpdatedAt")] UserDetail userDetail)
        {
            _logger.LogInformation($"Trying to update User Detail wiht User Id:{id}");
            if (id != userDetail.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    userDetail.UpdatedAt = DateTime.Now;
                    int i =_userDetailRepository.UpdateUserDetails(userDetail);
                    if(i > 0)
                    {
                        _logger.LogInformation($"User Detail is updated successfully for User Id:{userDetail.UserId}");
                        TempData["Alert"] = "success";
                        TempData["AlertMessage"] = $"User Detail is updated successfully for User Id:{userDetail.UserId}";
                    }
                    else
                    {
                        _logger.LogInformation($"User Detail is not updated successfully for User Id:{userDetail.UserId}");
                        TempData["Alert"] = "failure";
                        TempData["AlertMessage"] = $"User Detail is not updated successfully for User Id:{userDetail.UserId}";
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDetailExists(userDetail.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = userDetail.UserId });//nameof used to mitigate refactor issues
            }
            ViewData["BankDetailsId"] = new SelectList(_bankDetailBo.FetchAllBankDetails(), "BankDetailsId", "BankDetailsId", userDetail.BankDetailsId);
            ViewData["CityId"] = new SelectList(_userDetailBo.CityTables(), "CityId", "City", userDetail.BankDetailsId);
            ViewData["CountryId"] = new SelectList(_userDetailBo.CountryTables(), "CountryId", "Country", userDetail.BankDetailsId);
            ViewData["GenderId"] = new SelectList(_userDetailBo.GenderTables(), "GenderId", "Gender", userDetail.BankDetailsId);
            ViewData["MaritalStatusId"] = new SelectList(_userDetailBo.MaritalStatusTables(), "MaritalStatusId", "MaritalStatus", userDetail.BankDetailsId);
            ViewData["NationalityId"] = new SelectList(_userDetailBo.NationalityTables(), "NationalityId", "Nationality", userDetail.BankDetailsId);
            ViewData["RoleId"] = new SelectList(_userDetailBo.RoleTables(), "RoleId", "Role", userDetail.BankDetailsId);
            ViewData["StateId"] = new SelectList(_userDetailBo.StateTables(), "StateId", "State", userDetail.BankDetailsId);
            return View(userDetail);
        }

        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"Trying to fetch User Detail for User Id:{id}");
            var userDetail = _userDetailRepository.FetchUserDetailsById(id);
            if (userDetail == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"User Detail is fetched is successfully for User Id:{id}");
            return View(userDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _logger.LogInformation($"Trying to delete User Detail for User Id:{id}");
            var userDetail = _userDetailRepository.FetchUserDetailsById(id);
            var bankDetail = userDetail?.BankDetails;
            int i = -1;
            if (bankDetail != null)
            {
                _logger.LogInformation($"Trying to delete Bank Detail for Id:{bankDetail.BankDetailsId}");
                i = _bankDetailBo.DeleteBankDetails(bankDetail);    
            }
            if (i == -1)
            {
                _logger.LogInformation($"Bank Detail is not deleted successfully for User Id:{id}");
                TempData["Alert"] = "failure";
                TempData["AlertMessage"] = $"Bank Detail is not deleted successfully for User Id:{id}";
            }
            else
            {
                _logger.LogInformation($"Bank Detail is deleted successfully for User Id:{id}");
                TempData["Alert"] = "success";
                TempData["AlertMessage"] = $"Bank Detail is deleted successfully for User Id:{id}";
            }
            i = -1;
            if (userDetail != null)
            {
                i = _userDetailBo.DeleteUserDetails(userDetail);
            }
            if(i == -1)
            {
                _logger.LogInformation($"User Detail is not deleted successfully for User Id:{id}");
                TempData["Alert"] = "failure";
                TempData["AlertMessage"] = $"User Detail is not deleted successfully for User Id:{userDetail.UserId}";
            }
            else
            {
                _logger.LogInformation($"User Detail is deleted successfully for User Id:{id}");
                TempData["Alert"] = "success";
                TempData["AlertMessage"] = $"User Detail is deleted successfully for User Id:{userDetail.UserId}";
            }
            return RedirectToAction(nameof(Index));
        }
        public bool UserDetailExists(int id)
        {
            return _userDetailBo.UserDetailExists(id);
        }

        public IActionResult FetchAllUsingJoin()
        {
            _logger.LogInformation("Trying to perform Fetch All Using Join operation");
            var joinedQuery = _userDetailBo.FetchAllUsingJoin();
            if (joinedQuery != null)
            {
                _logger.LogInformation($"Fetch All Using Join operation is performed successfully");
            }
            else
            {
                _logger.LogInformation($"Fetch all using join operation is not performed successfully");
                TempData["Alert"] = "failure";
                TempData["AlertMessage"] = "Due to some issue cannot perform Fetch All Using Join operation";
            }
            return View(joinedQuery);
        }
        
        public IActionResult Filter()
        {
            return View(); 
        }
        public IActionResult FilteredUsers(int annualIncome)
        {
            _logger.LogInformation("Trying to perform Filter operation");
            var filteredList = _userDetailBo.FilterByAnnualIncome(annualIncome);
            if (filteredList != null)
            {
                _logger.LogInformation($"Filter operation is performed successfully");
            }
            else
            {
                _logger.LogInformation($"Filter operation is not performed successfully");
                TempData["Alert"] = "failure";
                TempData["AlertMessage"] = "Due to some issue cannot perform Filter operation ";
            }
            TempData["annualIncome"] = annualIncome;
            return View(filteredList);
        }
        public IActionResult FetchAllUsingLazyLoading()
        {
            _logger.LogInformation($"Tryibg to Fetch All Using Lazy Loading operation");
            var userDetails = _userDetailBo.FetchAllUsingLazyLoading();
            if (userDetails != null)
            {
                _logger.LogInformation($"Fetch All Using Lazy Loading operation is performed successfully");
            }
            else
            {
                _logger.LogInformation($"Fetch All Using Lazy Loading operation is not performed successfully");
                TempData["Alert"] = "failure";
                TempData["AlertMessage"] = "Due to some issue cannot perform Fetch All Using Lazy Loading operation ";
            }
            return View(userDetails);
        }

        public IActionResult Transaction()
        {
            ViewData["CityId"] = new SelectList(_userDetailBo.CityTables(), "CityId", "City");
            ViewData["CountryId"] = new SelectList(_userDetailBo.CountryTables(), "CountryId", "Country");
            ViewData["GenderId"] = new SelectList(_userDetailBo.GenderTables(), "GenderId", "Gender");
            ViewData["MaritalStatusId"] = new SelectList(_userDetailBo.MaritalStatusTables(), "MaritalStatusId", "MaritalStatus");
            ViewData["NationalityId"] = new SelectList(_userDetailBo.NationalityTables(), "NationalityId", "Nationality");
            ViewData["RoleId"] = new SelectList(_userDetailBo.RoleTables(), "RoleId", "Role");
            ViewData["StateId"] = new SelectList(_userDetailBo.StateTables(), "StateId", "State");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Transaction([Bind("UserDetails,BankDetails")] UserDetailsWithBankDetails userDetailsWithBankDetails)
        {
            _logger.LogInformation($"Tryinng to perform Transaction operation");
            userDetailsWithBankDetails.UserDetails.CreatedAt = DateTime.Now;
            userDetailsWithBankDetails.UserDetails.UpdatedAt = DateTime.Now;
            userDetailsWithBankDetails.BankDetails.CreatedAt = DateTime.Now;
            userDetailsWithBankDetails.BankDetails.UpdatedAt = DateTime.Now;
            int i = -1;
            if (ModelState.IsValid)
            {
                 i = _userDetailBo.Transaction(userDetailsWithBankDetails);
            }
                 
            if (i == -1)
            {
                TempData["Alert"] = "failure";
                TempData["AlertMessage"] = $"Transaction operation is not performed successfully";
                _logger.LogInformation($"Transaction operation is not performed successfully");
            }
            else
            {
                TempData["Alert"] = "success";
                TempData["AlertMessage"] = "Transaction operation is performed successfully";
                _logger.LogInformation($"Transaction operation is performed successfully");
                return RedirectToAction(nameof(Details), new { id = userDetailsWithBankDetails.UserDetails.UserId });
            }


            ViewData["CityId"] = new SelectList(_userDetailBo.CityTables(), "CityId", "City", userDetailsWithBankDetails.UserDetails.CityId);
            ViewData["CountryId"] = new SelectList(_userDetailBo.CountryTables(), "CountryId", "Country", userDetailsWithBankDetails.UserDetails.CountryId);
            ViewData["GenderId"] = new SelectList(_userDetailBo.GenderTables(), "GenderId", "Gender", userDetailsWithBankDetails.UserDetails.GenderId);
            ViewData["MaritalStatusId"] = new SelectList(_userDetailBo.MaritalStatusTables(), "MaritalStatusId", "MaritalStatus", userDetailsWithBankDetails.UserDetails.MaritalStatusId);
            ViewData["NationalityId"] = new SelectList(_userDetailBo.NationalityTables(), "NationalityId", "Nationality", userDetailsWithBankDetails.UserDetails.NationalityId);
            ViewData["RoleId"] = new SelectList(_userDetailBo.RoleTables(), "RoleId", "Role", userDetailsWithBankDetails.UserDetails.RoleId);
            ViewData["StateId"] = new SelectList(_userDetailBo.StateTables(), "StateId", "State", userDetailsWithBankDetails.UserDetails.StateId);
            return View(userDetailsWithBankDetails);
        }
        public IActionResult AddUserStockDetails(int id)
        {
            ViewData["ProviderName"] = new SelectList(_userDetailBo.StockProviders(), "ProviderId", "ProviderName");
            ViewData["BrokerName"] = new SelectList(_userDetailBo.BrokerDetails(), "BrokerId", "BrokerName");
            ViewData["InvestmentType"] = new SelectList(_userDetailBo.InvestmentTypeTables(), "InvestmentTypeId", "InvestmentType");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public IActionResult AddUserStockDetails(int id,[Bind("UserStockDetail", "StockDetail", "StockProvider", "BrokerDetail", "OrderDetail")] StocksInfo stocksInfo)
        {
            _logger.LogInformation($"Trying to add Stock Detail for userId;{id}");
            var providerDetail = _userDetailBo.StockProvider(stocksInfo.StockProvider.ProviderId);
            if (stocksInfo.StockDetail == null)
            {
                stocksInfo.StockDetail = new StockDetail();
            }
            stocksInfo.StockDetail.ProviderId = stocksInfo.StockProvider.ProviderId;
            stocksInfo.StockDetail.BrokerId = stocksInfo.BrokerDetail.BrokerId;
            stocksInfo.UserStockDetail.UserId = id;
            DateTime dateTime = DateTime.Now;
            DateOnly dateOnly = new DateOnly(dateTime.Year,dateTime.Month,dateTime.Day);
            stocksInfo.OrderDetail.CreatedAt = DateTime.Now;
            stocksInfo.OrderDetail.UpdatedAt = DateTime.Now;
            stocksInfo.OrderDetail.OrderDate = dateOnly;
            stocksInfo.OrderDetail.TotalInvestment = stocksInfo.OrderDetail.PurchasedQuantity * providerDetail.PerStockPrice;
            int i = _userDetailBo.AddUserStockDetails(stocksInfo);
            if (i<0)
            {
                TempData["Alert"] = "failure";
                TempData["AlertMessage"] = $"Stock Detail for User Id:{id} is not added successfully";
                _logger.LogInformation($"Stock Detail for User Id:{id} is not added successfully");
            }
            else
            {
                TempData["Alert"] = "success";
                TempData["AlertMessage"] = $"Stock Detail is added successfully for User Id:{id}";
                _logger.LogInformation($"Stock Detail is added successfully for User Id:{id}");
                return RedirectToAction(nameof(Details), new { id = stocksInfo.UserStockDetail.UserId });
            }
            ViewData["ProviderName"] = new SelectList(_userDetailBo.StockProviders(), "ProviderId", "ProviderName");
            ViewData["BrokerName"] = new SelectList(_userDetailBo.BrokerDetails(), "BrokerId", "BrokerName");
            ViewData["InvestmentType"] = new SelectList(_userDetailBo.InvestmentTypeTables(), "InvestmentTypeId", "InvestmentType");
            return View(stocksInfo);
        }

    }
}

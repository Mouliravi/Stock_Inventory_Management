using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using SIM_Application.Controllers.api;
using SIM_Application.Models;
using System.Transactions;

namespace SIM_Application.Repository
{
    public class UserDetailRepository : IUserDetailRepository
    {
        private readonly StockInventoryManagementSystemContext _context;
        private readonly ILogger<UserDetailAPIController> _logger;
        public UserDetailRepository(StockInventoryManagementSystemContext context, ILogger<UserDetailAPIController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public UserDetailRepository()
        {
            _context = new StockInventoryManagementSystemContext();
        }
        public IEnumerable<UserDetail> FetchAllUserDetails()
        {
            try
            {
                var userDetailList = _context.UserDetails
                .Include(u => u.BankDetails)
                //.Include(u => u.City)
                //.Include(u => u.Country)
                //.Include(u => u.Gender)
                //.Include(u => u.MaritalStatus)
                //.Include(u => u.Nationality)
                //.Include(u => u.Role)
                //.Include(u => u.State)
                .ToList();
                return userDetailList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all User Details");
                return Enumerable.Empty<UserDetail>();
            }
           
        }
        public UserDetail FetchUserDetailsById(int UserId)
        {
            try
            {
                var userDetail = _context.UserDetails
                .Include(u => u.BankDetails)
                .Include(u => u.City)
                .Include(u => u.Country)
                .Include(u => u.Gender)
                .Include(u => u.MaritalStatus)
                .Include(u => u.Nationality)
                .Include(u => u.Role)
                .Include(u => u.State)
                .Include(u => u.UserStockDetails)
                .FirstOrDefault(m => m.UserId == UserId);
                return userDetail;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching User Detail for User Id:{UserId}");
                return (UserDetail)null;
            }

        }
        public IEnumerable<OrderDetail> fetchOrderDetails(int userId)
        {
            try
            {
                return _context.OrderDetails
               .Where(od => od.UserStock.UserId == userId).Include(od => od.InvestmentType)
               .Include(od => od.UserStock)
               .ThenInclude(us => us.Stock)
               .ThenInclude(s => s.Provider)
               .Include(od => od.UserStock)
               .ThenInclude(us => us.Stock)
               .ThenInclude(s => s.Broker)
               .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching Order Details for User Id:{userId}");
                return Enumerable.Empty<OrderDetail>();
            }
        }
        public int AddUserDetails(UserDetail userDetail)
        {
            if (userDetail != null)
            {
                try
                {
                    _context.UserDetails.Add(userDetail);
                    SaveUserDetails();
                    return userDetail.UserId;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error occurred while Adding User Detail with User Name: {userDetail.UserName}");
                    return -1;
                }
            }
            else
                return -1;
        }
        public int UpdateUserDetails(UserDetail userDetail)
        {
            try
            {
                _context.UserDetails.Update(userDetail);
                SaveUserDetails();
                return userDetail.UserId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurres while Updating the User Detail for User Id:{userDetail.UserId} ");
                return -1;
            }

        }
        public int DeleteUserDetails(UserDetail userDetail)
        {
            if(userDetail!=null)
            {
                try
               {
                    foreach (var userStock in userDetail.UserStockDetails)
                    {
                        var orderDeatil = _context.OrderDetails.FirstOrDefault(o => o.UserStockId == userStock.UserStockId);
                        _context.OrderDetails.Remove(orderDeatil);
                        var userStockDetail = _context.UserStockDetails.FirstOrDefault(us => us.UserStockId == userStock.UserStockId);
                        _context.UserStockDetails.Remove(userStockDetail);
                        var stockDetail = _context.StockDetails.FirstOrDefault(s => s.StockId == userStock.StockId);
                        _context.StockDetails.Remove(stockDetail);
                    }
                    _context.UserDetails.Remove(userDetail);
                    SaveUserDetails();
                    return 1;
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, $"Error occurred while Deleting User Detail for User Id: {userDetail.UserId} ");
                    return -1;
                }
                
            }
            else
                return -1;
           
        }

        
        public IEnumerable<UserDetailsWithBankDetails> FetchAllUsingJoin()
        {
            try
            {
                var userDetailList = _context.UserDetails.Include(u => u.BankDetails).Include(u => u.City).Include(u => u.Country).Include(u => u.Gender).Include(u => u.MaritalStatus).Include(u => u.Nationality).Include(u => u.Role).Include(u => u.State).ToList();
                var bankDetailList = _context.BankDetails.ToList();
                var joinedQuery = from userDetail in userDetailList
                                  join bankDetail in bankDetailList
                                  on userDetail.BankDetailsId equals bankDetail.BankDetailsId
                                  select new UserDetailsWithBankDetails
                                  {
                                      UserDetails = userDetail,
                                      BankDetails = bankDetail
                                  };
                return joinedQuery;
            }
            catch( Exception ex )
            {
                _logger.LogError(ex,"Error occurred while performing Fetch All Using Join operation");
                return Enumerable.Empty<UserDetailsWithBankDetails>();
            }
           
        }

        public IEnumerable<UserDetail> FilterByAnnualIncome(int annualIncome)
        {
            try
            {
                var filteredList = _context.UserDetails.Where(u => u.AnnualIncome > annualIncome).OrderBy(u => u.AnnualIncome).ToList();
                return filteredList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while performing Filter operation");
                return Enumerable.Empty<UserDetail>();
            }

        }
		public IEnumerable<UserDetail> FilterByState(int stateId)
		{
			try
			{
				var filteredList = _context.UserDetails.Include(u => u.State).Where(u => u.State.StateId == stateId).ToList();
				return filteredList;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while performing Filter operation");
				return Enumerable.Empty<UserDetail>();
			}

		}
		public IEnumerable<UserDetail> FilterByAge(int age)
		{
			try
			{
				var today = DateOnly.FromDateTime(DateTime.Today);
				var dateOfBirthCutoff = today.AddYears(-age);

				// Filter the UserDetail objects based on age
				var filteredList = _context.UserDetails
					.Where(u => u.DateOfBirth <= dateOfBirthCutoff)
					.OrderBy(u => u.DateOfBirth)
					.ToList();
				return filteredList;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while performing Filter operation");
				return Enumerable.Empty<UserDetail>();
			}

		}
		public IEnumerable<UserDetail> FilterByMaritalStatus(int maritalStatusId)
		{
			try
			{
				var filteredList = _context.UserDetails.Include(u => u.MaritalStatus).Where(u => u.MaritalStatus.MaritalStatusId ==  maritalStatusId).ToList();
				return filteredList;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while performing Filter operation");
				return Enumerable.Empty<UserDetail>();
			}

		}
		public IEnumerable<UserDetail> FilterByGender(int genderId)
		{
			try
			{
				var filteredList = _context.UserDetails.Include(u => u.Gender).Where(u => u.Gender.GenderId == genderId).ToList();
				return filteredList;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while performing Filter operation");
				return Enumerable.Empty<UserDetail>();
			}

		}

		public IEnumerable<UserDetail> FetchAllUsingLazyLoading()
        {
            try
            {
                return _context.UserDetails;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error occurred while performing Fetch All Using Lazy Loading operation");
                return Enumerable.Empty<UserDetail>();
            }
        }

        public int Transaction(UserDetailsWithBankDetails userDetailsWithBankDetails)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    _context.BankDetails.Add(userDetailsWithBankDetails.BankDetails);
                    _context.SaveChanges();
                    userDetailsWithBankDetails.UserDetails.BankDetailsId = userDetailsWithBankDetails.BankDetails.BankDetailsId;
                    _context.UserDetails.Add(userDetailsWithBankDetails.UserDetails);
                    _context.SaveChanges();
                    scope.Complete();
                    return userDetailsWithBankDetails.UserDetails.UserId;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while performing Transaction operation");
                return -1;
            }

        }

        public int AddUserStockDetails(StocksInfo stocksInfo)
        {
            try
            {
                _context.StockDetails.Add(stocksInfo.StockDetail);
                _context.SaveChanges();
                stocksInfo.UserStockDetail.StockId = stocksInfo.StockDetail.StockId;
                _context.UserStockDetails.Add(stocksInfo.UserStockDetail);
                _context.SaveChanges();
                stocksInfo.OrderDetail.BarCode = barCode();
                stocksInfo.OrderDetail.UserStockId = stocksInfo.UserStockDetail.UserStockId;
                _context.OrderDetails.Add(stocksInfo.OrderDetail);
                _context.SaveChanges();
                return stocksInfo.OrderDetail.OrderId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while adding stock details for userId:{stocksInfo.UserStockDetail.UserId}");
                return -1;
            }
        }
        public string barCode()
        {
            int codeLength = 8;
            Random random = new Random();
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] code = new char[codeLength];
            for (int i = 0; i < codeLength; i++)
            {
                int randomIndex = random.Next(0, characters.Length + 1);
                code[i] = characters[randomIndex];
            }
            return new string(code);
        }

        public void SaveUserDetails()
        {
            try
            {
            _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error occurred while Saving the Changes");
            }
        }

        public IEnumerable<RoleTable> RoleTables()
        {
            return _context.RoleTables;
        }
        public IEnumerable<StateTable> StateTables()
        {
            return _context.StateTables;
        }
        public IEnumerable<MaritalStatusTable> MaritalStatusTables()
        {
            return _context.MaritalStatusTables;
        }
        public IEnumerable<CityTable> CityTables()
        {
            return _context.CityTables;
        }
        public IEnumerable<CountryTable> CountryTables()
        {
            return _context.CountryTables;
        }
        public IEnumerable<GenderTable> GenderTables()
        {
            return _context.GenderTables;
        }
        public IEnumerable<InvestmentTypeTable> InvestmentTypeTables()
        {
            return _context.InvestmentTypeTables;
        }
        public IEnumerable<NationalityTable> NationalityTables()
        {
            return _context.NationalityTables;
        }
        public IEnumerable<StockExchangeTable> StockExchangeTables()
        {
            return _context.StockExchangeTables;
        }
        public bool UserDetailExists(int id)
        {
            return _context.UserDetails.Any(u => u.UserId == id);
        }
        public IEnumerable<UserStockDetail> UserStockDetail(int id)
        {
            
            try
            {
                return _context.UserStockDetails
        .Include(u => u.OrderDetails)
        .Include(u => u.Stock).ThenInclude(u => u.Provider)
        .Include(u => u.Stock).ThenInclude(u => u.Broker)
        .Include(u => u.User).ThenInclude(u => u.Gender)
        .Where(u => u.UserId == id)
        .ToList();
                //return _context.UserStockDetails
                //.Include(u => u.OrderDetails)
                //.Include(u => u.Stock).ThenInclude(u => u.Provider).Include(u => u.Stock).ThenInclude(u => u.Broker)
                //.Include(u => u.User).ThenInclude(u => u.Gender).FirstOrDefault(u => u.UserId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while fetching details for Id:{id}");
                return Enumerable.Empty<UserStockDetail>(); 
            }
        }
        public IEnumerable<UserStockDetail> UserStockDetails()
        {
            return _context.UserStockDetails.Include(u => u.OrderDetails).Include(u => u.Stock).ThenInclude(u => u.Provider).Include(u => u.Stock).ThenInclude(u => u.Broker)
                .ToList();
        }
        
        public IEnumerable<StockProvider> StockProviders()
        {
            return _context.StockProviders;
        }
        public IEnumerable<BrokerDetail> BrokerDetails()
        {
            return _context.BrokerDetails;
        }
        public StockProvider StockProvider(int providerId)
        {
            return _context.StockProviders.Find(providerId);
        }
       
        public BrokerDetail BrokerDetail(int brokerId)
        {
            return _context.BrokerDetails.Find(brokerId);
        }

		public IEnumerable<OrderDetail> Orders()
		{
			return _context.OrderDetails;
		}
	}
}

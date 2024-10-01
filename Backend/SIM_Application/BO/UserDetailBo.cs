using Microsoft.EntityFrameworkCore;
using SIM_Application.Models;
using SIM_Application.Repository;
using System.Configuration.Provider;

namespace SIM_Application.BO
{
    public class UserDetailBo
    {
        private readonly IUserDetailRepository _userDetailRepository;
        public UserDetailBo(IUserDetailRepository userDetailRepository)
        {
            _userDetailRepository = userDetailRepository;
        }
        public IEnumerable<UserDetail> FetchAllUserDetails()
        {
            return _userDetailRepository.FetchAllUserDetails();
        }
        public UserDetail FetchUserDetailsById(int UserId)
        {
            return (_userDetailRepository.FetchUserDetailsById(UserId));
        }
        public IEnumerable<OrderDetail> fetchOrderDetails(int userId)
        {
            return _userDetailRepository.fetchOrderDetails(userId);
        }
        public int AddUserDetails(UserDetail userDetail)
        {
            int i;
            try
            {
                if (userDetail.UserName.Length < 3)
                    return -1;
                i = _userDetailRepository.AddUserDetails(userDetail);
            }
            catch (Exception ex)
            {
                return -1;
            }
            return i;
        }
        public int UpdateUserDetails(UserDetail userDetail)
        {
            int i;
            try
            {
                i = _userDetailRepository.UpdateUserDetails(userDetail);
            }
            catch (Exception ex)
            {
                return -1;
            }
            return i;
        }
        public IEnumerable<UserDetailsWithBankDetails> FetchAllUsingJoin()
        {
            return _userDetailRepository.FetchAllUsingJoin();
        }
        public IEnumerable<UserDetail> FilterByAnnualIncome(int annualIncome)
        {
            return _userDetailRepository.FilterByAnnualIncome(annualIncome);
        }
        public IEnumerable<UserDetail> FilterByState(int stateId)
        {
			return _userDetailRepository.FilterByState(stateId);
		}
        public IEnumerable<UserDetail> FilterByAge(int age)
        {
			return _userDetailRepository.FilterByAge(age);
		}
        public IEnumerable<UserDetail> FilterByMaritalStatus(int maritalStatusId)
        {
			return _userDetailRepository.FilterByMaritalStatus(maritalStatusId);
		}
        public IEnumerable<UserDetail> FilterByGender(int genderId)
        {
            return _userDetailRepository.FilterByGender(genderId);
        }
		public IEnumerable<UserDetail> FetchAllUsingLazyLoading()
        {
            return _userDetailRepository.FetchAllUsingLazyLoading();
        }
        public int DeleteUserDetails(UserDetail userDetail)
        {
            return _userDetailRepository.DeleteUserDetails(userDetail);
        }
        public int Transaction(UserDetailsWithBankDetails userDetailsWithBankDetails)
        {
            return _userDetailRepository.Transaction(userDetailsWithBankDetails);
        }
        public bool UserDetailExists(int id)
        {
            return _userDetailRepository.UserDetailExists(id);
        }
        public IEnumerable<RoleTable> RoleTables()
        {
            return _userDetailRepository.RoleTables();
        }
        public IEnumerable<StateTable> StateTables()
        {
            return _userDetailRepository.StateTables();
        }
        public IEnumerable<MaritalStatusTable> MaritalStatusTables()
        {
            return _userDetailRepository.MaritalStatusTables();
        }
        public IEnumerable<CityTable> CityTables()
        {
            return _userDetailRepository.CityTables();
        }
        public IEnumerable<CountryTable> CountryTables()
        {
            return _userDetailRepository.CountryTables();
        }
        public IEnumerable<GenderTable> GenderTables()
        {
            return _userDetailRepository.GenderTables();
        }
        public IEnumerable<InvestmentTypeTable> InvestmentTypeTables()
        {
            return _userDetailRepository.InvestmentTypeTables();
        }
        public IEnumerable<NationalityTable> NationalityTables()
        {
            return _userDetailRepository.NationalityTables();
        }
        public IEnumerable<StockExchangeTable> StockExchangeTables()
        {
            return _userDetailRepository.StockExchangeTables();
        }
        public IEnumerable<UserStockDetail> UserStockDetail(int userId)
        {
            return (_userDetailRepository.UserStockDetail(userId));
        }
        public IEnumerable<UserStockDetail> UserStockDetails()
        {
            return _userDetailRepository.UserStockDetails();
        }
        public IEnumerable<StockProvider> StockProviders()
        {
            return _userDetailRepository.StockProviders();
        }
        public IEnumerable<BrokerDetail> BrokerDetails()
        {
            return _userDetailRepository.BrokerDetails();
        }
		public IEnumerable<OrderDetail> Orders()
		{
			return _userDetailRepository.Orders();
		}
		public StockProvider StockProvider(int providerId)
        {
            return _userDetailRepository.StockProvider(providerId);
        }

        public BrokerDetail BrokerDetail(int brokerId)
        {
            return _userDetailRepository.BrokerDetail(brokerId);
        }
        public int AddUserStockDetails(StocksInfo stocksInfo)
        {
            return _userDetailRepository.AddUserStockDetails(stocksInfo);
        }
        
    }
}

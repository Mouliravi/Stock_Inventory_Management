using Microsoft.EntityFrameworkCore;
using SIM_Application.Models;

namespace SIM_Application.Repository
{
    public interface IUserDetailRepository
    {
        IEnumerable<UserDetail> FetchAllUserDetails();
        UserDetail FetchUserDetailsById(int UserId);
        public IEnumerable<OrderDetail> fetchOrderDetails(int userId);
        int AddUserDetails(UserDetail userDetail);
        int UpdateUserDetails(UserDetail userDetail);
        int DeleteUserDetails(UserDetail userDetail);
        void SaveUserDetails();
        IEnumerable<UserDetailsWithBankDetails> FetchAllUsingJoin();
		public IEnumerable<UserDetail> FetchAllUsingLazyLoading();
        public IEnumerable<UserDetail> FilterByAnnualIncome(int annualIncome);
		public IEnumerable<UserDetail> FilterByState(int stateId);
		public IEnumerable<UserDetail> FilterByAge(int age);
		public IEnumerable<UserDetail> FilterByMaritalStatus(int maritalStatusId);
		public IEnumerable<UserDetail> FilterByGender(int genderId);
		public bool UserDetailExists(int id);
        public IEnumerable<RoleTable> RoleTables();
        public IEnumerable<StateTable> StateTables();
        public IEnumerable<MaritalStatusTable> MaritalStatusTables();
        public IEnumerable<CityTable> CityTables();
        public IEnumerable<CountryTable> CountryTables();
        public IEnumerable<GenderTable> GenderTables();
        public IEnumerable<InvestmentTypeTable> InvestmentTypeTables();
        public IEnumerable<NationalityTable> NationalityTables();
        public IEnumerable<StockExchangeTable> StockExchangeTables();
        public IEnumerable<UserStockDetail> UserStockDetail(int id);
        public IEnumerable<UserStockDetail> UserStockDetails();
        public IEnumerable<StockProvider> StockProviders();
		public IEnumerable<OrderDetail> Orders();
		public StockProvider StockProvider(int providerId);
        public BrokerDetail BrokerDetail(int brokerId);
        public IEnumerable<BrokerDetail> BrokerDetails();
        public int AddUserStockDetails(StocksInfo stocksInfo);
        public int Transaction(UserDetailsWithBankDetails userDetailsWithBankDetails);
    }
}

using SIM_Application.Models;

namespace SIM_Application.Repository
{
    public interface IBankDetailRepository
    {
        IEnumerable<BankDetail> FetchAllBankDetails();
        BankDetail FetchBankDetailsById(int bankDetailsId);
        int AddBankDetails(BankDetail bankDetail);
        int UpdateBankDetails(BankDetail bankDetail);
        int DeleteBankDetails(BankDetail bankDetail);
        void SaveBankDetails();
        IEnumerable<BankDetail> FetchAllUsingJoin();
        IEnumerable<BankDetail> FetchAllUsingLazyLoading();
        IEnumerable<BankDetail> FilterMethod();
        public bool BankDetailExists(int bankDetailsId);
    }
}

using Microsoft.EntityFrameworkCore;
using SIM_Application.Models;
using SIM_Application.Repository;

namespace SIM_Application.BO
{
    public class BankDetailBo
    {
        private readonly IBankDetailRepository _bankDetailRepository;
        public BankDetailBo(IBankDetailRepository bankDetailRepository)
        {
            _bankDetailRepository = bankDetailRepository;
        }
        public IEnumerable<BankDetail> FetchAllBankDetails()
        {
            return _bankDetailRepository.FetchAllBankDetails();
        }
        public BankDetail FetchBankDetailsById(int BankDetailsId)
        {
            return _bankDetailRepository.FetchBankDetailsById(BankDetailsId);
        }
        public int AddBankDetails(BankDetail bankDetail)
        {
            int i;
            try
            {
                i = _bankDetailRepository.AddBankDetails(bankDetail);
            }
            catch (Exception ex)
            {
                return -1;
            }
            return i;
        }
        public int UpdateBankDetails(BankDetail bankDetail)
        {
            int i;
            try
            {
                i = _bankDetailRepository.UpdateBankDetails(bankDetail);
            }
            catch (Exception ex)
            {
                return -1;
            }
            return i;
        }
        public IEnumerable<BankDetail> FetchAllUsingJoin()
        {
            return (_bankDetailRepository.FetchAllUsingJoin());
        }
        public IEnumerable<BankDetail> FilterMethod()
        {
            return _bankDetailRepository.FilterMethod();
        }
        public IEnumerable<BankDetail> FetchAllUsingLazyLoading()
        {
            return _bankDetailRepository.FetchAllUsingLazyLoading();
        }
        public bool BankDetailExists(int bankDetailsId)
        {
            return _bankDetailRepository.BankDetailExists(bankDetailsId);
        }
        public int DeleteBankDetails(BankDetail bankDetail)
        {
            return _bankDetailRepository.DeleteBankDetails(bankDetail);
        }
    }
}

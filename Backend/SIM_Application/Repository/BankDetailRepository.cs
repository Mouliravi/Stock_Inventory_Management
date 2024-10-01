using Microsoft.EntityFrameworkCore;
using SIM_Application.Controllers.api;
using SIM_Application.Models;

namespace SIM_Application.Repository
{
    
    public class BankDetailRepository : IBankDetailRepository
    {
        private readonly StockInventoryManagementSystemContext _context;
        private readonly ILogger<UserDetailAPIController> _logger;
        public BankDetailRepository(StockInventoryManagementSystemContext context, ILogger<UserDetailAPIController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<BankDetail> FetchAllBankDetails()
        {
            try
            {
                return _context.BankDetails.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Fetching all Bank Details ");
                return Enumerable.Empty<BankDetail>();
            }
        }

        public BankDetail FetchBankDetailsById(int bankDetailsId)
        {
            try
            {
                return _context.BankDetails.Find(bankDetailsId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while fetching Bank detail for Id:{bankDetailsId}");
                return (BankDetail)null;
            }
        }

        public int AddBankDetails(BankDetail bankDetail)
        {
            if (bankDetail != null)
            {
                try
                {
                    _context.BankDetails.Add(bankDetail);
                    SaveBankDetails();
                    return bankDetail.BankDetailsId;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error occurred while Adding Bank Detail with Bank Name: {bankDetail.BankName}");
                    return -1;
                }
            }
            else
                return -1;
        }


        public int UpdateBankDetails(BankDetail bankDetail)
        {
            try
            {
                _context.BankDetails.Update(bankDetail);
                SaveBankDetails();
                return bankDetail.BankDetailsId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurres while Updating the Bank Detail for Id:{bankDetail.BankDetailsId} ");
                return -1;
            }

        }

        public bool BankDetailExists(int bankDetailsId)
        {
            return _context.BankDetails.Any(e => e.BankDetailsId == bankDetailsId);
        }

        public int DeleteBankDetails(BankDetail bankDetail)
        {
            if (bankDetail != null)
            {
                try
                {
                    _context.BankDetails.Remove(bankDetail);
                    SaveBankDetails();
                    return 1;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error occurred while Deleting Bank Detail for Id: {bankDetail.BankDetailsId} ");
                    return -1;
                }

            }
            else
                return -1;
        }
        
        public IEnumerable<BankDetail> FetchAllUsingJoin()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BankDetail> FetchAllUsingLazyLoading()
        {
            try
            {
                return _context.BankDetails;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while performing Fetch All Using Lazy Loading");
                return Enumerable.Empty<BankDetail>();
            }
        }
        

        public IEnumerable<BankDetail> FilterMethod()
        {
            throw new NotImplementedException();
        }

        public void SaveBankDetails()
        {
            _context.SaveChanges();
        }

        
    }
}

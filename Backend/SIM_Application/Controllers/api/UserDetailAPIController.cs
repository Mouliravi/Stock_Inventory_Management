using Microsoft.AspNetCore.Mvc;
using SIM_Application.BO;
using SIM_Application.Repository;
using SIM_Application.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mono.TextTemplating;
//using System.Text.Json.Serialization;
//using System.Text.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIM_Application.Controllers.api
{
    //[EnableCors]
	[EnableCors("AllowOrigin")]
	[Route("api/[controller]")]
    [ApiController]
    public class UserDetailAPIController : ControllerBase
    {

        private readonly UserDetailBo _userDetailBo;
        private readonly BankDetailBo _bankDetailBo;
        private readonly IUserDetailRepository _userDetailRepository;
        private readonly IBankDetailRepository _bankDetailRepository;
        private readonly ILogger<UserDetailAPIController> _logger;
        public UserDetailAPIController(IUserDetailRepository userDetailRepository, IBankDetailRepository _bankDetailRepository, ILogger<UserDetailAPIController> logger)
        {
            _userDetailRepository = userDetailRepository;
            _userDetailBo = new UserDetailBo(userDetailRepository);
            _bankDetailBo = new BankDetailBo(_bankDetailRepository);
            _logger = logger;
        }
        //GET: api/<UserDetailAPIController>
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Get method is triggered from UserDetailsAPIController");
            var userDetailList = _userDetailBo.FetchAllUserDetails();
            var serializedData = JsonConvert.SerializeObject(userDetailList, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            Console.WriteLine("the api {0}", serializedData);
            return Ok(serializedData);
        }



        //public IActionResult Get()
        //{
        //    _logger.LogInformation("Get method is triggered from UserDetailsAPIController");
        //    var userDetailList = _userDetailBo.FetchAllUserDetails().ToArray();
        //    var options = new JsonSerializerOptions
        //    {
        //        ReferenceHandler = ReferenceHandler.Preserve,
        //    };
        //    var json = System.Text.Json.JsonSerializer.Serialize(userDetailList, options);
        //    Console.WriteLine(json);
        //    return Ok(json);
        //}

        // GET api/<UserDetailAPIController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Get method is triggered from UserDetailsAPIController for UserId:{id}");
            var userDetail = _userDetailBo.FetchUserDetailsById(id);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };
            var json = System.Text.Json.JsonSerializer.Serialize(userDetail, options);
            return json == null ? StatusCode(404, new { message = "UserDetail is not found" }) : Ok(json);
        }

        // POST api/<UserDetailAPIController>
        [HttpPost]
        public IActionResult Post([FromBody] UserDetail userDetail)
        {
            _logger.LogInformation("Post method is triggered from UserDetailsAPIController");
            userDetail.UpdatedAt = DateTime.Now;
            userDetail.CreatedAt = DateTime.Now;
            int i = _userDetailBo.AddUserDetails(userDetail);
            if (i > 0)
            {
                return CreatedAtAction(nameof(Get), new { id = i, message = "Created Successfully" });
            }
            else
                return StatusCode(405, new { message = "Failed to insert userDetails" });
        }

        // PUT api/<UserDetailAPIController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, UserDetail userDetail)
        {
            _logger.LogInformation($"Put method is triggered from UserDetailsAPIController for UserId:{id}");
            userDetail.UserId = id;
            userDetail.UpdatedAt = DateTime.Now;
            int i = _userDetailBo.UpdateUserDetails(userDetail);
            if (i > 0)
            {
                return Ok(new { id = i, message = "Successfully Updated" });
            }
            return StatusCode(405, new { message = "Error in updation" });
        }

        // DELETE api/<UserDetailAPIController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"Delete method is triggered from UserDetailsAPIController for UserId:{id}");
            var userDetail = _userDetailBo.FetchUserDetailsById(id);
            int i = _userDetailBo.DeleteUserDetails(userDetail);
            if (i > 0)
            {
                return Ok(new { message = "successfully deleted" });
            }
            else
            {
                return BadRequest(new { message = "Error in Deletion" });
            }

        }
        [HttpGet("filterByAnnualIncome")]
		public IActionResult FilterByAnnualIncome(int annualIncome)
		{
			_logger.LogInformation("Filter Method is triggered from UserDetailsAPIController ");
			var filteredList = _userDetailBo.FilterByAnnualIncome(annualIncome);
			return Ok(filteredList);
			
		}
		[HttpGet("filterByState")]
		public IActionResult FilterByState(int stateId)
		{
			_logger.LogInformation("Get method is triggered from UserDetailsAPIController");
			var filteredList = _userDetailBo.FilterByState(stateId);
			var serializedData = JsonConvert.SerializeObject(filteredList, new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});
			Console.WriteLine("the api {0}", serializedData);
			return Ok(serializedData);

		}
		[HttpGet("filterByGender")]
		public IActionResult FilterByGender(int genderId)
		{
			_logger.LogInformation("Get method is triggered from UserDetailsAPIController");
			var filteredList = _userDetailBo.FilterByGender(genderId);
			var serializedData = JsonConvert.SerializeObject(filteredList, new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});
			Console.WriteLine("the api {0}", serializedData);
			return Ok(serializedData);

		}
		[HttpGet("filterByMaritalStatus")]
		public IActionResult FilterByMaritalStatus(int maritalStatusId)
		{
			_logger.LogInformation("Get method is triggered from UserDetailsAPIController");
			var filteredList = _userDetailBo.FilterByMaritalStatus(maritalStatusId);
			var serializedData = JsonConvert.SerializeObject(filteredList, new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});
			Console.WriteLine("the api {0}", serializedData);
			return Ok(serializedData);
			

		}
		[HttpGet("filterByAge")]
		public IActionResult FilterByAge(int age)
		{
			_logger.LogInformation("Filter Method is triggered from UserDetailsAPIController ");
			var filteredList = _userDetailBo.FilterByAge(age);
			return Ok(filteredList);

		}
		[HttpGet("userOrderDetail")]
		public IActionResult Details(int id)
		{
			////_logger.LogInformation($"Trying to fetch User Detail for User Id:{id}");
			////var userDetail = _userDetailBo.FetchUserDetailsById(id);
			//_logger.LogInformation($"Trying to fetch Order Details for User Id:{id}");
			//var orderDetails = _userDetailBo.fetchOrderDetails(id);
			////_logger.LogInformation($"Order Details are fetched for User Id:{id}");
			////UserDetailsWithOrderDetails userDetailsWithOrderDetails = new UserDetailsWithOrderDetails();
			////userDetailsWithOrderDetails.OrderDetails = orderDetails;
			////userDetailsWithOrderDetails.UserDetail = userDetail;
			//var options = new JsonSerializerOptions
			//{
			//	ReferenceHandler = ReferenceHandler.Ignore,
			//};
			//var json = System.Text.Json.JsonSerializer.Serialize(orderDetails, options);
			//return json == null ? StatusCode(404, new { message = "UserDetail is not found" }) : Ok(json);
			////return Ok(orderDetails);
			
			_logger.LogInformation("Get method is triggered from UserDetailsAPIController");
			var orderDetails = _userDetailBo.fetchOrderDetails(id);
			var serializedData = JsonConvert.SerializeObject(orderDetails, new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});
			Console.WriteLine("the api {0}", serializedData);
			return Ok(serializedData);

		}

		[HttpPost("addStockDetails")]
		public IActionResult AddUserStockDetails(int id, [FromBody] StockInfo stockInfo)
		{
			_logger.LogInformation($"Trying to add Stock Detail for userId;{id}");
			StocksInfo stocksInfo = new StocksInfo();
			var providerDetail = _userDetailBo.StockProvider(stockInfo.ProviderId);
			if (stocksInfo.StockDetail == null)
			{
				stocksInfo.StockDetail = new StockDetail();
			}
			stocksInfo.StockDetail.ProviderId = stockInfo.ProviderId;
			stocksInfo.StockDetail.BrokerId = stockInfo.BrokerId;
			if (stocksInfo.UserStockDetail == null)
			{
				stocksInfo.UserStockDetail = new UserStockDetail();
			}
			stocksInfo.UserStockDetail.UserId = id;
			DateTime dateTime = DateTime.Now;
			DateOnly dateOnly = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
			if (stocksInfo.OrderDetail == null)
			{
				stocksInfo.OrderDetail = new OrderDetail();
			}
			stocksInfo.OrderDetail.CreatedAt = DateTime.Now;
			stocksInfo.OrderDetail.UpdatedAt = DateTime.Now;
			stocksInfo.OrderDetail.OrderDate = dateOnly;
			stocksInfo.OrderDetail.PurchasedQuantity = stockInfo.PurchasedQuantity;
			stocksInfo.OrderDetail.TotalInvestment = stockInfo.PurchasedQuantity * providerDetail.PerStockPrice;
			stocksInfo.OrderDetail.InvestmentTypeId = stockInfo.InvestmentTypeId;
			int i = _userDetailBo.AddUserStockDetails(stocksInfo);
			if (i > 0)
			{
				return CreatedAtAction(nameof(Get), new { id = i, message = "Added Successfully" });
			}
			else
				return StatusCode(405, new { message = "Failed to add add Stock Details" });

		}


		[HttpGet("roles")]
		public IActionResult GetRoles()
		{
			_logger.LogInformation("GetRoles method is triggered from UserDetailsAPIController");
            var roles = _userDetailBo.RoleTables();
			return Ok(roles);
		}

		[HttpGet("nationalities")]
		public IActionResult GetNationalities()
		{
			_logger.LogInformation("GetNationalities method is triggered from UserDetailsAPIController");
			var nationalities = _userDetailBo.NationalityTables();
			return Ok(nationalities);
		}

		[HttpGet("genders")]
		public IActionResult GetGenders()
		{
			_logger.LogInformation("GetGenders method is triggered from UserDetailsAPIController");
			var genders = _userDetailBo.GenderTables();
			return Ok(genders);
		}

		[HttpGet("marritalStatuses")]
		public IActionResult GetMarritalStatuses()
		{
			_logger.LogInformation("GetMarritalStatuses method is triggered from UserDetailsAPIController");
			var marritalStatuses = _userDetailBo.MaritalStatusTables();
			return Ok(marritalStatuses);
		}

		[HttpGet("cities")]
		public IActionResult GetCities()
		{
			_logger.LogInformation("GetCities method is triggered from UserDetailsAPIController");
			var cities = _userDetailBo.CityTables();
			return Ok(cities);
		}


		[HttpGet("states")]
		public IActionResult GetStates()
		{
			_logger.LogInformation("GetStates method is triggered from UserDetailsAPIController");
			var states = _userDetailBo.StateTables();
			return Ok(states);
		}


		[HttpGet("countries")]
		public IActionResult GetCountries()
		{
			_logger.LogInformation("GetCountries method is triggered from UserDetailsAPIController");
			var countries = _userDetailBo.CountryTables();
			return Ok(countries);
		}

		[HttpGet("brokers")]
		public IActionResult GetBrokers()
		{
			_logger.LogInformation("GetCountries method is triggered from UserDetailsAPIController");
			var brokers = _userDetailBo.BrokerDetails();
			return Ok(brokers);
		}

		[HttpGet("providers")]
		public IActionResult GetProviders()
		{
			_logger.LogInformation("GetCountries method is triggered from UserDetailsAPIController");
			var providers = _userDetailBo.StockProviders();
			return Ok(providers);
		}

		[HttpGet("investmentTypes")]
		public IActionResult GetInvestmentTypes()
		{
			_logger.LogInformation("GetCountries method is triggered from UserDetailsAPIController");
			var investmentTypes = _userDetailBo.InvestmentTypeTables();
			return Ok(investmentTypes);
		}

		[HttpGet("orders")]
		public IActionResult GetOrders()
		{
			_logger.LogInformation("GetCountries method is triggered from UserDetailsAPIController");
			var providers = _userDetailBo.Orders();
			return Ok(providers);
		}
	}
}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SIM_Application.BO;
using SIM_Application.Repository;
using System.Text.Json.Serialization;
using System.Text.Json;
using SIM_Application.Models;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIM_Application.Controllers.api
{
	[EnableCors("AllowOrigin")]
	[Route("api/[controller]")]
    [ApiController]
    public class BankDetailAPIController : ControllerBase
    {
		private readonly IBankDetailRepository _bankDetailRepository;
		private readonly BankDetailBo _bankDetailBo;
		private readonly ILogger<UserDetailAPIController> _logger;
		public BankDetailAPIController(IBankDetailRepository bankDetailRepository, ILogger<UserDetailAPIController> logger)
		{
			_bankDetailRepository = bankDetailRepository;
			_bankDetailBo = new BankDetailBo(_bankDetailRepository);
			_logger = logger;
		}
		// GET: api/<BankDetailAPIController>
		[HttpGet]
        public IActionResult Get()
        {
			_logger.LogInformation("Get method is triggered from BankDetailsAPIController");
			var bankDetailsList = _bankDetailBo.FetchAllBankDetails();

			var serializedData = JsonConvert.SerializeObject(bankDetailsList, new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});
			return Ok(serializedData);
		}

        // GET api/<BankDetailAPIController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
			_logger.LogInformation($"Get method is triggered from BankDetailsAPIController for BankDetailsId:{id}");
			var bankDetail = _bankDetailBo.FetchBankDetailsById(id);
			var options = new JsonSerializerOptions
			{
				ReferenceHandler = ReferenceHandler.Preserve,
			};
			var json = System.Text.Json.JsonSerializer.Serialize(bankDetail, options);
			return Ok(json);

		}

		// POST api/<BankDetailAPIController>
		[HttpPost]
		public IActionResult Post([FromBody] BankDetail bankDetail)
		{
			bankDetail.CreatedAt = DateTime.Now;
			bankDetail.UpdatedAt = DateTime.Now;
			_logger.LogInformation("Post method is triggered from BankDetailsAPIController");
			int i = _bankDetailBo.AddBankDetails(bankDetail);
			if (i > 0)
			{
				return CreatedAtAction(nameof(Get), new { id = i, message = "Created Successfully" });
			}
			else
				return StatusCode(405, new { message = "Failed to insert userDetails" });
		}

		// PUT api/<BankDetailAPIController>/5
		[HttpPut("{id}")]
		public IActionResult Put(int id, BankDetail bankDetail)
		{
			_logger.LogInformation($"Put method is triggered from BankDetailsAPIController for BankDetailsId:{id}");
			bankDetail.UpdatedAt = DateTime.Now;
			bankDetail.BankDetailsId = id;
			int i = _bankDetailBo.UpdateBankDetails(bankDetail);
			if (i > 0)
			{
				return Ok(new { id = i, message = "Successfully Updated" });
			}
			return StatusCode(405, new { message = "Error in updation" });
		}

		// DELETE api/<BankDetailAPIController>/5
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_logger.LogInformation($"Delete method is triggered from BankDetailAPIController for UserId:{id}");
			var bankDetail = _bankDetailBo.FetchBankDetailsById(id);
			int i = _bankDetailBo.DeleteBankDetails(bankDetail);
			if (i > 0)
			{
				return Ok(new { message = "successfully deleted" });
			}
			else
			{
				return BadRequest(new { message = "Error in Deletion" });
			}

		}
	}
}

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringValidation.Common.Models;
using StringValidation.Library.Helper;
using StringValidation.Library.Repository;

namespace StringValidation.Library.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class StringValidationController : ControllerBase
	{
        private readonly ILogger<StringValidationController> _logger;
        //private readonly IUserInputRepository _repository;


        public StringValidationController(ILogger<StringValidationController> logger)
		{
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            //_repository = repository ?? throw new ArgumentNullException(nameof(repository)); 
		}

        [HttpPost("UserInput")]
        [ProducesResponseType(StatusCodes.Status200OK)] // Optional: Specify response types
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ValidateInput([NotNull] [FromBody] string input)
        {
            try
            {
                var getResult = await StringValidationHelper.IsValidInput(input);
                
                return Ok(getResult);
            }
            catch(Exception exception)
            {
                _logger.LogError(exception, nameof(ValidateInput));
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        //[HttpGet(Name = "GetInput")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<IEnumerable<UserInput>>> GetUserInputs()
        //{
        //    try
        //    {
        //        var getResult = await _repository.GetAllUserInput().ConfigureAwait(false);

        //        if (getResult == null)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(getResult); // Return 200 OK with the result
        //    }
        //    catch (Exception exception)
        //    {
        //        _logger.Error(exception, nameof(GetUserInputs));
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        //    }
        //}

        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserInput))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> GetUserInput(int id)
        //{
        //    try
        //    {
        //        var result = await _repository.GetUserInput(id).ConfigureAwait(false);

        //        if (result != null)
        //        {
        //            return Ok(result); // Return 200 OK with the result
        //        }
        //        else
        //        {
        //            return BadRequest("User input not found."); // Return 400 Bad Request if result is null or not found
        //        }

        //    }
        //    catch(Exception exception)
        //    {
        //        _logger.Error(exception, nameof(GetUserInputs));
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        //    }
        //}

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK)] // Optional: Specify response types
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Post(UserInput input)
        //{
        //    try
        //    {
        //        var result = await _repository.AddUserInput(input).ConfigureAwait(false);

        //        if (result is OkResult)
        //        {
        //            return Ok(); // Return 200 OK if POST request was successful
        //        }
        //        else if (result is NotFoundResult)
        //        {
        //            return NotFound(); // Return 404 Not Found if resource was not found (optional)
        //        }
        //        else
        //        {
        //            // Handle other error cases here if needed
        //            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        _logger.Error(exception, nameof(Post));
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error"); // Return 500 Internal Server Error with a message
        //    }
        //}

        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)] // Optional: Specify response types
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> DeleteUserInput(int id)
        //{
        //    try
        //    {
        //        var result = await _repository.DeleteUserInput(id).ConfigureAwait(false);

        //        if (result != null)
        //        {
        //            return Ok(result); // Return 200 OK with the result
        //        }
        //        else
        //        {
        //            return BadRequest("User input not found."); // Return 400 Bad Request if result is null or not found
        //        }

        //    }
        //    catch (Exception exception)
        //    {
        //        _logger.Error(exception, nameof(GetUserInputs));
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        //    }
        //}


    }
}


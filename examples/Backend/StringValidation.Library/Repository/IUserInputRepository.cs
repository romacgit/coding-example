using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using StringValidation.Common.Models;

namespace StringValidation.Library.Repository
{
	public interface IUserInputRepository
	{
		Task<ActionResult<bool>> AddUserInput([NotNull] UserInput input);

		Task<ActionResult<UserInput>> GetUserInput(int id);

		Task<ActionResult<IEnumerable<UserInput>>> GetAllUserInput();

		Task<ActionResult<bool>> UpdateUserInput(int id, [NotNull] UserInput newUserInput);

		Task<ActionResult<bool>> DeleteUserInput(int id); 
	}
}


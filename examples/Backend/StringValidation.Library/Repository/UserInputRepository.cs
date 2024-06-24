using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StringValidation.Common.Models;
using StringValidation.Library.Helper;
using StringValidation.Library.Mappings;
using StringValidation.Library.Models;

namespace StringValidation.Library.Repository
{
    public class UserInputRepository : IUserInputRepository
    {
        private readonly DatabaseContext _context;
        private MapperConfiguration _mapperConfiguration;
        private IMapper _mapper;
        private readonly Serilog.ILogger _logger;

        public UserInputRepository(DatabaseContext context, Serilog.ILogger logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<UserInputProfile>());
            _mapper = _mapperConfiguration.CreateMapper();
            _logger = logger;
        }


        public async Task<ActionResult<bool>> AddUserInput([NotNull] UserInput input)
        {
            try
            {
                var dbItem = _mapper.Map<DbUserInput>(input);

                var existingItem = await _context.UserInput.FindAsync(input.Id).ConfigureAwait(false);

                if (existingItem != null)
                {
                    _logger.Information($"{nameof(AddUserInput)} - Input already in Database.");
                    return new ActionResult<bool>(false);
                }

                dbItem.IsValid = await StringValidationHelper.IsValidInput(dbItem.Input);

                await _context.UserInput.AddAsync(dbItem).ConfigureAwait(false);

                var addResult = await _context.SaveChangesAsync().ConfigureAwait(false);

                if (addResult <= 0)
                {
                    _logger.Information($"{nameof(AddUserInput)} - Can not add UserInput to database.");
                    return new ActionResult<bool>(false);
                }
                return new ActionResult<bool>(true);

            }
            catch(Exception exception)
            {
                _logger.Error(exception, $"{nameof(AddUserInput)}", false);
                return new ActionResult<bool>(false);
            }
        }

        public async Task<ActionResult<bool>> DeleteUserInput(int id)
        {
            try
            {

                var existingItem = await _context.UserInput.FindAsync(id).ConfigureAwait(false);

                if (existingItem == null)
                {
                    _logger.Information($"{nameof(DeleteUserInput)} - No such entry in Database found.");
                    return new NotFoundResult();
                }

                _context.UserInput.Remove(existingItem);

                var deleteResult = await _context.SaveChangesAsync().ConfigureAwait(false);

                if (deleteResult <= 0)
                {
                    _logger.Information($"{nameof(DeleteUserInput)} - Could not delete UserInput from database.");
                    return new BadRequestResult();
                }
                return new ActionResult<bool>(true);

            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"{nameof(DeleteUserInput)}", false);
                return new BadRequestResult();
            }
        }

        public async Task<ActionResult<IEnumerable<UserInput>>> GetAllUserInput()
        {
            try
            {
                var userInputs = await _context.UserInput.ToListAsync().ConfigureAwait(false);

                if (userInputs == null || !userInputs.Any())
                {
                    _logger.Information($"{nameof(GetAllUserInput)} - No entries found in Database.");
                    return new NotFoundResult();
                }

                var mappedInputs = userInputs.Select(ui => _mapper.Map<UserInput>(ui));
                return mappedInputs.ToList();
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"{nameof(GetAllUserInput)}");
                return new BadRequestResult();
            }
        }

        public async Task<ActionResult<UserInput>> GetUserInput(int id)
        {
            try
            {
                var dbUserInput = await _context.UserInput.FindAsync(id).ConfigureAwait(false);

                if (dbUserInput == null)
                {
                    _logger.Information($"{nameof(GetUserInput)} - UserInput with Id '{id}' not found.");
                    return new NotFoundResult();
                }

                var userInput = _mapper.Map<UserInput>(dbUserInput);
                return new ActionResult<UserInput>(userInput);
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"{nameof(GetUserInput)}", false);
                return new BadRequestResult();
            }
        }

        public async Task<ActionResult<bool>> UpdateUserInput(int id, [NotNull] UserInput newUserInput)
        {
            try
            {
                var existingItem = await _context.UserInput.FindAsync(id).ConfigureAwait(false);

                if (existingItem == null)
                {
                    _logger.Information($"{nameof(UpdateUserInput)} - UserInput with Id '{id}' not found.");
                    return new NotFoundResult();
                }

                existingItem.Input = newUserInput.Input;
                existingItem.IsValid = await StringValidationHelper.IsValidInput(newUserInput.Input);

                _context.UserInput.Update(existingItem);
                var updateResult = await _context.SaveChangesAsync().ConfigureAwait(false);

                if (updateResult <= 0)
                {
                    _logger.Information($"{nameof(UpdateUserInput)} - Could not update UserInput with Id '{id}'.");
                    return new ActionResult<bool>(false);
                }

                return new ActionResult<bool>(true);
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"{nameof(UpdateUserInput)}", false);
                return new BadRequestResult();
            }
        }
    }
}


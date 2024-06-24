using AutoMapper;
using StringValidation.Common.Models;
using StringValidation.Library.Models;

namespace StringValidation.Library.Mappings
{
	public class UserInputProfile : Profile
	{ 
		public UserInputProfile()
		{
			CreateMap<DbUserInput, UserInput>().ReverseMap();
		}
	}
}


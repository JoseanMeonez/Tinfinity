using AutoMapper;
using Tinfinity.Application.Dtos;
using Tinfinity.Domain.Enums;
using static Tinfinity.Utilities.Utilities;

namespace Tinfinity.Application.Features.User.Queries
{
	public class GetUser
	{
		private readonly IMapper _mapper;

		public GetUser(IMapper mapper)
		{
			_mapper = mapper;
		}

		public async static Task<UserDto> GetUserBasicData(string name)
		{
			UserDto user = null;

			try
			{
				// Here we put the url were the files are staged
				using (FileStream fileStream = new FileStream(
					MainEnums.CharTadsUrl + $"{name}.TAD", FileMode.Open, FileAccess.Read))

				using (StreamReader streamReader = new StreamReader(fileStream))
				{
					// Read the first line of the document
					string tad = await streamReader.ReadLineAsync();

					if (tad != null)
					{
						string character = tad.Substring(88, 18);
						string password = tad.Substring(52, 32);
						user = new UserDto
						{
							UserName = HexToString(bin2hex(character)),
							Password = HexToString(bin2hex(password))
						};
					}
				}
			}
			catch (Exception e)
			{
				user = new UserDto
				{
					Message = $"No se encontró el usuario: {name}",
					Completed = false
				};
			}

			return user;
		}
	}
}

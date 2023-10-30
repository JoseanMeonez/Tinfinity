namespace Tinfinity.Application.Dtos
{
	public class CharacterDto : ResponseDto
	{
		public string Name { get; set; } = null!;
		public string God { get; set; } = null!;
		public string Tribe { get; set; } = null!;
		public int Level { get; set; }
		public string Class { get; set; } = null!;
		public ZoneDto Zone { get; set; } = null!;
		public string RegenerationZone { get; set; } = null!;
		public ChakrasDto Chakras { get; set; } = null!;
		public int Gold { get; set; }
		public int GodPoints { get; set; }
		public int Pet { get; set; }
		public int PetLvl { get; set; }
		public List<EquipmentDto> Equipment { get; set; } = new List<EquipmentDto>();
	}
}

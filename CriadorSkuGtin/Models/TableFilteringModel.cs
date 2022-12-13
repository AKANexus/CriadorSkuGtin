namespace CriadorSkuGtin.Models
{
	public class TableFilteringModel
	{
		//public List<TableFilteringColumn>? columns { get; set; }
		//public List<TableFilteringOrder>? ordering { get; set; }
		public int recordsPerPage { get; set; }
		public int pageNumber { get; set; }
	}

	public class TableFilteringColumn
	{
		public string columnName { get; set; }
		public TableFilteringFilter filter { get; set; }
	}

	public class TableFilteringFilter
	{
		public string initialValue { get; set; }
		public string terminalValue { get; set; }
		public string filterType { get; set; }

		public FilterType? filterTypeEnum
		{
			get
			{
				if (Enum.TryParse(filterType, true, out FilterType result))
				{
					return result;
				}

				return null;
			}
		}

	}

	public enum FilterType {Igual, Contem, ComecaCom, DeAte}

	public class TableFilteringOrder
	{
		public string columnName { get; set; }
		public string direction { get; set; }

		public OrderingDirection? directionEnum
		{
			get
			{
				if (Enum.TryParse(direction, true, out OrderingDirection result))
				{
					return result;
				}

				return OrderingDirection.Ascend;
			}
		}

	}

	public enum OrderingDirection { Ascend, Descend }
}

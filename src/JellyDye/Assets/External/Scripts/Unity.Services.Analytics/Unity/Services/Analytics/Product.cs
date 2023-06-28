using System.Collections.Generic;

namespace Unity.Services.Analytics
{
	public struct Product
	{
		public RealCurrency? RealCurrency;

		public List<VirtualCurrency> VirtualCurrencies;

		public List<Item> Items;
	}
}

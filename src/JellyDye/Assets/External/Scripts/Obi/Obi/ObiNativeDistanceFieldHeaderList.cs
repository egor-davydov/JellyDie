using System;

namespace Obi
{
	[Serializable]
	public class ObiNativeDistanceFieldHeaderList : ObiNativeList<DistanceFieldHeader>
	{
		public ObiNativeDistanceFieldHeaderList(int capacity = 8, int alignment = 16)
		{
			((ObiNativeList<>)(object)this)._002Ector(0, 0);
		}
	}
}

using System;

namespace Obi
{
	[Serializable]
	public class ObiNativeTriangleMeshHeaderList : ObiNativeList<TriangleMeshHeader>
	{
		public ObiNativeTriangleMeshHeaderList(int capacity = 8, int alignment = 16)
		{
			((ObiNativeList<>)(object)this)._002Ector(0, 0);
		}
	}
}

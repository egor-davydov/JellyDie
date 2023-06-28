using UnityEngine;
using UnityEngine.UI;

namespace SRF.UI
{
	[ExecuteInEditMode]
	public class CopyPreferredSize : LayoutElement
	{
		public RectTransform CopySource;

		public float PaddingHeight;

		public float PaddingWidth;

		public override float preferredWidth => 0f;

		public override float preferredHeight => 0f;

		public override int layoutPriority => 0;
	}
}

using System;

namespace Sirenix.OdinInspector
{
	[DontApplyToListElements]
	public class TitleAttribute : Attribute
	{
		public string Title;

		public string Subtitle;

		public bool Bold;

		public bool HorizontalLine;

		public TitleAlignments TitleAlignment;

		public TitleAttribute(string title, string subtitle = null, TitleAlignments titleAlignment = TitleAlignments.Left, bool horizontalLine = true, bool bold = true)
		{
		}
	}
}

using System;
using System.Runtime.CompilerServices;

namespace SRF.Service
{
	public sealed class ServiceAttribute : Attribute
	{
		public Type ServiceType
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		public ServiceAttribute(Type serviceType)
		{
		}
	}
}

using System;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace Gjg.Io.GameConfig.Proto.PostInit.V2.Services
{
	public sealed class PostInitRequest : IMessage<PostInitRequest>, IMessage, IEquatable<PostInitRequest>, IDeepCloneable<PostInitRequest>, IBufferMessage
	{
		private static readonly MessageParser<PostInitRequest> _parser;

		private UnknownFieldSet _unknownFields;

		public const int AdvertisingIdFieldNumber = 1;

		private string advertisingId_;

		public const int AppBundleIdFieldNumber = 2;

		private string appBundleId_;

		public const int OsFieldNumber = 3;

		private string os_;

		public const int BuildNumberFieldNumber = 4;

		private string buildNumber_;

		public const int AppBundleVersionFieldNumber = 5;

		private string appBundleVersion_;

		public const int SdkVersionFieldNumber = 6;

		private string sdkVersion_;

		public const int DeviceIdFieldNumber = 7;

		private string deviceId_;

		public const int GameIdFieldNumber = 8;

		private string gameId_;

		public const int UserIdFieldNumber = 9;

		private string userId_;

		public const int LanguageFieldNumber = 10;

		private string language_;

		public const int IsTabletFieldNumber = 11;

		private bool isTablet_;

		public const int IspFieldNumber = 12;

		private string isp_;

		public const int LocaleFieldNumber = 13;

		private string locale_;

		public const int DeviceModelFieldNumber = 14;

		private string deviceModel_;

		public const int DeviceOsVersionFieldNumber = 15;

		private string deviceOsVersion_;

		public const int TimezoneOffsetFieldNumber = 16;

		private string timezoneOffset_;

		public const int FreeDiskpaceFieldNumber = 17;

		private string freeDiskpace_;

		public const int TotalDiskspaceFieldNumber = 18;

		private string totalDiskspace_;

		public const int AttStatusFieldNumber = 19;

		private long attStatus_;

		public const int FirstEventTimestampFieldNumber = 20;

		private long firstEventTimestamp_;

		public const int SessionNumberFieldNumber = 21;

		private long sessionNumber_;

		[DebuggerNonUserCode]
		public static MessageParser<PostInitRequest> Parser => null;

		[DebuggerNonUserCode]
		public static MessageDescriptor Descriptor => null;

		[DebuggerNonUserCode]
		private MessageDescriptor pb_003A_003AGoogle_002EProtobuf_002EIMessage_002EDescriptor => null;

		[DebuggerNonUserCode]
		public string AdvertisingId
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		[DebuggerNonUserCode]
		public string AppBundleId
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		[DebuggerNonUserCode]
		public string Os
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		[DebuggerNonUserCode]
		public string BuildNumber
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		[DebuggerNonUserCode]
		public string AppBundleVersion
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		[DebuggerNonUserCode]
		public string SdkVersion
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		[DebuggerNonUserCode]
		public string DeviceId
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		[DebuggerNonUserCode]
		public string GameId
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		[DebuggerNonUserCode]
		public string UserId
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		[DebuggerNonUserCode]
		public string Language
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		[DebuggerNonUserCode]
		public bool IsTablet
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		[DebuggerNonUserCode]
		public string Isp
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		[DebuggerNonUserCode]
		public string Locale
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		[DebuggerNonUserCode]
		public string DeviceModel
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		[DebuggerNonUserCode]
		public string DeviceOsVersion
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		[DebuggerNonUserCode]
		public string TimezoneOffset
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		[DebuggerNonUserCode]
		public string FreeDiskpace
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		[DebuggerNonUserCode]
		public string TotalDiskspace
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		[DebuggerNonUserCode]
		public long AttStatus
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		[DebuggerNonUserCode]
		public long FirstEventTimestamp
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		[DebuggerNonUserCode]
		public long SessionNumber
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		[DebuggerNonUserCode]
		public PostInitRequest()
		{
		}

		[DebuggerNonUserCode]
		public PostInitRequest(PostInitRequest other)
		{
		}

		[DebuggerNonUserCode]
		public PostInitRequest Clone()
		{
			return null;
		}

		[DebuggerNonUserCode]
		public override bool Equals(object other)
		{
			return false;
		}

		[DebuggerNonUserCode]
		public bool Equals(PostInitRequest other)
		{
			return false;
		}

		[DebuggerNonUserCode]
		public override int GetHashCode()
		{
			return 0;
		}

		[DebuggerNonUserCode]
		public override string ToString()
		{
			return null;
		}

		[DebuggerNonUserCode]
		public void WriteTo(CodedOutputStream output)
		{
		}

		[DebuggerNonUserCode]
		private void pb_003A_003AGoogle_002EProtobuf_002EIBufferMessage_002EInternalWriteTo(ref WriteContext output)
		{
		}

		[DebuggerNonUserCode]
		public int CalculateSize()
		{
			return 0;
		}

		[DebuggerNonUserCode]
		public void MergeFrom(PostInitRequest other)
		{
		}

		[DebuggerNonUserCode]
		public void MergeFrom(CodedInputStream input)
		{
		}

		[DebuggerNonUserCode]
		private void pb_003A_003AGoogle_002EProtobuf_002EIBufferMessage_002EInternalMergeFrom(ref ParseContext input)
		{
		}
	}
}

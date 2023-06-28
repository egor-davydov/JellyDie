using System;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace Gjg.Io.GameConfig.Proto.CrossPromo.V1.Services
{
	public sealed class GetAdRequest : IMessage<GetAdRequest>, IMessage, IEquatable<GetAdRequest>, IDeepCloneable<GetAdRequest>, IBufferMessage
	{
		private static readonly MessageParser<GetAdRequest> _parser;

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

		public const int SkAdNetworkVersionFieldNumber = 10;

		private string skAdNetworkVersion_;

		public const int SourceAppIdFieldNumber = 11;

		private string sourceAppId_;

		[DebuggerNonUserCode]
		public static MessageParser<GetAdRequest> Parser => null;

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
		public string SkAdNetworkVersion
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
		public string SourceAppId
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
		public GetAdRequest()
		{
		}

		[DebuggerNonUserCode]
		public GetAdRequest(GetAdRequest other)
		{
		}

		[DebuggerNonUserCode]
		public GetAdRequest Clone()
		{
			return null;
		}

		[DebuggerNonUserCode]
		public override bool Equals(object other)
		{
			return false;
		}

		[DebuggerNonUserCode]
		public bool Equals(GetAdRequest other)
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
		public void MergeFrom(GetAdRequest other)
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

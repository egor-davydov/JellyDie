using System;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace Gjg.Io.Iris.App.V1
{
	public sealed class GetAppMonetizationNetworkIDsRequest : IMessage<GetAppMonetizationNetworkIDsRequest>, IMessage, IEquatable<GetAppMonetizationNetworkIDsRequest>, IDeepCloneable<GetAppMonetizationNetworkIDsRequest>, IBufferMessage
	{
		private static readonly MessageParser<GetAppMonetizationNetworkIDsRequest> _parser;

		private UnknownFieldSet _unknownFields;

		public const int IdFieldNumber = 1;

		private string id_;

		[DebuggerNonUserCode]
		public static MessageParser<GetAppMonetizationNetworkIDsRequest> Parser => null;

		[DebuggerNonUserCode]
		public static MessageDescriptor Descriptor => null;

		[DebuggerNonUserCode]
		private MessageDescriptor pb_003A_003AGoogle_002EProtobuf_002EIMessage_002EDescriptor => null;

		[DebuggerNonUserCode]
		public string Id
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
		public GetAppMonetizationNetworkIDsRequest()
		{
		}

		[DebuggerNonUserCode]
		public GetAppMonetizationNetworkIDsRequest(GetAppMonetizationNetworkIDsRequest other)
		{
		}

		[DebuggerNonUserCode]
		public GetAppMonetizationNetworkIDsRequest Clone()
		{
			return null;
		}

		[DebuggerNonUserCode]
		public override bool Equals(object other)
		{
			return false;
		}

		[DebuggerNonUserCode]
		public bool Equals(GetAppMonetizationNetworkIDsRequest other)
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
		public void MergeFrom(GetAppMonetizationNetworkIDsRequest other)
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

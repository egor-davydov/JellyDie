using System;
using System.Diagnostics;
using Gjg.Io.Scio.Core.Event;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace Gjg.Io.Scio.Client.Event
{
	public sealed class AddReceiverNetworkOperation : IMessage<AddReceiverNetworkOperation>, IMessage, IEquatable<AddReceiverNetworkOperation>, IDeepCloneable<AddReceiverNetworkOperation>, IBufferMessage
	{
		private static readonly MessageParser<AddReceiverNetworkOperation> _parser;

		private UnknownFieldSet _unknownFields;

		public const int ReceiverNetworkFieldNumber = 1;

		private ReceiverNetwork receiverNetwork_;

		[DebuggerNonUserCode]
		public static MessageParser<AddReceiverNetworkOperation> Parser => null;

		[DebuggerNonUserCode]
		public static MessageDescriptor Descriptor => null;

		[DebuggerNonUserCode]
		private MessageDescriptor pb_003A_003AGoogle_002EProtobuf_002EIMessage_002EDescriptor => null;

		[DebuggerNonUserCode]
		public ReceiverNetwork ReceiverNetwork
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
		public AddReceiverNetworkOperation()
		{
		}

		[DebuggerNonUserCode]
		public AddReceiverNetworkOperation(AddReceiverNetworkOperation other)
		{
		}

		[DebuggerNonUserCode]
		public AddReceiverNetworkOperation Clone()
		{
			return null;
		}

		[DebuggerNonUserCode]
		public override bool Equals(object other)
		{
			return false;
		}

		[DebuggerNonUserCode]
		public bool Equals(AddReceiverNetworkOperation other)
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
		public void MergeFrom(AddReceiverNetworkOperation other)
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

using System;
using System.Diagnostics;
using Gjg.Io.Scio.Core.Event;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace Gjg.Io.Scio.Client.Event
{
	public sealed class ListSegmentsByBundleIdResponse : IMessage<ListSegmentsByBundleIdResponse>, IMessage, IEquatable<ListSegmentsByBundleIdResponse>, IDeepCloneable<ListSegmentsByBundleIdResponse>, IBufferMessage
	{
		private static readonly MessageParser<ListSegmentsByBundleIdResponse> _parser;

		private UnknownFieldSet _unknownFields;

		public const int SegmentsFieldNumber = 1;

		private static readonly FieldCodec<Segment> _repeated_segments_codec;

		private readonly RepeatedField<Segment> segments_;

		[DebuggerNonUserCode]
		public static MessageParser<ListSegmentsByBundleIdResponse> Parser => null;

		[DebuggerNonUserCode]
		public static MessageDescriptor Descriptor => null;

		[DebuggerNonUserCode]
		private MessageDescriptor pb_003A_003AGoogle_002EProtobuf_002EIMessage_002EDescriptor => null;

		[DebuggerNonUserCode]
		public RepeatedField<Segment> Segments => null;

		[DebuggerNonUserCode]
		public ListSegmentsByBundleIdResponse()
		{
		}

		[DebuggerNonUserCode]
		public ListSegmentsByBundleIdResponse(ListSegmentsByBundleIdResponse other)
		{
		}

		[DebuggerNonUserCode]
		public ListSegmentsByBundleIdResponse Clone()
		{
			return null;
		}

		[DebuggerNonUserCode]
		public override bool Equals(object other)
		{
			return false;
		}

		[DebuggerNonUserCode]
		public bool Equals(ListSegmentsByBundleIdResponse other)
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
		public void MergeFrom(ListSegmentsByBundleIdResponse other)
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

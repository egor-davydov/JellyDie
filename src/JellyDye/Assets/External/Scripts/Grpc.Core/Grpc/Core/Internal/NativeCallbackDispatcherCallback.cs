using System;

namespace Grpc.Core.Internal
{
	internal delegate int NativeCallbackDispatcherCallback(IntPtr tag, IntPtr arg0, IntPtr arg1, IntPtr arg2, IntPtr arg3, IntPtr arg4, IntPtr arg5);
}

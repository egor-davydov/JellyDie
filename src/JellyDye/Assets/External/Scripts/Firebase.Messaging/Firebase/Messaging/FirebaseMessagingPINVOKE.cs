using System;
using System.Runtime.InteropServices;

namespace Firebase.Messaging
{
	internal class FirebaseMessagingPINVOKE
	{
		protected class SWIGExceptionHelper
		{
			public delegate void ExceptionDelegate(string message);

			public delegate void ExceptionArgumentDelegate(string message, string paramName);

			private static ExceptionDelegate applicationDelegate;

			private static ExceptionDelegate arithmeticDelegate;

			private static ExceptionDelegate divideByZeroDelegate;

			private static ExceptionDelegate indexOutOfRangeDelegate;

			private static ExceptionDelegate invalidCastDelegate;

			private static ExceptionDelegate invalidOperationDelegate;

			private static ExceptionDelegate ioDelegate;

			private static ExceptionDelegate nullReferenceDelegate;

			private static ExceptionDelegate outOfMemoryDelegate;

			private static ExceptionDelegate overflowDelegate;

			private static ExceptionDelegate systemDelegate;

			private static ExceptionArgumentDelegate argumentDelegate;

			private static ExceptionArgumentDelegate argumentNullDelegate;

			private static ExceptionArgumentDelegate argumentOutOfRangeDelegate;

			[PreserveSig]
			public static extern void SWIGRegisterExceptionCallbacks_FirebaseMessaging(ExceptionDelegate applicationDelegate, ExceptionDelegate arithmeticDelegate, ExceptionDelegate divideByZeroDelegate, ExceptionDelegate indexOutOfRangeDelegate, ExceptionDelegate invalidCastDelegate, ExceptionDelegate invalidOperationDelegate, ExceptionDelegate ioDelegate, ExceptionDelegate nullReferenceDelegate, ExceptionDelegate outOfMemoryDelegate, ExceptionDelegate overflowDelegate, ExceptionDelegate systemExceptionDelegate);

			[PreserveSig]
			public static extern void SWIGRegisterExceptionCallbacksArgument_FirebaseMessaging(ExceptionArgumentDelegate argumentDelegate, ExceptionArgumentDelegate argumentNullDelegate, ExceptionArgumentDelegate argumentOutOfRangeDelegate);

			private static void SetPendingApplicationException(string message)
			{
			}

			private static void SetPendingArithmeticException(string message)
			{
			}

			private static void SetPendingDivideByZeroException(string message)
			{
			}

			private static void SetPendingIndexOutOfRangeException(string message)
			{
			}

			private static void SetPendingInvalidCastException(string message)
			{
			}

			private static void SetPendingInvalidOperationException(string message)
			{
			}

			private static void SetPendingIOException(string message)
			{
			}

			private static void SetPendingNullReferenceException(string message)
			{
			}

			private static void SetPendingOutOfMemoryException(string message)
			{
			}

			private static void SetPendingOverflowException(string message)
			{
			}

			private static void SetPendingSystemException(string message)
			{
			}

			private static void SetPendingArgumentException(string message, string paramName)
			{
			}

			private static void SetPendingArgumentNullException(string message, string paramName)
			{
			}

			private static void SetPendingArgumentOutOfRangeException(string message, string paramName)
			{
			}

			static SWIGExceptionHelper()
			{
			}
		}

		public class SWIGPendingException
		{
			[ThreadStatic]
			private static Exception pendingException;

			private static int numExceptionsPending;

			private static object exceptionsLock;

			public static void Set(Exception e)
			{
			}

			public static Exception Retrieve()
			{
				return null;
			}

			static SWIGPendingException()
			{
			}
		}

		protected class SWIGStringHelper
		{
			public delegate string SWIGStringDelegate(string message);

			private static SWIGStringDelegate stringDelegate;

			[PreserveSig]
			public static extern void SWIGRegisterStringCallback_FirebaseMessaging(SWIGStringDelegate stringDelegate);

			private static string CreateString(string cString)
			{
				return null;
			}

			static SWIGStringHelper()
			{
			}
		}

		protected static SWIGExceptionHelper swigExceptionHelper;

		protected static SWIGStringHelper swigStringHelper;

		static FirebaseMessagingPINVOKE()
		{
		}

		[PreserveSig]
		public static extern void delete_FirebaseMessage(HandleRef jarg1);

		[PreserveSig]
		public static extern void SetListenerCallbacks(FirebaseMessaging.Listener.MessageReceivedDelegate jarg1, FirebaseMessaging.Listener.TokenReceivedDelegate jarg2);

		[PreserveSig]
		public static extern void SetListenerCallbacksEnabled(bool jarg1, bool jarg2);

		[PreserveSig]
		public static extern void SendPendingEvents();
	}
}

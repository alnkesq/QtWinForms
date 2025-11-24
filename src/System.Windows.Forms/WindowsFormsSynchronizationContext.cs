using System.Threading;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace System.Windows.Forms
{
    public class WindowsFormsSynchronizationContext : SynchronizationContext
    {
        public unsafe override void Post(SendOrPostCallback d, object? state)
        {
            var info = new CallbackInfo{ Callback = d, State = state, CatchException = false };
            var handle = new GCHandle<CallbackInfo>(info);
            NativeMethods.QApplication_InvokeOnMainThread(&InvokeCallback, GCHandle<CallbackInfo>.ToIntPtr(handle));
        }
        
        public unsafe override void Send(SendOrPostCallback d, object? state)
        {
            if (Environment.CurrentManagedThreadId == Application._mainThreadId)
            {
                d(state);
            }
            else
            {
                using (var ev = new ManualResetEventSlim(false))
                {
                    var info = new CallbackInfo { Callback = d, State = state, CatchException = true, Event = ev };
                    var handle = new GCHandle<CallbackInfo>(info);
                    NativeMethods.QApplication_InvokeOnMainThread(&InvokeCallback, GCHandle<CallbackInfo>.ToIntPtr(handle));
                    ev.Wait();
                    if (info.Exception != null)
                        ExceptionDispatchInfo.Capture(info.Exception).Throw();
                }
            }
        }

        public override SynchronizationContext CreateCopy()
        {
            return new WindowsFormsSynchronizationContext();
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static void InvokeCallback(IntPtr state)
        {
            var handle = GCHandle<CallbackInfo>.FromIntPtr(state);
            var info = handle.Target;
            try
            {
                 info.Callback(info.State);
            }
            catch (Exception ex) when (info.CatchException)
            {
                info.Exception = ex;
            }
            finally
            {
                handle.Dispose();
                info.Event?.Set();
            }
        }

        private class CallbackInfo
        {
            public required SendOrPostCallback Callback;
            public required object? State;
            public Exception? Exception;
            public bool CatchException;
            public ManualResetEventSlim? Event;
        }
    }
}

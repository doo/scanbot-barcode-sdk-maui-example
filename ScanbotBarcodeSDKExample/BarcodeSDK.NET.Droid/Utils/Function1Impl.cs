using System;
using Kotlin.Jvm.Functions;

namespace BarcodeSDK.NET.Droid
{
    /**
     * Snippet from: 
     * https://stackoverflow.com/questions/64013415/pass-lambda-function-to-c-sharp-generated-code-of-kotlin-in-xamarin-android-bind
     */
    class Function1Impl<T> : Java.Lang.Object, IFunction1 where T : Java.Lang.Object
    {
        private readonly Action<T> OnInvoked;

        public Function1Impl(Action<T> onInvoked)
        {
            this.OnInvoked = onInvoked;
        }

        public Java.Lang.Object Invoke(Java.Lang.Object objParameter)
        {
            try
            {
                T parameter = (T)objParameter;
                OnInvoked?.Invoke(parameter);
                return null;
            }
            catch (Exception ex)
            {
                // Exception handling, if needed
            }

            return null;
        }
    }
}

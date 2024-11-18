using Polly;
using Polly.Retry;

namespace Policies;

public static class RetryPolicyFactory
{
    static RetryPolicyFactory()
    {
        // Static constructor; can be used to initialize static members, if any
    }

    public static AsyncRetryPolicy CreateAsyncRetryPolicy<T>(int retryCount, Func<int, TimeSpan> sleepDurationProvider) where T : Exception
    {
        // Create and configure the retry policy
        return Policy
            .Handle<T>(ex => ex is T)
            .WaitAndRetryAsync(retryCount, sleepDurationProvider);
       
    }
}
namespace SynchronizatonSamples;

public class Job
{
    private readonly SharedState _sharedState;

    public Job(SharedState sharedState) => _sharedState = sharedState;

    public void DoTheJob()
    {
        for (int i = 0; i < 50000; i++)
        {
            // lock (_sharedState)
            {
                _sharedState.State += 1;
            }
        }
    }
}

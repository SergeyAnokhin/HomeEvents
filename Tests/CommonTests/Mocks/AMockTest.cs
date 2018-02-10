namespace CommonTests.Mocks
{
    public abstract class AMockTest
    {
        protected bool IsUseMock { get; set; }

        protected AMockTest()
        {
            IsUseMock = false;
        }
    }
}

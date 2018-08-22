using Apache.Log.Repository;

namespace Apache.Log.Test.Factories
{
    internal interface IBlacklistedResourceRepositoryFactory
    {
        IBlacklistedResourceRepository New();
    }

    internal class BlacklistedResourceRepositoryFactory : IBlacklistedResourceRepositoryFactory
    {
        public IBlacklistedResourceRepository New()
        {
            var apacheLogContextFactory = new ApacheLogContextFactory();
            return new BlacklistedResourceRepository(apacheLogContextFactory.NewTestContext());
        }
    }
}

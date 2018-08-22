using Apache.Log.Repository;

namespace Apache.Log.Test.Factories
{
    internal interface IWhitelistedResourceRepositoryFactory
    {
        IWhitelistedResourceRepository New();
    }

    internal class WhitelistedResourceRepositoryFactory : IWhitelistedResourceRepositoryFactory
    {
        public IWhitelistedResourceRepository New()
        {
            var apacheLogContextFactory = new ApacheLogContextFactory();
            return new WhitelistedResourceRepository(apacheLogContextFactory.NewTestContext());
        }
    }
}

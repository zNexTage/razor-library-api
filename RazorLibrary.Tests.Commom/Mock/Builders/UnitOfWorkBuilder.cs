using Moq;
using RazorLibrary.Domain.Adapters.Repositories;

namespace RazorLibrary.Tests.Commom.Mock.Builders
{
    public static class UnitOfWorkBuilder
    {
        public static IUnitOfWork Build()
        {
            var unit = new Mock<IUnitOfWork>();

            return unit.Object;
        }
    }
}

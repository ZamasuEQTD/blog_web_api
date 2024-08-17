using Application.Abstractions;
using Application.Abstractions.Data;
using NSubstitute;
using NUnit.Framework;
using SharedKernel.Abstractions;

namespace Tests.Application
{
    abstract public class BaseCommand
    {
        public IUserContext _user;
        public IUnitOfWork _unitOfWork;
        public IDateTimeProvider _timeProvider;
        public BaseCommand()
        {
            _timeProvider = Substitute.For<IDateTimeProvider>();
            _user = Substitute.For<IUserContext>();
            _unitOfWork = Substitute.For<IUnitOfWork>();

            _timeProvider.UtcNow.Returns(DateTime.UtcNow);
        }
    }
}
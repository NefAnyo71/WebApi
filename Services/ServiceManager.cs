using Repositories.Contracts;
using Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging.Abstractions;

namespace Services
{
    public class ServiceManager : IServerManager
    {
        private readonly Lazy<IBookServices> _iBookServices;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _iBookServices = new Lazy<IBookServices>(() => new BookManager(
                repositoryManager, NullLogger.Instance));
        }

        public IBookServices Book => _iBookServices.Value;
    }
}

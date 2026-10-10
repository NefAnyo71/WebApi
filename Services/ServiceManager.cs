using Repositories.Contracts;
using Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging.Abstractions;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookServices> _iBookServices;
        private readonly IRepositoryManager _repositoryManager;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
            _iBookServices = new Lazy<IBookServices>(() => new BookManager(
                repositoryManager, NullLogger.Instance));
        }

        public IBookServices Book => _iBookServices.Value;

        public void Save()
        {
            _repositoryManager.Save();
        }
    }
}

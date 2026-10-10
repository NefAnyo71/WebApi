using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contract
{
    public interface IServiceManager
    {
        IBookServices Book { get; }
        void Save();
    }
}

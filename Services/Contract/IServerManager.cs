using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contract
{
    public interface IServerManager
    {
        IBookServices Book { get; }

    }
}

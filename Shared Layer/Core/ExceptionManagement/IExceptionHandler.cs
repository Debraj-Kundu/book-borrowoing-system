using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLayer.Core.ExceptionManagement
{
    public interface IExceptionHandler
    {
        Exception Process(Exception exception);
    }
}

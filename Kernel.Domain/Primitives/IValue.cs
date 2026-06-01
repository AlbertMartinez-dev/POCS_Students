using System;
using System.Collections.Generic;
using System.Text;

namespace Kernel.Domain.Primitives
{
    public interface IValue<T>
    {
        T Value { get; }
    }
}

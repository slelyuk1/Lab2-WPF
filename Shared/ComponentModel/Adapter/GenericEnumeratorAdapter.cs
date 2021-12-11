using System.Collections;
using System.Collections.Generic;

namespace Shared.ComponentModel.Adapter
{
    public class GenericEnumeratorAdapter<T> : IEnumerator<T>
    {
        private readonly IEnumerator _implementation;

        public GenericEnumeratorAdapter(IEnumerator implementation)
        {
            _implementation = implementation;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            return _implementation.MoveNext();
        }

        public void Reset()
        {
            _implementation.Reset();
        }

        public T Current => ((T) _implementation.Current)!;

        object? IEnumerator.Current => Current;
    }
}
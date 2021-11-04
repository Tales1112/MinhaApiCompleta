using System.Collections.Generic;

namespace DevIo.Testes.Builder
{
    public class BaseBuilder<T> where T : new()
    {
        protected readonly T _instance = new T();
        public T Build() => _instance;
        public List<T> BuildList() => new List<T>() { _instance };
        public T[] BuildArray() => new List<T>() { _instance }.ToArray();
    }
}

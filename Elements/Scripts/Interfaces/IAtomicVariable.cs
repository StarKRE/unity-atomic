namespace Atomic.Elements
{
    public interface IAtomicVariable<T> : IAtomicValue<T>
    {
        new T Value { get; set; }
    }
}
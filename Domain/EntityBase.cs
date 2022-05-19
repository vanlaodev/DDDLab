namespace Domain;

public abstract class EntityBase<T>
{
    public T Id { get; private set; }

    protected EntityBase(T id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        return obj is EntityBase<T> @base &&
               EqualityComparer<T>.Default.Equals(Id, @base.Id);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }
}
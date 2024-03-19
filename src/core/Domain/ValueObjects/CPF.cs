using Domain.Base;

namespace Domain.ValueObjects;

public class CPF : ValueObject 
{
    private CPF(string value)
    {
        _value = value;
    }

    public string _value;
    
    public static implicit operator CPF(string value)
    {
        return new CPF(value);
    }
    
    public static implicit operator string(CPF value)
    {
        return value._value;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }
    
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj is string)
        {
            return (string)obj == _value;
        }

        return true;
    }

    protected bool Equals(CPF other)
    {
        return base.Equals(other) && _value == other._value;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), _value);
    }
}
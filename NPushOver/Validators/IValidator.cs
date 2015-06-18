namespace NPushover.Validators
{
    public interface IValidator<T>
    {
        void Validate(string paramName, T obj);
    }
}

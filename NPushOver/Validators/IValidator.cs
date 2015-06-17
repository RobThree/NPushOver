namespace NPushOver.Validators
{
    public interface IValidator<T>
    {
        void Validate(string paramName, T obj);
    }
}

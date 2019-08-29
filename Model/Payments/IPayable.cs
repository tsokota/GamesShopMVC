namespace Model.Payments
{
    public interface IPayable
    {
        bool Pay(PayArgs args);
    }
}

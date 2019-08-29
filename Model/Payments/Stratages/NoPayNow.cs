namespace Model.Payments.Stratages
{
    public class NoPayNow:IPayable
    {
        public bool Pay(PayArgs args = null)
        {
            return false;
        }
    }
}

namespace Model.Payments
{
    public abstract class PayArgs
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
    }
}

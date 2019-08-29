namespace Model.Payments.Args
{
    public class iboxPayArgs : PayArgs
    {
        public string AccountNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public double Sum { get; set; }
    }
}

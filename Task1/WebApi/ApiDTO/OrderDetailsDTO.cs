namespace Yevhenii_KoliesnikTask1.WebApi.ApiDTO
{
    public class OrderDetailsDTO
    {
        public int OrderDeatilsId { get; set; }

        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public decimal Price { get; set; }

        public short Quantity { get; set; }

        public float? Discount { get; set; }

        public GameDTOeasy Product { get; set; }
    }
}
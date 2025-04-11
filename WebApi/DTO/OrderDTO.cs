namespace WebApi.DTO
{
    public class OrderDto
    {
        public DateTime Date { get; set; }
        public int ClientId { get; set; }

        public List<OrderProductDto> OrderProducts { get; set; } = new List<OrderProductDto>();
    }
}

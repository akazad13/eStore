namespace Exino.Application.CQRS.Product.Dtos
{
    public class ProductCommentDto
    {
        public long Id { get; set; }
        public int Product_Id { get; set; }

        public long UserId { get; set; }
        public string? UserName { get; set; }
        public string? Text { get; set; }
        public string? UserImage { get; set; }
        public string? CreateDate { get; set; }
    }
}

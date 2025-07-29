namespace Foundation.Protocol;

sealed record BookRating(int BookId)
{
    public int Rating { get; set; }
}

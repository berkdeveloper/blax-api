namespace BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete
{
    public class PagedListBaseResponse<T> : ListBaseResponse<T>
    {
        public int PageCount { get; set; }
        public int DataCount { get; set; }
    }
}

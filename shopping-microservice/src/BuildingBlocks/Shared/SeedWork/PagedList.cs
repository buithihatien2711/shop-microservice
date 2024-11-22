namespace Shared.SeedWork
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> items, long totalItem, int pageNumer, int pageSize)
        {
            _metaData = new MetaData()
            {
                TotalItem = totalItem,
                PageSize = pageSize,
                CurrentPage = pageNumer,
                TotalPage = (int)Math.Ceiling(totalItem / (double)pageSize)
            };
            AddRange(items);
        }

        private MetaData _metaData { get; }

        public MetaData GetMetaData()
        {
            return _metaData;
        }
    }
}

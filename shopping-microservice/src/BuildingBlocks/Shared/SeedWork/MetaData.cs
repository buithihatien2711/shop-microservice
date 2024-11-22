﻿namespace Shared.SeedWork
{
    public class MetaData
    {
        public int CurrentPage { get; set; }

        public int TotalPage { get; set; }

        public int PageSize { get; set; }

        public long TotalItem { get; set; }

        public bool HasPrevious => CurrentPage > 1;

        public bool HasNext => CurrentPage < TotalPage;

        public int FirstRowInPage => TotalItem > 0 ? (CurrentPage - 1) * PageSize + 1 : 0;

        public int LastRowOnPage => (int)Math.Min(CurrentPage * PageSize, TotalItem);
    }
}

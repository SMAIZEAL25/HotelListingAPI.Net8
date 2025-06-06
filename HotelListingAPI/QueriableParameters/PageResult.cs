﻿public class PageResult <T>
{
    public int TotalCount { get; set; }

    public int PageNumber { get; set; }

    public int RecordNumber { get; set; }

    // return the list of item or request that the user requested 
    public List <T> Items { get; set; }
}

﻿namespace Book_Task.DTOs.Response
{
    public class BookResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Auther { get; set; } = string.Empty;
        public int Stock { get; set; }

        public int CategoryId { get; set; }
    }
}

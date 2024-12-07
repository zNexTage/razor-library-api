﻿namespace RazorLibrary.Domain.Entities
{
    public class Book
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;

        public string Photo { get; set; } = string.Empty;

        public List<string> Authors { get; set; } = new List<string>();
    }
}

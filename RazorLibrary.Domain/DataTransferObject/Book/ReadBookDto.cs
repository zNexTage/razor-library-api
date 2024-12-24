using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorLibrary.Domain.DataTransferObject.Book
{
    public class ReadBookDto
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;

        public string Photo { get; set; } = string.Empty;

        public List<string> Authors { get; set; } = new();
    }
}

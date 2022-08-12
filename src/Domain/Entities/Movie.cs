using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Movie
{
    public int id { get; set; }

    public string title { get; set; }

    public string description { get; set; }

    public DateTime CreatedDate { get; set; }
}

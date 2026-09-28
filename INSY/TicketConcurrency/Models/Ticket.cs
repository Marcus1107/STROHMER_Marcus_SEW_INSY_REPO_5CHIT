using System.ComponentModel.DataAnnotations;

namespace Models;

public class Ticket
{
    public int Id { get; set; }
    [ConcurrencyCheck]
    public string Name { get; set; } = string.Empty;
}

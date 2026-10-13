using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

[Table("changelog")]
public class ChangelogEntry
{
    [Key]
    [Column("updateId")]
    public int UpdateId { get; set; }

    [Column("oldValue")]
    public int OldValue { get; set; }

    // Der Spaltenname in der bestehenden Datenbank enthält diesen Tippfehler.
    [Column("newVlaue")]
    public int NewValue { get; set; }

    [Column("accountId")]
    public int AccountId { get; set; }

    // Ältere Einträge haben keinen bekannten Änderungszeitpunkt.
    [Column("changedAt", TypeName = "timestamp(6)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime? ChangedAt { get; set; }
}

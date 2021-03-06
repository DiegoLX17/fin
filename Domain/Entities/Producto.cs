using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;
public class Producto
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Descripcion { get; set; }

    [Required]
    public decimal Precio { get; set; }

    [Required]
    public int Cantidad { get; set; }
}

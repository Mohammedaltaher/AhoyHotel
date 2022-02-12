using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class Facility : BaseEntity
{
    public string Name { get; set; }
    public string Icon { get; set; }
}

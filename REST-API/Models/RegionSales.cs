using Microsoft.EntityFrameworkCore;

namespace REST_API.Models
{
    [Keyless]
    public class RegionSales
    {
        public int? region_id { get; set; }
        public int? game_platform_id { get; set; }
        public int? num_sales { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace Pagination_Template.Models
{
	[Table("event")]
	public class Event
	{
        [Column("id")]
        public int Id { get; set; }
        
		[Column("created_on")]
        public DateTime CreatedOn { get; set; }
        
		[Column("description")]
        public string Description { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("image_name")]
        public string ImageName { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("updated_on")]
        public DateTime UpdatedOn { get; set; }

        [Column("person_id")]
        public int PersonId { get; set; }
	}
}

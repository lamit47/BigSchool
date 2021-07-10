namespace BigSchool.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            Attendances = new HashSet<Attendance>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string LectunerId { get; set; }

        [Required(ErrorMessage = "Place không được để trống")]
        [StringLength(255)]
        public string Place { get; set; }

        [Required(ErrorMessage = "DateTime không được để trống")]
        public DateTime DateTimea { get; set; }

        public int CategoryId { get; set; }

        public List<Category> ListCategory = new List<Category>();
        public virtual Category Category { get; set; }
        public string Name;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendance> Attendances { get; set; }
        public string LectureName;
    }
}

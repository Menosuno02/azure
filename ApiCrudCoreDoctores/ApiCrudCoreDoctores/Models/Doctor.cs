using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCrudCoreDoctores.Models
{
    [Table("DOCTOR")]
    public class Doctor
    {
        [Column("HOSPITAL_COD")]
        public int HospitalCod { get; set; }
        [Key]
        [Column("DOCTOR_NO")]
        public int DoctorNo { get; set; }
        [Column("APELLIDO")]
        public string Apellido { get; set; }
        [Column("ESPECIALIDAD")]
        public string Especialidad { get; set; }
        [Column("SALARIO")]
        public int Salario { get; set; }
    }
}

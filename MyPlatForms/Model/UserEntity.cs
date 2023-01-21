using Yu3zx.DapperExtend;
namespace MyPlatForms
{
    [Table("TUser")]
    public class UserEntity : BaseEntity
    {
        [Key]
        [Column("Id")]
        public override long Id { get; set; }

        [Column("UserName")]
        public string UserName { get; set; }//不要与关键字相同

        [Column("Password")]
        public string Pwd { get; set; }

        [Column("Age")]
        public int Age { get; set; }
    }
}

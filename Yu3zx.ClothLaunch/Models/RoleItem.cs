using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Yu3zx.DapperExtend;

namespace Yu3zx.ClothLaunch.Models
{
    [Table("Sys_Role")]
    public class RoleItem : BaseEntityOne
    {

        [Column("OrganizeId")]
        public string OrganizeId
        {
            get;
            set;
        }

        [Column("EnCode")]
        public string EnCode
        {
            get;
            set;
        }

        [Column("Name")]
        public string RoleName
        {
            get;
            set;
        }

        [Column("Remark")]
        public string Remark
        {
            get;
            set;
        }


    }
}

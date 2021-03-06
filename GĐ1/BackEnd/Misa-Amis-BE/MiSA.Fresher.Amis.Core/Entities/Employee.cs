using MiSA.Fresher.Amis.Core.Attributes;
using MiSA.Fresher.Amis.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiSA.Fresher.Amis.Core.Entities
{
    public class Employee
    {
        /// <summary>
        /// Id nhân viên
        /// </summary>
        /// CreateBy: DQDUY(20/12/2021)
        [PrimaryKey]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        /// CreateBy: DQDUY(20/12/2021)
        [NotEmpty]
        [CheckDuplicate]
        [PropertyName("Mã nhân viên")]
        public string? EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        /// CreateBy: DQDUY(20/12/2021)
        [NotEmpty]
        [PropertyName("Tên nhân viên")]
        public string? EmployeeName { get; set; }

        /// <summary>
        /// Ngày sinh nhân viên
        /// </summary>
        /// CreatedBy: DQDUY(20/12/2021)
        [checkDate]
        [PropertyName("Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        /// CreateBy: DQDUY(20/12/2021)
        public Gender Gender { get; set; }

        /// <summary>
        /// Lấy ra giới tính dựa vào tên
        /// </summary>
        /// CreateBy: DQDUY(20/12/2021)
        public string GenderName
        {
            get
            {
                switch (Gender)
                {
                    case Gender.Male:
                        return "Nữ";
                        case Gender.Female:
                        return "Nam";
                    case Gender.Other:
                        return "Khác";
                        default: return null;
                }
            }

        }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        /// CreateBy: DQDUY(20/12/2021)
        [NotEmpty]
        [PropertyName("Phòng ban")]
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Căn cước công dân
        /// </summary>
        /// CreateBy: DQDUY(20/12/2021)
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        /// CreateBy: DQDUY(20/12/2021)
        [checkDate]
        [PropertyName("Ngày cấp chứng minh dân")]
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp chứng minh thư nhân dân
        /// </summary>
        /// CreateBy: DQDUY(20/12/2021)
        public string? IdentityPlace{ get; set; }

        /// <summary>
        /// Chức danh
        /// </summary>
        /// CreateBy: DQDUY(20/12/2021)
        public string? EmployeePosition { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        /// CreateBy: DQDUY(20/12/2021)
        public string? Address { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        /// CreateBy: DQDUY(20/12/2021)
        public string? BankAccountNumber { get; set; }

        /// <summary>
        /// Tên tài khoản
        /// </summary>
        /// CreateBy: DQDUY(20/12/2021)
        public string? BankName { get; set; }

        /// <summary>
        /// Tên chi nhánh ngân hàng
        /// </summary>
        /// CreateBy: DQDUY(20/12/2021)
        public string? BankBranchName { get; set; }

        /// <summary>
        /// Tỉnh ngân hàng
        /// </summary>
        /// CreateBy: DQDUY(20/12/2021)
        public string? BankProvinceName { get; set; }

        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        /// CreateBy: DQDUY(20/12/2021)
        [PropertyName("Số điện thoại")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        /// CreateBy: DQDUY(20/12/2021)
        public string? TelephoneNumber { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// CreateBy: DQDUY(20/12/2021)
        public string? DepartmentName { get; }

        /// <summary>
        /// Email
        /// </summary>
        /// CreateBy: DQDUY(20/12/2021)
        public string? Email { get; set; }

        /// <summary>
        /// Người tạo bản ghi
        /// </summary>
        /// CreateBy: DQDUY(20/12/2021)
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        ///  CreateBy: DQDUY(20/12/2021)
        public DateTime CreatedDate { get;set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        /// CreateBy: DQDUY(20/12/2021)
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        /// CreateBy: DQDUY(20/12/2021)
        public DateTime? ModifiedDate  { get; set; }
    }
}

using MiSA.Fresher.Amis.Core.Attributes;
using MiSA.Fresher.Amis.Core.Entities;
using MiSA.Fresher.Amis.Core.Exceptions;
using MiSA.Fresher.Amis.Core.InterFace.Repository;
using MiSA.Fresher.Amis.Core.InterFace.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiSA.Fresher.Amis.Core.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity>
    {
        #region initialization
        IBaseRepository<TEntity> _baseRepository;
        #endregion
        #region Contructor
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        #endregion
        #region Method
        /// <summary>
        /// Xoá bản ghi
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// CreatedBy: DQDUY (20/12/2021)
        public virtual int Delete(Guid entityId)
        {
            var rowAffect = _baseRepository.Delete(entityId);
            return rowAffect;
        }

        /// <summary>
        /// Lấy toàn bộ bản ghi
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: DQDUY (20/12/2021)
        public IEnumerable<TEntity> GetAll()
        {
            var entities = _baseRepository.GetAll();
            return entities;
        }

        /// <summary>
        /// Lấy bản ghi tương ứng theo Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// CreatedBy: DQDUY (20/12/2021)
        public virtual TEntity GetById(Guid entityId)
        {
            var entity = _baseRepository.GetById(entityId);
            return entity;
        }

        /// <summary>
        /// Thêm bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy: DQDUY (20/12/2021)
        public int? Insert(TEntity entity)
        {
            // validate chung - base xử lý
            var isValid = ValidateObject(entity);
            // Validate đặc thù từng đối tượng
            if (isValid==true)
            {
                isValid = ValidateObjectCustom(entity);
            }
            if (isValid == true)
            {
                var rowAffect = _baseRepository.Insert(entity);
                return rowAffect;
            }
            return null;
        }

        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy: DQDUY (20/12/2021)
        public virtual int? Update(Guid entityId, TEntity entity)
        {

            // validate chung - base xử lý
            var isValid = ValidateObject(entity);
            // Validate đặc thù từng đối tượng
            if (isValid == true)
            {
                isValid = ValidateObjectCustom(entity);
            }
            if (isValid == true)
            {
                var rowAffect = _baseRepository.Update(entityId,entity);
                return rowAffect;
            }
            return null;
        }
        #endregion
        #region Validate
        /// <summary>
        /// Thực hiện validate dùng chung
        /// </summary>
        /// <param name="entity">Đối tượng cần validate</param>
        /// <returns>true hợp lệ, false lỗi</returns>
        /// createBy:DQDUY(20/12/2021)
        bool ValidateObject(TEntity entity)
        {
            // List chứa lỗi
            List<string> errorMsg = new List<string>();
            // Các thông tin bắt buộc nhập
            //1. Lấy ra tất cả các properties của đối tượng
            var properties = typeof(TEntity).GetProperties();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(entity);
                var propertyNameOriginal = property.Name;
                // Lấy ra các propertyName
                var propertyNames = property.GetCustomAttributes(typeof(PropertyName), true);
                // lấy ra các trường không được trống
                var proNotEmptys = property.GetCustomAttributes(typeof(NotEmpty), true);
                // Lấy ra độ dài của kí tự của property
                var propertyMaxLength = property.GetCustomAttributes(typeof(MaxLength), true);
                // Lấy ra attribute Duplicate nếu property đó có khai báo
                var propertyDuplicate = property.GetCustomAttributes(typeof(CheckDuplicate), true);
                // Lấy ra attribute Date nếu property đó có khai báo
                var propertyCheckDate = property.GetCustomAttributes(typeof(checkDate), true);
                // Xem các property đó có có tồn tại PropertyName không
                if (propertyNames.Length > 0)
                {
                    // Thay đổi giá trị cũ của entity gán bằng PropertyName được đặt
                    propertyNameOriginal = ((PropertyName)propertyNames[0]).Name;
                }
                // Xem các property đó có phải là property có phải thuộc tính bắt buộc nhập
                if (proNotEmptys.Length > 0)
                {
                    //3. nếu thông tin bắt buộc nhập hiển thị cảnh báo hoặc đánh giấu trang thái không hợp lệ
                    if (propertyValue == null ||string.IsNullOrEmpty(propertyValue.ToString()))
                    {
                        errorMsg.Add(String.Format(Properties.VNResources.Check_NotEmpty, propertyNameOriginal));
                    }
                }
                if (propertyMaxLength.Length > 0)
                {
                    var length = ((MaxLength)propertyMaxLength[0]).Length;
                    //3. nếu thông tin bắt buộc nhập hiển thị cảnh báo hoặc đánh giấu trang thái không hợp lệ
                    if (propertyValue!=null&&((string)propertyValue).Trim().Length > length)
                    {
                        errorMsg.Add(String.Format(Properties.VNResources.Check_Length, propertyNameOriginal, length));
                    }
                }
                if (propertyDuplicate.Length > 0)
                {
                    if (propertyValue != null)
                    {
                        var checkDuplicate = _baseRepository.GetEntityByProperty(entity, property.Name, propertyValue);
                        if (checkDuplicate != null)
                        {
                            errorMsg.Add(String.Format(Properties.VNResources.Check_Duplicate, propertyNameOriginal));
                        }
                    }
                }
                if (propertyCheckDate.Length > 0)
                {
                    if (propertyValue != null)
                    {
                        if ((DateTime)propertyValue > DateTime.Now)
                        {
                            errorMsg.Add(string.Format(Properties.VNResources.Check_Date_Time, propertyNameOriginal));
                        }
                    }
                }

            }   
            if(errorMsg.Count > 0)
            {
                throw new HttpResponseException(errorMsg);
            }
            return true;
        }
        /// <summary>
        /// Người dùng có thể custom lại validate nếu cần
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <returns></returns>
        /// CreateBy: DQDUY(20/12/2021)
        protected virtual bool ValidateObjectCustom(TEntity entity)
        {
            return true;
        }
       
        #endregion
    }
}

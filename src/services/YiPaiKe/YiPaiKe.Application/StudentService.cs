using Fur.DatabaseAccessor;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using YiPaiKe.Core.Entities;

namespace YiPaiKe.Application
{
    /// <summary>
    /// 学生服务
    /// </summary>
    public class StudentService : ServiceBase<Student>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public StudentService(IRepository<Student> repository) : base(repository)
        {
        }
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public override async Task<List<Student>> GetAll()
        {
            return null;
        }
    }
}

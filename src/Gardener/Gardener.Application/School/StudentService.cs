using Furion.DatabaseAccessor;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gardener.Core.Entites;

namespace Gardener.Application
{
    /// <summary>
    /// 学生服务
    /// </summary>
    [ApiDescriptionSettings("BaseDataServices")]
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
        /// 通过名字获取
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<List<Student>> GetList(string name)
        {
            return null;
        }
    }
}

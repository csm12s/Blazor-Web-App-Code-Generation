// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 每个子项
    /// </summary>
    public class Fill:Entity
    {

        /// <summary>
        /// 老师唯一标识
        /// </summary>
        public string TeacherId { get; set; }
        /// <summary>
        /// 老师名称
        /// </summary>
        public string TeacherName { get; set; }
        /// <summary>
        /// 班级唯一标识
        /// </summary>
        public string ClassesId { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>

        public string ClassName { get; set; }
        /// <summary>
        /// 课程唯一标识
        /// </summary>

        public string CurriculumId { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>

        public string CurriculumName { get; set; }
    }
}

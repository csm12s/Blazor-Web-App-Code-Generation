using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Gardener.Base;

/// <summary>
/// 基础服务定义
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ISqlSugarRepository<T> where T : class, new()
{
	
}
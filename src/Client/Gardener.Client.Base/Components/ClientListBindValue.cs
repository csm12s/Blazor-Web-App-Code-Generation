// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gardener.Client.Base.Components
{
    /// <summary>
    /// 客户端列表绑定-应用于列表
    /// </summary>
    public class ClientListBindValue<Tkey, TValue> where Tkey : notnull
    {
        private readonly TValue _defaultValue;
        /// <summary>
        /// values
        /// </summary>
        private Dictionary<Tkey, TValue> _values = new Dictionary<Tkey, TValue>();

        /// <summary>
        /// 客户端列表绑定-应用于列表
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        public ClientListBindValue(TValue defaultValue)
        {
            this._defaultValue = defaultValue;
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue this[Tkey key]
        {
            get { return GetValue(key); }
            set { SetValue(key, value); }
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue GetValue(Tkey key)
        {
            if (!_values.ContainsKey(key))
            {
                _values.Add(key, _defaultValue);
            }
            return _values[key];
        }
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="key"></param>
        public void SetValue(Tkey key, TValue value)
        {
            if (!_values.ContainsKey(key))
            {
                _values.Add(key, value);
            }
            else
            {
                _values[key] = value;
            }
        }
        /// <summary>
        /// 清除所有项
        /// </summary>
        public void Clear()
        {
            _values.Clear();
        }
        /// <summary>
        /// 设置所有项的值
        /// </summary>
        /// <param name="value"></param>
        public void SetAllValue(TValue? value)
        {
            foreach (var kv in _values)
            {
                _values[kv.Key] = value ?? _defaultValue;
            }
        }
        /// <summary>
        /// 获取指定value的key集合
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEnumerable<Tkey> SelectKeys(TValue value)
        {
            return _values.Where(x => value != null && value.Equals(x.Value)).Select(x => x.Key);
        }
        /// <summary>
        /// 获取所有值-只读，避免外部修改
        /// </summary>
        /// <returns></returns>
        public ReadOnlyDictionary<Tkey, TValue> GetValues()
        {
            return _values.AsReadOnly<Tkey, TValue>();
        }
    }
}

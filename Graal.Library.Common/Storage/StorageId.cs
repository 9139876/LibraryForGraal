using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graal.Library.Common.Storage
{
    /// <summary>
    /// Класс для хранения идентификатора объекта в хранилище
    /// </summary>
    public class StorageId
    {
        /// <summary>
        /// Значение по умолчанию (id неизвестен)
        /// </summary>
        public const int DefaultValue = -1;

        private int value;
        /// <summary>
        /// Получение значения.
        /// </summary>        
        public int Get() => value;

        /// <summary>
        /// Установка значения.
        /// </summary>
        /// <param name="_value">The value.</param>
        /// <exception cref="ArgumentException">Попытка присвоить Id отрицательное значение {_value}</exception>
        /// <exception cref="InvalidOperationException">Попытка замены валидного Id</exception>
        public void Set(int _value)
        {
            if (_value < 0)
                throw new ArgumentException($"Попытка присвоить Id отрицательное значение {_value}");

            if (value != -1)
                throw new InvalidOperationException("Попытка замены валидного Id");

            value = _value;
        }

        public override bool Equals(object obj)
        {
            return obj is StorageId id &&
                   value == id.value;
        }

        public override int GetHashCode()
        {
            return -1584136870 + value.GetHashCode();
        }

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public StorageId()
        {
            value = DefaultValue;
        }

        /// <summary>
        /// Конструктор, применяемый, когда id известен.
        /// </summary>
        /// <param name="_value">Значение id.</param>
        public StorageId(int _value)
        {
            Set(_value);
        }

        public static bool operator ==(StorageId left, StorageId right) => left.Equals(right);

        public static bool operator !=(StorageId left, StorageId right) => (left == right);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graal.Library.Common
{
    public interface IStorageManager
    {
        /// <summary>
        /// Существование соединения с хранилищем
        /// </summary>
        bool StorageStatus { get; }

        /// <summary>
        /// Строковое описание соединения с хранилищем
        /// </summary>
        string StorageStatusS { get; }

        /// <summary>
        /// Изменение настроек хранилища
        /// </summary>
        void ChangeStorageSettings();

        /// <summary>
        /// Событие изменения состояния хранилища
        /// </summary>
        event Action StorageStatusChanged;

        /// <summary>
        /// Существует и корректна ли схема для Грааля
        /// </summary>
        /// <returns></returns>
        bool GraalSchemaExist();

        /// <summary>
        /// Создание схемы для Грааля.
        /// </summary>
        void GraalSchemaCreate();
    }
}

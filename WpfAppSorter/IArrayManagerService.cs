using System;
using System.Collections.Generic;

namespace WpfAppSorter.Interfaces
{
    /// <summary>
    /// Интерфейс для управления массивом
    /// </summary>
    public interface IArrayManagerService
    {
        /// <summary>
        /// Текущий массив
        /// </summary>
        List<object> CurrentArray { get; }

        /// <summary>
        /// Тип данных массива
        /// </summary>
        Type DataType { get; set; }

        /// <summary>
        /// Максимальный размер массива
        /// </summary>
        int MaxSize { get; set; }

        /// <summary>
        /// Флаг инициализации массива
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// Добавляет элемент в массив
        /// </summary>
        /// <param name="element">Элемент для добавления</param>
        /// <returns>True, если элемент добавлен успешно</returns>
        bool AddElement(object element);

        /// <summary>
        /// Удаляет последний элемент из массива
        /// </summary>
        /// <returns>True, если элемент удален успешно</returns>
        bool RemoveLastElement();

        /// <summary>
        /// Очищает массив
        /// </summary>
        void ClearArray();

        /// <summary>
        /// Проверяет, можно ли добавить элемент
        /// </summary>
        /// <returns>True, если можно добавить элемент</returns>
        bool CanAddElement();

        /// <summary>
        /// Проверяет, заполнен ли массив полностью
        /// </summary>
        /// <returns>True, если массив заполнен полностью</returns>
        bool IsArrayFull();

        /// <summary>
        /// Парсит строку в соответствии с типом данных
        /// </summary>
        /// <param name="input">Входная строка</param>
        /// <returns>Распарсенный объект</returns>
        object ParseElement(string input);

        /// <summary>
        /// Получает строковое представление массива
        /// </summary>
        /// <returns>Строковое представление массива</returns>
        string GetArrayDisplay();
    }
}

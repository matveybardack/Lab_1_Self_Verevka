using System;
using System.Collections.Generic;

namespace WpfAppSorter.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с сортировкой массивов
    /// </summary>
    public interface IArraySorterService
    {
        /// <summary>
        /// Сортирует массив пузырьковой сортировкой
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        /// <returns>Отсортированный массив</returns>
        List<object> BubbleSort(List<object> array);

        /// <summary>
        /// Сортирует массив сортировкой выбором
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        /// <returns>Отсортированный массив</returns>
        List<object> SelectionSort(List<object> array);

        /// <summary>
        /// Сортирует массив сортировкой вставками
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        /// <returns>Отсортированный массив</returns>
        List<object> InsertionSort(List<object> array);

        /// <summary>
        /// Сортирует массив быстрой сортировкой
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        /// <returns>Отсортированный массив</returns>
        List<object> QuickSort(List<object> array);

        /// <summary>
        /// Сравнивает два объекта
        /// </summary>
        /// <param name="a">Первый объект</param>
        /// <param name="b">Второй объект</param>
        /// <returns>Результат сравнения</returns>
        int CompareObjects(object a, object b);
    }
}

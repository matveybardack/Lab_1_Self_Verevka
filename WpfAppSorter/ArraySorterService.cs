using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppSorter.Interfaces;
using ClassLibrarySorter;

namespace WpfAppSorter.Services
{
    /// <summary>
    /// Сервис для сортировки массивов
    /// </summary>
    public class ArraySorterService : IArraySorterService
    {
        private readonly ArraySorter _arraySorter;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ArraySorterService()
        {
            _arraySorter = new ArraySorter();
        }

        /// <summary>
        /// Сортирует массив пузырьковой сортировкой
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        /// <returns>Отсортированный массив</returns>
        public List<object> BubbleSort(List<object> array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            return _arraySorter.BubbleSort(array);
        }

        /// <summary>
        /// Сортирует массив сортировкой выбором
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        /// <returns>Отсортированный массив</returns>
        public List<object> SelectionSort(List<object> array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            return _arraySorter.SelectionSort(array);
        }

        /// <summary>
        /// Сортирует массив сортировкой вставками
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        /// <returns>Отсортированный массив</returns>
        public List<object> InsertionSort(List<object> array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            return _arraySorter.InsertionSort(array);
        }

        /// <summary>
        /// Сортирует массив быстрой сортировкой
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        /// <returns>Отсортированный массив</returns>
        public List<object> QuickSort(List<object> array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            return _arraySorter.QuickSort(array);
        }

        /// <summary>
        /// Сравнивает два объекта
        /// </summary>
        /// <param name="a">Первый объект</param>
        /// <param name="b">Второй объект</param>
        /// <returns>Результат сравнения</returns>
        public int CompareObjects(object a, object b)
        {
            return _arraySorter.CompareObjects(a, b);
        }
    }
}

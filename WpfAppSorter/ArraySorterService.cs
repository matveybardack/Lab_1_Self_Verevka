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
            return _arraySorter.BubbleSort(array);
        }

        /// <summary>
        /// Сортирует массив сортировкой выбором
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        /// <returns>Отсортированный массив</returns>
        public List<object> SelectionSort(List<object> array)
        {
            return _arraySorter.SelectionSort(array);
        }

        /// <summary>
        /// Сортирует массив сортировкой вставками
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        /// <returns>Отсортированный массив</returns>
        public List<object> InsertionSort(List<object> array)
        {
            return _arraySorter.InsertionSort(array);
        }

        /// <summary>
        /// Сортирует массив быстрой сортировкой
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        /// <returns>Отсортированный массив</returns>
        public List<object> QuickSort(List<object> array)
        {
            return _arraySorter.QuickSort(array);
        }

        
    }
}

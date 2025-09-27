using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrarySorter
{
    /// <summary>
    /// Класс для сортировки массивов различными алгоритмами
    /// </summary>
    public class ArraySorter
    {
        /// <summary>
        /// Сортирует массив пузырьковой сортировкой
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        /// <returns>Отсортированный массив</returns>
        public List<object> BubbleSort(List<object> array)
        {
            if (array == null || array.Count <= 1)
                return new List<object>(array);

            var sortedArray = new List<object>(array);
            
            for (int i = 0; i < sortedArray.Count - 1; i++)
            {
                for (int j = 0; j < sortedArray.Count - 1 - i; j++)
                {
                    if (CompareObjects(sortedArray[j], sortedArray[j + 1]) > 0)
                    {
                        object temp = sortedArray[j];
                        sortedArray[j] = sortedArray[j + 1];
                        sortedArray[j + 1] = temp;
                    }
                }
            }
            
            return sortedArray;
        }

        /// <summary>
        /// Сортирует массив сортировкой выбором
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        /// <returns>Отсортированный массив</returns>
        public List<object> SelectionSort(List<object> array)
        {
            if (array == null || array.Count <= 1)
                return new List<object>(array);

            var sortedArray = new List<object>(array);
            
            for (int i = 0; i < sortedArray.Count - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < sortedArray.Count; j++)
                {
                    if (CompareObjects(sortedArray[j], sortedArray[minIndex]) < 0)
                    {
                        minIndex = j;
                    }
                }
                if (minIndex != i)
                {
                    object temp = sortedArray[i];
                    sortedArray[i] = sortedArray[minIndex];
                    sortedArray[minIndex] = temp;
                }
            }
            
            return sortedArray;
        }

        /// <summary>
        /// Сортирует массив сортировкой вставками
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        /// <returns>Отсортированный массив</returns>
        public List<object> InsertionSort(List<object> array)
        {
            if (array == null || array.Count <= 1)
                return new List<object>(array);

            var sortedArray = new List<object>(array);
            
            for (int i = 1; i < sortedArray.Count; i++)
            {
                object key = sortedArray[i];
                int j = i - 1;
                while (j >= 0 && CompareObjects(sortedArray[j], key) > 0)
                {
                    sortedArray[j + 1] = sortedArray[j];
                    j--;
                }
                sortedArray[j + 1] = key;
            }
            
            return sortedArray;
        }

        /// <summary>
        /// Сортирует массив быстрой сортировкой
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        /// <returns>Отсортированный массив</returns>
        public List<object> QuickSort(List<object> array)
        {
            if (array == null || array.Count <= 1)
                return new List<object>(array);

            var sortedArray = new List<object>(array);
            QuickSortInternal(sortedArray, 0, sortedArray.Count - 1);
            return sortedArray;
        }

        /// <summary>
        /// Внутренний метод быстрой сортировки
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        /// <param name="low">Нижний индекс</param>
        /// <param name="high">Верхний индекс</param>
        private void QuickSortInternal(List<object> array, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(array, low, high);
                QuickSortInternal(array, low, pivotIndex - 1);
                QuickSortInternal(array, pivotIndex + 1, high);
            }
        }

        /// <summary>
        /// Разделение массива для быстрой сортировки
        /// </summary>
        /// <param name="array">Массив</param>
        /// <param name="low">Нижний индекс</param>
        /// <param name="high">Верхний индекс</param>
        /// <returns>Индекс опорного элемента</returns>
        private int Partition(List<object> array, int low, int high)
        {
            object pivot = array[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (CompareObjects(array[j], pivot) <= 0)
                {
                    i++;
                    object temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }

            object temp2 = array[i + 1];
            array[i + 1] = array[high];
            array[high] = temp2;

            return i + 1;
        }

        /// <summary>
        /// Сравнивает два объекта
        /// </summary>
        /// <param name="a">Первый объект</param>
        /// <param name="b">Второй объект</param>
        /// <returns>Результат сравнения</returns>
        public int CompareObjects(object a, object b)
        {
            if (a == null && b == null) return 0;
            if (a == null) return -1;
            if (b == null) return 1;

            if (a is IComparable comparableA && b is IComparable comparableB)
            {
                return comparableA.CompareTo(comparableB);
            }
            
            return 0;
        }
    }
}

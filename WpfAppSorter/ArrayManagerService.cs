using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppSorter.Interfaces;
using WpfAppSorter.Models;

namespace WpfAppSorter.Services
{
    /// <summary>
    /// Сервис для управления массивом
    /// </summary>
    public class ArrayManagerService : IArrayManagerService
    {
        private List<object> currentArray;
        private Type dataType;
        private int maxSize;
        private bool isInitialized;

        /// <summary>
        /// Текущий массив
        /// </summary>
        public List<object> CurrentArray => new List<object>(currentArray);

        /// <summary>
        /// Тип данных массива
        /// </summary>
        public Type DataType
        {
            get => dataType;
            set
            {
                if (!isInitialized)
                {
                    dataType = value;
                }
            }
        }

        /// <summary>
        /// Максимальный размер массива
        /// </summary>
        public int MaxSize
        {
            get => maxSize;
            set
            {
                if (!isInitialized)
                {
                    maxSize = value;
                }
            }
        }

        /// <summary>
        /// Флаг инициализации массива
        /// </summary>
        public bool IsInitialized => isInitialized;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ArrayManagerService()
        {
            currentArray = new List<object>();
            dataType = typeof(int);
            maxSize = 5;
            isInitialized = false;
        }

        /// <summary>
        /// Добавляет элемент в массив
        /// </summary>
        /// <param name="element">Элемент для добавления</param>
        /// <returns>True, если элемент добавлен успешно</returns>
        public bool AddElement(object element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (!CanAddElement())
                return false;

            currentArray.Add(element);
            
            // После добавления первого элемента блокируем изменение типа и размера
            if (currentArray.Count == 1)
            {
                isInitialized = true;
            }

            return true;
        }

        /// <summary>
        /// Удаляет последний элемент из массива
        /// </summary>
        /// <returns>True, если элемент удален успешно</returns>
        public bool RemoveLastElement()
        {
            if (currentArray.Count == 0)
                return false;

            currentArray.RemoveAt(currentArray.Count - 1);
            
            // Если массив стал пустым, разблокируем изменение типа и размера
            if (currentArray.Count == 0)
            {
                isInitialized = false;
            }

            return true;
        }

        /// <summary>
        /// Очищает массив
        /// </summary>
        public void ClearArray()
        {
            currentArray.Clear();
            isInitialized = false;
        }

        /// <summary>
        /// Проверяет, можно ли добавить элемент
        /// </summary>
        /// <returns>True, если можно добавить элемент</returns>
        public bool CanAddElement()
        {
            return currentArray.Count < maxSize;
        }

        /// <summary>
        /// Проверяет, заполнен ли массив полностью
        /// </summary>
        /// <returns>True, если массив заполнен полностью</returns>
        public bool IsArrayFull()
        {
            return currentArray.Count >= maxSize;
        }

        /// <summary>
        /// Парсит строку в соответствии с типом данных
        /// </summary>
        /// <param name="input">Входная строка</param>
        /// <returns>Распарсенный объект</returns>
        public object ParseElement(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Входная строка не может быть пустой", nameof(input));

            try
            {
                return ClassLibrarySorter.ValueParser.ParseValue(input, dataType);
            }
            catch (FormatException ex)
            {
                throw new FormatException($"Не удалось преобразовать '{input}' в тип {dataType.Name}", ex);
            }
        }

        /// <summary>
        /// Получает строковое представление массива
        /// </summary>
        /// <returns>Строковое представление массива</returns>
        public string GetArrayDisplay()
        {
            if (currentArray.Count == 0)
            {
                return "Массив пуст";
            }
            else
            {
                return $"Массив ({currentArray.Count} элементов):\n" + 
                       string.Join(", ", currentArray);
            }
        }
    }
}

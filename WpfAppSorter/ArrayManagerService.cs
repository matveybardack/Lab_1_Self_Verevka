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
        private List<object> _currentArray;
        private Type _dataType;
        private int _maxSize;
        private bool _isInitialized;

        /// <summary>
        /// Текущий массив
        /// </summary>
        public List<object> CurrentArray => new List<object>(_currentArray);

        /// <summary>
        /// Тип данных массива
        /// </summary>
        public Type DataType
        {
            get => _dataType;
            set
            {
                if (!_isInitialized)
                {
                    _dataType = value;
                }
            }
        }

        /// <summary>
        /// Максимальный размер массива
        /// </summary>
        public int MaxSize
        {
            get => _maxSize;
            set
            {
                if (!_isInitialized)
                {
                    _maxSize = value;
                }
            }
        }

        /// <summary>
        /// Флаг инициализации массива
        /// </summary>
        public bool IsInitialized => _isInitialized;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ArrayManagerService()
        {
            _currentArray = new List<object>();
            _dataType = typeof(int);
            _maxSize = 5;
            _isInitialized = false;
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

            _currentArray.Add(element);
            
            // После добавления первого элемента блокируем изменение типа и размера
            if (_currentArray.Count == 1)
            {
                _isInitialized = true;
            }

            return true;
        }

        /// <summary>
        /// Удаляет последний элемент из массива
        /// </summary>
        /// <returns>True, если элемент удален успешно</returns>
        public bool RemoveLastElement()
        {
            if (_currentArray.Count == 0)
                return false;

            _currentArray.RemoveAt(_currentArray.Count - 1);
            
            // Если массив стал пустым, разблокируем изменение типа и размера
            if (_currentArray.Count == 0)
            {
                _isInitialized = false;
            }

            return true;
        }

        /// <summary>
        /// Очищает массив
        /// </summary>
        public void ClearArray()
        {
            _currentArray.Clear();
            _isInitialized = false;
        }

        /// <summary>
        /// Проверяет, можно ли добавить элемент
        /// </summary>
        /// <returns>True, если можно добавить элемент</returns>
        public bool CanAddElement()
        {
            return _currentArray.Count < _maxSize;
        }

        /// <summary>
        /// Проверяет, заполнен ли массив полностью
        /// </summary>
        /// <returns>True, если массив заполнен полностью</returns>
        public bool IsArrayFull()
        {
            return _currentArray.Count >= _maxSize;
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
                return ClassLibrarySorter.ValueParser.ParseValue(input, _dataType);
            }
            catch (FormatException ex)
            {
                throw new FormatException($"Не удалось преобразовать '{input}' в тип {_dataType.Name}", ex);
            }
        }

        /// <summary>
        /// Получает строковое представление массива
        /// </summary>
        /// <returns>Строковое представление массива</returns>
        public string GetArrayDisplay()
        {
            if (_currentArray.Count == 0)
            {
                return "Массив пуст";
            }
            else
            {
                return $"Массив ({_currentArray.Count} элементов):\n" + 
                       string.Join(", ", _currentArray);
            }
        }
    }
}

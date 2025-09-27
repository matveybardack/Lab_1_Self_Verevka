using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrarySorter
{
    /// <summary>
    /// Класс для работы с файлами и директориями
    /// </summary>
    public class DirectoryViewer
    {
        private List<string> _trackedFiles;

        /// <summary>
        /// Конструктор
        /// </summary>
        public DirectoryViewer()
        {
            _trackedFiles = new List<string>();
        }

        /// <summary>
        /// Сохраняет массив в файл
        /// </summary>
        /// <param name="array">Массив для сохранения</param>
        /// <param name="filePath">Путь к файлу</param>
        public void SaveArrayToFile(List<object> array, string filePath)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("Путь к файлу не может быть пустым", nameof(filePath));

            try
            {
                string content = string.Join(Environment.NewLine, array);
                File.WriteAllText(filePath, content);
                
                // Добавляем файл в список отслеживаемых, если его там нет
                if (!_trackedFiles.Contains(filePath))
                {
                    _trackedFiles.Add(filePath);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Ошибка при сохранении файла: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Загружает массив из файла
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="dataType">Тип данных массива</param>
        /// <returns>Загруженный массив</returns>
        public List<object> LoadArrayFromFile(string filePath, Type dataType)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("Путь к файлу не может быть пустым", nameof(filePath));
            
            if (dataType == null)
                throw new ArgumentNullException(nameof(dataType));

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Файл не найден: {filePath}");

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                var array = new List<object>();
                
                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        object parsedValue = ParseValue(line, dataType);
                        array.Add(parsedValue);
                    }
                }
                
                // Добавляем файл в список отслеживаемых, если его там нет
                if (!_trackedFiles.Contains(filePath))
                {
                    _trackedFiles.Add(filePath);
                }
                
                return array;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Ошибка при загрузке файла: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Добавляет файл в список отслеживаемых файлов
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        public void AddFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("Путь к файлу не может быть пустым", nameof(filePath));

            if (!_trackedFiles.Contains(filePath))
            {
                _trackedFiles.Add(filePath);
            }
        }

        /// <summary>
        /// Удаляет файл из списка отслеживаемых файлов
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        public void RemoveFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("Путь к файлу не может быть пустым", nameof(filePath));

            _trackedFiles.Remove(filePath);
        }

        /// <summary>
        /// Получает список всех отслеживаемых файлов
        /// </summary>
        /// <returns>Список путей к файлам</returns>
        public List<string> GetTrackedFiles()
        {
            return new List<string>(_trackedFiles);
        }

        /// <summary>
        /// Очищает список отслеживаемых файлов
        /// </summary>
        public void ClearTrackedFiles()
        {
            _trackedFiles.Clear();
        }

        /// <summary>
        /// Парсит значение в соответствии с типом данных
        /// </summary>
        /// <param name="value">Строковое значение</param>
        /// <param name="dataType">Тип данных</param>
        /// <returns>Распарсенный объект</returns>
        private object ParseValue(string value, Type dataType)
        {
            try
            {
                if (dataType == typeof(int))
                {
                    return int.Parse(value);
                }
                else if (dataType == typeof(float))
                {
                    return float.Parse(value);
                }
                else if (dataType == typeof(DateTime))
                {
                    return DateTime.Parse(value);
                }
                else
                {
                    throw new ArgumentException($"Неподдерживаемый тип данных: {dataType.Name}");
                }
            }
            catch (FormatException ex)
            {
                throw new FormatException($"Не удалось преобразовать '{value}' в тип {dataType.Name}", ex);
            }
        }
    }
}

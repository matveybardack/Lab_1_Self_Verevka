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
                        object parsedValue = ValueParser.ParseValue(line, dataType);
                        array.Add(parsedValue);
                    }
                }
                
                return array;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Ошибка при загрузке файла: {ex.Message}", ex);
            }
        }
    }
}

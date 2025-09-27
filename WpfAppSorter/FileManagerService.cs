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
    /// Сервис для работы с файлами
    /// </summary>
    public class FileManagerService : IFileManagerService
    {
        private readonly DirectoryViewer _directoryViewer;

        /// <summary>
        /// Конструктор
        /// </summary>
        public FileManagerService()
        {
            _directoryViewer = new DirectoryViewer();
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

            _directoryViewer.SaveArrayToFile(array, filePath);
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

            return _directoryViewer.LoadArrayFromFile(filePath, dataType);
        }

        /// <summary>
        /// Добавляет файл в список отслеживаемых файлов
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        public void AddFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("Путь к файлу не может быть пустым", nameof(filePath));

            _directoryViewer.AddFile(filePath);
        }

        /// <summary>
        /// Удаляет файл из списка отслеживаемых файлов
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        public void RemoveFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("Путь к файлу не может быть пустым", nameof(filePath));

            _directoryViewer.RemoveFile(filePath);
        }

        /// <summary>
        /// Получает список всех отслеживаемых файлов
        /// </summary>
        /// <returns>Список путей к файлам</returns>
        public List<string> GetTrackedFiles()
        {
            return _directoryViewer.GetTrackedFiles();
        }

        /// <summary>
        /// Очищает список отслеживаемых файлов
        /// </summary>
        public void ClearTrackedFiles()
        {
            _directoryViewer.ClearTrackedFiles();
        }
    }
}

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
        private readonly DirectoryViewer directoryViewer;
        private readonly List<string> trackedFiles = new List<string>();

        /// <summary>
        /// Конструктор
        /// </summary>
        public FileManagerService()
        {
            directoryViewer = new DirectoryViewer();
        }

        /// <summary>
        /// Сохраняет массив в файл
        /// </summary>
        /// <param name="array">Массив для сохранения</param>
        /// <param name="filePath">Путь к файлу</param>
        public void SaveArrayToFile(List<object> array, string filePath)
        {
            directoryViewer.SaveArrayToFile(array, filePath);
            if (!trackedFiles.Contains(filePath))
            {
                trackedFiles.Add(filePath);
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
            var result = directoryViewer.LoadArrayFromFile(filePath, dataType);
            if (!trackedFiles.Contains(filePath))
            {
                trackedFiles.Add(filePath);
            }
            return result;
        }

        /// <summary>
        /// Добавляет файл в список отслеживаемых файлов
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        public void AddFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("Путь к файлу не может быть пустым", nameof(filePath));

            if (!trackedFiles.Contains(filePath))
            {
                trackedFiles.Add(filePath);
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

            trackedFiles.Remove(filePath);
        }

        /// <summary>
        /// Получает список всех отслеживаемых файлов
        /// </summary>
        /// <returns>Список путей к файлам</returns>
        public List<string> GetTrackedFiles()
        {
            return new List<string>(trackedFiles);
        }
    }
}

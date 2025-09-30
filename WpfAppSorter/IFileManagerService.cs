using System;
using System.Collections.Generic;

namespace WpfAppSorter.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с файлами
    /// </summary>
    public interface IFileManagerService
    {
        /// <summary>
        /// Сохраняет массив в файл
        /// </summary>
        /// <param name="array">Массив для сохранения</param>
        /// <param name="filePath">Путь к файлу</param>
        void SaveArrayToFile(List<object> array, string filePath);

        /// <summary>
        /// Загружает массив из файла
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="dataType">Тип данных массива</param>
        /// <returns>Загруженный массив</returns>
        List<object> LoadArrayFromFile(string filePath, Type dataType);

        /// <summary>
        /// Добавляет файл в список отслеживаемых файлов
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        void AddFile(string filePath);

        /// <summary>
        /// Удаляет файл из списка отслеживаемых файлов
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        void RemoveFile(string filePath);

        /// <summary>
        /// Получает список всех отслеживаемых файлов
        /// </summary>
        /// <returns>Список путей к файлам</returns>
        List<string> GetTrackedFiles();
    }
}

using System;

namespace WpfAppSorter.Models
{
    /// <summary>
    /// Перечисление типов данных для массива
    /// </summary>
    public enum ArrayDataType
    {
        Integer,
        Float,
        DateTime
    }

    /// <summary>
    /// Модель для работы с типами данных массива
    /// </summary>
    public static class ArrayDataTypes
    {
        /// <summary>
        /// Получает тип .NET по перечислению
        /// </summary>
        /// <param name="dataType">Тип данных</param>
        /// <returns>Тип .NET</returns>
        public static Type GetNetType(ArrayDataType dataType)
        {
            switch (dataType)
            {
                case ArrayDataType.Integer:
                    return typeof(int);
                case ArrayDataType.Float:
                    return typeof(float);
                case ArrayDataType.DateTime:
                    return typeof(DateTime);
                default:
                    throw new ArgumentException($"Неподдерживаемый тип данных: {dataType}");
            }
        }

        /// <summary>
        /// Получает подсказку для ввода в зависимости от типа данных
        /// </summary>
        /// <param name="dataType">Тип данных</param>
        /// <returns>Подсказка для ввода</returns>
        public static string GetInputHint(ArrayDataType dataType)
        {
            switch (dataType)
            {
                case ArrayDataType.Integer:
                    return "Введите целое число";
                case ArrayDataType.Float:
                    return "Введите дробное число";
                case ArrayDataType.DateTime:
                    return "Введите дату (например: 01.01.2024)";
                default:
                    return "Введите значение";
            }
        }

        /// <summary>
        /// Парсит строку в объект указанного типа
        /// </summary>
        /// <param name="input">Входная строка</param>
        /// <param name="dataType">Тип данных</param>
        /// <returns>Распарсенный объект</returns>
        public static object ParseValue(string input, ArrayDataType dataType)
        {
            switch (dataType)
            {
                case ArrayDataType.Integer:
                    return int.Parse(input);
                case ArrayDataType.Float:
                    return float.Parse(input);
                case ArrayDataType.DateTime:
                    return DateTime.Parse(input);
                default:
                    throw new ArgumentException($"Неподдерживаемый тип данных: {dataType}");
            }
        }
    }
}

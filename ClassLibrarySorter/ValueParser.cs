using System;

namespace ClassLibrarySorter
{
    /// <summary>
    /// Центральный парсер значений для поддерживаемых типов.
    /// </summary>
    public static class ValueParser
    {
        /// <summary>
        /// Парсит строку в указанный тип .NET.
        /// </summary>
        /// <param name="value">Строковое значение</param>
        /// <param name="dataType">Целевой тип</param>
        /// <returns>Распарсенный объект указанного типа</returns>
        public static object ParseValue(string value, Type dataType)
        {
            if (dataType == null)
                throw new ArgumentNullException(nameof(dataType));

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



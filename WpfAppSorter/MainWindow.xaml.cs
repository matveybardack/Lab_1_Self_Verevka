using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using WpfAppSorter.Interfaces;
using WpfAppSorter.Services;
using WpfAppSorter.Models;

namespace WpfAppSorter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IArrayManagerService arrayManagerService;
        private readonly IArraySorterService arraySorterService;
        private readonly IFileManagerService fileManagerService;
        private ArrayDataType currentDataType;

        public MainWindow()
        {
            InitializeComponent();

            // Инициализация сервисов
            arrayManagerService = new ArrayManagerService();
            arraySorterService = new ArraySorterService();
            fileManagerService = new FileManagerService();

            currentDataType = ArrayDataType.Integer;

            // Проверяем, что все элементы XAML инициализированы
            if (InputHint != null && ArrayDisplay != null)
            {
                UpdateInputHint();
                UpdateArrayDisplay();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Ошибка загрузки окна!");
            }
        }

        #region Обработчики событий меню

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (arrayManagerService.CurrentArray.Count == 0)
                {
                    MessageBox.Show("Массив пуст. Нечего сохранять.", "Предупреждение",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                    DefaultExt = "txt"
                };

                if (saveDialog.ShowDialog() == true)
                {
                    fileManagerService.SaveArrayToFile(arrayManagerService.CurrentArray, saveDialog.FileName);
                    MessageBox.Show("Массив успешно сохранен в файл.", "Успех",
                                  MessageBoxButton.OK, MessageBoxImage.Information);

                    // Добавляем файл в дерево
                    AddFileToTree(saveDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog
                {
                    Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
                };

                if (openDialog.ShowDialog() == true)
                {
                    Type dataType = ArrayDataTypes.GetNetType(currentDataType);
                    var loadedArray = fileManagerService.LoadArrayFromFile(openDialog.FileName, dataType);

                    // Очищаем текущий массив и добавляем загруженные элементы
                    arrayManagerService.ClearArray();
                    foreach (var element in loadedArray)
                    {
                        arrayManagerService.AddElement(element);
                    }

                    UpdateArrayDisplay();
                    UpdateUIState();
                    AddFileToTree(openDialog.FileName);
                    AddElementButton.IsEnabled = false; //временная заглушка 
                    MessageBox.Show("Массив успешно загружен из файла.", "Успех",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Сортировщик массивов\n\nПриложение для работы с массивами различных типов данных и их сортировки.",
                          "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion

        #region Обработчики событий ToolBar

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            HelpPopup.IsOpen = !HelpPopup.IsOpen;
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainTabControl.SelectedItem is TabItem selectedTab)
            {
                CurrentTabTitle.Text = selectedTab.Header.ToString();

                // Обновляем подсказку в зависимости от выбранной вкладки
                switch (selectedTab.Header.ToString())
                {
                    case "Тип данных":
                        HelpText.Text = "Выберите тип данных для работы с массивом. Доступны: целые числа, дробные числа и даты.";
                        break;
                    case "Массив":
                        HelpText.Text = "Установите размер массива ползунком и заполните его элементами. После добавления первого элемента размер будет заблокирован.";
                        break;
                    case "Сортировки":
                        HelpText.Text = "Выберите алгоритм сортировки и нажмите кнопку для выполнения. Доступна только при заполненном массиве.";
                        break;
                    case "Файлы":
                        HelpText.Text = "Управляйте файлами проекта. Добавляйте файлы в дерево и открывайте их через контекстное меню.";
                        break;
                }
            }
        }

        #endregion

        #region Обработчики событий вкладки "Тип данных"

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UpdateInputHint(); 
        }

        private void UpdateInputHint()
        {
            // Проверяем, что элемент InputHint инициализирован
            if (InputHint == null)
            {
                System.Diagnostics.Debug.WriteLine("Ошибка при инициализации поля ввода массива.");
                return;
            }

            if (IntRadioButton.IsChecked == true)
            {
                currentDataType = ArrayDataType.Integer;
                InputHint.Text = ArrayDataTypes.GetInputHint(currentDataType);
                arrayManagerService.DataType = ArrayDataTypes.GetNetType(currentDataType);
            }
            else if (FloatRadioButton.IsChecked == true)
            {
                currentDataType = ArrayDataType.Float;
                InputHint.Text = ArrayDataTypes.GetInputHint(currentDataType);
                arrayManagerService.DataType = ArrayDataTypes.GetNetType(currentDataType);
            }
            else if (DateTimeRadioButton.IsChecked == true)
            {
                currentDataType = ArrayDataType.DateTime;
                InputHint.Text = ArrayDataTypes.GetInputHint(currentDataType);
                arrayManagerService.DataType = ArrayDataTypes.GetNetType(currentDataType);
            }
        }

        #endregion

        #region Обработчики событий вкладки "Массив"

        private void ArraySizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ArraySizeLabel != null)
            {
                ArraySizeLabel.Text = $"Размер: {(int)e.NewValue}";
                arrayManagerService.MaxSize = (int)e.NewValue;
            }
        }

        private void ElementInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            AddElementButton.IsEnabled = !string.IsNullOrWhiteSpace(ElementInput.Text) && arrayManagerService.CanAddElement();
        }

        private void ElementInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddElementButton_Click(sender, e);
            }
        }

        private void AddElementButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ElementInput.Text))
                {
                    MessageBox.Show("Введите элемент для добавления.", "Предупреждение",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!arrayManagerService.CanAddElement())
                {
                    MessageBox.Show("Массив уже заполнен до максимального размера.", "Предупреждение",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                object newElement = arrayManagerService.ParseElement(ElementInput.Text);

                if (arrayManagerService.AddElement(newElement))
                {
                    ElementInput.Clear();
                    UpdateArrayDisplay();
                    UpdateUIState();

                    //временная заглушка
                    if (arrayManagerService.IsArrayFull())
                    {
                        AddElementButton.IsEnabled = false;
                    }
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Неверный формат данных: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении элемента: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveElementButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (arrayManagerService.CurrentArray.Count == 0)
                {
                    MessageBox.Show("Массив пуст. Нечего удалять.", "Предупреждение",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (arrayManagerService.RemoveLastElement())
                {
                    UpdateArrayDisplay();
                    UpdateUIState();
                    AddElementButton.IsEnabled = true; //временная заглушка
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении элемента: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearArrayButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                arrayManagerService.ClearArray();
                UpdateArrayDisplay();
                UpdateUIState();

                AddElementButton.IsEnabled = true; //временная заглушка
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при очистке массива: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateArrayDisplay()
        {
            ArrayDisplay.Text = arrayManagerService.GetArrayDisplay();
        }

        private void UpdateUIState()
        {
            bool isInitialized = arrayManagerService.IsInitialized;
            IntRadioButton.IsEnabled = !isInitialized;
            FloatRadioButton.IsEnabled = !isInitialized;
            DateTimeRadioButton.IsEnabled = !isInitialized;
            ArraySizeSlider.IsEnabled = !isInitialized;
            RemoveElementButton.IsEnabled = isInitialized;
            ClearArrayButton.IsEnabled = isInitialized;
            SortButton.IsEnabled = arrayManagerService.IsArrayFull(); // Доступ к сортировке при заполненном массиве (возможно переделать на вариант "есть хоть один элемент")
        }

        #endregion

        #region Обработчики событий вкладки "Сортировки"

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (arrayManagerService.CurrentArray.Count == 0)
                {
                    MessageBox.Show("Массив пуст. Нечего сортировать.", "Предупреждение",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!arrayManagerService.IsArrayFull())
                {
                    MessageBox.Show("Массив не полностью заполнен. Заполните массив полностью перед сортировкой.",
                                  "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                List<object> sortedArray;
                string sortType = "";

                if (BubbleSortRadio.IsChecked == true)
                {
                    sortedArray = arraySorterService.BubbleSort(arrayManagerService.CurrentArray);
                    sortType = "Пузырьковая сортировка";
                }
                else if (SelectionSortRadio.IsChecked == true)
                {
                    sortedArray = arraySorterService.SelectionSort(arrayManagerService.CurrentArray);
                    sortType = "Сортировка выбором";
                }
                else if (InsertionSortRadio.IsChecked == true)
                {
                    sortedArray = arraySorterService.InsertionSort(arrayManagerService.CurrentArray);
                    sortType = "Сортировка вставками";
                }
                else if (QuickSortRadio.IsChecked == true)
                {
                    sortedArray = arraySorterService.QuickSort(arrayManagerService.CurrentArray);
                    sortType = "Быстрая сортировка";
                }
                else
                {
                    MessageBox.Show("Выберите алгоритм сортировки.", "Предупреждение",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                SortResultDisplay.Text = $"Исходный массив:\n{string.Join(", ", arrayManagerService.CurrentArray)}\n\n" +
                                       $"Результат {sortType}:\n{string.Join(", ", sortedArray)}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сортировке: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Обработчики событий вкладки "Файлы"

        private void LoadFromFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FilesTreeView.SelectedItem is TreeViewItem selectedItem)
                {
                    string filePath = selectedItem.Tag?.ToString();
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        Type dataType = ArrayDataTypes.GetNetType(currentDataType);
                        var loadedArray = fileManagerService.LoadArrayFromFile(filePath, dataType);

                        // Очищаем текущий массив и добавляем загруженные элементы
                        arrayManagerService.ClearArray();
                        foreach (var element in loadedArray)
                        {
                            arrayManagerService.AddElement(element);
                        }

                        UpdateArrayDisplay();

                        MessageBox.Show("Массив успешно загружен из файла.", "Успех",
                                      MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Выберите файл для загрузки.", "Предупреждение",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FilesTreeView.SelectedItem is TreeViewItem selectedItem)
                {
                    string filePath = selectedItem.Tag?.ToString();
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        fileManagerService.RemoveFile(filePath);
                        FilesTreeView.Items.Remove(selectedItem);
                    }
                }
                else
                {
                    MessageBox.Show("Выберите файл для удаления.", "Предупреждение",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении файла: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshFilesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FilesTreeView.Items.Clear();
                var trackedFiles = fileManagerService.GetTrackedFiles();
                foreach (var filePath in trackedFiles)
                {
                    AddFileToTree(filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении списка файлов: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        

        private void OpenFileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FilesTreeView.SelectedItem is TreeViewItem selectedItem)
                {
                    string filePath = selectedItem.Tag?.ToString();
                    if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = filePath,
                            UseShellExecute = true
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии файла: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveFromListMenuItem_Click(object sender, RoutedEventArgs e)
        {
            RemoveFileButton_Click(sender, e);
        }

        private void AddFileToTree(string filePath)
        {
            try
            {
                TreeViewItem item = new TreeViewItem
                {
                    Header = System.IO.Path.GetFileName(filePath),
                    Tag = filePath
                };
                FilesTreeView.Items.Add(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении файла в дерево: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion
    }

}

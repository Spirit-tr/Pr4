using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Pr4
{
    public partial class MainWindow : Window
    {
        private List<WeatherData> weatherDataList = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadData_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "CSV Files (*.csv)|*.csv",
                Title = "Wybierz plik z danymi pogodowymi"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                weatherDataList = WeatherService.LoadWeatherData(openFileDialog.FileName);
                WeatherDataGrid.ItemsSource = weatherDataList;
                UpdateStatistics();
            }
        }

        private void ParameterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (weatherDataList.Any())
                UpdateStatistics();
        }

        private void UpdateStatistics()
        {
            if (!weatherDataList.Any()) return;

            string selected = ((ComboBoxItem)ParameterComboBox.SelectedItem).Content.ToString();
            List<double> values = selected switch
            {
                "Temperatura" => weatherDataList.Select(d => d.Temperature).ToList(),
                "Wilgotność" => weatherDataList.Select(d => d.Humidity).ToList(),
                "Ciśnienie" => weatherDataList.Select(d => d.Pressure).ToList(),
                _ => new List<double>()
            };

            if (values.Any())
            {
                AverageValueText.Text = values.Average().ToString("F2");
                MinValueText.Text = values.Min().ToString("F2");
                MaxValueText.Text = values.Max().ToString("F2");
            }
        }
    }
}

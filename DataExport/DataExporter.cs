using DatabaseManagers;
using System;
using System.Collections.Generic;
using WinForms = System.Windows.Forms;

namespace DataExport
{
    public class DocumentExporter
    {
        // Константы для директорий и названий файлов
        public const string DocumentDirectory = "Samples";

        private static DocumentExporter _instance;

        // Получение экземпляра класса DocumentExporter (реализация паттерна Singleton)
        public static DocumentExporter GetInstance()
        {
            if (_instance == null)
                _instance = new DocumentExporter();

            return _instance;
        }

        private DocumentExporter()
        { }

        /// <summary>
        /// Выбор директории для сохранения документов
        /// </summary>
        /// <returns>Путь к выбранной директории</returns>
        private string GetFolder()
        {
            WinForms.FolderBrowserDialog folderBrowser = new WinForms.FolderBrowserDialog();
            WinForms.DialogResult result = folderBrowser.ShowDialog();

            if (result != WinForms.DialogResult.OK)
                throw new Exception("Папка не выбрана");

            if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
                return folderBrowser.SelectedPath;

            throw new Exception("Папка имеет некорректный формат");
        }

        /// <summary>
        /// Создание документа с информацией о рейсах в формате Excel
        /// </summary>
        /// <param name="catalogs">Список каталогов книг </param>
        public void CreateTripsTable(List<TripModel> trips)
        {
            ExcelDocumentCreator excelDocumentCreator = new ExcelDocumentCreator();

            string[] titles = new string[] { "Id", "Номер заказа", "Отправитель", "Дата заказа", "Получатель", "Дата получения", "Адрес погрузки", "Адрес разгрузки", "Автомобиль", "Гос. номер", "Экипаж" };
            string[,] data = new string[trips.Count, titles.Length];

            // Заполняем данные для таблицы
            for (int i = 0; i < trips.Count; i++)
            {
                TripModel dataModel = trips[i];

                data[i, 0] = dataModel.Id.ToString();
                data[i, 1] = dataModel.Order.Id.ToString();
                data[i, 2] = dataModel.Order.SenderClient.Name;
                data[i, 3] = dataModel.Order.OrderDate;
                data[i, 4] = dataModel.Order.ReceiverClient.Name;
                data[i, 5] = dataModel.Data;
                data[i, 6] = dataModel.Order.LoadingAddress;
                data[i, 7] = dataModel.Order.UnloadingAddress;
                data[i, 8] = dataModel.Vehicle.Brand.BrandName;
                data[i, 9] = dataModel.Vehicle.LicencePlate;
                data[i, 10] = dataModel.Crew.Name.ToString();
            }

            string filePath = GetFolder();
            excelDocumentCreator.ExportDataToExcel(titles, data, filePath, "Рейсы");
        }

        /// <summary>
        /// Создание документа с информацией о грузах в заказах в формате Excel
        /// </summary>
        /// <param name="catalogs">Список каталогов книг </param>
        public void CreateCargoTable(List<CargoOrders> cargoList)
        {
            ExcelDocumentCreator excelDocumentCreator = new ExcelDocumentCreator();

            string[] titles = new string[] { "Id", "Номер заказа", "Отправитель", "Получатель", "Наименование", "Количество" };
            string[,] data = new string[cargoList.Count, titles.Length];

            // Заполняем данные для таблицы
            for (int i = 0; i < cargoList.Count; i++)
            {
                CargoOrders dataModel = cargoList[i];

                data[i, 0] = dataModel.Id.ToString();
                data[i, 1] = dataModel.Order.Id.ToString();
                data[i, 2] = dataModel.Order.ReceiverClient.Name;
                data[i, 3] = dataModel.Order.SenderClient.Name;
                data[i, 4] = dataModel.Cargo.CargoName;
                data[i, 5] = $"{dataModel.Quantity} {dataModel.Unit.UnitName}";
            }

            string filePath = GetFolder();
            excelDocumentCreator.ExportDataToExcel(titles, data, filePath, "ГрузыВРейсах");
        }
    }
}

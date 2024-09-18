using DatabaseManagers;
using System;
using System.Collections.Generic;
using WinForms = System.Windows.Forms;

namespace DataExport
{
	public class DocumentCreator
	{
		// Константы для директорий и названий файлов
		private const string DocumentStorageName = "ЦентральныйСклад";
		private const string DocumentSupliiesName = "Список поставок";
		private const string DocumentSalesName = "Список продаж";

		private static DocumentCreator _instance;

		// Получение экземпляра класса DocumentMaster (реализация паттерна Singleton)
		public static DocumentCreator Instance()
		{
			if (_instance == null)
				_instance = new DocumentCreator();

			return _instance;
		}

		private DocumentCreator()
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
		/// Создание документа в формате excel
		/// </summary>
		/// <param name="applicationModel">Грузы на цент. складе</param>
		public void CreateCargoDocument(List<ProductStocksData> stocksList)
		{
			ExcelDocumentCreator excelDocumentCreator = new ExcelDocumentCreator();
			var productsList = DatabaseManager.Instance.GetProducts();

			string[] titles = new string[] { "ID", "Продукция", "Ед. измерения", "Количество" };

			string[,] data = new string[stocksList.Count, titles.Length];

			// Заполняем данные для таблицы
			for (int i = 0; i < stocksList.Count; i++)
			{
				var item = stocksList[i];

				var product = productsList.Find(p => p.ProductId == item.ProductId);

				data[i, 0] = item.StockId.ToString();
				data[i, 1] = item.ProductName.ToString();
				data[i, 2] = product.UnitName.ToString();
				data[i, 3] = item.Balance.ToString();
			}

			string filePath = GetFolder();
			excelDocumentCreator.ExportDataToExcel(titles, data, filePath, DocumentStorageName);
		}

		/// <summary>
		/// Создание документа в формате excel
		/// </summary>
		/// <param name="applicationModel">пополнения склада</param>
		public void CreateSupliiesDocument(List<SupplyData> appsList)
		{
			ExcelDocumentCreator excelDocumentCreator = new ExcelDocumentCreator();
			var productsList = DatabaseManager.Instance.GetProducts();

			string[] titles = new string[] { "ID", "Поставщик", "Продукция", "Ед. измерения", "Количество", "Дата" };

			string[,] data = new string[appsList.Count, titles.Length];

			// Заполняем данные для таблицы
			for (int i = 0; i < appsList.Count; i++)
			{
				var item = appsList[i];

				var product = productsList.Find(p => p.ProductId == item.ProductId);

				data[i, 0] = item.SupplyId.ToString();
				data[i, 1] = item.ProviderData.ProviderFullName.ToString();
				data[i, 2] = item.ProductName.ToString();
				data[i, 3] = product.UnitName.ToString();
				data[i, 4] = item.Count.ToString();
				data[i, 5] = item.Date.ToString();
			}

			string filePath = GetFolder();
			excelDocumentCreator.ExportDataToExcel(titles, data, filePath, DocumentSupliiesName);
		}

		/// <summary>
		/// Создание документа в формате excel
		/// </summary>
		/// <param name="applicationModel">Грузы на цент. складе</param>
		public void CreateSalesDocument(List<SaleData> salesList)
		{
			ExcelDocumentCreator excelDocumentCreator = new ExcelDocumentCreator();
			var productsList = DatabaseManager.Instance.GetProducts();

			string[] titles = new string[] { "ID", "Продукция", "Ед. измерения", "Количество" };

			string[,] data = new string[salesList.Count, titles.Length];

			// Заполняем данные для таблицы
			for (int i = 0; i < salesList.Count; i++)
			{
				var item = salesList[i];

				var product = productsList.Find(p => p.ProductId == item.ProductId);

				data[i, 0] = item.SaleId.ToString();
				data[i, 1] = item.ProductName.ToString();
				data[i, 2] = product.UnitName.ToString();
				data[i, 3] = item.Count.ToString();
			}

			string filePath = GetFolder();
			excelDocumentCreator.ExportDataToExcel(titles, data, filePath, DocumentSalesName);
		}


	}
}

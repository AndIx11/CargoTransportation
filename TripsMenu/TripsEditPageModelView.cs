using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using DatabaseManagers;
using ModelViewSystem;
using DataExport;

namespace TripsMenu
{
    public class TripsEditPageModelView : TableEditPageModelView
    {
        public ButtonCommand ExportCommand { get; set; }

        public TripsEditPageModelView(Button[] buttons, DataGrid dataGrid, AccessInfo access) : base(buttons, dataGrid, access)
        {
            buttons[1].Visibility = System.Windows.Visibility.Hidden;
			buttons[2].Visibility = System.Windows.Visibility.Hidden;
			buttons[3].Visibility = System.Windows.Visibility.Hidden;

			ExportCommand = new ButtonCommand(Export);
			buttons[5].Command = ExportCommand;
		}

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);
                var modelView = new TripsEditWindowModelView();
                WindowEvents.OpenWindow(typeof(TripsEditWindow), modelView);
                LoadDataTable();
            }
            catch (Exception ex)
            {
                ErrorMessage(ex.Message);
            }
        }

        protected override void Edit(object obj)
        {
            try
            {
                base.Edit(obj);
                var item = DatabaseManager.GetInstance().TripsList.Find(a => a.Id == SelectedItem.Id);
                //var modelView = new TripsEditWindowModelView(item);
                //WindowEvents.OpenWindow(typeof(TripsEditWindow), modelView);
                LoadDataTable();
            }
            catch (Exception ex)
            {
                ErrorMessage(ex.Message);
            }
        }

        protected override void Delete(object obj)
        {
            try
            {
                base.Delete(obj);
                var item = DatabaseManager.GetInstance().TripsList.Find(a => a.Id == SelectedItem.Id);
                Database.Delete(item);
                LoadDataTable();
            }
            catch (Exception ex)
            {
                ErrorMessage("Невозможно удалить элемент, так как он связан с другими элементами в базе данных.");
            }
        }

        protected override void LoadDataTable()
        {
            base.LoadDataTable();

            var individuals = DatabaseManager.GetInstance().TripsList;
            Items = new ObservableCollection<DataModel>(individuals);
        }

        protected void Export(object obj)
        {
			try
			{
                SuccessMessage("Экспорт");
                DataExport.DocumentExporter.GetInstance().CreateTripsTable(Database.TripsList);
                SuccessMessage("Документ успешно создан");
			}
			catch (Exception ex)
			{
				ErrorMessage("Невозможно удалить элемент, так как он связан с другими элементами в базе данных.");
			}
		}

    }
}

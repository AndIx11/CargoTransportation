using DatabaseManagers;
using ModelViewSystem;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace CargosInOrdersMenu
{
    public class CargosInOrdersEditPageModelView : TableEditPageModelView
    {
        public ButtonCommand ExportCommand { get; set; }

        public CargosInOrdersEditPageModelView(Button[] buttons, DataGrid dataGrid, AccessInfo access) : base(buttons, dataGrid, access)
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
                var modelView = new CargosInOrdersEditWindowModelView();
                WindowEvents.OpenWindow(typeof(CargosInOrdersEditWindow), modelView);
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
                var item = DatabaseManager.GetInstance().CargoOrdersList.Find(a => a.Id == SelectedItem.Id);
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
                var item = DatabaseManager.GetInstance().CargosList.Find(a => a.Id == SelectedItem.Id);
                Database.Delete(item);
                LoadDataTable();
            }
            catch (Exception ex)
            {
                 ErrorMessage(ex.Message);
            }
        }

        protected override void LoadDataTable()
        {
            base.LoadDataTable();

            var individuals = DatabaseManager.GetInstance().CargoOrdersList;
            Items = new ObservableCollection<DataModel>(individuals);
        }

        protected void Export(object obj)
        {
            try
            {
                SuccessMessage("Экспорт");
                DataExport.DocumentExporter.GetInstance().CreateCargoTable(Database.CargoOrdersList);
                SuccessMessage("Документ успешно создан");
            }
            catch (Exception ex)
            {
                ErrorMessage("Невозможно создать документ.");
            }
        }

    }
}

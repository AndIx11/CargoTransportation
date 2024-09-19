using DatabaseManagers;
using ModelViewSystem;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace VehiclesMenu
{
    public class VehiclesPageModelView : TableEditPageModelView
    {
        public VehiclesPageModelView(Button[] buttons, DataGrid dataGrid, AccessInfo access) : base(buttons, dataGrid, access)
        { }

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);
                var modelView = new VehiclesEditWindowModelView();
                WindowEvents.OpenWindow(typeof(VehiclesEditWindow), modelView);
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
                var item = DatabaseManager.GetInstance().VehiclesList.Find(a => a.Id == SelectedItem.Id);
                var modelView = new VehiclesEditWindowModelView(item);
                WindowEvents.OpenWindow(typeof(VehiclesEditWindow), modelView);
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
                var item = Database.VehiclesList.Find(a => a.Id == SelectedItem.Id);
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
            Items = new ObservableCollection<DataModel>(DatabaseManager.GetInstance().VehiclesList);
        }

    }
}

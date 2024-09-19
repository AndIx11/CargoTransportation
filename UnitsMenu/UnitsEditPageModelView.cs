using DatabaseManagers;
using ModelViewSystem;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace UnitsMenu
{
    public class UnitsEditPageModelView : TableEditPageModelView
    {
        public UnitsEditPageModelView(Button[] buttons, DataGrid dataGrid, AccessInfo access) : base(buttons, dataGrid, access)
        { }

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);
                var modelView = new UnitEditWindowModelView();
                WindowEvents.OpenCatalogEdit(modelView);
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
                var item = DatabaseManager.GetInstance().UnitsList.Find(g => g.Id == SelectedItem.Id);
                var modelView = new UnitEditWindowModelView(item);
                WindowEvents.OpenCatalogEdit(modelView);
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
                var item = DatabaseManager.GetInstance().UnitsList.Find(g => g.Id == SelectedItem.Id);
                Database.DeleteUnit(item.Id);
                LoadDataTable();
                SuccessMessage("Успешно удалено");
            }
            catch (Exception ex)
            {
                ErrorMessage("Невозможно удалить элемент, так как он связан с другими элементами в базе данных.");
            }
        }

        protected override void LoadDataTable()
        {
            base.LoadDataTable();
            Items = new ObservableCollection<DataModel>(DatabaseManager.GetInstance().UnitsList);
        }
    }
}

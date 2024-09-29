using DatabaseManagers;
using ModelViewSystem;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace CrewsMenu
{
    public class CrewsEditPageModelView : TableEditPageModelView
    {
        public CrewsEditPageModelView(Button[] buttons, DataGrid dataGrid, AccessInfo access) : base(buttons, dataGrid, access)
        { }

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);
                var modelView = new CrewsEditWindowModelView();
                WindowEvents.OpenWindow(typeof(CrewEditWindow), modelView);
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
                var item = DatabaseManager.GetInstance().CrewsList().Find(g => g.Id == SelectedItem.Id);
                var modelView = new CrewsEditWindowModelView(item);
                WindowEvents.OpenWindow(typeof(CrewEditWindow), modelView);
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
                var item = DatabaseManager.GetInstance().CrewsList().Find(g => g.Id == SelectedItem.Id);
                Database.Delete(item);
                LoadDataTable();
                SuccessMessage("Успешно удалено");
            }
            catch (Exception ex)
            {
                 ErrorMessage(ex.Message);
            }
        }

        protected override void LoadDataTable()
        {
            base.LoadDataTable();
            Items = new ObservableCollection<DataModel>(DatabaseManager.GetInstance().CrewsList());
        }
    }
}

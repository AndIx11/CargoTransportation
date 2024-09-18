using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DatabaseManagers;
using ModelViewSystem;

namespace EntityClientsMenu
{
    public class EntityClientsEditPageModelView : TableEditPageModelView
    {
        public EntityClientsEditPageModelView(Button[] buttons, DataGrid dataGrid, AccessInfo access) : base(buttons, dataGrid, access)
        { }

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);
                var modelView = new EntityClientsEditWindowModelView();
                WindowEvents.OpenWindow(typeof(EntityClientsEditWindow), modelView);
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
                var item = DatabaseManager.GetInstance().EntityClientsList.Find(a => a.Id == SelectedItem.Id);
                var modelView = new EntityClientsEditWindowModelView(item);
                WindowEvents.OpenWindow(typeof(EntityClientsEditWindow), modelView);
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
                var item = DatabaseManager.GetInstance().EntityClientsList.Find(a => a.Id == SelectedItem.Id);
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

            var drivers = DatabaseManager.GetInstance().EntityClientsList;
            Items = new ObservableCollection<DataModel>(drivers);
        }

    }
}

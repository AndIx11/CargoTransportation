using DatabaseManagers;
using ModelViewSystem;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace OrdersMenu
{
    public class OrdersEditPageModelView : TableEditPageModelView
    {
        public OrdersEditPageModelView(Button[] buttons, DataGrid dataGrid, AccessInfo access) : base(buttons, dataGrid, access)
        { }

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);
                var modelView = new OrdersEditWindowModelView();
                WindowEvents.OpenWindow(typeof(OrdersEditWindow), modelView);
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
                var item = DatabaseManager.GetInstance().OrdersList.Find(a => a.Id == SelectedItem.Id);
                var modelView = new OrdersEditWindowModelView(item);
                WindowEvents.OpenWindow(typeof(OrdersEditWindow), modelView);
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
                var item = DatabaseManager.GetInstance().OrdersList.Find(a => a.Id == SelectedItem.Id);
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

            var individuals = DatabaseManager.GetInstance().OrdersList;
            Items = new ObservableCollection<DataModel>(individuals);
        }

    }
}

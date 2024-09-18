using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DatabaseManagers;
using ModelViewSystem;

namespace IndividualsMenu
{
    public class IndividualsEditPageModelView : TableEditPageModelView
    {
        public IndividualsEditPageModelView(Button[] buttons, DataGrid dataGrid, AccessInfo access) : base(buttons, dataGrid, access)
        { }

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);
                var modelView = new IndividualsEditWindowModelView();
                WindowEvents.OpenWindow(typeof(IndividualsEditWindow), modelView);
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
                var item = DatabaseManager.GetInstance().IndividualsClientsList.Find(a => a.Id == SelectedItem.Id);
                var modelView = new IndividualsEditWindowModelView(item);
                WindowEvents.OpenWindow(typeof(IndividualsEditWindow), modelView);
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
                var item = DatabaseManager.GetInstance().IndividualsClientsList.Find(a => a.Id == SelectedItem.Id);
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

            var individuals = DatabaseManager.GetInstance().IndividualsClientsList;
            Items = new ObservableCollection<DataModel>(individuals);
        }

    }
}

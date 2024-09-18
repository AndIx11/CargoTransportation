using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DatabaseManagers;
using ModelViewSystem;

namespace ClassificationsMenu
{
    public class ClassificationsPageModelView : TableEditPageModelView
    {
        public ClassificationsPageModelView(Button[] buttons, DataGrid dataGrid, AccessInfo access) : base(buttons, dataGrid, access)
        { }

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);
                var modelView = new ClassificationsEditWindowModelView();
                WindowEvents.OpenWindow(typeof(ClassificationsEditWindow), modelView);
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
                var item = DatabaseManager.GetInstance().ClassificationsList.Find(a => a.Id == SelectedItem.Id);
                var modelView = new ClassificationsEditWindowModelView(item);
                WindowEvents.OpenWindow(typeof(ClassificationsEditWindow), modelView);
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
                var item = DatabaseManager.GetInstance().ClassificationsList.Find(a => a.Id == SelectedItem.Id);
                Database.DeleteClassification(item.Id);
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

            var classifications = DatabaseManager.GetInstance().ClassificationsList;
            List<ClassificationModel> classificationsModels = new List<ClassificationModel>(0);

            foreach (var classification in classifications)
            {
                classificationsModels.Add(new ClassificationModel()
                {
                    Id = classification.Id,
                    Name = classification.Name,
                    Description = classification.Description == null ? "Описание отсутствует" : classification.Description,
                });
            }

            Items = new ObservableCollection<DataModel>(classificationsModels);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DatabaseManagers;
using ModelViewSystem;

namespace CategoriesMenu
{
    public class CategoriesPageModelView : TableEditPageModelView
    {
        public CategoriesPageModelView(Button[] buttons, DataGrid dataGrid, AccessInfo access) : base(buttons, dataGrid, access)
        { }

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);
                var modelView = new CategoriesEditWindowModelView();
                WindowEvents.OpenWindow(typeof(CategoriesEditWindow), modelView);
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
                var item = DatabaseManager.GetInstance().CategoriesList.Find(a => a.Id == SelectedItem.Id);
                var modelView = new CategoriesEditWindowModelView(item);
                WindowEvents.OpenWindow(typeof(CategoriesEditWindow), modelView);
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
                var item = DatabaseManager.GetInstance().CategoriesList.Find(a => a.Id == SelectedItem.Id);
                Database.DeleteCategory(item.Id);
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

            var categories = DatabaseManager.GetInstance().CategoriesList;
            List<CategoryModel> categoriesModels = new List<CategoryModel>(0);

            foreach (var category in categories)
            {
                categoriesModels.Add(new CategoryModel()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description == null ? "Описание отсутствует" : category.Description,
                });
            }

            Items = new ObservableCollection<DataModel>(categoriesModels);
        }

    }
}

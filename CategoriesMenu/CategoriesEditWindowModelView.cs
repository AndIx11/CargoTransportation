using DatabaseManagers;
using ModelViewSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CategoriesMenu
{
    public class CategoriesEditWindowModelView : DataModelEditWindowModelView
    {
        private string _name;
        private string _description;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public CategoriesEditWindowModelView() : base()
        {
            Tittle = "Новая категория";
            Name = "";
            Description = "";
        }

        public CategoriesEditWindowModelView(CategoryModel categoryModel) : base(categoryModel)
        {
            Tittle = "Редактирование категории";
            Description = categoryModel.Description;
            Name = categoryModel.Name;
        }

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);

                if (Name == "")
                    throw new Exception("Название не может быть пустым");

                CategoryModel categoryModel = new CategoryModel()
                {                    
                    Name = Name,
                    Description = Description,
                };

                Database.Add(categoryModel);
                SuccessMessage("Успешно добавлено");
                WindowVisibility = Visibility.Hidden;
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

                if (Name == "")
                    throw new Exception("Название не может быть пустым");

                CategoryModel categoryModel = new CategoryModel()
                {
                    Id = DataModel.Id,
                    Name = Name,
                    Description = Description,
                };

                Database.Edit(categoryModel);
                SuccessMessage("Успешно отредактировано");
                WindowVisibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                ErrorMessage(ex.Message);
            }
        }
    }
}

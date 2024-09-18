using DatabaseManagers;
using ModelViewSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassificationsMenu
{
    public class ClassificationsEditWindowModelView : DataModelEditWindowModelView
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

        public ClassificationsEditWindowModelView() : base()
        {
            Tittle = "Новая классность";
            Name = "";
            Description = "";
        }

        public ClassificationsEditWindowModelView(ClassificationModel classificationModel) : base(classificationModel)
        {
            Tittle = "Редактирование классности";
            Description = classificationModel.Description;
            Name = classificationModel.Name;
        }

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);

                if (Name == "")
                    throw new Exception("Название не может быть пустым");

                ClassificationModel classificationModel = new ClassificationModel()
                {
                    Name = Name,
                    Description = Description,
                };

                Database.Add(classificationModel);
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

                ClassificationModel classificationModel = new ClassificationModel()
                {
                    Id = DataModel.Id,
                    Name = Name,
                    Description = Description,
                };

                Database.Edit(classificationModel);
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

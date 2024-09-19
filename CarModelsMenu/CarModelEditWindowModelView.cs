using DatabaseManagers;
using ModelViewSystem;
using System;
using System.Windows;

namespace CarModelsMenu
{
    public class CarModelEditWindowModelView : CatalogEditModelView
    {
        public CarModelEditWindowModelView() : base()
        {
            Tittle = "Новая модель";
            Description = "Наименование модели";
        }

        public CarModelEditWindowModelView(CarModelModel carModel) : base(carModel)
        {
            Tittle = "Редактирование модели";
            Description = "Наименование модели";
            Name = carModel.ModelName;
        }

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);

                if (Name == "")
                    throw new Exception("Наименование - пусто");

                CarModelModel carModel = new CarModelModel()
                {
                    ModelName = Name,
                };

                DatabaseManager.GetInstance().Add(carModel);
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
                    throw new Exception("Наименование - пусто");

                CarModelModel carModel = new CarModelModel()
                {
                    Id = DataModel.Id,
                    ModelName = Name,
                };

                Database.Edit(carModel);
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

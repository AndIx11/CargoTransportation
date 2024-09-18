using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DatabaseManagers;
using ModelViewSystem;

namespace CargosMenu
{
    public class CargoEditWindowModelView : CatalogEditModelView
    {
        public CargoEditWindowModelView() : base()
        {
            Tittle = "Новый груз";
            Description = "Наименование груза";
        }

        public CargoEditWindowModelView(CargoModel cargoModel) : base(cargoModel)
        {
            Tittle = "Редактирование груза";
            Description = "Наименование груза";
            Name = cargoModel.CargoName;
        }

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);

                if (Name == "")
                    throw new Exception("Наименование - пусто");

                CargoModel cargoModel = new CargoModel()
                {
                    CargoName = Name,
                };

                Database.Add(cargoModel);
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

                CargoModel cargoModel = new CargoModel()
                {
                    Id = DataModel.Id,
                    CargoName = Name,
                };

                Database.Edit(cargoModel);
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

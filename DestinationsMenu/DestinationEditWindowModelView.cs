using DatabaseManagers;
using ModelViewSystem;
using System;
using System.Windows;

namespace DestinationsMenu
{
    public class DestinationEditWindowModelView : CatalogEditModelView
    {
        public DestinationEditWindowModelView() : base()
        {
            Tittle = "Новый вид груза";
            Description = "Наименование вида груза";
        }

        public DestinationEditWindowModelView(DestinationModel destinationModel) : base(destinationModel)
        {
            Tittle = "Редактирование вида груза";
            Description = "Наименование вида груза";
            Name = destinationModel.CargoTypeName;
        }

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);

                if (Name == "")
                    throw new Exception("Наименование - пусто");

                DestinationModel destinationModel = new DestinationModel()
                {
                    CargoTypeName = Name,
                };

                DatabaseManager.GetInstance().Add(destinationModel);
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

                DestinationModel destinationModel = new DestinationModel()
                {
                    Id = DataModel.Id,
                    CargoTypeName = Name,
                };

                Database.Edit(destinationModel);
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

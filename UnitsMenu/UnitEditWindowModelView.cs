using DatabaseManagers;
using ModelViewSystem;
using System;
using System.Windows;

namespace UnitsMenu
{
    public class UnitEditWindowModelView : CatalogEditModelView
    {
        public UnitEditWindowModelView() : base()
        {
            Tittle = "Новая единица измерения";
            Description = "Наименование единицы измерения";
        }

        public UnitEditWindowModelView(UnitModel unitModel) : base(unitModel)
        {
            Tittle = "Редактирование единицы измерения";
            Description = "Наименование единицы измерения";
            Name = unitModel.UnitName;
        }

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);

                if (Name == "")
                    throw new Exception("Наименование - пусто");

                UnitModel unitModel = new UnitModel()
                {
                    UnitName = Name,
                };

                DatabaseManager.GetInstance().Add(unitModel);
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

                UnitModel unitModel = new UnitModel()
                {
                    Id = DataModel.Id,
                    UnitName = Name,
                };

                Database.Edit(unitModel);
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

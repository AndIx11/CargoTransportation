using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DatabaseManagers;
using ModelViewSystem;

namespace BrandsMenu
{
    public class BrandEditWindowModelView : CatalogEditModelView
    {
        public BrandEditWindowModelView() : base()
        {
            Tittle = "Новая марка";
            Description = "Наименование марки";
        }

        public BrandEditWindowModelView(BrandModel brandModel) : base(brandModel)
        {
            Tittle = "Редактирование марки";
            Description = "Наименование марки";
            Name = brandModel.BrandName;
        }

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);

                if (Name == "")
                    throw new Exception("Наименование - пусто");

                BrandModel brandModel = new BrandModel()
                {
                    BrandName = Name,
                };

                DatabaseManager.GetInstance().Add(brandModel);
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

                BrandModel brandModel = new BrandModel()
                {
                    Id = DataModel.Id,
                    BrandName = Name,
                };

                Database.Edit(brandModel);
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

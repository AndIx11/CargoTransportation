using DatabaseManagers;
using ModelViewSystem;
using System;
using System.Windows;

namespace BanksMenu
{
    public class BankEditWindowModelView : CatalogEditModelView
    {
        public BankEditWindowModelView() : base()
        {
            Tittle = "Новый банк";
            Description = "Наименование банка";
        }

        public BankEditWindowModelView(BankModel bankModel) : base(bankModel)
        {
            Tittle = "Редактирование банка";
            Description = "Наименование банка";
            Name = bankModel.BankName;
        }

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);

                if (Name == "")
                    throw new Exception("Наименование - пусто");

                BankModel bankModel = new BankModel()
                {
                    BankName = Name,
                };

                Database.Add(bankModel);
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

                BankModel bankModel = new BankModel()
                {
                    Id = DataModel.Id,
                    BankName = Name,
                };

                Database.Edit(bankModel);
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

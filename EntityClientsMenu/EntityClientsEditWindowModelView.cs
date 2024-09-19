using DatabaseManagers;
using ModelViewSystem;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace EntityClientsMenu
{
    public class EntityClientsEditWindowModelView : DataModelEditWindowModelView
    {
        private ObservableCollection<BankModel> _banks;
        private BankModel _selectedBank;

        private string _name;
        private string _ceoName;
        private string _inn;
        private string _legalAddress;

        private string _phone;

        public ObservableCollection<BankModel> Banks
        {
            get { return _banks; }
            set
            {
                _banks = value;
                OnPropertyChanged(nameof(Banks));
            }
        }
        public BankModel SelectedBank
        {
            get { return _selectedBank; }
            set
            {
                _selectedBank = value;
                OnPropertyChanged(nameof(SelectedBank));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public string CEOName
        {
            get => _ceoName;
            set
            {
                if (_ceoName != value)
                {
                    _ceoName = value;
                    OnPropertyChanged(nameof(CEOName));
                }
            }
        }
        public string INN
        {
            get => _inn;
            set
            {
                if (_inn != value)
                {
                    _inn = value;
                    OnPropertyChanged(nameof(INN));
                }
            }
        }
        public string LegalAddress
        {
            get => _legalAddress;
            set
            {
                if (_legalAddress != value)
                {
                    _legalAddress = value;
                    OnPropertyChanged(nameof(LegalAddress));
                }
            }
        }
        public string Phone
        {
            get => _phone;
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    OnPropertyChanged(nameof(Phone));
                }
            }
        }


        public EntityClientsEditWindowModelView() : base()
        {
            Tittle = "Новый юр. клиент";
            Banks = new ObservableCollection<BankModel>(Database.BanksList);
        }

        public EntityClientsEditWindowModelView(EntityClientModel clientModel) : base(clientModel)
        {
            Tittle = "Редактирование клиента";
            Banks = new ObservableCollection<BankModel>(Database.BanksList);
            SelectedBank = clientModel.Bank;

            Name = clientModel.Name;
            CEOName = clientModel.CEOName;
            INN = clientModel.INN;
            LegalAddress = clientModel.LegalAddress;

            Phone = clientModel.Client.Phone;
        }

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);

                if (Name == "" || Name == null)
                    throw new Exception("Строка \"Название\" не может быть пустой!");

                if (CEOName == "" || CEOName == null)
                    throw new Exception("Строка \"ФИО руководителя\" не может быть пустой!");

                if (INN == "" || INN == null)
                    throw new Exception("Строка \"ИНН\" не может быть пустой!");

                if (Phone == "" || Phone == null)
                    throw new Exception("Строка \"Телефон\" не может быть пустой!");
                if (!Regex.IsMatch(Phone, @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$"))
                    throw new Exception("Используйте российские стандарты номеров!\nНапример: +7(ххх)ххх-хх-хх; 8хххххххххх; (495)ххххххх и т.д.");

                if (LegalAddress == "" || LegalAddress == null)
                    throw new Exception("Строка \"Юридический адрес\" не может быть пустой!");

                if (SelectedBank == null)
                    throw new Exception("Строка \"Банк клиента\" не может быть пустой!");

                ClientModel clientModel = new ClientModel()
                {
                    Phone = Phone,
                    Name = Name,
                };

                EntityClientModel entityClientModel = new EntityClientModel()
                {
                    Name = Name,
                    CEOName = CEOName,
                    INN = INN,
                    LegalAddress = LegalAddress,
                    BankID = SelectedBank.Id,
                };

                Database.Add(entityClientModel, clientModel);
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

                if (Name == "" || Name == null)
                    throw new Exception("Строка \"ФИО\" не может быть пустой!");

                if (CEOName == "" || CEOName == null)
                    throw new Exception("Строка \"Паспортные данные\" не может быть пустой!");

                if (INN == "" || INN == null)
                    throw new Exception("Строка \"ИНН\" не может быть пустой!");

                if (Phone == "" || Phone == null)
                    throw new Exception("Строка \"Телефон\" не может быть пустой!");
                if (!Regex.IsMatch(Phone, @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$"))
                    throw new Exception("Используйте российские стандарты номеров!\nНапример: +7(ххх)ххх-хх-хх; 8хххххххххх; (495)ххххххх и т.д.");

                if (LegalAddress == "" || LegalAddress == null)
                    throw new Exception("Строка \"Юридический адрес\" не может быть пустой!");

                if (SelectedBank == null)
                    throw new Exception("Строка \"Банк клиента\" не может быть пустой!");

                ClientModel clientModel = new ClientModel()
                {
                    Id = (DataModel as EntityClientModel).ClientID,
                    Phone = Phone,
                    Name = Name,
                };

                EntityClientModel entityClientModel = new EntityClientModel()
                {
                    Id = DataModel.Id,
                    Name = Name,
                    CEOName = CEOName,
                    INN = INN,
                    LegalAddress = LegalAddress,
                    BankID = SelectedBank.Id,
                    ClientID = clientModel.Id,
                };

                Database.Edit(entityClientModel);
                Database.Edit(clientModel);
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

using DatabaseManagers;
using ModelViewSystem;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace IndividualsMenu
{
    public class IndividualsEditWindowModelView : DataModelEditWindowModelView
    {
        private string _fullName;
        private string _passwordNumber;
        private DateTime _issueDate;
        private string _issuedBy;
        private string _phone;

        public string FullName
        {
            get => _fullName;
            set
            {
                if (_fullName != value)
                {
                    _fullName = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }
        public string PasswordNumber
        {
            get => _passwordNumber;
            set
            {
                if (_passwordNumber != value)
                {
                    _passwordNumber = value;
                    OnPropertyChanged(nameof(PasswordNumber));
                }
            }
        }
        public DateTime IssueDate
        {
            get => _issueDate;
            set
            {
                if (_issueDate != value)
                {
                    _issueDate = value;
                    OnPropertyChanged(nameof(IssueDate));
                }
            }
        }
        public string IssuedBy
        {
            get => _issuedBy;
            set
            {
                if (_issuedBy != value)
                {
                    _issuedBy = value;
                    OnPropertyChanged(nameof(IssuedBy));
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


        public IndividualsEditWindowModelView() : base()
        {
            Tittle = "Новое физ. лицо";

            FullName = "";
            PasswordNumber = "";
            IssueDate = DateTime.Now;
            IssuedBy = "";
        }

        public IndividualsEditWindowModelView(IndividualModel individualModel) : base(individualModel)
        {
            Tittle = "Редактирование физ. лица";

            FullName = individualModel.FullName;
            PasswordNumber = individualModel.PassportNumber;
            IssueDate = DateTime.Parse(individualModel.IssueDate);
            IssuedBy = individualModel.IssuedBy;
            Phone = individualModel.Client.Phone;
        }

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);

                if (FullName == "" || FullName == null)
                    throw new Exception("Строка \"ФИО\" не может быть пустой!");

                if (PasswordNumber == "" || PasswordNumber == null)
                    throw new Exception("Строка \"Паспортные данные\" не может быть пустой!");

                if (IssueDate == null)
                    throw new Exception("Строка \"Дата выдачи\" не может быть пустой!");
                if (IssueDate > DateTime.Now)
                    throw new Exception("Дата не может быть позже сегодняшнего дня!");

                if (IssuedBy == "" || IssuedBy == null)
                    throw new Exception("Строка \"Кем выдано\" не может быть пустой!");

                if (Phone == "" || Phone == null)
                    throw new Exception("Строка \"Телефон\" не может быть пустой!");
                if (!Regex.IsMatch(Phone, @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$"))
                    throw new Exception("Используйте российские стандарты номеров!\nНапример: +7(ххх)ххх-хх-хх; 8хххххххххх; (495)ххххххх и т.д.");

                ClientModel clientModel = new ClientModel()
                {
                    Phone = Phone,
                    Name = FullName,
                };

                IndividualModel individualModel = new IndividualModel()
                {
                    FullName = FullName,
                    PassportNumber = PasswordNumber,
                    IssueDate = IssueDate.ToShortDateString(),
                    IssuedBy = IssuedBy,
                };


                Database.Add(individualModel, clientModel);
                SuccessMessage("Успешно отредактировано");
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

                if (FullName == "" || FullName == null)
                    throw new Exception("Строка \"ФИО\" не может быть пустой!");

                if (PasswordNumber == "" || PasswordNumber == null)
                    throw new Exception("Строка \"Паспортные данные\" не может быть пустой!");

                if (IssueDate == null)
                    throw new Exception("Строка \"Дата выдачи\" не может быть пустой!");
                if (IssueDate > DateTime.Now)
                    throw new Exception("Дата не может быть позже сегодняшнего дня!");

                if (IssuedBy == "" || IssuedBy == null)
                    throw new Exception("Строка \"Кем выдано\" не может быть пустой!");

                if (Phone == "" || Phone == null)
                    throw new Exception("Строка \"Телефон\" не может быть пустой!");
                if (!Regex.IsMatch(Phone, @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$"))
                    throw new Exception("Используйте российские стандарты номеров!\nНапример: +7(ххх)ххх-хх-хх; 8хххххххххх; (495)ххххххх и т.д.");

                ClientModel clientModel = new ClientModel()
                {
                    Id = (DataModel as IndividualModel).ClientID,
                    Phone = Phone,
                    Name = FullName,
                };


                IndividualModel individualModel = new IndividualModel()
                {
                    Id = DataModel.Id,
                    FullName = FullName,
                    PassportNumber = PasswordNumber,
                    IssueDate = IssueDate.ToShortDateString(),
                    IssuedBy = IssuedBy,
                    ClientID = clientModel.Id,
                };

                Database.Edit(clientModel);
                Database.Edit(individualModel);

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

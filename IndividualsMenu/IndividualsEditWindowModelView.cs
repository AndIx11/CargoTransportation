using DatabaseManagers;
using ModelViewSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

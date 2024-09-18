using DatabaseManagers;
using ModelViewSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrewsMenu
{
    public class CrewsEditWindowModelView : DataModelEditWindowModelView
	{
        private ObservableCollection<DriverModel> _driversTableList;
		private ObservableCollection<DriverModel> _driversCrewList;

        private DriverModel _selectedTable;
        private DriverModel _selectedCrew;

		public ObservableCollection<DriverModel> AllDrivers
		{
			get { return _driversTableList; }
			set

			{
				_driversTableList = value;
				OnPropertyChanged(nameof(AllDrivers));
			}
		}
		public ObservableCollection<DriverModel> CrewDrivers
		{
			get { return _driversCrewList; }
			set

			{
				_driversCrewList = value;
				OnPropertyChanged(nameof(CrewDrivers));
			}
		}

        public DriverModel SelectedDriver
		{
			get { return _selectedTable; }
			set
			{
				_selectedTable = value;
				OnPropertyChanged(nameof(SelectedDriver));
			}
		}
		public DriverModel SelectedCrewDriver
		{
			get { return _selectedCrew; }
			set
			{
				_selectedCrew = value;
				OnPropertyChanged(nameof(SelectedCrewDriver));
			}
		}


        public ButtonCommand AddDriverCommand { get; set; }
		public ButtonCommand DeleteDriverCommand { get; set; }

		public CrewsEditWindowModelView() : base()
        {
			AddDriverCommand = new ButtonCommand(AddDriver);
			DeleteDriverCommand = new ButtonCommand(DeleteDriver);

			Tittle = "Новый экипаж";
			CrewDrivers = new ObservableCollection<DriverModel>(new List<DriverModel>(0));
			LoadData();
		}

        public CrewsEditWindowModelView(CrewModel crewModel) : base(crewModel)
        {
			AddDriverCommand = new ButtonCommand(AddDriver);
			DeleteDriverCommand = new ButtonCommand(DeleteDriver);

			Tittle = $"Редактирование экипижа {crewModel.Id}"; 
			CrewDrivers = new ObservableCollection<DriverModel>(Database.DriversCrewList(DataModel.Id));
			LoadData();
		}

        protected void LoadData()
        {
            var list = Database.DriversList;
			var otherCrewsDrivers = DataModel != null ?
				Database.DriverCrewsList().Where(c => c.CrewID != DataModel.Id).ToList() :
                Database.DriverCrewsList();

			var driversInOtherCrews = Database.DriversList.Where(d => 
				otherCrewsDrivers
				.Select(dc => dc.DriverID)
				.ToList()
				.Contains(d.Id));

			list = list.Where(d => !driversInOtherCrews.Select(od => od.Id).Contains(d.Id)).ToList();

            AllDrivers = new ObservableCollection<DriverModel>(list
                .Where(dr => 
                !CrewDrivers.Select(d => d.Id).Contains(dr.Id))
                .ToList());

		}

        protected void AddDriver(object obj)
        {
			try
			{
				if (SelectedDriver == null)
				{
					ErrorMessage("Водитель для добавления не выбран");
					return;
				}

				CrewDrivers.Add(SelectedDriver);
				LoadData();
			}
			catch
			{ }
		}

		protected void DeleteDriver(object obj)
		{
            if(SelectedCrewDriver == null)
			{
				ErrorMessage("Водитель для удаления не выбран");
				return;
			}

			CrewDrivers.Remove(SelectedCrewDriver);
			LoadData();
		}

		protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);


                CrewModel crewModel = new CrewModel()
                { };

				Database.Add(crewModel);
                Database.Add(CrewDrivers.ToArray(), crewModel);
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

				CrewModel crewModel = new CrewModel()
				{ 
                    Id = DataModel.Id,
                };

				Database.Edit(CrewDrivers.ToArray(), crewModel);
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

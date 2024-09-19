using DatabaseManagers;
using ModelViewSystem;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace OrdersMenu
{
    public class CargoOrderEditWindowModelView : ViewModelBase
    {
        private Visibility _windowVisibility = Visibility.Visible;

        private string _totalWeight;
        private string _insuranceValue;
        private string _quantity;

        private ObservableCollection<CargoModel> _cargosList;
        private CargoModel _selectedCargo;

        private ObservableCollection<UnitModel> _unitList;
        private UnitModel _selectedUnit;

        public string TotalWeight
        {
            get => _totalWeight;
            set
            {
                if (_totalWeight != value)
                {
                    _totalWeight = value;
                    OnPropertyChanged(nameof(TotalWeight));
                }
            }
        }
        public string Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }
        public string InsuranceValue
        {
            get => _insuranceValue;
            set
            {
                if (_insuranceValue != value)
                {
                    _insuranceValue = value;
                    OnPropertyChanged(nameof(InsuranceValue));
                }
            }
        }

        public ObservableCollection<CargoModel> CargosList
        {
            get => _cargosList;
            set
            {
                if (_cargosList != value)
                {
                    _cargosList = value;
                    OnPropertyChanged(nameof(CargosList));
                }
            }
        }
        public CargoModel SelectedCargo
        {
            get => _selectedCargo;
            set
            {
                if (_selectedCargo != value)
                {
                    _selectedCargo = value;
                    OnPropertyChanged(nameof(SelectedCargo));
                }
            }
        }

        public ObservableCollection<UnitModel> UnitsList
        {
            get => _unitList;
            set
            {
                if (_unitList != value)
                {
                    _unitList = value;
                    OnPropertyChanged(nameof(UnitsList));
                }
            }
        }
        public UnitModel SelectedUnit
        {
            get => _selectedUnit;
            set
            {
                if (_selectedUnit != value)
                {
                    _selectedUnit = value;
                    OnPropertyChanged(nameof(SelectedUnit));
                }
            }
        }

        public ButtonCommand CreateButtonCommand { get; set; }
        public Visibility WindowVisibility
        {
            get { return _windowVisibility; }
            set
            {
                _windowVisibility = value;
                OnPropertyChanged(nameof(WindowVisibility));
            }
        }

        public CargoOrders CargoOrder { get; set; }
        public bool Success { get; set; }

        public CargoOrderEditWindowModelView()
        {
            CargosList = new ObservableCollection<CargoModel>(DatabaseManager.GetInstance().CargosList);
            UnitsList = new ObservableCollection<UnitModel>(DatabaseManager.GetInstance().UnitsList);

            CreateButtonCommand = new ButtonCommand(CreateData);
        }

        public void CreateData(object obj)
        {
            if (!int.TryParse(Quantity, out int quantity))
                throw new Exception("Некорректный формат");

            if (!double.TryParse(TotalWeight, out double totalWeight))
                throw new Exception("Некорректный формат");

            if (!double.TryParse(InsuranceValue, out double insuranceValue))
                throw new Exception("Некорректный формат");

            CargoOrder = new CargoOrders()
            {
                CargoID = SelectedCargo.Id,
                UnitID = SelectedUnit.Id,

                Quantity = quantity,
                TotalWeight = totalWeight,
                InsuranceValue = insuranceValue,

                Cargo = SelectedCargo,
                Unit = SelectedUnit,
            };

            Success = true;
            WindowVisibility = Visibility.Hidden;
        }
    }
}

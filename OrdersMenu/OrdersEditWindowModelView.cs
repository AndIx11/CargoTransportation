using DatabaseManagers;
using ModelViewSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OrdersMenu
{
    public class OrdersEditWindowModelView : DataModelEditWindowModelView
    {
        private DateTime _orderDate;
		private DateTime _receiverDate;

        private string _loadingAddress;
		private string _unloadingAddress;
        private string _routeLength;
        private string _orderCost;

		private ObservableCollection<CargoOrders> _cargoOrders;

        private ObservableCollection<ClientModel> _clients;
        private ObservableCollection<CrewModel> _crews;
		private ObservableCollection<VehicleModel> _vehicles;

		private ClientModel _selectedSenderClient;
		private ClientModel _selectedReceiverClient;
		private CrewModel _selectedCrew;
		private VehicleModel _selectedVehicle;
		private CargoOrders _selectedCargoOrders;

		public DateTime ReceiverOrderDate
		{
			get => _receiverDate;
			set
			{
				if (_receiverDate != value)
				{
					_receiverDate = value;
					OnPropertyChanged(nameof(ReceiverOrderDate));
				}
			}
		}
		public DateTime OrderDate
		{
			get => _orderDate;
			set
			{
				if (_orderDate != value)
				{
					_orderDate = value;
					OnPropertyChanged(nameof(OrderDate));
				}
			}
		}

        public string LoadingAddress
		{
			get => _loadingAddress;
			set
			{
				if (_loadingAddress != value)
				{
					_loadingAddress = value;
					OnPropertyChanged(nameof(LoadingAddress));
				}
			}
		}
		public string UnloadingAddress
		{
            get => _unloadingAddress;
            set
            {
                if (_unloadingAddress != value)
                {
					_unloadingAddress = value;
                    OnPropertyChanged(nameof(UnloadingAddress));
                }
            }
        }
		public string RouteLength
		{
			get => _routeLength;
			set
			{
				if (_routeLength != value)
				{
					_routeLength = value;
					OnPropertyChanged(nameof(RouteLength));
				}
			}
		}
		public string OrderCost
		{
			get => _orderCost;
			set
			{
				if (_orderCost != value)
				{
					_orderCost = value;
					OnPropertyChanged(nameof(OrderCost));
				}
			}
		}

        public ObservableCollection<ClientModel> Clients
        {
			get => _clients;
			set
			{
				if (_clients != value)
				{
					_clients = value;
					OnPropertyChanged(nameof(Clients));
				}
			}
		}
		public ObservableCollection<CrewModel> Crews
		{
			get => _crews;
			set
			{
				if (_crews != value)
				{
					_crews = value;
					OnPropertyChanged(nameof(Crews));
				}
			}
		}
		public ObservableCollection<VehicleModel> Vehicles
		{
			get => _vehicles;
			set
			{
				if (_vehicles != value)
				{
					_vehicles = value;
					OnPropertyChanged(nameof(Vehicles));
				}
			}
		}
		public ObservableCollection<CargoOrders> CargoOrders
		{
			get => _cargoOrders;
			set
			{
				if (_cargoOrders != value)
				{
					_cargoOrders = value;
					OnPropertyChanged(nameof(CargoOrders));
				}
			}
		}

		public VehicleModel SelectedVehicle
		{
			get => _selectedVehicle;
			set
			{
				if (_selectedVehicle != value)
				{
					_selectedVehicle = value;
					OnPropertyChanged(nameof(SelectedVehicle));
				}
			}
		}
		public ClientModel SelectedSenderClient
		{
			get => _selectedSenderClient;
			set
			{
				if (_selectedSenderClient != value)
				{
					_selectedSenderClient = value;
					OnPropertyChanged(nameof(SelectedSenderClient));
				}
			}
		}
		public ClientModel SelectedReceiverClient
		{
			get => _selectedReceiverClient;
			set
			{
				if (_selectedReceiverClient != value)
				{
					_selectedReceiverClient = value;
					OnPropertyChanged(nameof(SelectedReceiverClient));
				}
			}
		}
		public CrewModel SelectedCrew
		{
			get => _selectedCrew;
			set
			{
				if (_selectedCrew != value)
				{
					_selectedCrew = value;
					OnPropertyChanged(nameof(SelectedCrew));
				}
			}
		}
		public CargoOrders SelectedCargoOrder
		{
			get => _selectedCargoOrders;
			set
			{
				if (_selectedCargoOrders != value)
				{
					_selectedCargoOrders = value;
					OnPropertyChanged(nameof(SelectedCargoOrder));
				}
			}
		}

		public ButtonCommand AddCargoCommand { get; set; }
		public ButtonCommand DeleteCargoCommand { get; set; }

		public OrdersEditWindowModelView() : base()
        {
			AddCargoCommand = new ButtonCommand(AddCargo);
			DeleteCargoCommand = new ButtonCommand(DeleteCargo);

            Tittle = "Пополнение заказа";

			CargoOrders = new ObservableCollection<CargoOrders>(new List<CargoOrders>(0));
			Vehicles = new ObservableCollection<VehicleModel>(Database.VehiclesList);
			Clients = new ObservableCollection<ClientModel>(Database.ClientsList);
			Crews = new ObservableCollection<CrewModel>(Database.CrewsList());

			ReceiverOrderDate = DateTime.Now;
			OrderDate = DateTime.Now;
		}

        public OrdersEditWindowModelView(OrderModel orderModel) : base(orderModel)
        {
			AddCargoCommand = new ButtonCommand(AddCargo);
			DeleteCargoCommand = new ButtonCommand(DeleteCargo);

			Tittle = "Редактирование заказа";

			CargoOrders = new ObservableCollection<CargoOrders>(new List<CargoOrders>(0));
			Vehicles = new ObservableCollection<VehicleModel>(Database.VehiclesList);
			Clients = new ObservableCollection<ClientModel>(Database.ClientsList);
			Crews = new ObservableCollection<CrewModel>(Database.CrewsList());

			var tripData = Database.TripsList.Find(t => t.OrderID == orderModel.Id);

            ReceiverOrderDate = DateTime.Parse(tripData.Data);
			OrderDate = DateTime.Parse(orderModel.OrderDate);

			SelectedVehicle = tripData.Vehicle;
            SelectedCrew = tripData.Crew;

            SelectedSenderClient = orderModel.SenderClient;
			SelectedReceiverClient = orderModel.ReceiverClient;
			OrderDate = DateTime.Parse(orderModel.OrderDate);
			LoadingAddress = orderModel.LoadingAddress;
			UnloadingAddress = orderModel.UnloadingAddress;
			RouteLength = orderModel.RouteLength.ToString();
			OrderCost = orderModel.OrderCost.ToString();

			var cargoList = Database.CargoOrdersList.Where(co => co.OrderID == orderModel.Id).ToList();
			CargoOrders = new ObservableCollection<CargoOrders>(cargoList);
		}

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);

				if (!double.TryParse(RouteLength, out double routeLength))
					throw new Exception("Некорректный формат");

				if(!double.TryParse(OrderCost, out double orderCost))
					throw new Exception("Некорректный формат");

				OrderModel orderModel = new OrderModel()
				{
					SenderClientID = SelectedSenderClient.Id,
					ReceiverClientID = SelectedReceiverClient.Id,
					OrderDate = DateTime.Now.ToShortDateString(),
					LoadingAddress = LoadingAddress,
					UnloadingAddress = UnloadingAddress,
					RouteLength = routeLength,
					OrderCost = (decimal)orderCost,
				};

                Database.Add(orderModel);

                TripModel tripModel = new TripModel() 
				{
                    VehicleID = SelectedVehicle.Id,
					CrewID = SelectedCrew.Id,
					OrderID = orderModel.Id,
					Data = ReceiverOrderDate.ToShortDateString(),
				};

				Database.Add(tripModel);

				foreach (var cargo in CargoOrders)
				{
					cargo.OrderID = orderModel.Id;
				}

				Database.Add(CargoOrders.ToArray());

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
				if (!double.TryParse(RouteLength, out double routeLength))
					throw new Exception("Некорректный формат");

				if (!double.TryParse(OrderCost, out double orderCost))
					throw new Exception("Некорректный формат");

				if (DateTime.Now > ReceiverOrderDate)
                    throw new Exception("Дата доставки не может быть раньше даты заказа");

                OrderModel orderModel = new OrderModel()
				{
					Id = DataModel.Id,
					SenderClientID = SelectedSenderClient.Id,
					ReceiverClientID = SelectedReceiverClient.Id,
					OrderDate = DateTime.Now.ToShortDateString(),
					LoadingAddress = LoadingAddress,
					UnloadingAddress = UnloadingAddress,
					RouteLength = routeLength,
					OrderCost = (decimal)orderCost,
				};

				Database.Edit(orderModel);
                var tripData = Database.TripsList.Find(t => t.OrderID == orderModel.Id);

                TripModel tripModel = new TripModel()
				{
					Id = tripData.Id,
                    VehicleID = SelectedVehicle.Id,
					CrewID = SelectedCrew.Id,
					OrderID = orderModel.Id,
					Data = ReceiverOrderDate.ToShortDateString(),
				};

				Database.Edit(tripModel);

				foreach (var cargo in CargoOrders)
				{
					cargo.OrderID = orderModel.Id;
				}

				Database.Edit(CargoOrders.ToArray(), orderModel);

				SuccessMessage("Успешно отредактировано");
                WindowVisibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                ErrorMessage(ex.Message);
            }
        }

		protected void AddCargo(object obj)
		{
			CargoOrderEditWindow cargoOrderWindow = new CargoOrderEditWindow();
			cargoOrderWindow.ShowDialog();

			var modelView = (cargoOrderWindow.DataContext as CargoOrderEditWindowModelView);

			if (!modelView.Success)
				return;

			CargoOrders driverCategories = modelView.CargoOrder;

			CargoOrders.Add(driverCategories);
		}

		protected void DeleteCargo(object obj) 
		{
			if (SelectedCargoOrder == null)
			{
				ErrorMessage("Груз для удаления не выбран");
				return;
			}

			CargoOrders.Remove(SelectedCargoOrder);
		}
    }
}

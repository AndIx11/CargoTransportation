using DatabaseManagers;
using Microsoft.Win32;
using ModelViewSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace VehiclesMenu
{
    public class VehiclesEditWindowModelView : DataModelEditWindowModelView
    {
        private ObservableCollection<BrandModel> _brandsList;
        private BrandModel _selectedBrand;

        private ObservableCollection<CarModelModel> _modelsList;
        private CarModelModel _selectedCarModel;

        private string _payloadCapacity;
        private string _yearOfManufacture;
        private string _yearOfOverhaul;
        private string _licencePlate;
        private string _mileage;

		private BitmapImage _imageProperty;
        private byte[] _imageData;

		public BitmapImage ImageProperty
		{
			get => _imageProperty;
			set
			{
				_imageProperty = value;
				OnPropertyChanged(nameof(ImageProperty));
			}
		}

		public ObservableCollection<BrandModel> BrandsList
        {
            get { return _brandsList; }
            set

            {
                _brandsList = value;
                OnPropertyChanged(nameof(BrandsList));
            }
        }
        public BrandModel SelectedBrand
        {
            get { return _selectedBrand; }
            set
            {
                _selectedBrand = value;
                OnPropertyChanged(nameof(SelectedBrand));
            }
        }

        public ObservableCollection<CarModelModel> ModelsList
        {
            get { return _modelsList; }
            set
            {
                _modelsList = value;
                OnPropertyChanged(nameof(ModelsList));
            }
        }
        public CarModelModel SelectedModel
        {
            get { return _selectedCarModel; }
            set
            {
                _selectedCarModel = value;
                OnPropertyChanged(nameof(SelectedModel));
            }
        }

        public ButtonCommand ImageLoadCommand { get; set; }

        public string PayloadCapacity
        {
            get => _payloadCapacity;
            set
            {
                if (_payloadCapacity != value)
                {
                    _payloadCapacity = value;
                    OnPropertyChanged(nameof(PayloadCapacity));
                }
            }
        }
        public string YearOfManufacture
        {
            get => _yearOfManufacture;
            set
            {
                if (_yearOfManufacture != value)
                {
                    _yearOfManufacture = value;
                    OnPropertyChanged(nameof(YearOfManufacture));
                }
            }
        }
        public string YearOfOverhaul
        {
            get => _yearOfOverhaul;
            set
            {
                if (_yearOfOverhaul != value)
                {
                    _yearOfOverhaul = value;
                    OnPropertyChanged(nameof(YearOfOverhaul));
                }
            }
        }
        public string LicencePlate
        {
            get => _licencePlate;
            set
            {
                if (_licencePlate != value)
                {
                    _licencePlate = value;
                    OnPropertyChanged(nameof(LicencePlate));
                }
            }
        }
        public string Mileage
        {
            get => _mileage;
            set 
            {
                if (_mileage != value)
                {
                    _mileage = value;
					OnPropertyChanged(nameof(Mileage));
				}
            }
        }


		public VehiclesEditWindowModelView() : base()
        {
            Tittle = "Новый автомобиль";
            BrandsList = new ObservableCollection<BrandModel>(Database.BrandsList);
            ModelsList = new ObservableCollection<CarModelModel>(Database.CarModelsList);
            ImageLoadCommand = new ButtonCommand(LoadImage);
        }

        public VehiclesEditWindowModelView(VehicleModel vehicleModel) : base(vehicleModel)
        {
            Tittle = "Редактирование автомобиля";

            BrandsList = new ObservableCollection<BrandModel>(Database.BrandsList);
            ModelsList = new ObservableCollection<CarModelModel>(Database.CarModelsList);

            SelectedBrand = vehicleModel.Brand;
            SelectedModel = vehicleModel.CarModel;

            PayloadCapacity = vehicleModel.PayloadCapacity;
            YearOfManufacture = vehicleModel.YearOfManufacture;
            YearOfOverhaul = vehicleModel.YearOfOverhaul;
            LicencePlate = vehicleModel.LicencePlate;
            Mileage = vehicleModel.Mileage.ToString();
            ImageLoadCommand = new ButtonCommand(LoadImage);
			ImageProperty = LoadImageFromDatabase(vehicleModel.Photo);

            _imageData = vehicleModel.Photo;
		}

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);

                if(SelectedBrand == null)
                    throw new Exception("Бренд не выбран");

                if(SelectedModel == null)
                    throw new Exception("Модель не выбрана");

                if (PayloadCapacity == "")
                    throw new Exception("Не введена грузоподъёмность");

                if (YearOfManufacture == "")
                    throw new Exception("Год выпуска отсутствует");

                if (YearOfOverhaul == "")
                    throw new Exception("Год кап. ремонта отсутствует");

                if (LicencePlate == "")
                    throw new Exception("Гос. номер отсутствует");

                if (!int.TryParse(Mileage, out int mileage))
					throw new Exception("Пробег - некорректный формат");

				VehicleModel vehicleModel = new VehicleModel()
				{
					BrandID = SelectedBrand.Id,
					ModelID = SelectedModel.Id,
                    PayloadCapacity = PayloadCapacity,
                    YearOfManufacture = YearOfManufacture,
                    YearOfOverhaul = YearOfOverhaul,
                    LicencePlate = LicencePlate,
                    Photo = _imageData,
					Mileage = mileage,
				};

				Database.Add(vehicleModel);
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

                if (SelectedBrand == null)
                    throw new Exception("Бренд не выбран");

                if (SelectedModel == null)
                    throw new Exception("Модель не выбрана");

                if (PayloadCapacity == "")
                    throw new Exception("Не введена грузоподъёмность");

                if (YearOfManufacture == "")
                    throw new Exception("Год выпуска отсутствует");

                if (YearOfOverhaul == "")
                    throw new Exception("Год кап. ремонта отсутствует");

                if (LicencePlate == "")
                    throw new Exception("Гос. номер отсутствует");

                if (!int.TryParse(Mileage, out int mileage))
                    throw new Exception("Пробег - некорректный формат");

                VehicleModel vehicleModel = new VehicleModel()
                {
                    Id = DataModel.Id,
                    BrandID = SelectedBrand.Id,
                    ModelID = SelectedModel.Id,
                    PayloadCapacity = PayloadCapacity,
                    YearOfManufacture = YearOfManufacture,
                    YearOfOverhaul = YearOfOverhaul,
                    LicencePlate = LicencePlate,
                    Photo = _imageData,
                    Mileage = mileage,
				};

                Database.Edit(vehicleModel);
                SuccessMessage("Успешно отредактировано");
                WindowVisibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                ErrorMessage(ex.Message);
            }
        }

		public BitmapImage LoadImageFromDatabase(byte[] imageData)
		{
	        using (var stream = new MemoryStream(imageData))
			{
				BitmapImage bitmap = new BitmapImage();
				bitmap.BeginInit();
				bitmap.StreamSource = stream;
				bitmap.CacheOption = BitmapCacheOption.OnLoad;
				bitmap.EndInit();
				return bitmap;
			}
		}

		public byte[] LoadImageFile()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp;*.gif";

			if (openFileDialog.ShowDialog() == true)
			{
                _imageData = File.ReadAllBytes(openFileDialog.FileName);
				return _imageData;
			}

			return null; 
		}

        protected void LoadImage(object obj)
        {
            try
            {
                var file = LoadImageFile();
				ImageProperty = LoadImageFromDatabase(file);
               
			}
            catch (Exception ex)
            { }
		}
	}
}

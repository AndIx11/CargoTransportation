using Microsoft.EntityFrameworkCore;
using System;
using SQLitePCL;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Emit;

namespace DatabaseManagers
{
    public class DatabaseManager : DbContext
    {
        // SINGLETON
        #region SINGLETON

        private static DatabaseManager _instance;
        public static DatabaseManager GetInstance()
        {
            if (_instance == null)
                _instance = new DatabaseManager();
            return _instance;
        }

        #endregion

        //Связь с таблицами базы данных
        /// <summary> Пользователи </summary>
        private DbSet<UserModel> Users { get; set; }
        /// <summary> Меню </summary>
        private DbSet<MenuItemModel> MenuItems { get; set; }
        /// <summary> Доступ к меню </summary>
        private DbSet<AccessModel> MenuAccess { get; set; }

        private DbSet<BankModel> Banks { get; set; }
        private DbSet<BrandModel> Brands { get; set; }
        private DbSet<CargoModel> Cargos { get; set; }

        private DbSet<CategoryModel> Categories { get; set; }
        private DbSet<ClassificationModel> Classifications { get; set; }
        private DbSet<ClientModel> Clients { get; set; }
        private DbSet<DestinationModel> Destinations { get; set; }
        private DbSet<DriverCategories> DriverCategories { get; set; }

        private DbSet<CrewModel> Crews { get; set; }
        private DbSet<DriverCrews> DriverCrews { get; set; }
        private DbSet<DriverModel> Drivers { get; set; }
        private DbSet<EntityClientModel> EntityClients { get; set; }
        private DbSet<IndividualModel> Individuals { get; set; }
        //public DbSet<VehiclesMModel> ModelModels { get; set; }
        private DbSet<UnitModel> Units { get; set; }
        private DbSet<VehicleDestinations> VehicleDestinations { get; set; }
        private DbSet<VehicleModel> Vehicles { get; set; }
        private DbSet<CarModelModel> CarModels { get; set; }

		private DbSet<CargoOrders> CargoOrders { get; set; }
		private DbSet<TripModel> Trips { get; set; }

		/// <summary> Заказы </summary>
		private DbSet<OrderModel> Orders { get; set; }

        #region MODEL_CREATING
        /// <summary>
        /// Переопределенный метод для настройки параметров подключения к базе данных.
        /// </summary>
        /// <param name="optionsBuilder">Построитель опций контекста базы данных.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            string connectString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            connectString = baseDirectory + "\\" + connectString;

            optionsBuilder.UseSqlite($"Data Source={connectString}");
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DatabaseManager() : base()
        {
            Batteries.Init();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AccessModel>().ToTable("MenuAccess");
            modelBuilder.Entity<AccessModel>().HasKey(e => e.Id);

            modelBuilder.Entity<AccessModel>()
                .HasOne(m => m.MenuItem)
                .WithMany()
                .HasForeignKey(m => m.MenuId);

            modelBuilder.Entity<AccessModel>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId);

            modelBuilder.Entity<BankModel>().HasKey(e => e.Id);
            modelBuilder.Entity<BrandModel>().HasKey(e => e.Id);
            modelBuilder.Entity<CargoModel>().HasKey(e => e.Id);
            modelBuilder.Entity<CargoOrders>().HasKey(e => e.Id);
            modelBuilder.Entity<CategoryModel>().HasKey(e => e.Id);
            modelBuilder.Entity<ClassificationModel>().HasKey(e => e.Id);
            modelBuilder.Entity<ClientModel>().HasKey(e => e.Id);
            modelBuilder.Entity<CrewModel>().HasKey(e => e.Id);
            modelBuilder.Entity<DestinationModel>().HasKey(e => e.Id);

            modelBuilder.Entity<DriverCrews>().HasKey(e => e.Id);

            modelBuilder.Entity<EntityClientModel>().HasKey(e => e.Id);
            modelBuilder.Entity<EntityClientModel>()
                .HasOne(m => m.Bank)
                .WithMany()
                .HasForeignKey(m => m.BankID);
            modelBuilder.Entity<EntityClientModel>()
                .HasOne(m => m.Bank)
                .WithMany()
                .HasForeignKey(m => m.BankID);

            modelBuilder.Entity<IndividualModel>().HasKey(e => e.Id);
            modelBuilder.Entity<IndividualModel>()
                .HasOne(m => m.Client)
                .WithMany()
                .HasForeignKey(m => m.ClientID);

            modelBuilder.Entity<OrderModel>().HasKey(e => e.Id);
            modelBuilder.Entity<TripModel>().HasKey(e => e.Id);
            modelBuilder.Entity<TripModel>()
                .HasOne(m => m.Order)
                .WithMany()
                .HasForeignKey(m => m.OrderID);
			modelBuilder.Entity<TripModel>()
				.HasOne(m => m.Vehicle)
				.WithMany()
				.HasForeignKey(m => m.VehicleID);
			modelBuilder.Entity<TripModel>()
				.HasOne(m => m.Crew)
				.WithMany()
				.HasForeignKey(m => m.CrewID);

			modelBuilder.Entity<UnitModel>().HasKey(e => e.Id);
            modelBuilder.Entity<VehicleDestinations>().HasKey(e => e.Id);

            modelBuilder.Entity<DriverModel>().HasKey(e => e.Id);
            modelBuilder.Entity<DriverModel>()
                .HasOne(m => m.Classification)
                .WithMany()
                .HasForeignKey(m => m.ClassID);

            modelBuilder.Entity<VehicleModel>().HasKey(e => e.Id);
            modelBuilder.Entity<VehicleModel>()
                .HasOne(m => m.CarModel)
                .WithMany()
                .HasForeignKey(m => m.ModelID);
            modelBuilder.Entity<VehicleModel>()
                .HasOne(m => m.Brand)
                .WithMany()
                .HasForeignKey(m => m.BrandID);

            modelBuilder.Entity<DriverCategories>().HasKey(e => e.Id);
            modelBuilder.Entity<DriverCategories>()
                .HasOne(m => m.Category)
                .WithMany().
                HasForeignKey(c => c.CategoryID);
            modelBuilder.Entity<DriverCategories>()
                .HasOne(m => m.Driver)
                .WithMany()
                .HasForeignKey(m => m.DriverID);

            modelBuilder.Entity<OrderModel>().HasKey(e => e.Id);
            modelBuilder.Entity<OrderModel>()
                .HasOne(m => m.SenderClient)
                .WithMany()
                .HasForeignKey(m => m.SenderClientID);
            modelBuilder.Entity<OrderModel>()
                .HasOne(m => m.ReceiverClient)
                .WithMany()
                .HasForeignKey(m => m.ReceiverClientID);
            modelBuilder.Entity<CargoOrders>()
                .HasOne(m => m.Unit)
                .WithMany()
                .HasForeignKey(m => m.UnitID);
			modelBuilder.Entity<CargoOrders>()
				.HasOne(m => m.Order)
				.WithMany()
				.HasForeignKey(m => m.OrderID);
			modelBuilder.Entity<CargoOrders>()
				.HasOne(m => m.Cargo)
				.WithMany()
				.HasForeignKey(m => m.CargoID);

		}

        #endregion

        // Пользователи
        #region USERS

        public List<UserModel> GetUsersList()
        {
            var userList = Users.ToList();
            return Users.ToList();
        }

        public void Add(UserModel user)
        {

            if (user.Login.Length < 4)
                throw new Exception("Логин слишком короткий");

            if (user.Password.Length < 5)
                throw new Exception("Пароль слишком короткий");

            if (Users.Any(u => u.Login == user.Login))
                throw new Exception($"Пользователь с логином {user.Login} уже существует");

            user.Password = Encryption.EncryptString(user.Password);

            Users.Add(user);
            SaveChanges();

            var defaultAccess = LoadDefaultAccess(user);

            foreach (var access in defaultAccess)
                Add(access);

            SaveChanges();
        }

        public void Edit(UserModel user)
        {
            var userToEdit = Users.Find(user.Id);

            if (user.Password.Length < 5)
                throw new Exception("Пароль слишком короткий");

            user.Password = Encryption.EncryptString(user.Password);

            if (userToEdit == null)
                throw new Exception("Данные для редактирования не найдены");

            Entry(userToEdit).CurrentValues.SetValues(user);
            SaveChanges();
        }

        public void Delete(UserModel user)
        {
            if (!Users.Any(u => u.Id == user.Id))
                throw new Exception("Данного пользователя не существует");

            Users.Remove(user);
            SaveChanges();
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        public int Authorization(string login, string password)
        {
            password = Encryption.EncryptString(password);

            if (!Users.Any(u => u.Login == login && u.Password == password))
                throw new Exception("Неверный логин или пароль");

            return Users.First(u => u.Login == login && u.Password == password).Id;
        }

        #endregion

        // Меню
        #region MENU_ITEMS

        public List<MenuItemModel> GetMenuList() => MenuItems.ToList();

        #endregion

        // Доступ к меню
        #region ACCESS

        private static readonly int[] DefaultItemsId = new int[] { 3, 22, 24, 25 };

        private List<AccessModel> LoadDefaultAccess(UserModel user)
        {
            List<AccessModel> access = new List<AccessModel>();

            var menuList = GetMenuList().Where(m => DefaultItemsId.Contains(m.Id)).ToList();

            foreach (var menu in menuList)
            {
                Add(new AccessModel()
                {
                    UserId = user.Id,
                    User = user,

                    MenuId = menu.Id,
                    MenuItem = menu,

                    Read = 1,
                    Add = 1,
                    Edit = 1,
                    Delete = 1,
                });
            }

            return access;
        }

        public List<AccessModel> GetMenuAccessList() => MenuAccess.OrderBy(access => access.MenuItem.Order).ToList();

        /// <summary>
        /// Загрузка доступа к родительскому меню
        /// </summary>
        private List<AccessModel> LoadParentMenuAccess() => GetMenuAccessList().Where(a => a.MenuItem.DLL == null || a.MenuItem.Key == null).ToList();

        /// <summary>
        /// Загрузка меню для пользователя
        /// </summary>
        public List<AccessModel> LoadItems(UserModel user)
        {
            var userAcess = MenuAccess.Where(a => a.UserId == user.Id).OrderBy(access => access.MenuItem.Order).ToList();

            List<AccessModel> newAccess = LoadParentMenuAccess();

            foreach (var access in newAccess)
            {
                if (!userAcess.Any(a => a.MenuId == access.MenuId))
                    userAcess.Add(access);
            }

            userAcess = userAcess.OrderBy(a => a.MenuItem.Order).ToList();
            return userAcess;
        }

        public void Add(AccessModel menuItemAccessModel)
        {
            if (MenuAccess.Any(access => access.MenuId == menuItemAccessModel.MenuId && access.UserId == menuItemAccessModel.UserId))
                throw new Exception("Подобный доступ уже открыт");

            MenuAccess.Add(menuItemAccessModel);
            SaveChanges();
        }

        public void Edit(AccessModel menuItemAccessModel)
        {
            var accessToEdit = MenuAccess.Find(menuItemAccessModel.Id);

            if (accessToEdit == null)
                throw new Exception("Данные для редактирования не найдены");

            Entry(accessToEdit).CurrentValues.SetValues(menuItemAccessModel);
            SaveChanges();

        }

        public void Delete(AccessModel menuItemAccessModel)
        {
            var accessToDelete = MenuAccess.Find(menuItemAccessModel.Id);

            if (DefaultItemsId.Contains(menuItemAccessModel.MenuId))
                throw new Exception("Это меню - выдается по умолчанию");

            if (accessToDelete == null)
                throw new Exception("Данные для редактирования не найдены");

            MenuAccess.Remove(accessToDelete);
            SaveChanges();
        }

        #endregion

        #region BANKS

        public void Add(BankModel bankModel)
        {
            if (bankModel == null || string.IsNullOrEmpty(bankModel.BankName))
                throw new Exception("Название банка не может быть пустым");

            Banks.Add(bankModel);
            SaveChanges();
        }

        public void Edit(BankModel bankModel)
        {
            if (bankModel == null || string.IsNullOrEmpty(bankModel.BankName))
                throw new Exception("Название банка не может быть пустым");

            var bank = Banks.ToList().Find(b => b.Id == bankModel.Id);
            bank.BankName = bankModel.BankName;

            SaveChanges();
        }

        public void Delete(BankModel bankModel)
        {
            var bank = Banks.Find(bankModel.Id);
            if (bank == null)
                throw new InvalidOperationException("Банк не найден.");

            Banks.Remove(bank);
            SaveChanges();
        }

        public List<BankModel> BanksList => Banks.ToList();

        #endregion

        #region UNITS

        public void Add(UnitModel unitModel)
        {
            if (unitModel == null || string.IsNullOrEmpty(unitModel.UnitName))
                throw new Exception("Название единицы измерения не может быть пустым");

            Units.Add(unitModel);
            SaveChanges();
        }

        public void Edit(UnitModel unitModel)
        {
            if (unitModel == null || string.IsNullOrEmpty(unitModel.UnitName))
                throw new Exception("Название единицы измерения не может быть пустым");

            var unit = Units.Find(unitModel.Id);
            if (unit != null)
            {
                unit.UnitName = unitModel.UnitName;
                SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Единица измерения не найдена.");
            }
        }

        public void DeleteUnit(int id)
        {
            var unit = Units.Find(id);
            if (unit == null)
                throw new InvalidOperationException("Единица измерения не найдена.");

            Units.Remove(unit);
            SaveChanges();
        }

        public List<UnitModel> UnitsList => Units.ToList();

        #endregion

        #region DESTINATIONS

        public void Add(DestinationModel destinationModel)
        {
            if (destinationModel == null || string.IsNullOrEmpty(destinationModel.CargoTypeName))
                throw new Exception("Название типа груза не может быть пустым");

            Destinations.Add(destinationModel);
            SaveChanges();
        }

        public void Edit(DestinationModel destinationModel)
        {
            if (destinationModel == null || string.IsNullOrEmpty(destinationModel.CargoTypeName))
                throw new Exception("Название типа груза не может быть пустым");

            var destination = Destinations.Find(destinationModel.Id);
            if (destination != null)
            {
                destination.CargoTypeName = destinationModel.CargoTypeName;
                SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Тип груза не найден.");
            }
        }

        public void DeleteDestination(int id)
        {
            var destination = Destinations.Find(id);
            if (destination == null)
                throw new InvalidOperationException("Тип груза не найден.");

            Destinations.Remove(destination);
            SaveChanges();
        }

        public List<DestinationModel> DestinationsList => Destinations.ToList();

        #endregion

        #region CLIENTS

        public void Add(ClientModel clientModel)
        {
            if (clientModel == null || string.IsNullOrEmpty(clientModel.Phone))
                throw new Exception("Телефон клиента не может быть пустым");

            Clients.Add(clientModel);
            SaveChanges();
        }
        public void Edit(ClientModel clientModel)
        {
            if (clientModel == null || string.IsNullOrEmpty(clientModel.Phone))
                throw new Exception("Телефон клиента не может быть пустым");

            var client = Clients.Find(clientModel.Id);
            if (client != null)
            {
                client.Phone = clientModel.Phone;
                SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Клиент не найден.");
            }
        }
        public void Delete(ClientModel clientModel)
        {
            var client = Clients.Find(clientModel.Id);
            if (client == null)
                throw new InvalidOperationException("Клиент не найден.");

            Clients.Remove(client);
            SaveChanges();
        }

        public List<ClientModel> ClientsList => Clients.ToList();

        #endregion

        #region CLASSIFICATIONS

        public void Add(ClassificationModel classificationModel)
        {
            if (classificationModel == null || string.IsNullOrEmpty(classificationModel.Name))
                throw new Exception("Название классификации не может быть пустым");

            Classifications.Add(classificationModel);
            SaveChanges();
        }

        public void Edit(ClassificationModel classificationModel)
        {
            if (classificationModel == null || string.IsNullOrEmpty(classificationModel.Name))
                throw new Exception("Название классификации не может быть пустым");

            var classification = Classifications.Find(classificationModel.Id);
            if (classification == null)
                throw new InvalidOperationException("Классификация не найдена.");

            classification.Name = classificationModel.Name;
            classification.Description = classificationModel.Description;
            SaveChanges();
        }

        public void DeleteClassification(int id)
        {
            var classification = Classifications.Find(id);
            if (classification == null)
                throw new InvalidOperationException("Классификация не найдена.");

            Classifications.Remove(classification);
            SaveChanges();
        }

        public List<ClassificationModel> ClassificationsList => Classifications.ToList();

        #endregion

        #region CATEGORIES

        public void Add(CategoryModel categoryModel)
        {
            if (categoryModel == null || string.IsNullOrEmpty(categoryModel.Name))
                throw new Exception("Название категории не может быть пустым");

            Categories.Add(categoryModel);
            SaveChanges();
        }

        public void Edit(CategoryModel categoryModel)
        {
            if (categoryModel == null || string.IsNullOrEmpty(categoryModel.Name))
                throw new Exception("Название категории не может быть пустым");

            var category = Categories.Find(categoryModel.Id);
            if (category == null)
                throw new InvalidOperationException("Категория не найдена.");

            category.Name = categoryModel.Name;
            category.Description = categoryModel.Description;
            SaveChanges();

        }

        public void DeleteCategory(int id)
        {
            var category = Categories.Find(id);
            if (category == null)
                throw new InvalidOperationException("Категория не найдена.");

            Categories.Remove(category);
            SaveChanges();
        }

        public List<CategoryModel> CategoriesList => Categories.ToList();

        #endregion

        #region CARGOS

        public void Add(CargoModel cargoModel)
        {
            if (cargoModel == null || string.IsNullOrEmpty(cargoModel.CargoName))
                throw new Exception("Название груза не может быть пустым");

            Cargos.Add(cargoModel);
            SaveChanges();
        }

        public void Edit(CargoModel cargoModel)
        {
            if (cargoModel == null || string.IsNullOrEmpty(cargoModel.CargoName))
                throw new Exception("Название груза не может быть пустым");

            var cargo = Cargos.Find(cargoModel.Id);
            if (cargo == null)
                throw new InvalidOperationException("Груз не найден.");

            cargo.CargoName = cargoModel.CargoName;
            SaveChanges();
        }

        public void Delete(CargoModel cargoModel)
        {
            var cargo = Cargos.Find(cargoModel.Id);
            if (cargo == null)
                throw new InvalidOperationException("Груз не найден.");

            Cargos.Remove(cargo);
            SaveChanges();
        }

        public List<CargoModel> CargosList => Cargos.ToList();

        #endregion

        #region BRANDS

        public void Add(BrandModel brandModel)
        {
            if (brandModel == null || string.IsNullOrEmpty(brandModel.BrandName))
                throw new Exception("Название бренда не может быть пустым");

            Brands.Add(brandModel);
            SaveChanges();
        }

        public void Edit(BrandModel brandModel)
        {
            if (brandModel == null || string.IsNullOrEmpty(brandModel.BrandName))
                throw new Exception("Название бренда не может быть пустым");

            var brand = Brands.Find(brandModel.Id);

            if (brand == null)
                throw new Exception("Бренд не найден.");

            brand.BrandName = brandModel.BrandName;
            SaveChanges();
        }

        public void DeleteBrand(int id)
        {
            var brand = Brands.Find(id);
            if (brand == null)
                throw new Exception("Бренд не найден.");

            Brands.Remove(brand);
            SaveChanges();
        }

        public List<BrandModel> BrandsList => Brands.ToList();

        #endregion

        #region CAR_MODELS

        public void Add(CarModelModel carModel)
        {
            CarModels.Add(carModel);
            SaveChanges();
        }

        public void Edit(CarModelModel carModel)
        {
            var edit = CarModels.Find(carModel.Id);

            if (edit == null)
                throw new Exception("Модель не найдена");

            edit.ModelName = carModel.ModelName;
            SaveChanges();

        }

        public void Delete(CarModelModel carModel)
        {
            var data = CarModels.Find(carModel.Id);

            if (data == null)
                throw new Exception("Модель не найдена");

            CarModels.Remove(carModel);
            SaveChanges();
        }

        public List<CarModelModel> CarModelsList => CarModels.ToList();

        #endregion

        #region CLIENTS

        #region INDIVIDUAL

        public void Add(IndividualModel individualModel, ClientModel clientModel)
        {
            Clients.Add(clientModel);
			SaveChanges();

			individualModel.ClientID = clientModel.Id;

            Individuals.Add(individualModel);
            SaveChanges();
        }

        public void Edit(IndividualModel individualModel)
        {
            var edit = Individuals.Find(individualModel.Id);

            if (edit == null)
                throw new Exception("Клиент не найден");

            Entry(edit).CurrentValues.SetValues(individualModel);
            SaveChanges();
        }

        public void Delete(IndividualModel individualModel)
        {
            var data = Individuals.Find(individualModel.Id);

            if (data == null)
                throw new Exception("Клиент не найден");

            Individuals.Remove(individualModel);
            SaveChanges();
        }

        public List<IndividualModel> IndividualsClientsList => Individuals.ToList();

        #endregion

        #region ENTITY 

        public void Add(EntityClientModel entityClient, ClientModel clientModel)
        {
            Clients.Add(clientModel);
			SaveChanges();

			entityClient.ClientID = clientModel.Id;

            EntityClients.Add(entityClient);
            SaveChanges();
        }

        public void Edit(EntityClientModel entityClient)
        {
            var edit = EntityClients.Find(entityClient.Id);

            if (edit == null)
                throw new Exception("Клиент не найден");

            Entry(edit).CurrentValues.SetValues(entityClient);
            SaveChanges();
        }

        public void Delete(EntityClientModel entityClient)
        {
            var data = EntityClients.Find(entityClient.Id);

            if (data == null)
                throw new Exception("Клиент не найден");

            EntityClients.Remove(entityClient);
            SaveChanges();
        }

        public List<EntityClientModel> EntityClientsList => EntityClients.ToList();

        #endregion

        #endregion

        #region DRIVERS

        public void Add(DriverModel driver)
        {
            Drivers.Add(driver);
            SaveChanges();
        }

        public void Edit(DriverModel driver)
        {
            var edit = Drivers.Find(driver.Id);
            Entry(edit).CurrentValues.SetValues(driver);
            SaveChanges();
        }

        public void Delete(DriverModel driver)
        {
            var delete = Drivers.Find(driver.Id);
            Drivers.Remove(delete);
            SaveChanges();
        }

        public List<DriverModel> DriversList => Drivers.ToList();

        #endregion

        #region DRIVERS_CATEGORIES

        public void Add(DriverCategories[] categories)
        {
            foreach (var category in categories)
                DriverCategories.Add(category);

            SaveChanges();
        }

        public void Edit(DriverModel driverModel, DriverCategories[] categories)
        {
            var toDelete = DriverCategoriesList().Where(dc => dc.DriverID == driverModel.Id).ToArray();
            Delete(toDelete);

            Add(categories);
            SaveChanges();
        }

        public void Delete(DriverCategories[] categories)
        {
            var toDelete = DriverCategories.Where(c => categories.Select(cd => cd.Id).Contains(c.Id)).ToList();

            for (int i = 0; i < toDelete.Count; i++)
                DriverCategories.Remove(toDelete[i]);

            SaveChanges();
        }

        public List<DriverCategories> DriverCategoriesList() => DriverCategories.ToList();

        public List<CategoryModel> DriverCategoriesList(int driverId)
        {
            var ids = DriverCategoriesList().Where(dc => dc.DriverID == driverId).Select(c => c.CategoryID);
            var categories = CategoriesList.Where(c => ids.Contains(c.Id)).ToList();

            return categories;
        }

        #endregion

        #region CREW

        public void Add(CrewModel crewModel)
        {
            Crews.Add(crewModel);
            SaveChanges();
        }

        public void Edit(CrewModel crewModel)
        {
            SaveChanges();
        }

        public void Delete(CrewModel crewModel)
        {
            var delete = Crews.Find(crewModel.Id);
            Crews.Remove(delete);
            SaveChanges();
        }

        public List<CrewModel> CrewsList() => Crews.ToList();

        #endregion

        #region DRIVERS_CREW

        public void Add(DriverModel[] drivers, CrewModel crewModel)
        {
            var delete = DriverCrewsList().Where(dc => dc.CrewID == crewModel.Id).ToArray();
            Delete(delete);

            foreach (var driver in drivers)
            {
                DriverCrews.Add(new DriverCrews()
                {
                    DriverID = driver.Id,
                    CrewID = crewModel.Id,
                });
            }

            SaveChanges();
        }

        public void Edit(DriverModel[] driver, CrewModel crewModel)
        {
            var delete = DriverCrewsList().Where(dc => dc.CrewID == crewModel.Id).ToArray();
            Delete(delete);

            Add(driver, crewModel);
        }

        public void Delete(DriverCrews[] driverCrews)
        {
            foreach (var crew in driverCrews)
                DriverCrews.Remove(crew);

            SaveChanges();
        }

        public List<DriverCrews> DriverCrewsList() => DriverCrews.ToList();

        public List<DriverModel> DriversCrewList(int crewId)
        {
            var list = DriverCrewsList();

            var ids = list.Where(dc => dc.CrewID == crewId).Select(dc => dc.DriverID);
            var drivers = DriversList.Where(d => ids.Contains(d.Id)).ToList();

            return drivers;
        }

		#endregion

		#region VEHICLES

		public void Add(VehicleModel vehicleModel)
        {
            Vehicles.Add(vehicleModel);
            SaveChanges();
        }

        public void Edit(VehicleModel vehicleModel)
        {
            var edit = Vehicles.Find(vehicleModel.Id);
            Entry(edit).CurrentValues.SetValues(vehicleModel);
            SaveChanges();
        }

        public void Delete(VehicleModel vehicleModel)
        {
            var delete = Vehicles.Find(vehicleModel.Id);
            Vehicles.Remove(delete);
            SaveChanges();
        }

        public List<VehicleModel> VehiclesList => Vehicles.ToList();

        #endregion

        #region TRIPS

        public void Add(TripModel tripModel)
        {
            Trips.Add(tripModel);
            SaveChanges();
        }

        public void Edit(TripModel tripModel)
        {
			var edit = Trips.Find(tripModel.Id);
			Entry(edit).CurrentValues.SetValues(tripModel);
			SaveChanges();
        }

        public void Delete(TripModel tripModel)
        {
			Trips.Remove(tripModel);
			SaveChanges();
        }

        public List<TripModel> TripsList => Trips.ToList();

        #endregion

        #region ORDERS

        public void Add(OrderModel orderModel)
        {
            Orders.Add(orderModel);
            SaveChanges();
        }

        public void Edit(OrderModel orderModel)
        {
			var edit = Orders.Find(orderModel.Id);
			Entry(edit).CurrentValues.SetValues(orderModel);
			SaveChanges();
        }

        public void Delete(OrderModel orderModel)
        {
            Orders.Remove(orderModel);
            SaveChanges();
        }

        public List<OrderModel> OrdersList => Orders.ToList();

        #endregion

        #region CARGO_ORDERS

        public void Add(CargoOrders[] cargoOrders)
        {
			foreach (var order in cargoOrders)
				CargoOrders.Add(order);

            SaveChanges();
		}

		public void Edit(CargoOrders[] cargoOrders, OrderModel orderModel)
		{
            var toDelete = CargoOrdersList.Where(co => co.OrderID == orderModel.Id).ToArray();
            Delete(toDelete);
			SaveChanges();

			Add(cargoOrders);
			SaveChanges();
		}

		public void Delete(CargoOrders[] orderModel)
		{
            foreach(var order in orderModel)
			    CargoOrders.Remove(order);

			SaveChanges();
		}

		public List<CargoOrders> CargoOrdersList => CargoOrders.ToList();

		#endregion

	}
}

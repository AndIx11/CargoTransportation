using DatabaseManagers;
using ModelViewSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DriversMenu
{
    public class DriversEditWindowModelView : DataModelEditWindowModelView
    {
        private ObservableCollection<ClassificationModel> _classID;
        private ClassificationModel _selectedClassID;

        private ObservableCollection<CategoryModel> _categories;
        private ObservableCollection<CategoryModel> _allCategories;
        private CategoryModel _selectedCategory;
        private CategoryModel _selectedTableCategory;

        private string _badgeNumber;
        private string _fullName;
        private string _birthYear;
        private string _workExperience;


        public ObservableCollection<ClassificationModel> ClassIDList
        {
            get { return _classID; }
            set

            {
                _classID = value;
                OnPropertyChanged(nameof(ClassIDList));
            }
        }
        public ClassificationModel SelectedClassID
        {
            get { return _selectedClassID; }
            set
            {
                _selectedClassID = value;
                OnPropertyChanged(nameof(SelectedClassID));
            }
        }
        public ObservableCollection<CategoryModel> AllCategories
        {
            get { return _allCategories; }
            set
            {
                _allCategories = value;
                OnPropertyChanged(nameof(AllCategories));
            }
        }
        public ObservableCollection<CategoryModel> Categories
        {
            get { return _categories; }
            set

            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }
        public CategoryModel SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }
        public CategoryModel SelectedTableCategory
        {
            get { return _selectedTableCategory; }
            set
            {
                _selectedTableCategory = value;
                OnPropertyChanged(nameof(SelectedTableCategory));
            }
        }

        public string BadgeNumber
        {
            get => _badgeNumber;
            set
            {
                if (_badgeNumber != value)
                {
                    _badgeNumber = value;
                    OnPropertyChanged(nameof(BadgeNumber));
                }
            }
        }
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
        public string BirthYear
        {
            get => _birthYear;
            set
            {
                if (_birthYear != value)
                {
                    _birthYear = value;
                    OnPropertyChanged(nameof(BirthYear));
                }
            }
        }
        public string WorkExperience
        {
            get => _workExperience;
            set
            {
                if (_workExperience != value)
                {
                    _workExperience = value;
                    OnPropertyChanged(nameof(WorkExperience));
                }
            }
        }


        public ButtonCommand AddCategoryCommand { get; private set; }
        public ButtonCommand DeleteCategoryCommand { get; private set; }

        public DriversEditWindowModelView() : base()
        {
            Tittle = "Новый водитель";
            ClassIDList = new ObservableCollection<ClassificationModel>(Database.ClassificationsList);

            Categories = new ObservableCollection<CategoryModel>(new List<CategoryModel>(0));
            UpdateData();

            AddCategoryCommand = new ButtonCommand(AddCategory);
            DeleteCategoryCommand = new ButtonCommand(DeleteCategory);
        }

        public DriversEditWindowModelView(DriverModel driverModel) : base(driverModel)
        {
            Tittle = "Редактирование водителя";
            BadgeNumber = driverModel.BadgeNumber;
            FullName = driverModel.FullName;
            BirthYear = driverModel.BirthYear;
            WorkExperience = driverModel.WorkExperience.ToString();

            ClassIDList = new ObservableCollection<ClassificationModel>(Database.ClassificationsList);
            SelectedClassID = driverModel.Classification;

            Categories = new ObservableCollection<CategoryModel>(Database.DriverCategoriesList(driverModel.Id));
            UpdateData();

            AddCategoryCommand = new ButtonCommand(AddCategory);
            DeleteCategoryCommand = new ButtonCommand(DeleteCategory);
        }

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);

                if (SelectedClassID == null)
                    throw new Exception("Классность не выбрана");

                if (!int.TryParse(BirthYear, out int year))
                    throw new Exception("Год рождения – некорректный формат");

                if (year < 1924 || year > DateTime.Now.Year)
                    throw new Exception("Год рождения не может превышать нынешний год и не может быть меньше \"1924\"");

                if (!int.TryParse(WorkExperience, out int experince))
                    throw new Exception("Стаж - некорректный формат");

                if(experince < 0)
                    throw new Exception("Стаж не может быть отрицательным числом");

                DriverModel driverModel = new DriverModel()
                {
                    ClassID = SelectedClassID.Id,
                    BadgeNumber = BadgeNumber,
                    FullName = FullName,
                    BirthYear = BirthYear,
                    WorkExperience = experince,
                };
                

                Database.Add(driverModel);
                CategoriesAdding(driverModel);
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

                if (SelectedClassID == null)
                    throw new Exception("Классность не выбрана");

                if (!int.TryParse(BirthYear, out int year))
                    throw new Exception("Год рождения – некорректный формат");

                if (year < 1924 || year > DateTime.Now.Year)
                    throw new Exception("Год рождения не может превышать нынешний год и не может быть меньше \"1924\"");

                if (!int.TryParse(WorkExperience, out int experince))
                    throw new Exception("Стаж - некорректный формат");

                if (experince < 0)
                    throw new Exception("Стаж не может быть отрицательным числом");

                DriverModel driverModel = new DriverModel()
                {
                    Id = DataModel.Id,
                    ClassID = SelectedClassID.Id,
                    BadgeNumber = BadgeNumber,
                    FullName = FullName,
                    BirthYear = BirthYear,
                    WorkExperience = experince,
                };

                Database.Edit(driverModel);
                CategoriesEdit(driverModel);
                SuccessMessage("Успешно отредактировано");
                WindowVisibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                ErrorMessage(ex.Message);
            }
        }

        protected void UpdateData()
        {
            var list = Database.CategoriesList;

            AllCategories = new ObservableCollection<CategoryModel>(list
                .Where(d => !_categories.Select(c => c.Id)
                .Contains(d.Id))
                .ToList());

        }

        private void CategoriesAdding(DriverModel driverModel)
        {
            List<DriverCategories> categories = new List<DriverCategories>(0);

            foreach (var ct in _categories)
            {
                categories.Add(new DriverCategories()
                {
                    DriverID = driverModel.Id,
                    CategoryID = ct.Id,
                });
            }

            Database.Add(categories.ToArray());
        }
        private void CategoriesEdit(DriverModel driverModel)
        {
            List<DriverCategories> categories = new List<DriverCategories>(0);

            foreach (var ct in _categories)
            {
                categories.Add(new DriverCategories()
                {
                    DriverID = DataModel.Id,
                    CategoryID = ct.Id,
                });
            }

            Database.Edit(driverModel, categories.ToArray());
        }

        protected void AddCategory(object obj)
        {
            if (_selectedCategory == null)
            {
                ErrorMessage("Выберите категорию");
                return;
            }

            Categories.Add(_selectedCategory);
            UpdateData();
        }
        protected void DeleteCategory(object obj)
        {
            if (_selectedTableCategory == null)
            {
                ErrorMessage("Выберите категорию для удаления");
                return;
            }
            Categories.Remove(_selectedTableCategory);
            UpdateData();
        }
    }
}

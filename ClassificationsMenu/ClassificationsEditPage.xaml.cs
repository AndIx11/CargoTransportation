using DatabaseManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassificationsMenu
{
    /// <summary>
    /// Логика взаимодействия для ClassificationsEditPage.xaml
    /// </summary>
    public partial class ClassificationsEditPage : Page
    {
        public ClassificationsEditPage(AccessInfo accessInfo)
        {
            InitializeComponent();
            DataContext = new ClassificationsPageModelView(new Button[5] { null, AddButton, EditButton, DeleteButton, LogOutButton },
            MainTable,
                accessInfo
            );
        }
    }
}

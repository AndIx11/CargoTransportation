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

namespace CrewsMenu
{
    /// <summary>
    /// Логика взаимодействия для BanksEditPage.xaml
    /// </summary>
    public partial class CrewsEditPage : Page
    {
        public CrewsEditPage(AccessInfo accessInfo)
        {
            InitializeComponent();
            DataContext = new CrewsEditPageModelView(new Button[5] { null, AddButton, EditButton, DeleteButton, LogOutButton },
            MainTable,
                accessInfo
                );
        }
    }
}

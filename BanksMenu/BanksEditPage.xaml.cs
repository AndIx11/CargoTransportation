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

namespace BanksMenu
{
    /// <summary>
    /// Логика взаимодействия для BanksEditPage.xaml
    /// </summary>
    public partial class BanksEditPage : Page
    {
        public BanksEditPage(AccessInfo accessInfo)
        {
            InitializeComponent();
            DataContext = new BanksEditPageModelView(new Button[5] { null, AddButton, EditButton, DeleteButton, LogOutButton },
            MainTable,
                accessInfo
                );
        }
    }
}

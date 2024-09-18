using DatabaseManagers;
using ModelViewSystem;
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

namespace CargosInOrdersMenu
{
    public partial class CargosInOrdersEditPage : Page
    {
        public CargosInOrdersEditPage(AccessInfo accessInfo)
        {
            InitializeComponent();
            DataContext = new CargosInOrdersEditPageModelView(new Button[6] { null, AddButton, EditButton, DeleteButton, LogOutButton, ExportButton },
            MainTable,
                accessInfo
            );
        }
    }
}

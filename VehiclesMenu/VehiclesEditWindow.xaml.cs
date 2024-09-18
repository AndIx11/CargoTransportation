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
using System.Windows.Shapes;

namespace VehiclesMenu
{
    /// <summary>
    /// Логика взаимодействия для VehiclesEditWindow.xaml
    /// </summary>
    public partial class VehiclesEditWindow : Window
    {
        public static Image Image { get; set; }

        public VehiclesEditWindow()
        {
            InitializeComponent();
            Image = Photo;
        }
    }
}

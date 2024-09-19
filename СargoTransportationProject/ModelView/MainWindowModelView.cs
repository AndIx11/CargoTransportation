using DatabaseManagers;
using ModelViewSystem;
using System.Collections.Generic;

namespace СargoTransportationProject
{
    public class MainWindowModelView : BaseUIModelView
    {
        public MainWindowModelView() : base()
        { }

        public MainWindowModelView(UserModel user)
        {
            CurrentUser = user;
        }

        public List<CustomMenuItem> LoadMenu()
        {
            //try
            //{
            var items = DatabaseManager.GetInstance().LoadItems(CurrentUser);

            MenuCreator menuCreator = new MenuCreator();
            return menuCreator.GetMenuItems(items);
            //}
            ////catch (Exception ex)
            //{
            //	ErrorMessage(ex.Message);
            //	return new List<CustomMenuItem>(0);
            //}
        }

    }
}

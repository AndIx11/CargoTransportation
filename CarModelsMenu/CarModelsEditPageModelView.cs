﻿using DatabaseManagers;
using ModelViewSystem;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace CarModelsMenu
{
    public class CarModelsEditPageModelView : TableEditPageModelView
    {
        public CarModelsEditPageModelView(Button[] buttons, DataGrid dataGrid, AccessInfo access) : base(buttons, dataGrid, access)
        { }

        protected override void Add(object obj)
        {
            try
            {
                base.Add(obj);
                var modelView = new CarModelEditWindowModelView();
                WindowEvents.OpenCatalogEdit(modelView);
                LoadDataTable();
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
                var item = DatabaseManager.GetInstance().CarModelsList.Find(g => g.Id == SelectedItem.Id);
                var modelView = new CarModelEditWindowModelView(item);
                WindowEvents.OpenCatalogEdit(modelView);
                LoadDataTable();
            }
            catch (Exception ex)
            {
                ErrorMessage(ex.Message);
            }
        }

        protected override void Delete(object obj)
        {
            try
            {
                base.Delete(obj);
                var item = DatabaseManager.GetInstance().CarModelsList.Find(g => g.Id == SelectedItem.Id);
                DatabaseManager.GetInstance().Delete(item);
                LoadDataTable();
                SuccessMessage("Успешно удалено");
            }
            catch (Exception ex)
            {
                 ErrorMessage(ex.Message);
            }
        }

        protected override void LoadDataTable()
        {
            base.LoadDataTable();
            Items = new ObservableCollection<DataModel>(DatabaseManager.GetInstance().CarModelsList);
        }
    }
}

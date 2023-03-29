using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
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
using WpfApp1.DAL;

namespace WpfApp1
{

    public partial class MainWindow : Window
    {
        CarContext carContext = new CarContext();
        public MainWindow()
        {

            CarInitializer carInitializer = new CarInitializer();

            InitializeComponent();

            reloadCarList();

            carList.SelectionChanged += CarList_SelectionChanged;

            var carAttributes = GetCarAttributes();
            attributesComboBox.ItemsSource = carAttributes;

        }



        private void reloadCarList()
        {
            //if (carList.Items.Count != 0) { 
                //carList.Items.Clear(); 
            //}

            //foreach (Car car in carContext.Cars)
            //{
            //    carList.Items.Add(car);
            //}
            List<Car> tempCars = new List<Car>();
            foreach (Car car in carContext.Cars)
            {
                tempCars.Add(car);
            }
            carList.ItemsSource = tempCars;
        }

        private void addCar(object sender, RoutedEventArgs e)
        {
            Car c = new Car(BrandTextBox.Text, ModelTextBox.Text, ColorTextBox.Text );
            carContext.Cars.Add(c);
            carContext.SaveChanges();
            BrandTextBox.Text = "";
            ModelTextBox.Text = "";
            ColorTextBox.Text = "";
            reloadCarList();


        }

        private void updateCar(object sender, RoutedEventArgs e)
        {
            Car c = (Car)carList.SelectedItem;
            if (c != null)
            {
                c.Brand = BrandTextBox.Text;
                c.Model = ModelTextBox.Text;
                c.Color = ColorTextBox.Text;
                carContext.SaveChanges();
                reloadCarList();
            }
        }

        //private void searchFor(object sender, RoutedEventArgs e)
        //{
        //    //carList.Items.Clear();
        //    List<Car> tempCars = new List<Car>();
        //    foreach (Car car in carContext.Cars)
        //    {
        //        tempCars.Add(car);
        //    }

        //    if (searchText.Text == "")
        //    {
        //        reloadCarList();
        //    }
        //    else
        //    {
        //        string str = attributesComboBox.SelectedItem.ToString();
        //        if (str == "") { str = "Model"; }
        //        // Filter the list of cars by model name
        //        var filteredCars = tempCars.Where(car => car.Model.Contains(searchText.Text)).ToList();
        //        //carList.Items.Clear();

        //        carList.ItemsSource = filteredCars;
        //    }
        //}

        private void searchFor(object sender, RoutedEventArgs e)
        {
            // get the selected attribute from the attributeComboBox
            string selectedAttribute = attributesComboBox.SelectedValue.ToString();

            // filter the list of cars based on the selected attribute
            List<Car> filteredCars = new List<Car>();
            foreach (Car car in carContext.Cars)
            {
                PropertyInfo propertyInfo = typeof(Car).GetProperty(selectedAttribute);
                if (propertyInfo != null)
                {
                    string attributeValue = propertyInfo.GetValue(car)?.ToString() ?? "";
                    if (attributeValue.Contains(searchText.Text))
                    {
                        filteredCars.Add(car);
                    }
                }
            }

            // update the carList with the filtered cars
            carList.ItemsSource = filteredCars;
        }



        private void removeCar(object sender, RoutedEventArgs e)
        {
            Car c= (Car)carList.SelectedItem;
            carContext.Cars.Remove(c);
            //carList.Items.Remove(c);
            carContext.SaveChanges();
            reloadCarList();
        }

        private void CarList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected Car object from the ListBox's SelectedItem property
            Car selectedCar = (Car)carList.SelectedItem;
            if (selectedCar != null)
            {
                // Populate the TextBoxes with the selected Car's properties
                BrandTextBox.Text = selectedCar.Brand;
                ModelTextBox.Text = selectedCar.Model;
                ColorTextBox.Text = selectedCar.Color;
            }

            else
            {
                BrandTextBox.Text = "";
                ModelTextBox.Text = "";
                ColorTextBox.Text = "";
            }
        }

        private List<string> GetCarAttributes()
        {
            Type carType = typeof(Car);
            var properties = carType.GetProperties();

            List<string> attributeNames = new List<string>();

            foreach (var property in properties)
            {
                attributeNames.Add(property.Name);
            }
            attributeNames.Remove("Id");
            attributesComboBox.SelectedItem = attributeNames[0];
            return attributeNames;
        }

        private void AttributeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var selectedAttribute = (string)attributesComboBox.SelectedItem;
            //var car = (Car)carList.SelectedItem;

            //var attributeValue = car.GetType().GetProperty(selectedAttribute).GetValue(car, null);
            //attributeValueTextBox.Text = attributeValue.ToString();
        }
    }
}

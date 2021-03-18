using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace HandsClothes.Pages
{
    /// <summary>
    /// Логика взаимодействия для AlterPage.xaml
    /// </summary>
    public partial class AlterPage : Page
    {
        Material material = new Material();
        public AlterPage(Material material)
        {
            InitializeComponent();
            this.material = material;
            this.DataContext = material;
            Type.ItemsSource = Database.db.MaterialTypes.ToList();
            SelectSup.ItemsSource = Database.db.Suppliers.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpg|*.jpg|*.jpeg|*.jpeg"    //"*.png|*.png|*.jpg|*.jpg"
            };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                material.ByteImage = File.ReadAllBytes(dialog.FileName);
                //Image.Source = new BitmapImage(new Uri(dialog.FileName));
                material.Image = dialog.FileName;
                Image.Source = new BitmapImage(new Uri(dialog.FileName));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Database.db.UpdateHistories.RemoveRange(material.UpdateHistories);
            Database.db.MaterialSuppliers.RemoveRange(material.MaterialSuppliers);
            Database.db.Materials.Remove(material);
            Database.db.SaveChanges();
            this.NavigationService.Navigate(new MainPage());
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (material.ID==0)
            {
                Database.db.Materials.Add(material);
            }
        }
    }
}

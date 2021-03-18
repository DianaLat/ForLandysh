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

namespace HandsClothes.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        List<string> sorter = new List<string> { "Наименование", "Остаток на складе", "Стоимоcть", "Показать все" };
        public MainPage()
        {
            InitializeComponent();
            Materials.ItemsSource = Database.db.Materials.ToList();
            Counter.Text = Database.db.Materials.Count() + "/" + Database.db.Materials.Count();
            SortWane.ItemsSource=sorter;
            SortRise.ItemsSource = sorter;
            Filter.ItemsSource = Tools.MyTool.CreateNewItem(Database.db.MaterialTypes.ToList(), new MaterialType { ID = 0, Name = "Показать все" });
        }
        public void Refresh()
        {
            List<Material> content = Database.db.Materials.ToList();
            if (!String.IsNullOrEmpty(Find.Text))
            {
                content = content.Where(i => i.Name.ToLower().Contains(Find.Text.ToLower())).ToList();
            }
            if (Filter.SelectedIndex>0)
            {
                int id = (Filter.SelectedItem as MaterialType).ID;   
                content = content.Where(i => i.MaterialType.ID==id).ToList();
            }
            if (SortWane.SelectedIndex>=0)
            {
                content = Material.OrderToWane(content, SortWane.SelectedIndex);
            }
            if (SortRise.SelectedIndex >= 0)
            {
                content = Material.OrderToRise(content, SortRise.SelectedIndex);
            }
            Counter.Text=content.Count+"/"+ Database.db.Materials.Count();
            Materials.ItemsSource = content;
        }

        private void Find_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void SortWane_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortRise.SelectedIndex = -1;
            Refresh();
        }

        private void SortRise_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortWane.SelectedIndex = -1;
            Refresh();
        }
    }
}

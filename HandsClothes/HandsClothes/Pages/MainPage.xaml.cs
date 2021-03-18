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
        int elementsCount = 15;
        int selectedButtonNum = 1;
        int buttonsCount = 0;
        List<Material> content = new List<Material>();
        List<string> sorter = new List<string> { "Наименование", "Остаток на складе", "Стоимоcть", "Показать все" };
        public MainPage()
        {
            InitializeComponent();
            //Materials.ItemsSource = Database.db.Materials.ToList().Take(elementsCount);
            content = Database.db.Materials.ToList();
            GetButtons(content);
            Counter.Text = Database.db.Materials.Count() + "/" + Database.db.Materials.Count();
            SortWane.ItemsSource=sorter;
            SortRise.ItemsSource = sorter;
            Filter.ItemsSource = Tools.MyTool.CreateNewItem  (Database.db.MaterialTypes.ToList(), new MaterialType { ID = 0, Name = "Показать все" });
        }
        public void Refresh()
        {
            content = Database.db.Materials.ToList();
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
            GetButtons(content);
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
        public void GetButtons(List<Material> list)
        {
            ButtonsStack.Children.Clear();

            buttonsCount = (int)(list.Count() / elementsCount) + (list.Count() % elementsCount == 0 ? 0 : 1);
            Button b1=new Button
            {
                Content="<",
                Margin=new Thickness(5,0,5,0),
                FontSize=16,
            };
            b1.Click += B1_Click;
            ButtonsStack.Children.Add(b1);
            for (int i=1;i<=buttonsCount;i++)
            {
                Button bx=new Button
                {
                    Content = i,
                    Margin = new Thickness(5, 0, 5, 0),
                    FontSize = 16,
                }; bx.Click += Bx_Click;
                ButtonsStack.Children.Add(bx);
            }
            Button b2 = new Button
            {
                Content = ">",
                Margin = new Thickness(5, 0, 5, 0),
                FontSize = 16,
            };
            b2.Click += B2_Click;
            ButtonsStack.Children.Add(b2);

            Materials.ItemsSource = content.Take(elementsCount);
        }

        private void B2_Click(object sender, RoutedEventArgs e)
        {
            if (selectedButtonNum == buttonsCount) return;
            selectedButtonNum = selectedButtonNum+1;
            Materials.ItemsSource = content.Skip(15 * (selectedButtonNum - 1)).Take(elementsCount);
        }

        private void Bx_Click(object sender, RoutedEventArgs e)
        {
            selectedButtonNum = (int)(sender as Button).Content;
            Materials.ItemsSource = content.Skip(15 * (selectedButtonNum-1)).Take(elementsCount);
        }

        private void B1_Click(object sender, RoutedEventArgs e)
        {
            if (selectedButtonNum == 1) return;
            selectedButtonNum = selectedButtonNum - 1;
            Materials.ItemsSource = content.Skip(15 * (selectedButtonNum - 1)).Take(elementsCount);
        }

        private void Materials_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.NavigationService.Navigate(new AlterPage(Materials.SelectedItem as Material));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AlterPage(new Material()));
        }
    }
}

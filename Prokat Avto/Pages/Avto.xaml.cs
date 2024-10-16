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

namespace Prokat_Avto.Pages
{
    /// <summary>
    /// Логика взаимодействия для Avto.xaml
    /// </summary>
    public partial class Avto : Page
    {
        public Avto()
        {
            InitializeComponent();
            Avto1.ItemsSource = ProkatAvtoEntities1.GetContext().Автомобили.OrderBy(p =>p.Модель).ToList();

            var marki = ProkatAvtoEntities1.GetContext().МаркиАвтомобиля.OrderBy(p => p.НаименованиеМаркиАвто).ToList();
            marki.Insert(0, new МаркиАвтомобиля
            {
                НаименованиеМаркиАвто = "Все марки"
            }
            );
            CmbMarki.ItemsSource = marki;
            CmbMarki.SelectedIndex = 0; 

        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
                ProkatAvtoEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            Avto1.ItemsSource = ProkatAvtoEntities1.GetContext().Автомобили.OrderBy(p => p.Модель).ToList();

        }

        private void tb1_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void CmbMarki_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            var currentAvto = ProkatAvtoEntities1.GetContext().Автомобили.OrderBy(p => p.МаркиАвтомобиля).ToList();
            if (CmbMarki.SelectedIndex > 0)
                currentAvto = currentAvto.Where(p => p.МаркиАвтомобиля.НаименованиеМаркиАвто == (CmbMarki.SelectedItem as МаркиАвтомобиля).НаименованиеМаркиАвто).ToList();

            currentAvto = currentAvto.Where(p => p.Модель.ToLower().Contains(tb1.Text.ToLower())).ToList();

            if (CmbSort.SelectedIndex >= 0)
            {
                if (CmbSort.SelectedIndex == 1)
                    currentAvto = currentAvto.OrderBy(p => p.СтоимостьАрендыВСутки).ToList();
                if (CmbSort.SelectedIndex == 0)
                    currentAvto = currentAvto.OrderByDescending(p => p.СтоимостьАрендыВСутки).ToList();
                Avto1.ItemsSource = currentAvto;
            }
            Avto1.ItemsSource = currentAvto;
        }

    }

}

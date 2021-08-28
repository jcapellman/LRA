using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace LRA.Pages
{
    public partial class FavoritesPage : ContentPage
    {
        public FavoritesPage()
        {
            InitializeComponent();

         //   BindingContext = new FavoritesViewModel();

            NavBar.ActiveTab = "Favorites";
        }
    }
}
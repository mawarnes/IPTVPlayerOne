﻿using Xamarin.Forms;

using iptvplayer.Models;
using iptvplayer.ViewModels;

namespace iptvplayer.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}

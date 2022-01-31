using System;
using System.Collections.Generic;
using iptvplayer.Models;
using iptvplayer.Services;
using iptvplayer.ViewModels;
using Xamarin.Forms;

namespace iptvplayer.Views
{
    public partial class PlaylistGroupPage : ContentPage
    {
        PlaylistViewModel _viewModel;

        public PlaylistGroupPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new PlaylistViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}

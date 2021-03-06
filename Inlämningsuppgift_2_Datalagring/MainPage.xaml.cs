﻿using DataAccessLibrary.Data;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Inlämningsuppgift_2_Datalagring
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IEnumerable<Issue> issues { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(AddNewIssue));
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem item = args.SelectedItem as NavigationViewItem;

            switch (item.Tag.ToString())
            {
                case "AddNewIssue":
                    ContentFrame.Navigate(typeof(AddNewIssue));
                    break;
                case "ActiveIssues":
                    ContentFrame.Navigate(typeof(ActiveIssues));
                    break;
                case "InactiveIssues":
                    ContentFrame.Navigate(typeof(InactiveIssues));
                    break;
            }
        }
    }
}

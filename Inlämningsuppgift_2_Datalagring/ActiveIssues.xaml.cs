using DataAccessLibrary.Data;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Inlämningsuppgift_2_Datalagring
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ActiveIssues : Page
    {
        private IEnumerable<Issue> issues { get; set; }
        public ActiveIssues()
        {
            this.InitializeComponent();
            GetIssuesAsync().GetAwaiter();
        }
        private async Task GetIssuesAsync()
        {
            issues = await SQLiteAccess.GetIssues();
            await GetActiveIssuesAsync();
        }
        private async Task GetActiveIssuesAsync()
        {
            lvActiveIssues.ItemsSource = issues.Where(i => i.IssueStatus != "inactive").ToList();
        }
    }
}

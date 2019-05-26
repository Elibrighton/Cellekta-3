using Cellekta_3.Model;
using Cellekta_3.ViewModel;
using Cellekta_3.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace Cellekta_3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IUnityContainer container = new UnityContainer();
            container.RegisterType<ISongListViewModel, SongListViewModel>();
            container.RegisterType<ISongListModel, SongListModel>();

            var window = container.Resolve<SongListView>();
            window.Show();
        }
    }
}

using Cellekta_3.Model;
using Cellekta_3.ViewModel;
using Cellekta_3.View;
using System.Windows;
using Unity;
using SongInterface;
using XmlWrapperInterface;
using TraktorLibraryInterface;
using TraktorLibraryImplementation;
using XmlWrapperImplementation;
using SongImplementation;
using TagInterface;
using TagImplementation;
using MixableRangeInterface;
using MixableRangeImplementation;
using TrackSearchInterface;
using TrackSearchImplementation;
using MixDiscInterface;
using MixDiscImplementation;

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
            container.RegisterType<ITraktorLibrary, TraktorLibrary>();
            container.RegisterType<IXmlWrapper, XmlWrapper>();
            container.RegisterType<ISong, Song>();
            container.RegisterType<ITag, Tag>();
            container.RegisterType<ITempoRange, TempoRange>();
            container.RegisterType<IHarmonicKeyRange, HarmonicKeyRange>();
            container.RegisterType<ITrackSearch, TrackSearch>();
            container.RegisterType<IMixDisc, MixDisc>();
            container.RegisterType<IIntensityRange, IntensityRange>();

            var window = container.Resolve<SongListView>();
            window.Show();
        }
    }
}

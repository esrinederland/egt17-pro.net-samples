using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace demo_configuration.UI
{
    /// <summary>
    /// Business logic for the StartPage
    /// </summary>
    class StartPageViewModel : PropertyChangedBase
    {
        #region fields
        private Presenter _presenter = null;
        private ImageSource _startImage;
        #endregion

        #region Properties

        /// <summary>
        /// XAML bindable implementation of StartImage
        /// </summary>
        public ImageSource StartImage
        {
            get { return _startImage; }
            set { SetProperty(ref _startImage, value, () => StartImage); }
        }

        #endregion

        #region Commands

        /// <summary>
        /// XAML bindable implementation of AreasOfInterest
        /// </summary>
        public List<Presenter> AreasOfInterest => GetPresenterData();

        /// <summary>
        /// Method wich is clled when a project is chosen
        /// </summary>
        public Presenter SelectedAreaOfInterest
        {
            get { return _presenter; }
            set
            {
                if (value == null)
                    return;

                _presenter = value;

                NotifyPropertyChanged();

                Project.OpenAsync(_presenter.Project);
            }
        }

        /// <summary>
        /// method to create and return a list of presenters
        /// </summary>
        /// <returns></returns>
        internal static List<Presenter> GetPresenterData()
        {
            return new List<Presenter>()
            {
                new Presenter()
                {
                    AreaName = "Nijmegen",
                    Image = new BitmapImage(new Uri(@"D:\GISTech\2017\Pro\demo_configuration\demo_configuration\Images\PM.png")),
                    Project = @"D:\GISTech\2017\Pro\Maps\Nijmegen\Nijmegen.aprx"
                },
                new Presenter()
                {
                    AreaName = "Utrecht",
                    Image = new BitmapImage(new Uri(@"D:\GISTech\2017\Pro\demo_configuration\demo_configuration\Images\FH.png")),
                    Project = @"D:\GISTech\2017\Pro\Maps\Utrecht\Utrecht.aprx"
                },
                new Presenter()
                {
                    AreaName = "Zwolle",
                    Image = new BitmapImage(new Uri(@"D:\GISTech\2017\Pro\demo_configuration\demo_configuration\Images\MVH.png")),
                    Project = @"D:\GISTech\2017\Pro\Maps\Zwolle\Zwolle.aprx"
                },
                new Presenter()
                {
                    AreaName = "Heerenveen",
                    Image = new BitmapImage(new Uri(@"D:\GISTech\2017\Pro\demo_configuration\demo_configuration\Images\MJ.png")),
                    Project = @"D:\GISTech\2017\Pro\Maps\Heerenveen\Heerenveen.aprx"
                }
            };
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using System.Collections.ObjectModel;

namespace demo_DL
{
    internal class Dockpane1ViewModel : DockPane
    {
        private const string _dockPaneID = "demo_DL_Dockpane1";
        private ObservableCollection<Presenter> _presenterList;

        protected Dockpane1ViewModel()
        {
            _presenterList = new ObservableCollection<Presenter>();
            FillPresenterList();
        }

        /// <summary>
        /// method to fill the presenterlist with the dev day presenters
        /// </summary>
        private void FillPresenterList()
        {
            PresenterList.Add(new Presenter() { ID = 1, FirstName = "Mark", LastName = "Jagt" });
            PresenterList.Add(new Presenter() { ID = 2, FirstName = "Fons", LastName = "Hissink" });
            PresenterList.Add(new Presenter() { ID = 3, FirstName = "Peter", LastName = "Mallo" });
            PresenterList.Add(new Presenter() { ID = 4, FirstName = "Maarten", LastName = "van Hulzen" });
        }

        public ObservableCollection<Presenter> PresenterList
        {
            get { return _presenterList; }
            set { SetProperty(ref _presenterList, value, () => PresenterList); }
        }

        /// <summary>
        /// Show the DockPane.
        /// </summary>
        internal static void Show()
        {
            DockPane pane = FrameworkApplication.DockPaneManager.Find(_dockPaneID);
            if (pane == null)
                return;

            pane.Activate();
        }

        /// <summary>
        /// Text shown near the top of the DockPane.
        /// </summary>
        private string _heading = "My DockPane";
        public string Heading
        {
            get { return _heading; }
            set
            {
                SetProperty(ref _heading, value, () => Heading);
            }
        }
    }

    /// <summary>
    /// Button implementation to show the DockPane.
    /// </summary>
    internal class Dockpane1_ShowButton : Button
    {
        protected override void OnClick()
        {
            Dockpane1ViewModel.Show();
        }
    }

    /// <summary>
    /// Presenter object class
    /// </summary>
    public class Presenter
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        public int ID { get { return _id; } set { _id = value; } }
        public String LastName { get { return _firstName; } set { _firstName = value; } }
        public String FirstName { get { return _lastName; } set { _lastName = value; } }
    }
}

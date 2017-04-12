using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace demo_popup
{
    internal class AdvancedCustomMessageBox : MapTool
    {
        public AdvancedCustomMessageBox()
        {
            IsSketchTool = true;
            SketchType = SketchGeometryType.Rectangle;
            SketchOutputMode = SketchOutputMode.Map;
        }

        protected override Task OnToolActivateAsync(bool active)
        {
            return base.OnToolActivateAsync(active);
        }

        protected override async Task<bool> OnSketchCompleteAsync(ArcGIS.Core.Geometry.Geometry geometry)
        {
            var popupContent = await QueuedTask.Run(() =>
            {
            var mapView = MapView.Active;
            if (mapView == null)
                return null;

            //Get the features that intersect the sketch geometry.
            var result = mapView.GetFeatures(geometry);

            //For each feature in the result create a new instance of our custom popup content class.
            List<PopupContent> popups = new List<PopupContent>();

                foreach (var kvp in result)
                {                    
                    kvp.Value.ForEach(id => popups.Add(new DynamicPopupContent(kvp.Key, id)));
                }

                //Flash the features that intersected the sketch geometry.
                mapView.FlashFeature(result);

                //return the collection of popup content object.
                return popups;
            });

            //Show the custom pop-up with the custom commands and the default pop-up commands. 
            MapView.Active.ShowCustomPopup(popupContent);
            return true;
        }

        private List<PopupCommand> CreateCommands()
        {
            var commands = new List<PopupCommand>();

            //Add a new instance of a popup command passing in the delegate to be run when the button is clicked.
            commands.Add(new PopupCommand(OnPopupCommand, CanExecutePopupCommand,
              "Show attendees",
              new BitmapImage(new Uri("pack://application:,,,/CustomPopup;component/Images/GenericButtonRed12.png")) as ImageSource)
            {
                IsSeparator = true
            });

            return commands;
        }

        void OnPopupCommand(PopupContent content)
        {
            //Cast the content parameter to our custom popup content class.
            DynamicPopupContent dynamicContent = content as DynamicPopupContent;
            if (dynamicContent == null)
                return;
        }

        bool CanExecutePopupCommand(PopupContent content)
        {
            return content != null;
        }
    }
   
}

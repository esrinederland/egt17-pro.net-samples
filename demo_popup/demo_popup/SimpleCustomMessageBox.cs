using System.Collections.Generic;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Mapping;
using System.IO;

namespace demo_popup
{
    internal class SimpleCustomMessageBox : Button
    {
        protected override void OnClick()
        {
            // load the html template
            var html = File.ReadAllText(@"D:\GISTech\2017\Pro\demo_popup\demo_popup\simpleTemplate.html");
            
            // create popupcontent from the html template
            PopupContent simplePopupContent = new PopupContent(html, "Hallo DevDay");

            // add popupcontent to list of popupcontents
            List<PopupContent> popups = new List<PopupContent>();
            popups.Add(simplePopupContent);

            //show list of popupcontents
            MapView.Active.ShowCustomPopup(popups);
        }
    }
}

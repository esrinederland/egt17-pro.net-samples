using ArcGIS.Desktop.Framework.Contracts;

namespace demo_popup
{
    internal class ArcGISMessageBox : Button
    {
        protected override void OnClick()
        {
            ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Dit is een standaard ArcGIS messagebox", "Hallo DevDay");
        }
    }
}

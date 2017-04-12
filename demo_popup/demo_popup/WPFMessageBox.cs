using ArcGIS.Desktop.Framework.Contracts;

namespace demo_popup
{
    internal class WPFMessageBox : Button
    {
        protected override void OnClick()
        {
            System.Windows.MessageBox.Show("Dit is een standaard messagebox", "Hallo DevDay");
        }
    }
}

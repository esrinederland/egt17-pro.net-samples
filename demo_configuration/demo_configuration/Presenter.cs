using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace demo_configuration
{
    /// <summary>
    /// Presenter object
    /// </summary>
    class Presenter
    {
        // bindable area object
        public String AreaName { get; set; }
        // bindable image
        public ImageSource Image { get; set; }
        // project to open
        public string Project { get; set; }

    }
}

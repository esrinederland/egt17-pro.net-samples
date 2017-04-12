using ArcGIS.Core.Data;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_popup
{
    internal class DynamicPopupContent : PopupContent
    {
        private Dictionary<FieldDescription, string> _values = new Dictionary<FieldDescription, string>();

        /// <summary>
        /// Constructor initializing the base class with the layer and object id associated with the pop-up content
        /// </summary>
        public DynamicPopupContent(MapMember mapMember, long id) : base(mapMember, id)
        {
            //Set property indicating the html content will be generated on demand when the content is viewed.
            IsDynamicContent = true;
        }

        /// <summary>
        /// Called the first time the pop-up content is viewed. This is good practice when you may show a pop-up for multiple items at a time. 
        /// This allows you to delay generating the html content until the item is actually viewed.
        /// </summary>
        protected override Task<string> OnCreateHtmlContent()
        {
            return QueuedTask.Run(() =>
            {
                var invalidPopup = "<p>Pop-up content could not be generated for this feature.</p>";

                var layer = MapMember as BasicFeatureLayer;
                if (layer == null)
                    return invalidPopup;

                //Get all the visible numeric fields for the layer.
                var fields = layer.GetFieldDescriptions().Where(f => f.IsVisible);

                //Create a query filter using the fields found above and a where clause for the object id associated with this pop-up content.
                var tableDef = layer.GetTable().GetDefinition();
                var oidField = tableDef.GetObjectIDField();

                var queryFilter = new QueryFilter() { WhereClause = $@"{oidField} = {ID}", SubFields = string.Join(",", fields.Select(f => f.Name)) };
                var rows = layer.Search(queryFilter);

                //Get the first row, there should only be 1 row.
                if (!rows.MoveNext())
                    return invalidPopup;

                var row = rows.Current;

                //Loop through the fields, extract the value for the row and add to a dictionary.
                foreach (var field in fields)
                {
                    if (field.Alias.Equals("User_Naam", StringComparison.CurrentCultureIgnoreCase) || 
                        field.Alias.Equals("User_Aanta", StringComparison.CurrentCultureIgnoreCase))
                    {
                        var val = row[field.Name];
                        if (val is DBNull || val == null)
                            continue;

                        string value = val.ToString();

                        _values.Add(field, value);
                    }
                }

                if (_values.Count == 0)
                    return invalidPopup;

                //Construct a new html string that we will use to update our html template.
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("data.addColumn('string', 'Bedrijf')");  // create a header label
                sb.AppendLine("data.addColumn('number', 'Deelnemers')"); // create a header label
                sb.AppendLine("data.addRows([");

                ////Add each value to the html string.                
                foreach (var v in _values)
                {
                    int outvalue = -1;
                    if (int.TryParse(v.Value, out outvalue))
                    {
                        sb.Append(string.Format("{{v: {0} }}],", v.Value));

                    }
                    else
                    {
                        sb.AppendLine($@"['{v.Value}',");
                    }
                }

                sb.AppendLine("]);");

                //Get the html from the template file on disk that we have packaged with our add-in.
                var htmlPath = @"D:\GISTech\2017\Pro\demo_popup\demo_popup\advancedtemplate.html";
                var html = File.ReadAllText(htmlPath);

                //Update the template with our custom html and return it to be displayed in the pop-up window.
                html = html.Replace("//data.addColumn", sb.ToString());
                return html;
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cdLAYER
{
    class Data
    {
        public static DataTable ConvertListToDataTable(List<LayerItem> list)
        {
            DataTable table = new System.Data.DataTable();

            table.Columns.Add(); //Add empty column
            table.Columns[0].ColumnName = "Layer";

            table.Columns.Add(); //Add empty column
            table.Columns[1].ColumnName = "Layoutname";
            int incr = 2;
            foreach (LayerItem layer in list)
            {
                DataRow newRow = table.NewRow();
                table.Rows.Add(layer);
                incr++;
            }

            return table;
        }

    }
}

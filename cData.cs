using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cdLAYER
{
    class cData
    {
        public static DataTable ConvertListToDataTable(List<LayerItem> list)
        {
            DataTable table = new System.Data.DataTable();

            table.Columns.Add(); //Add empty column
            table.Columns[0].ColumnName = "Layer";
            table.Columns.Add(); //Add empty column
            table.Columns[1].ColumnName = "IsOff";
            table.Columns.Add(); //Add empty column
            table.Columns[2].ColumnName = "IsFrozen";


            int incr = 2;
            foreach (LayerItem layer in list)
            {
                DataRow newRow = table.NewRow();
                table.Rows.Add(layer.Name, layer.IsOff, layer.IsFrozen);
                incr++;
            }

            return table;
        }

    }
}

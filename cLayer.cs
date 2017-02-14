using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Autocad usings
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.EditorInput;
#endregion


namespace cdLAYER
{
    class cLayer
    {
        public static void TurnLayerOff(string layername)
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                LayerTable acLyrTbl = acTrans.GetObject(acCurDb.LayerTableId, OpenMode.ForRead) as LayerTable;

                if (acLyrTbl.Has(layername) == false)
                {
                    using (LayerTableRecord acLyrTblRec = new LayerTableRecord())
                    {
                        acLyrTblRec.Name = layername;
                        acLyrTbl.UpgradeOpen();
                        acLyrTbl.Add(acLyrTblRec);
                        acTrans.AddNewlyCreatedDBObject(acLyrTblRec, true);

                        // Turn the layer off
                        acLyrTblRec.IsOff = true;
                    }
                }
                else
                {
                    LayerTableRecord acLyrTblRec = acTrans.GetObject(acLyrTbl[layername], OpenMode.ForWrite) as LayerTableRecord;

                    // Turn the layer off
                    acLyrTblRec.IsOff = true;
                }
                acTrans.Commit();
            }
        }

        public static void TurnLayerOn(string layername)
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                LayerTable acLyrTbl = acTrans.GetObject(acCurDb.LayerTableId, OpenMode.ForRead) as LayerTable;

                if (acLyrTbl.Has(layername) == false)
                {
                    using (LayerTableRecord acLyrTblRec = new LayerTableRecord())
                    {
                        acLyrTblRec.Name = layername;
                        acLyrTbl.UpgradeOpen();
                        acLyrTbl.Add(acLyrTblRec);
                        acTrans.AddNewlyCreatedDBObject(acLyrTblRec, true);

                        // Turn the layer off
                        acLyrTblRec.IsOff = false;
                    }
                }
                else
                {
                    LayerTableRecord acLyrTblRec = acTrans.GetObject(acLyrTbl[layername], OpenMode.ForWrite) as LayerTableRecord;

                    // Turn the layer off
                    acLyrTblRec.IsOff = false;
                }
                acTrans.Commit();
            }
        }

        public static List<LayerItem> ReadLayers(Database acCurDb)
        {
            List<LayerItem> result = new List<LayerItem>();

            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                LayerTable acLyrTbl = acTrans.GetObject(acCurDb.LayerTableId, OpenMode.ForRead) as LayerTable;

                foreach (ObjectId acObjId in acLyrTbl)
                {
                    LayerTableRecord acLyrTblRec;
                    acLyrTblRec = acTrans.GetObject(acObjId, OpenMode.ForRead) as LayerTableRecord;
                    result.Add(new LayerItem { Name = acLyrTblRec.Name, IsOff = acLyrTblRec.IsOff, IsFrozen = acLyrTblRec.IsFrozen });
                }
                return result;
            }
        }

    }

    public class LayerItem
    {
        public string Name { get; set; }
        public bool IsOff { get; set; }
        public bool IsFrozen { get; set; }
    }
}


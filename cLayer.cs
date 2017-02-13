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
        public static void CreateLayer(string layName, string layDescr, string linetype, short color)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    // Get the layer table from the drawing
                    LayerTable lt = (LayerTable)tr.GetObject(db.LayerTableId, OpenMode.ForRead);

                    // Define an array of colors for the layers
                    //Color[] acColors = new Color[3];
                    //acColors[0] = Color.FromColorIndex(ColorMethod.ByAci, 1);
                    //acColors[1] = Color.FromRgb(23, 54, 232);

                    if (lt.Has(layName))
                        ed.WriteMessage("\nLayername: {0} already exists.", layName);
                    else
                    {
                        // Create our new layer table record...
                        LayerTableRecord ltr = new LayerTableRecord();

                        // ... and set its properties
                        ltr.Name = layName;

                        // Add the new layer to the layer table
                        lt.UpgradeOpen();
                        ObjectId ltId = lt.Add(ltr);
                        tr.AddNewlyCreatedDBObject(ltr, true);

                        // Layer Description
                        ltr.Description = layDescr; 

                        // Layer Color
                        ltr.Color = Color.FromColorIndex(ColorMethod.ByAci, color);

                        // Layer Linetype
                        var lineTypeTable = (LinetypeTable)tr.GetObject(db.LinetypeTableId, OpenMode.ForRead);
                        ltr.LinetypeObjectId = lineTypeTable[linetype];

                        // Set the layer to be current for this drawing
                        db.Clayer = ltId;

                        // Report what we've done
                        ed.WriteMessage("\nCreated layer named {0}", layName);
                    }
                }
                catch
                {
                    // An exception has been thrown, indicating the
                    // name is invalid
                    ed.WriteMessage("\nInvalid layer name.");
                }
                // Commit the transaction
                tr.Commit();
            }
        }

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

        public static void ReadLayers()
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                LayerTable acLyrTbl = acTrans.GetObject(acCurDb.LayerTableId, OpenMode.ForRead) as LayerTable;

                string sLayerNames = "";

                foreach (ObjectId acObjId in acLyrTbl)
                {
                    LayerTableRecord acLyrTblRec; acLyrTblRec = acTrans.GetObject(acObjId, OpenMode.ForRead) as LayerTableRecord;

                    sLayerNames = sLayerNames + "\n" + acLyrTblRec.Name;
                }

                Application.ShowAlertDialog("The layers in this drawing are: " + sLayerNames);
            }
        }

    }
}


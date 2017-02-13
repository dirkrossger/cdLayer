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
using Autodesk.AutoCAD.EditorInput;
#endregion

namespace cdLAYER
{
    public class cCommands
    {
        [CommandMethod("xx")]
        public void LayerOnOff()
        {
            cLayer.ReadLayers();

            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Editor ed = acDoc.Editor;

            PromptStringOptions pso = new PromptStringOptions("\nGive Layer name");
            PromptResult res = ed.GetString(pso);
            if (res.Status != PromptStatus.OK)
                return;
            string name = res.StringResult;
            cLayer.TurnLayerOff(name);


        }
    }
}

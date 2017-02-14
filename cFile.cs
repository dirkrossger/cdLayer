using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;


namespace cdLAYER
{
    public class cFile
    {
        
        public List<string> Filenames
        {
            get { return _Filenames; }

            set { _Filenames = value; }
        }

        public string Filename
        {
            get
            {
                return _Filename;
            }

            set
            {
                _Filename = value;
            }
        }

        public List<LayerItem> List
        {
            get
            {
                return _List;
            }

            set
            {
                _List = value;
            }
        }

        private List<string> _Filenames;
        private string _Filename;
        public void Add()
        {
            System.Windows.Forms.OpenFileDialog _dialog;
            _dialog = new System.Windows.Forms.OpenFileDialog();
            _dialog.Multiselect = false;
            _dialog.InitialDirectory = "C:/";
            _dialog.Title = "Select Autocad dwg ritdef file";
            _dialog.Filter = "dwg file | *.dwg";
            _dialog.FilterIndex = 2;
            _dialog.RestoreDirectory = true;

            _Filenames = new List<string>();

            if (_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _Filename = _dialog.FileName;
            }
        }


        private List<LayerItem> _List;
        public bool Read(string filename)
        {
            Database workingDB = HostApplicationServices.WorkingDatabase;
            Database db = new Database(false, true);

            try
            {
                db.ReadDwgFile(filename, System.IO.FileShare.ReadWrite, false, "");
                db.CloseInput(true);
                HostApplicationServices.WorkingDatabase = db;
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("\nUnable to open .dwg file : " + ex.StackTrace);
                return false;
            }

            Transaction tr = db.TransactionManager.StartTransaction();
            using (tr)
            {
                _List = cLayer.ReadLayers(db);
                HostApplicationServices.WorkingDatabase = workingDB;
                db.SaveAs(filename, DwgVersion.Current);

                return true;
            }
        }
    }
}

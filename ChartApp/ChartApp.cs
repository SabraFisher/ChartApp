namespace ChartApp
{
    public partial class ChartApp : Form
    {
        private string _filePath = "";
        private string _rawFile = "";
        private List<string>_lines = new List<string>();
        public ChartApp()
        {
            InitializeComponent();
        }


        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            //open a file dialog content and file
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //setting the properties on the fileDialog
                openFileDialog.InitialDirectory = Path.Combine(Environment.CurrentDirectory, "Data");
                openFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*,*)|*.*";
                //open the file dialog and do something
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //process the file information

                    //get the path of the specified file
                    _filePath=openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        _rawFile = reader.ReadToEnd();
                        while (reader.Peek() >= 0)
                        {
                            
                            _lines.Add(reader.ReadLine());
                        }
                    }
                }
            }
            MessageBox.Show(_rawFile, "File Content at path: " + _filePath, MessageBoxButtons.OK);
        }

    }
}

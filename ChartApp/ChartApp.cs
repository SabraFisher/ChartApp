using System.Globalization;
using System.Linq;
using CsvHelper;

namespace ChartApp
{
    public partial class ChartApp : Form
    {
        private string _filePath = "";
        private string _rawFile = "";
        private List<WeatherEvent> _events = new List<WeatherEvent>();

        public ChartApp()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            //load the data into the chart
            lblDisplay.Text = "";

            for (int i = 0; i < _events.Count; i++)
            {
                lblDisplay.Text += _events[i].ToString() + "\n";
            }

            DrawGraph();
        }

        private void DrawGraph()
        {
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            Pen blackPen = new Pen(blackBrush);
            Graphics g = this.CreateGraphics();

            int startX = 100;
            int startY = 10;
            int sizeX= 300;
            int sizeY = 300;

            Point topLeft = new Point(startX, startY);
            Point bottomRight = new Point(startX + sizeX, startY + sizeY);
            
            g.DrawLine(blackPen, topLeft, bottomRight );
            //g.Clear(Color.White);
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
                    _filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        _events = csv.GetRecords<WeatherEvent>().ToList;
                        LoadData();
                    }

                        //{
                        //  _rawFile = reader.ReadToEnd();

                        //  while (reader.Peek() >= 0)
                        //  {
                        //      _lines.Add(reader.ReadLine());
                        //  }
                        // }
                    
                }
            }

        }
    }
}

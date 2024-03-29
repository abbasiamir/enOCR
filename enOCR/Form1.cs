namespace enOCR
{
    public partial class Form1 : Form
    {
        PatternRecognition patternRecognition;
        public Form1()
        {
            InitializeComponent();
            patternRecognition = new PatternRecognition();
        }
        string[] files;
        
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
           
            DialogResult result = openFileDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                files = openFileDialog1.FileNames;
            }
            for (int i = 0; i < files.Count(); i++)
            {
               Bitmap bmp = new Bitmap(files[i]);
                patternRecognition.MakeText(bmp, richTextBox1);
               
            }
            

            

        }
    }
}
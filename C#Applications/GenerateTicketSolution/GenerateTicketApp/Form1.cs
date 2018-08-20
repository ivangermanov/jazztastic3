using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using ZXing;
using System.IO;

namespace GenerateTicketApp
{
    public partial class Form1 : Form
    {
        PrintDocument pd;
        PaperSize ps;
        string projectPath;
        int govtIdForQR = 1990;
        int visitorNumber = 5;
        string visitorName = "Jaap Geurts";
        string ticketType = "2-day";
        string campingSpot = "Yes - spot 1";
        string price = "€55.00";

        public Form1()
        {
            InitializeComponent();
            projectPath = Directory.GetParent(Directory.GetParent(Application.StartupPath).ToString()).ToString();
            pd = new PrintDocument();
            ps = new PaperSize("ticket", 590, 594);
            PrintController printController = new StandardPrintController();
            pd.PrintController = printController;
            pd.DefaultPageSettings.PaperSize = ps;
            pd.PrinterSettings.PrintToFile = true;
            pd.PrinterSettings.PrintFileName = projectPath + @"\Resources\tickets\test.pdf";
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            pd.Print();
        }

        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Initialize graphics, fonts and brush
            Graphics g = e.Graphics;
            Font fInfo = new Font("Arial", 17, FontStyle.Regular);
            Font fBody = new Font("Arial", 14, FontStyle.Regular);
            Font fBold = new Font("Arial", 22, FontStyle.Bold);
            Font fTerms = new Font("Arial", 20, FontStyle.Bold);
            Font fJazztastic = new Font("Garamond", 28, FontStyle.Regular);
            SolidBrush sb = new SolidBrush(Color.Black);

            //Draw logo
            Image logo = Image.FromFile(projectPath + @"\Resources\jazzstastic3logo.png");
            g.DrawImage(logo, 40, 100, (float)409.5, (float)238.5);

            //Generate QR code, convert it to image and draw it
            BarcodeWriter writer = new BarcodeWriter { Format = BarcodeFormat.QR_CODE };
            Bitmap result = writer.Write(govtIdForQR.ToString());
            Bitmap qrBitmap = new Bitmap(result);
            Image imgQRCode = qrBitmap;
            g.DrawImage(imgQRCode, 465, 30, (float)367.5, (float)364.5);

            //Draw top lines
            g.DrawLine(new Pen(Color.Black, 2), new Point(60, 400), new Point(760, 400));
            g.DrawLine(new Pen(Color.Black, 2), new Point(60, 400), new Point(60, 485));
            g.DrawLine(new Pen(Color.Black, 2), new Point(760, 400), new Point(760, 485));
            g.DrawLine(new Pen(Color.Black, 2), new Point(410, 400), new Point(410, 485));

            /*Draw text between lines*/
            //Event
            g.DrawString("Event", fBold, sb, new Point(85, 405));
            g.DrawString("Jazztastic 3 2018", fJazztastic, sb, new Point(100, 435));

            //Ticket Number
            g.DrawString("Visitor number", fBold, sb, new Point(435, 405));
            g.DrawString(visitorNumber.ToString(), fInfo, sb, new Point(450, 445));

            //Location
            g.DrawString("Location", fBold, sb, new Point(85, 515));
            g.DrawString("Molecaten Park Kuierpad", fInfo, sb, new Point(100, 550));

            //Name
            g.DrawString("Name", fBold, sb, new Point(435, 515));
            g.DrawString(visitorName, fInfo, sb, new Point(450, 550));

            //City
            g.DrawString("Type", fBold, sb, new Point(85, 605));
            g.DrawString(ticketType, fInfo, sb, new Point(100, 640));

            //Type
            g.DrawString("Camping", fBold, sb, new Point(435, 605));
            g.DrawString(campingSpot, fInfo, sb, new Point(450, 640));

            //Address
            g.DrawString("Address", fBold, sb, new Point(85, 695));
            g.DrawString("Oranjekanaal Noordzijde 10", fInfo, sb, new Point(100, 730));

            //Camping
            g.DrawString("City", fBold, sb, new Point(435, 695));
            g.DrawString("Wezuperbrug", fInfo, sb, new Point(450, 730));

            //Date
            g.DrawString("Date", fBold, sb, new Point(85, 785));
            g.DrawString("00/00/0000", fInfo, sb, new Point(100, 820));

            //Price
            g.DrawString("Price", fBold, sb, new Point(435, 785));
            g.DrawString(price, fInfo, sb, new Point(450, 820));

            //Draw bottom line
            g.DrawLine(new Pen(Color.Black, 2), new Point(60, 900), new Point(760, 900));

            //Draw terms and conditions
            g.DrawString("General terms and conditions", fTerms, sb, new Point(60, 920));
            RectangleF rectF1 = new RectangleF(85, 960, 700, 200);
            g.DrawString("The person gaining access to the event stated on the ticket declares to agree to the general terms and conditions and regulations of this event. Both are accessible for inspection at the entrance of the event or can be consulted via http://www.jazztastic3.com. A copy of the general terms and regulations can also be obtained via clientservice at clientservice@jazztastic3.com", fBody, sb, rectF1);



            //Dispose graphics
            g.Dispose();
        }

        protected override void SetVisibleCore(bool value)
        {
            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
                value = false;
            }
            base.SetVisibleCore(value);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

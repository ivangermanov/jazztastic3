using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Drawing;
using System.Drawing.Printing;
using ZXing;
using System.IO;
using System.Net.Mime;
using System.Threading;
using System.Diagnostics;

namespace Jazztastic3ASPXWebForms
{
    static class GenerateTicket
    {
        //methods
        private static string folderPath;
        private static long visitorNo;
        private static Attachment attachment;

        private static void GetTicketAsAttachment()
        {
            attachment = new Attachment(folderPath + $@"\ticket{visitorNo}.pdf", MediaTypeNames.Application.Pdf);
        }
        public static Attachment GetAttachmentAsPDF(long visitorNo, string fName, string lName, string govtId, string ticketDates, string campingSpot, string areaLetter)
        {
            CreateTicketPdf(visitorNo, fName, lName, govtId, ticketDates, campingSpot, areaLetter);
            return attachment;
        }
        private static void DeleteTicketFromFolder()
        {
            File.Delete(folderPath + $@"/ticket{visitorNo}.pdf");
        }
        private static bool IsFileReady(string filename)
        {
            // If the file can be opened for exclusive access it means that the file
            // is no longer locked by another process.
            try
            {
                using (FileStream inputStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None))
                    return inputStream.Length > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private static void CreateTicketPdf(long visitorNumber, string fName, string lName, string govtId, string ticketDates, string campingSpot, string areaLetter)
        {
            PrintDocument pd = new PrintDocument();
            PaperSize ps = new PaperSize("ticket", 590, 594);
            PrintController printController = new StandardPrintController();
            pd.PrintController = printController;
            pd.DefaultPageSettings.PaperSize = ps;
            pd.PrinterSettings.PrintToFile = true;
            folderPath = HttpContext.Current.Server.MapPath("~/Resources/tickets");
            visitorNo = visitorNumber;

            string visitorName = fName + " " + lName;
            string ticketType = "";
            int numberOfDates = 0;
            if (ticketDates[0] == 'T')
            {
                ticketType += "01/07 ";
                numberOfDates++;
            }
            if (ticketDates[1] == 'T')
            {
                ticketType += "02/07 ";
                numberOfDates++;
            }
            if (ticketDates[2] == 'T')
            {
                ticketType += "03/07 ";
                numberOfDates++;
            }

            string price = "";
            if (numberOfDates == 1)
                price = "€40.00";
            else if (numberOfDates == 2)
                price = "€45.00";
            else if (numberOfDates == 3)
                price = "€55.00";

            pd.PrinterSettings.PrintFileName = folderPath + $@"\ticket{visitorNo}.pdf";
            pd.PrintPage += new PrintPageEventHandler(PrintPage);
            if (File.Exists(folderPath + $@"/ticket{visitorNo}.pdf"))
            {
                var time = Stopwatch.StartNew();
                while (!IsFileReady(folderPath + $@"\ticket{ visitorNo}.pdf"))
                {
                    if (time.ElapsedMilliseconds == 10000)
                    {
                        throw new Exception("Failed perform action within allotted time.");
                    }
                }
                File.Delete(folderPath + $@"/ticket{visitorNo}.pdf");
            }
            pd.Print();
            pd.Dispose();
            Thread.Sleep(1000);
            GetTicketAsAttachment();

            void PrintPage(object sender, PrintPageEventArgs e)
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
                Image logo = Image.FromFile(HttpContext.Current.Server.MapPath("~/Resources/jazzstastic3logo.png"));
                g.DrawImage(logo, 40, 100, (float)409.5, (float)238.5);

                //Generate QR code, convert it to image and draw it
                BarcodeWriter writer = new BarcodeWriter { Format = BarcodeFormat.QR_CODE };
                Bitmap result = writer.Write(govtId.ToString());
                Bitmap qrBitmap = new Bitmap(result);
                Image imgQRCode = qrBitmap;
                g.DrawImage(imgQRCode, 465, 30, (float)367.5, (float)364.5);

                //Draw top lines
                g.DrawLine(new Pen(Color.Black, 2), new Point(60, 400), new Point(760, 400));
                g.DrawLine(new Pen(Color.Black, 2), new Point(60, 400), new Point(60, 485));
                g.DrawLine(new Pen(Color.Black, 2), new Point(760, 400), new Point(760, 485));
                g.DrawLine(new Pen(Color.Black, 2), new Point(410, 400), new Point(410, 485));

                /*Draw text between lines*/
                g.DrawString("Event", fBold, sb, new Point(85, 405));
                g.DrawString("Jazztastic 3 2018", fJazztastic, sb, new Point(100, 435));

                //Ticket Number
                g.DrawString("Visitor number", fBold, sb, new Point(435, 405));
                g.DrawString(visitorNo.ToString(), fInfo, sb, new Point(450, 445));

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
                //TODO: camping spot == "NULL" fix after implementing camping spots
                if (campingSpot == "")
                    g.DrawString("No", fInfo, sb, new Point(450, 640));
                else
                    g.DrawString(areaLetter + " " + campingSpot, fInfo, sb, new Point(450, 640));

                //Address
                g.DrawString("Address", fBold, sb, new Point(85, 695));
                g.DrawString("Oranjekanaal Noordzijde 10", fInfo, sb, new Point(100, 730));

                //Camping
                g.DrawString("City", fBold, sb, new Point(435, 695));
                g.DrawString("Wezuperbrug", fInfo, sb, new Point(450, 730));

                //Date
                g.DrawString("Dates", fBold, sb, new Point(85, 785));
                g.DrawString("01/07/2018-03/07/2018", fInfo, sb, new Point(100, 820));

                //Price
                g.DrawString("Price", fBold, sb, new Point(435, 785));
                g.DrawString(price, fInfo, sb, new Point(450, 820));

                //Draw bottom line
                g.DrawLine(new Pen(Color.Black, 2), new Point(60, 900), new Point(760, 900));

                //Draw terms and conditions
                g.DrawString("General terms and conditions", fTerms, sb, new Point(60, 920));
                RectangleF rectF1 = new RectangleF(85, 960, 700, 200);
                g.DrawString("The person gaining access to the event stated on the ticket declares to agree to the general terms and conditions and regulations of this event. Both are accessible for inspection at the entrance of the event or can be consulted via http://www.jazztastic3.com. A copy of the general terms and regulations can also be obtained via clientservice at jazztastic3service@gmail.com", fBody, sb, rectF1);

                //Dispose graphics
                g.Dispose();
            }
        }
    }
}
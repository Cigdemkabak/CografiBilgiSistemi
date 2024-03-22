using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CografiBilgiSistemi
{
    public partial class Form1 : Form
    {
        GMapOverlay katman1;
        List<Arac> list;
        public Form1()
        {
            InitializeComponent();
            initializeMap();
            AracListesiOlustur();
        }

        private void AracListesiOlustur()
        {
            list = new List<Arac>();
            list.Add(new Arac("34GJG620", "SUV", "Ankara", "İstanbul", new PointLatLng(40.05, 32.22)));
            list.Add(new Arac("34GJK123", "TİCARİ", "İzmir", "İstanbul", new PointLatLng(39.22, 27.67)));
            list.Add(new Arac("34GJM610", "TIR", "İstanbul", "Erzincan", new PointLatLng(40.67, 30.24)));
            list.Add(new Arac("34MJK640", "KAMYON", "İstanbul", "İzmir", new PointLatLng(40.30, 37.47)));
            list.Add(new Arac("34HJK650", "TAKSİ", "İstanbul", "Antalya", new PointLatLng(38.75, 30.43)));
            list.Add(new Arac("34HJK650", "MİNİBÜS", "İstanbul", "Antalya", new PointLatLng(39.75, 21.43)));
            list.Add(new Arac("34HJK650", "TREN", "İstanbul", "Antalya", new PointLatLng(32.65, 30.43)));
            list.Add(new Arac("34HJK650", "OTOBÜS", "İstanbul", "Antalya", new PointLatLng(22.78, 30.43)));
            list.Add(new Arac("34HJK650", "TAKSİ", "İstanbul", "Antalya", new PointLatLng(38.75, 30.43)));
        }

        private void initializeMap()
        {
            Map.DragButton = MouseButtons.Left;
            Map.MapProvider = GMapProviders.GoogleMap;   //  GoogleHybridMap fena değil :)
            Map.Position = new GMap.NET.PointLatLng(36.0, 42.0);
            Map.Zoom = 4;
            Map.MinZoom = 3;
            Map.MaxZoom = 25;
            katman1 = new GMapOverlay();

            // Bir Overlay (katman) oluşturmamız lazım
            // Harita üzerinde görüntülenecek tüm componentleri bu katman (overlay) eklememiz gerekli.
            //GMapOverlay katman1 = new GMapOverlay();

            // İlk olarak da yeni oluşturduğumuz katmanı harita nesnemize eklemek gerekiyor.

            Map.Overlays.Add(katman1);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            PointLatLng lokasyon1 = new PointLatLng(Convert.ToDouble(textBoxEnlem.Text),
                                                   Convert.ToDouble(textBoxBoylam.Text));


            GMarkerGoogle marker = new GMarkerGoogle(lokasyon1, GMarkerGoogleType.orange_dot);

            marker.ToolTipText = "\nLokasyon\nTır\nFrom:Ankara\nTo:İstanbul\n";
            marker.ToolTip.Fill = Brushes.Bisque;
            marker.ToolTip.Foreground = Brushes.Black;
            marker.ToolTip.Stroke = Pens.Black;
            marker.ToolTip.TextPadding = new Size(10, 10);
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            marker.Tag = 101;


            // Daha sonra markerları ekliyoruz.
            // Dikkat!
            // Markerı önce eklersem yanlış yere koyabilir.

            katman1.Markers.Add(marker);
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Map.Dispose();       // Uygulamadan çıkıyor. Takılıp kalmasını önlemek için.
            Application.Exit();
        }



        private void Map_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            //int markerId = (int)item.Tag;
            //Console.WriteLine("id:" + markerId + " olan Markera tıklandı...");


            string secilenAracinPlakasi = (string)item.Tag;
            foreach (Arac arac in list)

            {
               if(secilenAracinPlakasi.Equals(arac.Plaka))
                {
                    textBox3.Text = secilenAracinPlakasi;
                    textBox6.Text = arac.Tipi;
                    textBox5.Text = arac.From;
                    textBox4.Text = arac.To;
                    break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            PointLatLng lokasyon2 = new PointLatLng(Convert.ToDouble(textBox2.Text),
                                                   Convert.ToDouble(textBox1.Text));

            GMarkerGoogle marker2 = new GMarkerGoogle(lokasyon2, GMarkerGoogleType.blue_dot);
            marker2.Tag = 102;
            katman1.Markers.Add(marker2);


        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Arac arac in list)
            {
                GMarkerGoogle markerTmp = new GMarkerGoogle ( arac.Konum, GMarkerGoogleType.orange_dot);
                markerTmp.Tag = arac.Plaka;
                markerTmp.ToolTipText = arac.ToString();
                markerTmp.ToolTipMode = MarkerTooltipMode.OnMouseOver;  // MarkerTooltipMode.Always sürekli gösterir bilgiler açık gelir. Örneğin veritabanında anlık değişirse anlık olarak görebilirim.
                katman1.Markers.Add (markerTmp);
                Console.WriteLine(arac.ToString());
            }
            {
                
            }
        }
    }
}

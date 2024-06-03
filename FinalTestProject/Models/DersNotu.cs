using FinalTestProject.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FinalTestProject.Models.Constants;

namespace FinalTestProject.Models
{
    public class DersNotu
	{
		[Key]
		public int Id { get; set; }
        [ForeignKey("Ogrenci")]
        public long? OgrenciTc { get; set; }
        public Ogrenci? Ogrenci { get; set; }

        [ForeignKey("Ders")]
        public string? DersKodu { get; set; }
		public Ders? Ders {  get; set; }

        public Sinav AraSinav = new() { yuzde = 0.4f };
		public Sinav FinalSinav = new() { yuzde = 0.6f };
		public float SonucNotu;
		public HarfNotlari HarfNotu;
		public int YoklamaDurumu;
		public string basari_durumu;
		
		public void setAraSinav(float vize_not)
		{
			this.AraSinav.notAta(vize_not);
		}
		public void setFinal(float final_not)
		{
			this.FinalSinav.notAta(final_not);
		}

		public void HesaplaGecmeNotu()
		{
			SonucNotu = (AraSinav.notHesapla() + FinalSinav.notHesapla())/100;
            HesaplaHarfNotu();
        }

		private void HesaplaHarfNotu()
		{
			if (YoklamaDurumu == 1) 
			{
                if (SonucNotu >= 90)
                {
                    HarfNotu = HarfNotlari.AA;
                    basari_durumu = "Basarili";

                }
                else if (SonucNotu >= 85 && SonucNotu < 90)
                {
                    HarfNotu = HarfNotlari.BA;
                    basari_durumu = "Basarili";
                }
                else if (SonucNotu >= 80 && SonucNotu < 85)
                {
                    HarfNotu = HarfNotlari.BB;
                    basari_durumu = "Basarili";
                }
                else if (SonucNotu >= 70 && SonucNotu < 80)
                {
                    HarfNotu = HarfNotlari.CB;
                    basari_durumu = "Basarili";
                }
                else if (SonucNotu >= 60 && SonucNotu < 70)
                {
                    HarfNotu = HarfNotlari.CC;
                    basari_durumu = "Basarili";
                }
                else if (SonucNotu >= 55 && SonucNotu < 60)
                {
                    HarfNotu = HarfNotlari.DC;
                    basari_durumu = "Kosullu Basarili";
                }
                else if (SonucNotu >= 50 && SonucNotu < 55)
                {
                    HarfNotu = HarfNotlari.DD;
                    basari_durumu = "Kosullu Basarili";
                }
                else if (SonucNotu >= 40 && SonucNotu < 50)
                {
                    HarfNotu = HarfNotlari.FD;
                    basari_durumu = "Basarisiz";
                }
                else
                {
                    HarfNotu = HarfNotlari.FF;
                    basari_durumu = "Basarisiz";
                }
            }
            else if (YoklamaDurumu == 0) 
            {
                HarfNotu = HarfNotlari.FF;
                basari_durumu = "Basarisiz";
            }
		}
	}

}

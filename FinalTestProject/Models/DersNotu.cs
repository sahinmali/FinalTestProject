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
		public bool YoklamaDurumu;
		
		public void setAraSinav(float vize_not)
		{
			this.AraSinav.notAta(vize_not);
		}
		public void setFinal(float final_not)
		{
			this.FinalSinav.notAta(final_not);
		}

		public void HesaplaSonuc()
		{
			HesaplaGecmeNotu();
		}

		private void HesaplaGecmeNotu()
		{
			SonucNotu = AraSinav.notHesapla() + FinalSinav.notHesapla();
		}

		private void HesaplaHarfNotu()
		{
			if (SonucNotu >= 90)
			{
				HarfNotu = HarfNotlari.AA;
			}
			else if (SonucNotu >= 85 && SonucNotu < 90)
			{
				HarfNotu = HarfNotlari.BA;
			}
			else if (SonucNotu >= 80 && SonucNotu < 85)
			{
				HarfNotu = HarfNotlari.BB;
			}
			else if (SonucNotu >= 70 && SonucNotu < 80)
			{
				HarfNotu = HarfNotlari.CB;
			}
			else if (SonucNotu >= 60 && SonucNotu < 70)
			{
				HarfNotu = HarfNotlari.CC;
			}
			else if (SonucNotu >= 55 && SonucNotu < 60)
			{
				HarfNotu = HarfNotlari.DC;
			}
			else if (SonucNotu >= 50 && SonucNotu < 55)
			{
				HarfNotu = HarfNotlari.DD;
			}
			else if (SonucNotu >= 40 && SonucNotu < 50)
			{
				HarfNotu = HarfNotlari.FD;
			}
			else
			{
				HarfNotu = HarfNotlari.FF;
			}
		}
	}

}

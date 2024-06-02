using FinalTestProject.Data;

namespace FinalTestProject.Models
{
    public class Sinav
	{
		public float ogr_not;
        public float yuzde;
		public void notAta(float not)
		{
			this.ogr_not = not;
		}
        public float notHesapla()
        {
            return this.ogr_not * this.yuzde;
        }
    }
}

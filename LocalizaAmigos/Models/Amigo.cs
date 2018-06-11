namespace LocalizaAmigos.Models
{
    public class Amigo
    {
        public int ID_Amigo { get; set; }
        public int ID_Usuario { get; set; }
        public string NM_Amigo { get; set; }
        public string NR_CEP { get; set; }
        public decimal NR_Lat { get; set; }
        public decimal NR_Lng { get; set; }
    }
}
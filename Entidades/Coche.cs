namespace Entidades
{
    public class Coche
    {

        public string marca { get; set; }
        public string modelo { get; set; }
        public string transmision { get; set; }
        public int capacidad { get; set; }
        public string placa { get; set; }

        public Coche()
        {
            marca = string.Empty;
            modelo = string.Empty;
            transmision = string.Empty;
            capacidad = 0;
            placa = string.Empty;
        }

    }
}

namespace MauiAppHotel.Models
{
    // guarda tudo o que a pessoa escolheu na reserva
    public class Hospedagem
    {
        public Quarto QuartoEscolhido { get; set; }
        public int Adultos { get; set; }
        public int Criancas { get; set; }
        public DateTime DataCheckIn { get; set; }
        public DateTime DataCheckOut { get; set; }

        // quantos dias a pessoa vai ficar
        public int Estadia => (DataCheckOut - DataCheckIn).Days;

        // valor de toda a estadia
        public double ValorTotal => Estadia *
            ((Adultos * QuartoEscolhido.ValorDiariaAdulto) +
             (Criancas * QuartoEscolhido.ValorDiariaCrianca));
    }
}
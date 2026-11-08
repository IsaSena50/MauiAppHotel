using MauiAppHotel.Models;

namespace MauiAppHotel.Views;

public partial class ContratacaoHospedagem : ContentPage
{
    // lista com as suítes do hotel (nomes e valores podem ser trocados)
    List<Quarto> lista_quartos = new List<Quarto>
    {
   new Quarto { Descricao = "Quarto Econômico", ValorDiariaAdulto = 120, ValorDiariaCrianca = 60 },
new Quarto { Descricao = "Quarto Conforto", ValorDiariaAdulto = 200, ValorDiariaCrianca = 100 },
new Quarto { Descricao = "Suíte Presidencial", ValorDiariaAdulto = 400, ValorDiariaCrianca = 200 }
    };

    public ContratacaoHospedagem()
    {
        InitializeComponent();

        // coloca as suítes dentro do Picker
        pck_quarto.ItemsSource = lista_quartos;

        // check-in: de hoje até 1 mês pra frente
        dtpck_checkin.MinimumDate = DateTime.Today;
        dtpck_checkin.MaximumDate = DateTime.Today.AddMonths(1);

        // check-out: sempre depois do check-in, até 6 meses depois
        DateTime checkinInicial = dtpck_checkin.Date ?? DateTime.Today;
        dtpck_checkout.MinimumDate = checkinInicial.AddDays(1);
        dtpck_checkout.MaximumDate = checkinInicial.AddMonths(6);
        dtpck_checkout.Date = checkinInicial.AddDays(1);
    }

    // quando muda o check-in, o check-out só pode ser depois dele
    private void dtpck_checkin_DateSelected(object sender, DateChangedEventArgs e)
    {
        DateTime novoCheckin = e.NewDate ?? DateTime.Today;

        dtpck_checkout.MinimumDate = novoCheckin.AddDays(1);
        dtpck_checkout.MaximumDate = novoCheckin.AddMonths(6);
        dtpck_checkout.Date = novoCheckin.AddDays(1);
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        // precisa escolher uma suíte
        if (pck_quarto.SelectedItem == null)
        {
            await DisplayAlert("Atenção", "Escolha uma suíte antes de avançar.", "OK");
            return;
        }

        // precisa de pelo menos 1 adulto
        if (stp_adultos.Value == 0)
        {
            await DisplayAlert("Atenção", "A reserva precisa de pelo menos 1 adulto.", "OK");
            return;
        }

        // junta tudo que a pessoa escolheu
        Hospedagem h = new Hospedagem
        {
            QuartoEscolhido = (Quarto)pck_quarto.SelectedItem,
            Adultos = (int)stp_adultos.Value,
            Criancas = (int)stp_criancas.Value,
            DataCheckIn = dtpck_checkin.Date ?? DateTime.Today,
            DataCheckOut = dtpck_checkout.Date ?? DateTime.Today.AddDays(1)
        };

        // manda os dados para a tela de resumo
        await Navigation.PushAsync(new HospedagemContratada { BindingContext = h });
    }

    private async void btn_sobre_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Sobre());
    }
}

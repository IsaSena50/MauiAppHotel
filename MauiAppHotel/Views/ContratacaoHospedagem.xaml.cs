namespace MauiAppHotel.Views;

public partial class ContratacaoHospedagem : ContentPage
{
    public ContratacaoHospedagem()
    {
        InitializeComponent();
    }

    private async void btn_sobre_Clicked(object sender, EventArgs e)
    {
        // abre a tela Sobre
        await Navigation.PushAsync(new Sobre());
    }
}
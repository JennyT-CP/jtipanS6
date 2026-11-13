
using System.Net;

namespace jtipanS6.Views;

public partial class vAgregarEstudiante : ContentPage
{
    public vAgregarEstudiante()
    {
        InitializeComponent();
    }

    private async void btnAgregar_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtEdad.Text))
            {
                await DisplayAlertAsync(
                    "Alerta",
                    "Todos los campos son obligatorios.",
                    "OK");
                return;
            }

            // Validar que la edad sea un número entero positivo
            if (!int.TryParse(txtEdad.Text, out int edad) || edad <= 0)
            {
                await DisplayAlertAsync(
                    "Alerta",
                    "La edad debe ser un número entero mayor a cero.",
                    "OK");
                return;
            }

            // Registrar estudiante
            WebClient cliente = new WebClient();

            var parametros =
                new System.Collections.Specialized.NameValueCollection();

            parametros.Add("nombre", txtNombre.Text.Trim());
            parametros.Add("apellido", txtApellido.Text.Trim());
            parametros.Add("edad", edad.ToString());

            cliente.UploadValues(
                "http://192.168.100.42/ws_estudiante/estudiante_ws.php",
                "POST",
                parametros);

            await DisplayAlertAsync(
                "Éxito",
                "Estudiante registrado correctamente.",
                "OK");

            await Navigation.PushAsync(new vEstudiante());
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync(
                "Alerta",
                "Error: " + ex.Message,
                "OK");
        }
    }

    private async void btnCancelar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new vEstudiante());
    }
}

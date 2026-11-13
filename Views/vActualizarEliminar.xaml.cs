
using jtipanS6.Models;
using System.Net.Http;

namespace jtipanS6.Views;

public partial class vActualizarEliminar : ContentPage
{
    private readonly HttpClient cliente = new HttpClient();

    private const string url =
        "http://192.168.100.42/ws_estudiante/estudiante_ws.php";

    public vActualizarEliminar(Estudiante datos)
    {
        InitializeComponent();

        txtCodigo.Text = datos.codigo.ToString();
        txtNombre.Text = datos.nombre;
        txtApellido.Text = datos.apellido;
        txtEdad.Text = datos.edad.ToString();
    }

    // Método PUT - Actualizar estudiante
    private async void btnActualizar_Clicked(
        object sender, EventArgs e)
    {
        try
        {
            string codigo = txtCodigo.Text;
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;
            string edad = txtEdad.Text;

            if (string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(apellido) ||
                !int.TryParse(edad, out int edadNumero) ||
                edadNumero <= 0)
            {
                await DisplayAlertAsync(
                    "Alerta",
                    "Todos los campos son obligatorios y la edad debe ser un número entero mayor a cero.",
                    "OK");
                return;
            }

            string urlActualizar = url +
                "?codigo=" + Uri.EscapeDataString(codigo) +
                "&nombre=" + Uri.EscapeDataString(nombre) +
                "&apellido=" + Uri.EscapeDataString(apellido) +
                "&edad=" + edadNumero;

            HttpResponseMessage respuesta =
                await cliente.PutAsync(urlActualizar, null);

            respuesta.EnsureSuccessStatusCode();

            await DisplayAlertAsync(
                "Éxito",
                "Estudiante actualizado correctamente",
                "OK");

            await Navigation.PushAsync(new vEstudiante());
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync(
                "Alerta",
                "Error al actualizar: " + ex.Message,
                "OK");
        }
    }

    // Método DELETE - Eliminar estudiante
    private async void btnEliminar_Clicked(
        object sender, EventArgs e)
    {
        try
        {
            bool confirmar = await DisplayAlertAsync(
                "Confirmar eliminación",
                "¿Está seguro de eliminar este estudiante?",
                "Sí", "No");

            if (!confirmar)
                return;

            string codigo = txtCodigo.Text;

            string urlEliminar = url +
                "?codigo=" + Uri.EscapeDataString(codigo);

            HttpResponseMessage respuesta =
                await cliente.DeleteAsync(urlEliminar);

            respuesta.EnsureSuccessStatusCode();

            await DisplayAlertAsync(
                "Éxito",
                "Estudiante eliminado correctamente",
                "OK");

            await Navigation.PushAsync(new vEstudiante());
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync(
                "Alerta",
                "Error al eliminar: " + ex.Message,
                "OK");
        }
    }
}

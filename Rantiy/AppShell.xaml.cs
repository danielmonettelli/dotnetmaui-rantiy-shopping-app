namespace Rantiy;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // No necesitamos registrar las páginas principales de las pestañas
        // ya que se definen directamente en AppShell.xaml
        
        // Registrar solo las rutas para navegación detallada
        Routing.RegisterRoute("details", typeof(ProductDetailPage));
    }
}

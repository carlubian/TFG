using ConfigAdapter.Xml;
using DotNet.Misc.Extensions.Linq;
using Microsoft.QueryStringDotNET;
using System;
using System.IO;
using System.Linq;
using TFG.Core.Model;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace TFG.UWP
{
    /// <summary>
    /// Proporciona un comportamiento específico de la aplicación para complementar la clase Application predeterminada.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Inicializa el objeto de aplicación Singleton. Esta es la primera línea de código creado
        /// ejecutado y, como tal, es el equivalente lógico de main() o WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
        }

        /// <summary>
        /// Se invoca cuando la aplicación la inicia normalmente el usuario final. Se usarán otros puntos
        /// de entrada cuando la aplicación se inicie para abrir un archivo específico, por ejemplo.
        /// </summary>
        /// <param name="e">Información detallada acerca de la solicitud y el proceso de inicio.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

            // No repetir la inicialización de la aplicación si la ventana aún tiene contenido,
            // solo asegurarse de que la ventana está activa
            if (!(Window.Current.Content is Frame rootFrame))
            {
                // Crear un marco para que actúe como contexto de navegación y navegar a la primera página.
                rootFrame = new Frame();

                rootFrame.NavigationFailed += this.OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Cargar el estado de la aplicación suspendida previamente
                }

                // Poner el marco en la ventana actual
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // Determinar si es el primer uso (redirigir a NUE)
                    var directory = ApplicationData.Current.LocalFolder.Path;
                    var config = XmlConfig.From(Path.Combine(directory, "Settings.xml"));
                    var nueSetting = config.Read("ExistingUser");

                    if (nueSetting is default(string))
                        rootFrame.Navigate(typeof(NUE), e.Arguments);
                    else
                    {
                        var ids = config.Read("ActiveSensors");
                        if (!(ids is default(string)))
                        {
                            SessionStorage.ClearSensores();
                            ids.Split('|', StringSplitOptions.RemoveEmptyEntries).ForEach(id =>
                            {
                                SessionStorage.AddSensor(new Sensor
                                {
                                    Nombre = config.Read($"{id}:Name"),
                                    IP = config.Read($"{id}:IP"),
                                    Puerto = config.Read($"{id}:Port"),
                                    Pais = config.Read($"{id}:Country"),
                                    Tipo = config.Read($"{id}:Type"),
                                    Lugar = config.Read($"{id}:Location"),
                                    Operaciones = config.Read($"{id}:Operations"),
                                    InternalID = id
                                });
                            });
                        }

                        rootFrame.Navigate(typeof(MainPage), e.Arguments);
                    }
                }
                // Asegurarse de que la ventana actual está activa.
                Window.Current.Activate();

                CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                titleBar.ButtonBackgroundColor = Colors.Transparent;
                titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            }
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            // No repetir la inicialización de la aplicación si la ventana aún tiene contenido,
            // solo asegurarse de que la ventana está activa
            if (!(Window.Current.Content is Frame rootFrame))
            {
                // Crear un marco para que actúe como contexto de navegación y navegar a la primera página.
                rootFrame = new Frame();

                rootFrame.NavigationFailed += this.OnNavigationFailed;

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Cargar el estado de la aplicación suspendida previamente
                }

                // Poner el marco en la ventana actual
                Window.Current.Content = rootFrame;
            }

            // Handle toast activation
            if (args is ToastNotificationActivatedEventArgs)
            {
                var toastActivationArgs = args as ToastNotificationActivatedEventArgs;

                // Parse the query string (using QueryString.NET)
                var query = QueryString.Parse(toastActivationArgs.Argument);

                // See what action is being requested 
                switch (query["action"])
                {
                    // Open the image
                    case "viewSensor":

                        // The URL retrieved from the toast args
                        string sensorId = query["sensorId"];

                        // If we're already viewing that image, do nothing
                        if (rootFrame.Content is DetallesSensor && 
                            (rootFrame.Content as DetallesSensor).sensor.InternalID.Equals(sensorId))
                            break;

                        var directory = ApplicationData.Current.LocalFolder.Path;
                        var config = XmlConfig.From(Path.Combine(directory, "Settings.xml"));

                        var ids = config.Read("ActiveSensors");
                        if (!(ids is default(string)))
                        {
                            SessionStorage.ClearSensores();
                            ids.Split('|', StringSplitOptions.RemoveEmptyEntries).ForEach(id =>
                            {
                                SessionStorage.AddSensor(new Sensor
                                {
                                    Nombre = config.Read($"{id}:Name"),
                                    IP = config.Read($"{id}:IP"),
                                    Puerto = config.Read($"{id}:Port"),
                                    Pais = config.Read($"{id}:Country"),
                                    Tipo = config.Read($"{id}:Type"),
                                    Lugar = config.Read($"{id}:Location"),
                                    Operaciones = config.Read($"{id}:Operations"),
                                    InternalID = id,
                                    StatusNotified = true
                                });
                            });
                        }

                        // Otherwise navigate to view it
                        rootFrame.Navigate(typeof(DetallesSensor), SessionStorage.GetSensores()
                            .FirstOrDefault(s => s.InternalID.Equals(sensorId)));
                        break;
                }

                // If we're loading the app for the first time, place the main page on
                // the back stack so that user can go back after they've been
                // navigated to the specific page
                if (rootFrame.BackStack.Count == 0)
                    rootFrame.BackStack.Add(new PageStackEntry(typeof(MainPage), null, null));
            }

            // TODO: Handle other types of activation

            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Se invoca cuando la aplicación la inicia normalmente el usuario final. Se usarán otros puntos
        /// </summary>
        /// <param name="sender">Marco que produjo el error de navegación</param>
        /// <param name="e">Detalles sobre el error de navegación</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e) => throw new Exception("Failed to load Page " + e.SourcePageType.FullName);

        /// <summary>
        /// Se invoca al suspender la ejecución de la aplicación. El estado de la aplicación se guarda
        /// sin saber si la aplicación finalizará o se reanudará con el contenido
        /// de la memoria aún intacto.
        /// </summary>
        /// <param name="sender">Origen de la solicitud de suspensión.</param>
        /// <param name="e">Detalles de la solicitud de suspensión.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Guardar el estado de la aplicación y detener toda actividad en segundo plano
            deferral.Complete();
        }
    }
}

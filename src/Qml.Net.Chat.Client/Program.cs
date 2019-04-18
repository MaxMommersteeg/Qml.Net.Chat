using Qml.Net;
using Qml.Net.Runtimes;

namespace Chat.Client.Controllers
{
    public class Program
    {
        public static int Main(string[] args)
        {
            RuntimeManager.DiscoverOrDownloadSuitableQtRuntime();
            
            QQuickStyle.SetStyle("Material");

            using (var application = new QGuiApplication(args))
            {
                using (var qmlEngine = new QQmlApplicationEngine())
                {
                    Qml.Net.Qml.RegisterType<ChatController>("ChatClient");

                    qmlEngine.Load("Main.qml");

                    return application.Exec();
                }
            }
        }
    }
}

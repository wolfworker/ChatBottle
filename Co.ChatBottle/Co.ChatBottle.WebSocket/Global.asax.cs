namespace Co.ChatBottle.WebSocket
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebSocket.StartSocket();
        }
    }
}

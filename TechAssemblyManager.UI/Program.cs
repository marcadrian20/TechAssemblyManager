namespace TechAssemblyManager.UI
{
    internal static class Program
    {
        /// <summary>  
        ///  The main entry point for the application.  
        /// </summary>  
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,  
            // see https://aka.ms/applicationconfiguration.  
            ApplicationConfiguration.Initialize();

            // Fix for CS1002: Added missing semicolon.  
            // Fix for CS0120: Accessed 'Instance' through an object reference instead of treating it as static.  
           //MainForm m=new MainForm();
            Application.Run(new MainForm());
        }
    }
}
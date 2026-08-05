using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HigherOrLowerAcademyApp
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
            MainMenu startUpForm = new MainMenu(new DBLiason()); // PICK WHAT FORM STARTS UP HERE - probablyu need a constructor arg to creat the gloabl dbLIason for the first time

            Application.Run(startUpForm);
        }
    }
}